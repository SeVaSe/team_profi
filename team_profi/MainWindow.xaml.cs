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
using team_profi.Pages.MainPages;

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
        }

        //----------------КНОПКИ ОБРАБОТЧИКИ ПРИЛОЖЕНИЯ----------------
        //свернуть
        private void Svernut_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //выход
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var mesClose = MessageBox.Show("Вы точно хоитет выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (mesClose)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }                
        }

        //перезапуск
        private void Perezapusk_Click(object sender, RoutedEventArgs e)
        {
            Window wind = new MainWindow();
            this.Close();
            wind.Show();
        }

        //перетаскивание окна
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();

        }

        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            ProfiMain.Navigate(new AuthPage());
        }
    }
}
