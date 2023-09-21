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

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new TeamProfiBDEntities())
            {
                var user = db.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == TxtBoxGmail.Text);

                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует!", "Не существующий пользователь", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtBoxGmail.Clear();
                    TxtBoxPasw.Clear();
                }
                else if (TxtBoxPasw.Text.Length >= 6)
                {
                    if (user.Password != TxtBoxPasw.Text)
                    {
                        MessageBox.Show($"Пароль указан не верно {user.Password}", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                        TxtBoxPasw.Clear();
                    }
                    else
                    {
                        switch (user.Role)
                        {
                            case "admin":
                                MessageBox.Show("ADMIN");
                                break;
                            case "user":
                                MessageBox.Show("USER");
                                break;
                            case "teach":
                                MessageBox.Show("TEACH");
                                break;
                        }
                        TxtBoxGmail.Clear();
                        TxtBoxPasw.Clear();
                    }
                }
                else if (TxtBoxPasw.Text.Length < 6)
                {
                    MessageBox.Show("Вы указали маленький пароль", "Маленький пароль", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxtBoxPasw.Clear();
                }
            }
        }
    }
}
