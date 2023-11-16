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
using team_profi.WorkWindow;

namespace team_profi.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Вход"
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            using (var db = new TeamProfiBDEntities()) // Использование контекста базы данных
            {
                var user = db.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == TxtBoxGmail.Text); // Поиск пользователя по логину

                if (user == null) // Если пользователь не найден
                {
                    // Отображение сообщения об ошибке
                    MessageBox.Show("Такого пользователя не существует!", "Не существующий пользователь", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtBoxGmail.Clear();
                    TxtBoxPasw.Clear();
                }
                else if (TxtBoxPasw.Text.Length >= 6) // Если пароль длиннее или равен 6 символам
                {
                    // Хэширование введенного пароля
                    string hashedPassword = PasswordHasherClass.HashPassword(TxtBoxPasw.Text);

                    switch (user.Role) // Проверка роли пользователя
                    {
                        case "admin":
                            if (user.Password != TxtBoxPasw.Text) // Если пароль не совпадает
                            {
                                // Отображение предупреждения о неверном пароле
                                MessageBox.Show("Пароль указан не верно", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                                TxtBoxPasw.Clear();
                            }
                            else
                            {
                                // Открытие окна для администратора и закрытие текущего окна
                                WindowOpenClass.OpenWindow<AdminWindow>();
                                LoginInfoAll.ShowLogin(user.Login);
                                DataDBControlClass.SetName(user.Login);
                                mainWindow.Close();
                            }
                            break;

                        case "user":
                            if (user.Password != hashedPassword) // Если хэшированный пароль не совпадает
                            {
                                // Отображение предупреждения о неверном пароле
                                MessageBox.Show("Пароль указан не верно", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                                TxtBoxPasw.Clear();
                            }
                            else
                            {
                                // Открытие окна для пользователя и закрытие текущего окна
                                WindowOpenClass.OpenWindow<UserWindow>();
                                LoginInfoAll.ShowLogin(user.Login);
                                mainWindow.Close();
                            }
                            break;
                    }
                    TxtBoxGmail.Clear();
                    TxtBoxPasw.Clear();
                }
                else if (TxtBoxPasw.Text.Length < 6) // Если пароль короче 6 символов
                {
                    // Отображение сообщения о коротком пароле
                    MessageBox.Show("Вы указали маленький пароль", "Маленький пароль", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxtBoxPasw.Clear();
                }
            }
        }

        // Обработчик нажатия кнопки "Забыли пароль?"
        private void ForgotPasword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DopPages.Pasword.ForgotPawordPage()); // Навигация на страницу восстановления пароля
        }
    }
}
