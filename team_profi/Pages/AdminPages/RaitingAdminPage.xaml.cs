using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для RaitingAdminPage.xaml
    /// </summary>
    /// 


    public partial class RaitingAdminPage : Page
    {
        private ObservableCollection<StudentRatingViewModel> studentRatings; // Коллекция для хранения рейтингов студентов

        public RaitingAdminPage()
        {
            InitializeComponent();
            LoadStudentRatings(); // При инициализации страницы загружается информация о рейтингах студентов
        }

        // Метод для загрузки рейтингов студентов
        private void LoadStudentRatings()
        {
            studentRatings = new ObservableCollection<StudentRatingViewModel>(); // Инициализация коллекции

            using (var db = new TeamProfiBDEntities()) // Использование контекста базы данных
            {
                // Получение отсортированного списка рейтингов студентов из базы данных по общему количеству баллов
                var sortedStudentRatings = db.StudentRatings
                    .OrderByDescending(sr => sr.TotalPoints)
                    .ToList();

                int rank = 1; // Инициализация переменной ранга
                foreach (var studentRating in sortedStudentRatings) // Итерация по списку отсортированных рейтингов
                {
                    int studentID = studentRating.StudentID;
                    var student = db.Users.FirstOrDefault(s => s.UserID == studentID); // Получение информации о студенте из базы данных

                    if (student != null) // Если студент найден
                    {
                        // Создание экземпляра модели представления рейтинга студента и добавление в коллекцию
                        var viewModel = new StudentRatingViewModel(rank, $"{student.LastName} {student.FirstName}", studentRating.TotalPoints);
                        studentRatings.Add(viewModel); // Добавление в коллекцию
                        rank++; // Увеличение ранга для следующего студента
                    }
                }
            }

            DataGridUser.ItemsSource = studentRatings; // Привязка коллекции к элементу управления DataGrid
        }
    }
}
