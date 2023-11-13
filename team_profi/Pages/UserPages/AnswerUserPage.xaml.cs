using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using team_profi.Classes;

namespace team_profi.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для AnswerUserPage.xaml
    /// </summary>
    public partial class AnswerUserPage : Page
    {
        public class AnswerTeachViewModel
        {
            public string Topic { get; set; }
            public int StudentID { get; set; }
            public string AnswerText { get; set; }
            public string SubmissionDate { get; set; }
            public string Grade { get; set; }
            public string Comment { get; set; }

        }


        private ObservableCollection<AnswerTeachViewModel> answers;

        public AnswerUserPage()
        {
            InitializeComponent();

            answers = new ObservableCollection<AnswerTeachViewModel>();
            string name = DataGetIDStudentClass.GetName();

            using (var db = new TeamProfiBDEntities())
            {
                int studentID = db.Users
                    .Where(u => u.Login == name)
                    .Select(u => u.UserID)
                    .FirstOrDefault();

                // Теперь фильтруем задания только для текущего студента
                var answersFromDb = db.Answers
                    .Where(a => a.StudentID == studentID)
                    .ToList();


                foreach (var answer in answersFromDb)
                {
                    // Перемещаем определение assigTopic сюда, чтобы оно было уникальным для каждой записи
                    string assigTopic = db.Assignments
                        .Where(a => a.AssigID == answer.AssignmentID)
                        .Select(a => a.Topic)
                        .FirstOrDefault();

                    // Проверяем наличие оценки в таблице Grades
                    var grade = db.Grades
                        .Where(g => g.AnswerID == answer.AnswerID)
                        .Select(g => g.Grade)
                        .FirstOrDefault();

                    var comm = db.Grades
                        .Where(g => g.AnswerID == answer.AnswerID)
                        .Select(g => g.Comment)
                        .FirstOrDefault();

                    // Создаем AnswerTeachViewModel и устанавливаем оценку или текст "Нет оценки"
                    answers.Add(new AnswerTeachViewModel
                    {
                        Topic = assigTopic,
                        StudentID = answer.StudentID,
                        AnswerText = answer.AnswerText,
                        SubmissionDate = answer.SubmissionDate,
                        Grade = (grade != 0) ? grade.ToString() + "\nпроверен" : "-\nотправлен",
                        Comment = comm

                        /*Grade = (grade != 0) ? grade.ToString() + "\nПроверен" : "-\nОтправлен"*/
                    });
                }

                DataGridUser.ItemsSource = answers;
            }
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Предположим, что вы определяете переменную gr где-то в этом блоке кода или в методе
            Grades gr = new Grades(); // Замените GetGrades() на то, как у вас определена переменная gr

            if (sender is Border border && border.DataContext is AnswerTeachViewModel viewModel)
            {
                DopPages.AnswerUserInfoPage descriptionPage = new DopPages.AnswerUserInfoPage(viewModel);
                NavigationService.Navigate(descriptionPage);
            }


        }
    }
}
