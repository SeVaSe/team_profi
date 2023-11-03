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
using team_profi.WorkWindow;

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для MainAminPage.xaml
    /// </summary>
    public partial class MainAminPage : Page
    {
        public MainAminPage()
        {
            InitializeComponent();
            this.Loaded += TitleNameUser;
        }

        private void TitleNameUser(object sender, RoutedEventArgs e)
        {
            // Получаем логин из LoginInfoAll
            string login = LoginInfoAll.GetLogin();

            using (var db = new TeamProfiBDEntities())
            {
                var users = db.Users
                    .AsNoTracking()
                    .Where(u => u.Login == login)
                    .ToList(); // Получаем список всех пользователей с указанным логином

                if (users.Any())
                {
                    
                    foreach (var user in users)
                    {
                        TxtBl_NameUser.Text += $"{user.LastName} {user.FirstName} {user.Otchestvo}\n";
                        TxtBl_Gmailtxt.Text = user.Login;
                        TxtBl_DateYearstxt.Text = user.BirthYear;
                        TxtBl_TeachHousetxt.Text = user.College;
                        TxtBl_Roletxt.Text = user.RoleUsers;
                    }
                    
                    
                    
                }
                else
                {
                    TxtBl_NameUser.Text = "Чет за херня ОПЯТЬ БАЗА БЛЯТЬ УПАЛА";
                }
            }
        }

        private void BtnExitAcc_Click(object sender, RoutedEventArgs e)
        {
            LoginInfoAll.ShowLogin("net");
            DataDBControlClass.SetName("noap");

            AdminWindow main = Window.GetWindow(this) as AdminWindow;

            if (main != null)
            {
                WindowOpenClass.OpenWindow<MainWindow>();
                TxtBl_NameUser.Text = "";
                TxtBl_Gmailtxt.Text = "";
                TxtBl_DateYearstxt.Text = "";
                TxtBl_TeachHousetxt.Text = "";
                TxtBl_Roletxt.Text = "";

                main.Close();
            }
        }
    }
}
