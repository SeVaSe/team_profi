using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace team_profi.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для MainUserPage.xaml
    /// </summary>
    public partial class MainUserPage : Page
    {
        public MainUserPage()
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
                        TxtBl_NameUser.Text += $"Добро пожаловать {user.LastName} {user.FirstName} {user.Otchestvo}!\n";
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
            
            UserWindow main = Window.GetWindow(this) as UserWindow;

            if (main != null)
            {
                
                WindowOpenClass.OpenWindow<MainWindow>();
                TxtBl_NameUser.Text = "";
                TxtBl_OsnInfo.Text = "";
                main.Close();
            }

        }
    }
}
