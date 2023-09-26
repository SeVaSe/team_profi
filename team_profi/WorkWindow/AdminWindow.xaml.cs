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
using System.Windows.Shapes;
using team_profi.Classes;
using team_profi.Pages.AdminPages;
using team_profi.Pages.MainPages;

namespace team_profi.WorkWindow
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            ControlClass cntrlCl = new ControlClass(this);
            Btn_Close.Click += cntrlCl.close_control;
            Btn_minim.Click += cntrlCl.minimized_control;
            Btn_perezapusk.Click += (sender, e) => cntrlCl.perezapusk_control(new AdminWindow());
            br_up.MouseLeftButtonDown += cntrlCl.Window_MouseLeftButtonDown;
            br_up.MouseMove += cntrlCl.Window_MouseMove;
        }


        
        // Главная Админа
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            ProfiMain.Navigate(new MainAminPage());
        }

        // Рейтинг Админа
        private void BtnRaiting_Click(object sender, RoutedEventArgs e)
        {
            ProfiMain.Navigate(new RaitingAdminPage());
        }

        // Задачи Админа
        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            ProfiMain.Navigate(new TasksAdminPage());
        }

        // Ответы Админа
        private void BtnAnswer_Click(object sender, RoutedEventArgs e)
        {
            ProfiMain.Navigate(new AnswerAdminPage());
        }
    }
}
