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
            this.Loaded += TitleNameUser; // Добавление обработчика события при загрузке страницы
        }

        // Метод для отображения информации о пользователе при загрузке страницы
        private void TitleNameUser(object sender, RoutedEventArgs e)
        {
            // Получение логина пользователя из LoginInfoAll
            string login = LoginInfoAll.GetLogin();

            using (var db = new TeamProfiBDEntities()) // Использование контекста базы данных
            {
                var users = db.Users
                    .AsNoTracking()
                    .Where(u => u.Login == login)
                    .ToList(); // Получение списка всех пользователей с указанным логином

                if (users.Any()) // Если найдены пользователи с указанным логином
                {
                    foreach (var user in users) // Отображение информации о каждом найденном пользователе
                    {
                        TxtBl_NameUser.Text += $"{user.LastName} {user.FirstName} {user.Otchestvo}\n";
                        TxtBl_Gmailtxt.Text = user.Login;
                        TxtBl_DateYearstxt.Text = user.BirthYear;
                        TxtBl_TeachHousetxt.Text = user.College;
                        TxtBl_Roletxt.Text = user.RoleUsers;
                    }

                    // Занесение логина пользователя в контроль за пользователем для отслеживания в профиле
                    DataGetIDStudentClass.SetIDStud(TxtBl_Gmailtxt.Text);
                }
                else
                {
                    TxtBl_NameUser.Text = "Чет за херня ОПЯТЬ БАЗА БЛЯТЬ УПАЛА"; // Отображение сообщения об ошибке
                }
            }
        }

        // Обработчик события клика на кнопке выхода из аккаунта
        private void BtnExitAcc_Click(object sender, RoutedEventArgs e)
        {
            // Сброс логина и ID студента и задания
            LoginInfoAll.ShowLogin("net");
            DataGetIDStudentClass.SetIDStud("noap");
            DataGetIDAssigmentClass.SetIDAssig("noap");

            // Получение главного окна пользователя
            UserWindow main = Window.GetWindow(this) as UserWindow;

            if (main != null) // Если главное окно не пустое
            {
                // Открытие нового главного окна и очистка полей информации о пользователе
                WindowOpenClass.OpenWindow<MainWindow>();
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
