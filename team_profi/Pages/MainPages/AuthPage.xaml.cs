using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using team_profi.Pages.MainPages;

namespace team_profi.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        // войти
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SignIn());

        }

        // зарегаться
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SignUo());
        }
    }
}
