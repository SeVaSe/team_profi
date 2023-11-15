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
using team_profi.Classes;

namespace team_profi.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для RaitingAdminPage.xaml
    /// </summary>
    /// 
    

    public partial class RaitingAdminPage : Page
    {
        private ObservableCollection<StudentRatingViewModel> studentRatings;
        public RaitingAdminPage()
        {
            InitializeComponent();
            LoadStudentRatings();
        }

        private void LoadStudentRatings()
        {
            studentRatings = new ObservableCollection<StudentRatingViewModel>();

            using (var db = new TeamProfiBDEntities())
            {
                var sortedStudentRatings = db.StudentRatings
                    .OrderByDescending(sr => sr.TotalPoints)
                    .ToList();

                int rank = 1;
                foreach (var studentRating in sortedStudentRatings)
                {
                    int studentID = studentRating.StudentID;
                    var student = db.Users.FirstOrDefault(s => s.UserID == studentID);

                    if (student != null)
                    {
                        var viewModel = new StudentRatingViewModel(rank, $"{student.LastName} {student.FirstName}", studentRating.TotalPoints);
                        studentRatings.Add(viewModel);
                        rank++;
                    }
                }
            }

            DataGridUser.ItemsSource = studentRatings;
        }
    }


}
