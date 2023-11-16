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
using static team_profi.Pages.AdminPages.AnswerAdminPage;

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AnswerAdminPage.xaml
    /// </summary>
    public partial class AnswerAdminPage : Page
    {
        // Класс, представляющий модель представления ответа, используемый для временной программной таблицы
        public class AnswerViewModel
        {
            public string Topic { get; set; } // Тема ответа
            public string FIO { get; set; } // ФИО студента, отвечающего на задание
            public string AnswerText { get; set; } // Текст ответа
            public string SubmissionDate { get; set; } // Дата предоставления ответа
        }

        public ObservableCollection<AnswerViewModel> answers; // Коллекция для хранения ответов

        public AnswerAdminPage()
        {
            InitializeComponent();
            answers = new ObservableCollection<AnswerViewModel>(); // Инициализация коллекции ответов
            LoadAnswers(); // Загрузка ответов из базы данных
        }

        // Метод для загрузки ответов из базы данных и заполнения коллекции
        private void LoadAnswers()
        {
            using (var db = new TeamProfiBDEntities()) // Использование контекста базы данных
            {
                var answersFromDB = db.Answers.ToList(); // Получение списка ответов из базы данных
                foreach (var answer in answersFromDB)
                {
                    // Получение соответствующего задания и пользователя из базы данных
                    var assignment = db.Assignments.FirstOrDefault(a => a.AssigID == answer.AssignmentID);
                    var user = db.Users.FirstOrDefault(u => u.UserID == answer.StudentID);

                    // Проверка наличия задания и пользователя в базе данных
                    if (assignment != null && user != null)
                    {
                        // Добавление нового элемента AnswerViewModel в коллекцию answers
                        answers.Add(new AnswerViewModel
                        {
                            Topic = assignment.Topic.ToUpper(), // Установка темы ответа в верхнем регистре
                            FIO = $"{user.LastName.ToUpper()} {user.FirstName.ToUpper()} {user.Otchestvo.ToUpper()}", // Сбор ФИО студента
                            AnswerText = answer.AnswerText, // Установка текста ответа
                            SubmissionDate = answer.SubmissionDate // Установка даты предоставления ответа
                        });
                    }
                }

                DataGridUser.ItemsSource = answers; // Привязка коллекции к элементу управления DataGrid
            }
        }

        // Обработчик события клика на элементе Border
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверка отправителя события и получение данных модели ответа AnswerViewModel
            if (sender is Border border && border.DataContext is AnswerViewModel answers)
            {
                // Создание новой страницы и передача данных ответа для отображения
                DopPages.AnswerAdminSend answerSend = new DopPages.AnswerAdminSend(answers);
                NavigationService.Navigate(answerSend); // Переход на новую страницу
            }
        }



    }
}
