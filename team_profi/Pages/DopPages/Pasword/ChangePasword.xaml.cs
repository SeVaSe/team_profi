using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace team_profi.Pages.DopPages.Pasword
{
    /// <summary>
    /// Логика взаимодействия для ChangePasword.xaml
    /// </summary>
    public partial class ChangePasword : Page
    {
        public ChangePasword()
        {
            InitializeComponent();
        }

        private void ChangePasw_Click(object sender, RoutedEventArgs e)
        {
            string newPasw = TxtBox_NewPasw1.Text;
            string confirmPasw = TxtBox_NewPasw2.Text;
            string username = DataDBControlClass.GetName();

            if (TxtBox_NewPasw1.Text != "" || TxtBox_NewPasw2.Text != "")
            {
                if (TxtBox_NewPasw1.Text.Length >= 6)
                {
                    if (newPasw == confirmPasw)
                    {
                        bool passwordChanged = ChangePasswordInDB(username, newPasw);

                        if (passwordChanged)
                        {
                            MessageBox.Show("Пароль успешно изменен!", "Успешное восстановление", MessageBoxButton.OK, MessageBoxImage.Information);
                            DataDBControlClass.SetName("noap");
                            NavigationService?.Navigate(new MainPages.AuthPage());
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при изменении пароля, попробуйте заново!", "Ошибка изменения", MessageBoxButton.OK, MessageBoxImage.Error);
                            TxtBox_NewPasw1.Clear();
                            TxtBox_NewPasw2.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введенные пароли не совпадают", "Не прошла проверка паролей", MessageBoxButton.OK, MessageBoxImage.Error);
                        TxtBox_NewPasw2.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать не менее 6 символов.", "Ошибка ввода пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы оставили пустые поля ввода, проверьте и заполните их!", "Ошибка пустого значения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool ChangePasswordInDB(string username, string newPassword)
        {
            using (var db = new TeamProfiBDEntities()) // Замените YourDbContext на ваш контекст базы данных
            {
                try
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == username);

                    if (user != null)
                    {
                        user.Password = PasswordHasherClass.HashPassword(newPassword);
                        db.SaveChanges(); // Сохраняем изменения в базе данных

                        return true;
                    }
                    else
                    {
                        // Пользователь не найден
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
