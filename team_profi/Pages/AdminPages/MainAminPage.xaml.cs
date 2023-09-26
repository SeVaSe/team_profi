﻿using System;
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

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для MainAminPage.xaml
    /// </summary>
    public partial class MainAminPage : Page
    {
        public MainAminPage()
        {
            InitializeComponent(); 
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка информации в TextBlock при загрузке окна.
            tttt.Text = LoginInfoAll.GetLogin();
        }
    }
}
