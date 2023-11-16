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

namespace team_profi.Pages.DopPages.Pasword
{
    /// <summary>
    /// Логика взаимодействия для CheckPaswPage.xaml
    /// </summary>
    public partial class CheckPaswPage : Page
    {
        public CheckPaswPage()
        {
            InitializeComponent();
        }

        private void CodeVerify_Click(object sender, RoutedEventArgs e)
        {
            string codePasw = ControlCodePaswClass.CodePasw;

            if (TxtBox_Code1.Text.Length >= 2 || TxtBox_Code2.Text.Length >= 2 || TxtBox_Code3.Text.Length >= 2 || TxtBox_Code4.Text.Length >= 2)
            {
                MessageBox.Show("Не правильный код! Вы ввели в одно поле, 2 или более значений, исправьте и попробуйте заново",
                    "Ошибка заполнения полей", MessageBoxButton.OK, MessageBoxImage.Error);

                foreach (var control in GridCode.Children)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Clear();
                    }
                }
            }
            else if(TxtBox_Code1.Text != "" && TxtBox_Code2.Text != "" && TxtBox_Code3.Text != "" && TxtBox_Code4.Text != "")
            {
                string fullCodePage = TxtBox_Code1.Text + TxtBox_Code2.Text + TxtBox_Code3.Text + TxtBox_Code4.Text;

                if (fullCodePage == codePasw)
                {
                    MessageBox.Show("Код подтвержден!", "Успешное подтверждения", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new ChangePasword());
                    ControlCodePaswClass.CodePasw = "noap";
                }
                else
                {
                    MessageBox.Show("Не правильный код!", "Провальное подтверждения", MessageBoxButton.OK, MessageBoxImage.Error);

                    foreach (var control in GridCode.Children)
                    {
                        if (control is TextBox textBox)
                        {
                            textBox.Clear();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не указали код! Введите код заново", "Ошибка пустого значения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
