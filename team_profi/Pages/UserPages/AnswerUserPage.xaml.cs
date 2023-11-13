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

        }


        private ObservableCollection<AnswerTeachViewModel> answers;

        public AnswerUserPage()
        {
            InitializeComponent();

            answers = new ObservableCollection<AnswerTeachViewModel>();
            string ab = "999";
            string name = DataGetIDStudentClass.GetName();
            MessageBox.Show(name);

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

                MessageBox.Show($"Found {answersFromDb.Count} answers for StudentID {studentID}");

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

                    // Создаем AnswerTeachViewModel и устанавливаем оценку или текст "Нет оценки"
                    answers.Add(new AnswerTeachViewModel
                    {
                        Topic = assigTopic,
                        StudentID = answer.StudentID,
                        AnswerText = answer.AnswerText,
                        SubmissionDate = answer.SubmissionDate,
                        Grade = (grade != 0) ? grade.ToString() + "\nпроверен" : "-\nотправлен"

                        /*Grade = (grade != 0) ? grade.ToString() + "\nПроверен" : "-\nОтправлен"*/
                    });
                }

                MessageBox.Show($"Added {answers.Count} answers to the collection");
                DataGridUser.ItemsSource = answers;
            }
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*if (sender is Border border && border.DataContext is Assignments assignment)
            {
                DopPages.AssigmentDescriptionUserPage descriptionPage = new DopPages.AssigmentDescriptionUserPage(assignment);
                NavigationService.Navigate(descriptionPage);
            }*/

            MessageBox.Show("ЩАС");
        }
    }
}
