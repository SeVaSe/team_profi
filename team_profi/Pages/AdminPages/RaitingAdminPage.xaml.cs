using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для RaitingAdminPage.xaml
    /// </summary>
    public partial class RaitingAdminPage : Page
    {
        

        public RaitingAdminPage()
        {
            InitializeComponent();
            DataGridUser.ItemsSource = TeamProfiBDEntities.GetEntities1().Users.ToList();

        }

        private void NumberTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock numberTextBlock = sender as TextBlock;

            if (numberTextBlock != null)
            {
                // Получите элемент данных, связанный с этой строкой
                var user = numberTextBlock.DataContext as Users; // Замените "User" на ваш тип данных

                if (user != null)
                {
                    // Получите индекс элемента данных и увеличьте его на 1
                    int index = TeamProfiBDEntities.GetEntities1().Users.ToList().IndexOf(user) + 1;

                    // Установите номер в текстовом блоке
                    numberTextBlock.Text = index.ToString();
                }
            }
        }




    }

}
