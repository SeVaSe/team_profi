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
            this.Loaded += TitleNameUser; // При загрузке страницы добавляется обработчик события для отображения информации о пользователе
        }

        // Метод для отображения информации о пользователе при загрузке страницы
        private void TitleNameUser(object sender, RoutedEventArgs e)
        {
            // Получение логина пользователя из LoginInfoAll
            string login = LoginInfoAll.GetLogin();

            using (var db = new TeamProfiBDEntities())
            {
                // Получение списка пользователей с указанным логином из базы данных
                var users = db.Users
                    .AsNoTracking()
                    .Where(u => u.Login == login)
                    .ToList();

                if (users.Any()) // Если найдены пользователи с указанным логином
                {
                    // Отображение информации о каждом найденном пользователе
                    foreach (var user in users)
                    {
                        TxtBl_NameUser.Text += $"{user.LastName} {user.FirstName} {user.Otchestvo}\n";
                        TxtBl_Gmailtxt.Text = user.Login;
                        TxtBl_DateYearstxt.Text = user.BirthYear;
                        TxtBl_TeachHousetxt.Text = user.College;
                        TxtBl_Roletxt.Text = user.RoleUsers;
                    }
                }
                else // Если пользователь не найден в базе данных
                {
                    TxtBl_NameUser.Text = "Чет за херня ОПЯТЬ БАЗА БЛЯТЬ УПАЛА"; // Отображение сообщения об ошибке
                }
            }
        }

        // Обработчик события для выхода из аккаунта
        private void BtnExitAcc_Click(object sender, RoutedEventArgs e)
        {
            // Сброс логина и имени пользователя
            LoginInfoAll.ShowLogin("net");
            DataDBControlClass.SetName("noap");

            // Получение главного окна
            AdminWindow main = Window.GetWindow(this) as AdminWindow;

            if (main != null) // Если главное окно не пустое
            {
                WindowOpenClass.OpenWindow<MainWindow>(); // Открытие нового главного окна
                                                          // Очистка полей информации о пользователе
                TxtBl_NameUser.Text = "";
                TxtBl_Gmailtxt.Text = "";
                TxtBl_DateYearstxt.Text = "";
                TxtBl_TeachHousetxt.Text = "";
                TxtBl_Roletxt.Text = "";

                main.Close(); // Закрытие текущего окна
            }
        }
    }

}
