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
            var studentRatings = TeamProfiBDEntities.GetEntities1().StudentRatings.OrderByDescending(sr => sr.TotalPoints).ToList();

            //  отсортированные данные к DataGrid
            DataGridUser.ItemsSource = studentRatings;
        }

        private void NumberTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock numberTextBlock = sender as TextBlock;

            if (numberTextBlock != null)
            {
                // Элемент данных, связанный с этой строкой
                var studentRating = numberTextBlock.DataContext as StudentRatings;

                if (studentRating != null)
                {
                    // Индекс элемента данных и увеличьте его на 1
                    int index = DataGridUser.Items.IndexOf(studentRating) + 1;

                    // Номер в текстовом блоке
                    numberTextBlock.Text = index.ToString();

                    int studentID = studentRating.StudentID;

                    // Данные студента
                    var student = TeamProfiBDEntities.GetEntities1().Users.FirstOrDefault(s => s.UserID == studentID);

                    if (student != null)
                    {
                        //  новый TextBlock для имени и фамилии студента
                        TextBlock txtBl_Info = new TextBlock
                        {
                            Style = (Style)FindResource("ModernTextBlockRaiting"),
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = new Thickness(5),
                            Padding = new Thickness(80, 0, 0, 0),
                            Text = $"{student.LastName} {student.FirstName} {student.Otchestvo}"
                        };

                        //  новый TextBlock к Grid для текущей строки
                        var gridText = numberTextBlock.Parent as Grid;
                        if (gridText != null)
                        {
                            gridText.Children.Add(txtBl_Info);
                        }
                    }
                }
            }
        }


        private T FindVisualChild<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null)
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == name)
                    {
                        return (T)child;
                    }

                    T result = FindVisualChild<T>(child, name);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }


}
