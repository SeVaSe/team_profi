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
using team_profi.Pages.AdminPages;
using team_profi.Pages.MainPages;
using team_profi.Classes;
using System.Text.RegularExpressions;

namespace team_profi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new MainWindow());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;



        }
        

       

        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            ProfiMain.Navigate(new AuthPage());     
        }


        // КНОПКИ ПРЕДОТВРАЩАЮТ ПЕРЕХОД НЕ АВТОРИЗОВАННОГО ПОЛЬЗОВАТЕЛЯ ПО РАЗДЕЛАМ
        private void BtnRaiting_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы не вошли в аккаунт, ввойдите и после переходите в нужные разделы!", "Ошибка не авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы не вошли в аккаунт, ввойдите и после переходите в нужные разделы!", "Ошибка не авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы не вошли в аккаунт, ввойдите и после переходите в нужные разделы!", "Ошибка не авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
