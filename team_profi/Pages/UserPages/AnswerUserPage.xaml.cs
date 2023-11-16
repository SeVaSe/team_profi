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
        // Внутренний класс, представляющий модель представления для ответов преподавателя
        public class AnswerTeachViewModel
        {
            public string Topic { get; set; } // Тема задания
            public int StudentID { get; set; } // ID студента
            public string AnswerText { get; set; } // Текст ответа
            public string SubmissionDate { get; set; } // Дата предоставления ответа
            public string Grade { get; set; } // Оценка
            public string Comment { get; set; } // Комментарий к оценке
        }

        private ObservableCollection<AnswerTeachViewModel> answers; // Коллекция для хранения ответов студента

        public AnswerUserPage()
        {
            InitializeComponent();

            answers = new ObservableCollection<AnswerTeachViewModel>(); // Инициализация коллекции
            string name = DataGetIDStudentClass.GetName(); // Получение имени студента

            using (var db = new TeamProfiBDEntities()) // Использование контекста базы данных
            {
                int studentID = db.Users
                    .Where(u => u.Login == name)
                    .Select(u => u.UserID)
                    .FirstOrDefault(); // Получение ID студента по его имени

                // Получение ответов студента из базы данных
                var answersFromDb = db.Answers
                    .Where(a => a.StudentID == studentID)
                    .ToList();

                foreach (var answer in answersFromDb) // Итерация по ответам студента
                {
                    // Получение темы задания по ID из таблицы Assignments
                    string assigTopic = db.Assignments
                        .Where(a => a.AssigID == answer.AssignmentID)
                        .Select(a => a.Topic)
                        .FirstOrDefault();

                    // Получение оценки и комментария к оценке из таблицы Grades
                    var grade = db.Grades
                        .Where(g => g.AnswerID == answer.AnswerID)
                        .Select(g => g.Grade)
                        .FirstOrDefault();

                    var comm = db.Grades
                        .Where(g => g.AnswerID == answer.AnswerID)
                        .Select(g => g.Comment)
                        .FirstOrDefault();

                    // Создание модели представления AnswerTeachViewModel и добавление в коллекцию
                    answers.Add(new AnswerTeachViewModel
                    {
                        Topic = assigTopic,
                        StudentID = answer.StudentID,
                        AnswerText = answer.AnswerText,
                        SubmissionDate = answer.SubmissionDate,
                        Grade = (grade != 0) ? grade.ToString() + "\nпроверен" : "-\nотправлен",
                        Comment = comm
                    });
                }

                DataGridUser.ItemsSource = answers; // Привязка коллекции к элементу управления DataGrid
            }
        }

        // Обработчик события клика на элементе Border
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is AnswerTeachViewModel viewModel)
            {
                // Создание новой страницы с информацией о конкретном ответе и переход на нее
                DopPages.AnswerUserInfoPage descriptionPage = new DopPages.AnswerUserInfoPage(viewModel);
                NavigationService.Navigate(descriptionPage);
            }
        }
    }
}
