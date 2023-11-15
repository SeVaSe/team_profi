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
    /// Логика взаимодействия для RaitingUserPage.xaml
    /// </summary>
    /// 



    public partial class RaitingUserPage : Page
    {
        // Создаем коллекцию для хранения данных о рейтинге студентов
        private ObservableCollection<StudentRatingViewModel> studentRatings;

        // Конструктор страницы
        public RaitingUserPage()
        {
            InitializeComponent();
            // Вызываем метод для загрузки данных о рейтинге студентов
            LoadStudentRatings();
        }

        // Метод для загрузки данных о рейтинге студентов
        private void LoadStudentRatings()
        {
            // Инициализируем коллекцию для хранения данных о рейтинге студентов
            studentRatings = new ObservableCollection<StudentRatingViewModel>();

            // Используем подключение к базе данных
            using (var db = new TeamProfiBDEntities())
            {
                // Получаем отсортированный список рейтинга студентов
                var sortedStudentRatings = db.StudentRatings
                    .OrderByDescending(sr => sr.TotalPoints)
                    .ToList();

                // Индекс для ранга студентов
                int rank = 1;

                // Обходим каждый элемент в отсортированном списке рейтинга студентов
                foreach (var studentRating in sortedStudentRatings)
                {
                    // Извлекаем идентификатор студента
                    int studentID = studentRating.StudentID;

                    // Получаем данные о студенте из таблицы Users по идентификатору
                    var student = db.Users.FirstOrDefault(s => s.UserID == studentID);

                    // Если данные о студенте получены
                    if (student != null)
                    {
                        // Создаем объект модели представления для текущего студента
                        var viewModel = new StudentRatingViewModel(rank, $"{student.LastName} {student.FirstName}", studentRating.TotalPoints);

                        // Добавляем модель представления в коллекцию
                        studentRatings.Add(viewModel);

                        // Инкрементируем ранг для следующего студента
                        rank++;
                    }
                }
            }

            // Устанавливаем источник данных для DataGridUser
            DataGridUser.ItemsSource = studentRatings;
        }
    }


}
