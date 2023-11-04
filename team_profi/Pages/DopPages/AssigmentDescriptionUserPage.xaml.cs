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
            TxtBl_NameTask.Text = assignment.Topic;
            TxtBl_DateTask.Text = assignment.CreationDate;
            TxtBl_DescriptTask.Text = assignment.TaskDescription;
        }

        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksUserPage());
        }

        private void ExecuteTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DopPages.AssigmentAnswerCreate());
        }
    }
}
