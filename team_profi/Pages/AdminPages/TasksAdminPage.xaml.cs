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
using team_profi.Pages.DopPages;

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для TasksAdminPage.xaml
    /// </summary>
    public partial class TasksAdminPage : Page
    {
        private ObservableCollection<Assignments> assignments; // Коллекция для хранения заданий

        public TasksAdminPage()
        {
            InitializeComponent();

            assignments = new ObservableCollection<Assignments>(); // Инициализация коллекции

            using (var db = new TeamProfiBDEntities()) // Использование контекста базы данных
            {
                var assignmentsFromDb = db.Assignments.ToList(); // Получение всех заданий из базы данных

                foreach (var assignment in assignmentsFromDb) // Итерация по полученным заданиям
                {
                    // Добавление каждого задания в коллекцию assignments
                    assignments.Add(new Assignments
                    {
                        AssigID = assignment.AssigID,
                        TeachID = assignment.TeachID,
                        Topic = assignment.Topic.ToUpper(), // Установка темы задания в верхний регистр
                        TaskDescription = assignment.TaskDescription,
                        CreationDate = assignment.CreationDate
                    });
                }

                DataGridUser.ItemsSource = assignments; // Привязка коллекции к элементу управления DataGrid
            }
        }

        // Обработчик события клика на элементе Border
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Проверка отправителя события и получение данных модели Assignments
            if (sender is Border border && border.DataContext is Assignments assignment)
            {
                // Создание новой страницы с описанием задания и переход на нее
                DopPages.AssignmentDescriptionPage descriptionPage = new DopPages.AssignmentDescriptionPage(assignment);
                NavigationService.Navigate(descriptionPage);
            }
        }

        // Обработчик события клика на кнопке создания нового задания
        private void CreateNewTask_Click(object sender, RoutedEventArgs e)
        {
            // Навигация на страницу создания нового задания
            NavigationService.Navigate(new AssigmentCreate());
        }
    }
}
