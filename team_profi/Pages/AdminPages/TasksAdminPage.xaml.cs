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
        private ObservableCollection<Assignments> assignments;

        public TasksAdminPage()
        {
            InitializeComponent();

            assignments = new ObservableCollection<Assignments>();

            using (var db = new TeamProfiBDEntities())
            {
                var assignmentsFromDb = db.Assignments.ToList();
                foreach (var assignment in assignmentsFromDb)
                {
                    assignments.Add(new Assignments
                    {
                        AssigID = assignment.AssigID,
                        TeachID = assignment.TeachID,
                        Topic = assignment.Topic.ToUpper(),
                        TaskDescription = assignment.TaskDescription,
                        CreationDate = assignment.CreationDate
                    });
                }

                DataGridUser.ItemsSource = assignments;
            }
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Assignments assignment)
            {
                DopPages.AssignmentDescriptionPage descriptionPage = new DopPages.AssignmentDescriptionPage(assignment);

                NavigationService.Navigate(descriptionPage);
            }
        }



        private void CreateNewTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AssigmentCreate());
        }
    }
}
