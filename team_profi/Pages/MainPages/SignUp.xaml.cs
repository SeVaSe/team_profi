using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace team_profi.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            string email = TxtBoxGmailUp.Text;



            // проверка на пустые значения
            List<string> emptyFields = new List<string>();

            foreach (var textBox in new TextBox[] { TxtBoxGmailUp, TxtBoxFirstnameUp, TxtBoxLastnameUp, TxtBoxOtchestvoUp,
                            TxtBoxPaswUp, TxtBox_Birth1, TxtBox_Birth2, TxtBox_Birth3,
                            TxtBoxUchebUp, TxtBoxRoleUp })
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    switch (textBox.Name)
                    {
                        case nameof(TxtBoxGmailUp):
                            emptyFields.Add("Почта");
                            break;
                        case nameof(TxtBoxPaswUp):
                            emptyFields.Add("Пароль");
                            break;
                        case nameof(TxtBoxLastnameUp):
                            emptyFields.Add("Фамилия");
                            break;
                        case nameof(TxtBoxFirstnameUp):
                            emptyFields.Add("Имя");
                            break;
                        case nameof(TxtBoxOtchestvoUp):
                            emptyFields.Add("Отчество");
                            break;
                        case nameof(TxtBox_Birth1):
                            emptyFields.Add("День рождения");
                            break;
                        case nameof(TxtBox_Birth2):
                            emptyFields.Add("Месяц рождения");
                            break;
                        case nameof(TxtBox_Birth3):
                            emptyFields.Add("Год рождения");
                            break;
                        case nameof(TxtBoxRoleUp):
                            emptyFields.Add("Роль");
                            break;
                        case nameof(TxtBoxUchebUp):
                            emptyFields.Add("Учебное заведение");
                            break;
                    }
                }
            }

            if (emptyFields.Count > 0)
            {
                string errorMessage = $"Пожалуйста, заполните следующие поля: {string.Join(", ", emptyFields)}";
                MessageBox.Show(errorMessage, "Ошибка не заполненных значений", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата почты
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка ввода почты", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            // Проверка пароля
            if (TxtBoxPaswUp.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов.", "Ошибка ввода пароля", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBoxPaswUp.Clear();
                return;
            }

            // Проверка фамилии, имени, отчества
            if (!IsValidName(TxtBoxFirstnameUp.Text) ||
                !IsValidName(TxtBoxLastnameUp.Text) ||
                !IsValidName(TxtBoxOtchestvoUp.Text))
            {
                MessageBox.Show("Введите корректные данные (только буквы, без чисел и других знаков).", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка даты рождения
            if (int.TryParse(TxtBox_Birth1.Text, out int day) &&
                int.TryParse(TxtBox_Birth2.Text, out int month) &&
                int.TryParse(TxtBox_Birth3.Text, out int year) &&
                month >= 1 && month <= 12 &&
                day >= 1 && day <= 31 &&
                year >= 1 && year <= 2025)
            {
                string dateBirth = $"{day}.{month}.{year}";

                var newUser = new Users
                {
                    Login = TxtBoxGmailUp.Text,
                    FirstName = TxtBoxFirstnameUp.Text,
                    LastName = TxtBoxLastnameUp.Text,
                    Otchestvo = TxtBoxOtchestvoUp.Text,
                    Password = PasswordHasherClass.HashPassword(TxtBoxPaswUp.Text),
                    Role = "user",
                    BirthYear = dateBirth,
                    College = TxtBoxUchebUp.Text,
                    RoleUsers = TxtBoxRoleUp.Text
                };

                using (var db = new TeamProfiBDEntities())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Получаем ID нового пользователя
                    int newUserId = newUser.UserID;

                    // Создаем запись в таблице StudentRatings
                    db.StudentRatings.Add(new StudentRatings
                    {
                        StudentID = newUserId,
                        TotalPoints = 0
                    });

                    db.SaveChanges();
                }

                // Очищаем поля ввода
                foreach (var textBox in new TextBox[] { TxtBoxGmailUp, TxtBoxFirstnameUp, TxtBoxLastnameUp, TxtBoxOtchestvoUp,
                                        TxtBoxPaswUp, TxtBox_Birth1, TxtBox_Birth2, TxtBox_Birth3,
                                        TxtBoxUchebUp, TxtBoxRoleUp })
                {
                    textBox.Clear();
                }
                NavigationService?.Navigate(new AuthPage());
            }
            else
            {
                MessageBox.Show("Введите корректную дату рождения.", "Ошибка ввода даты рождения", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBox_Birth1.Clear();
                TxtBox_Birth2.Clear();
                TxtBox_Birth3.Clear();
            }
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter);
        }

    }
}
