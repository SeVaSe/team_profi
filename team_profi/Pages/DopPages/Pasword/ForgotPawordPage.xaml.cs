using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для ForgotPawordPage.xaml
    /// </summary>
    public partial class ForgotPawordPage : Page
    {
        public ForgotPawordPage()
        {
            InitializeComponent();
        }

        private void GetCode_Click(object sender, RoutedEventArgs e)
        {
            string meEmail = "teamprofisupor@mail.ru";
            string mePassw = "PkGaTitAmgVgb8yy3Kpb";
            string emailTo = TxtBox_GmailPasw.Text; // Получение адреса электронной почты из TextBox
            string code = GenerateRandomCode(); // Генерация случайного 4-значного кода

            if (TxtBox_GmailPasw.Text != "" || TxtBox_GmailPasw.Text != null)
            {
                // Отправка письма
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(meEmail); // Ваш адрес электронной почты
                        mail.To.Add(emailTo); // Адрес получателя
                        mail.Subject = "Код восстановления пароля";
                        mail.Body = $"Ваш код восстановления пароля: {code}";

                        using (SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587))
                        {
                            smtp.Credentials = new NetworkCredential(meEmail, mePassw); // Учетные данные для доступа к вашему почтовому ящику
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                    MessageBox.Show("Код отправлен на почту.");

                    ControlCodePaswClass.CodePasw = code;
                    NavigationService?.Navigate(new CheckPaswPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьете свою почту, возможно вы ее не правильно указали", "Ошибка почты", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы не указали почту, заполните поле!", "Ошибка пустого значения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateRandomCode()
        {
            // Генерация случайного 4-значного кода
            Random random = new Random();
            int code = random.Next(1000, 9999);
            return code.ToString();
        }
    }
}
