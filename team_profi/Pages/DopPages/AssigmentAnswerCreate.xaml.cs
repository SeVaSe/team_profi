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
    /// Логика взаимодействия для AssigmentAnswerCreate.xaml
    /// </summary>
    public partial class AssigmentAnswerCreate : Page
    {
        public AssigmentAnswerCreate()
        {
            InitializeComponent();
            DateTime dateTime = DateTime.Now;
            TxtBl_DateTask.Text = dateTime.ToString("dd.MM.yyyy");
        }

        private void SendTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksUserPage());
        }
    }
}
