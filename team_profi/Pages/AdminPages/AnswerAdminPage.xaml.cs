using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AnswerAdminPage.xaml
    /// </summary>
    public partial class AnswerAdminPage : Page
    {
        public class AnswerViewModel
        {
            public string Topic { get; set; }
            public string FIO { get; set; }
            public string AnswerText { get; set; }
            public string SubmissionDate { get; set; }
        }

        public ObservableCollection<AnswerViewModel> answers;

        public AnswerAdminPage()
        {
            InitializeComponent();
            answers = new ObservableCollection<AnswerViewModel>();
            LoadAnswers();
        }

        private void LoadAnswers()
        {
            using (var db = new TeamProfiBDEntities())
            {
                var answersFromDB = db.Answers.ToList();
                foreach (var answer in answersFromDB)
                {
                    var assignment = db.Assignments.FirstOrDefault(a => a.AssigID == answer.AssignmentID);
                    var user = db.Users.FirstOrDefault(u => u.UserID == answer.StudentID);

                    if (assignment != null && user != null)
                    {
                        answers.Add(new AnswerViewModel
                        {
                            Topic = assignment.Topic.ToUpper(),
                            FIO = $"{user.LastName.ToUpper()} {user.FirstName.ToUpper()} {user.Otchestvo.ToUpper()}",
                            AnswerText = answer.AnswerText,
                            SubmissionDate = answer.SubmissionDate
                        });
                    }
                }

                DataGridUser.ItemsSource = answers;
            }
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is AnswerViewModel answers)
            {
                //DopPages.AssignmentDescriptionPage descriptionPage = new DopPages.AssignmentDescriptionPage(assignment);

                MessageBox.Show("ПЕРЕХОД НА КОММЕНТИРОВАНИЕ ЗАДАНИЯ");
            }
        }


    }
}
