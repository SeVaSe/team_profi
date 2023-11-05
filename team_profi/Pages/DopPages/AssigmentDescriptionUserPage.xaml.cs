using System;
using System.Collections.Generic;
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
using team_profi.Pages.UserPages;

namespace team_profi.Pages.DopPages
{
    /// <summary>
    /// Логика взаимодействия для AssigmentDescriptionUserPage.xaml
    /// </summary>
    public partial class AssigmentDescriptionUserPage : Page
    {
        private Assignments _assignment;

        public AssigmentDescriptionUserPage(Assignments assignment)
        {
            InitializeComponent();
            _assignment = assignment;
            TxtBl_NameTask.Text = assignment.Topic.ToUpper();
            TxtBl_DateTask.Text = assignment.CreationDate;
            TxtBl_DescriptTask.Text = assignment.TaskDescription;
        }

        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksUserPage());
        }

        private void ExecuteTask_Click(object sender, RoutedEventArgs e)
        {
            string a = ""; // контроль за тем, чтобы топик был не в КАПСЕ

            using (var db = new TeamProfiBDEntities())
            {
                var assigs = db.Assignments
                    .AsNoTracking()
                    .Where(u => u.Topic.ToUpper() == _assignment.Topic) // достаем все топики и сравниваем их под КАПСОМ
                    .ToList(); 

                foreach (var topic in assigs)
                {
                    a = topic.Topic; // после мы берем из бд оригиналы, и тем самым получаем нужный топик для работы в дальнейшем
                }

            }

            DataGetIDAssigmentClass.SetIDAssig(a); // отпраляем в класс
            MessageBox.Show(a);
            NavigationService.Navigate(new DopPages.AssigmentAnswerCreate());
             
        }
    }
}
