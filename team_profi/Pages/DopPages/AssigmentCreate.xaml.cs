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
using team_profi.Pages.AdminPages;
using team_profi.Pages.UserPages;

namespace team_profi.Pages.DopPages
{
    /// <summary>
    /// Логика взаимодействия для AssigmentCreate.xaml
    /// </summary>
    public partial class AssigmentCreate : Page
    {
        public AssigmentCreate()
        {
            InitializeComponent();
            TxtBl_NameTask.Text = "НОВОЕ ЗАДАНИЕ";
            
            DateTime dateTime = DateTime.Now;
            TxtBl_DateTask.Text = dateTime.ToString("dd.MM.yyyy");
        }

        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksAdminPage());
        }

        private void SendTask_Click(object sender, RoutedEventArgs e)
        {
            string nameAdmin = DataDBControlClass.GetName();

            using (var db = new TeamProfiBDEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == nameAdmin);

                if (user != null)
                {
                    int userIdAdmin = user.UserID;

                    var newAssigment = new Assignments()
                    {
                        TeachID = userIdAdmin,
                        Topic = TxtBox_Topic.Text,
                        TaskDescription = TxtBox_Descr.Text,
                        CreationDate = TxtBl_DateTask.Text
                    };

                    using (var dbs = new TeamProfiBDEntities())
                    {
                        dbs.Assignments.Add(newAssigment);
                        dbs.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Данного учителя нет, стоит обратиться в тех-поддержку и решить данный вопрос", "Сбой системы!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                
            }
            

            

            

            TxtBox_Topic.Clear();
            TxtBox_Descr.Clear();

        }
    }
}
