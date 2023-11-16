using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using team_profi.Pages.AdminPages;
using static team_profi.Pages.AdminPages.AnswerAdminPage;

namespace team_profi.Pages.DopPages
{
    /// <summary>
    /// Логика взаимодействия для AnswerAdminSend.xaml
    /// </summary>
    public partial class AnswerAdminSend : Page
    {
        private Answers _answers; // Объект Answers - возможно, не используется в этом контексте, можно удалить
        private AnswerViewModel answViewModel; // Модель ответа, получаемая из другой страницы

        // Конструктор класса, вызывается при создании страницы
        public AnswerAdminSend(AnswerViewModel answerViewModel)
        {
            InitializeComponent();
            answViewModel = answerViewModel;
            FillAnswerDetails();
        }

        // Метод для заполнения подробностей ответа на странице
        private void FillAnswerDetails()
        {
            TxtBl_AnswerFio.Text = answViewModel.FIO;
            TxtBl_AnswerName.Text = answViewModel.Topic;
            TxtBl_AnswerText.Text = answViewModel.AnswerText;
            TxtBl_AnswerDate.Text = answViewModel.SubmissionDate;

            using (var db = new TeamProfiBDEntities())
            {
                // Получение ID ответа из базы данных по тексту ответа
                int answID = db.Answers
                    .Where(a => a.AnswerText == answViewModel.AnswerText)
                    .Select(a => (int)a.AnswerID)
                    .FirstOrDefault();

                // Получение информации о проверке ответа администратором и комментария
                bool? reviewed = db.Grades
                    .Where(g => g.AnswerID == answID)
                    .Select(g => g.ReviewedByAdmin)
                    .FirstOrDefault();
                string gradeCom = db.Grades
                    .Where(g => g.AnswerID == answID)
                    .Select(g => g.Comment)
                    .FirstOrDefault();
                string gradeAnsw = db.Grades
                    .Where(g => g.AnswerID == answID)
                    .Select(g => g.Grade.ToString())
                    .FirstOrDefault();

                TxtBox_Grade.Text = gradeAnsw; // Отображение оценки
                TxtBox_Comment.Text = gradeCom; // Отображение комментария


                if (reviewed.HasValue)
                {
                    if (reviewed.Value)
                    {
                        TxtBox_Grade.IsEnabled = false;
                        TxtBox_Comment.IsEnabled = false;
                    }
                }
            }
        }

        // Метод для отправки комментария к ответу
        private void SendCommentAnswer_Click(object sender, RoutedEventArgs e)
        {
            int gradeTxt = Convert.ToInt32(TxtBox_Grade.Text);
            string commentTxt = TxtBox_Comment.Text;
            string nameTeach = DataDBControlClass.GetName();

            int _gradeTxt = Convert.ToInt32(TxtBox_Grade.Text);
            if (_gradeTxt >= 1 && _gradeTxt <= 5)
            {
                gradeTxt = _gradeTxt;
            }
            else
            {
                MessageBox.Show("Доступная оценка находится в диапазоне от 1 до 5", "Примечание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            using (var db = new TeamProfiBDEntities())
            {
                try
                {
                    int answID = db.Answers
                        .Where(a => a.AnswerText == answViewModel.AnswerText)
                        .Select(a => (int)a.AnswerID)
                        .FirstOrDefault();

                    bool? isReviewedByAdmin = db.Grades
            .Where(g => g.AnswerID == answID)
            .Select(g => g.ReviewedByAdmin)
            .FirstOrDefault();

                    if (isReviewedByAdmin == null && isReviewedByAdmin != true)
                    {
                        int studentID = db.Answers
                            .Where(a => a.AnswerID == answID)
                            .Select(a => a.StudentID)
                            .FirstOrDefault();

                        int teachID = db.Users
                            .Where(u => u.Login == nameTeach)
                            .Select(u => u.UserID)
                            .FirstOrDefault();

                        var grade = new Grades()
                        {
                            AnswerID = answID,
                            TeacherID = teachID,
                            Grade = gradeTxt,
                            Comment = commentTxt,
                            ReviewedByAdmin = true
                        };

                        var ratingStudents = new StudentRatings()
                        {
                            StudentID = studentID,
                            TotalPoints = gradeTxt
                        };

                        int currentTotalPoints = db.StudentRatings
                            .Where(s => s.StudentID == studentID)
                            .Select(s => s.TotalPoints)
                            .FirstOrDefault();

                        int updatedTotalPoints = currentTotalPoints + gradeTxt;

                        db.StudentRatings
                            .Where(s => s.StudentID == studentID)
                            .FirstOrDefault()
                            .TotalPoints = updatedTotalPoints;

                        db.Grades.Add(grade);
                        db.SaveChanges();
                        MessageBox.Show("Ответ был успешно отправлен", "Отправка ответа", MessageBoxButton.OK, MessageBoxImage.Information);
                        DisableControlsIfReviewed();
                        NavigationService?.Navigate(new AnswerAdminPage());

                        TxtBox_Comment.Clear();
                        TxtBox_Grade.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Вы уже проверили данное задание и установили оценку.", "Примечание", MessageBoxButton.OK, MessageBoxImage.Warning);
                       
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка, попробуйте заново ввести значения и проверить их ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            
        }

        // Метод для отключения элементов управления, если ответ уже проверен
        private void DisableControlsIfReviewed()
        {
            using (var db = new TeamProfiBDEntities())
            {
                int answID = db.Answers
                    .Where(a => a.AnswerText == answViewModel.AnswerText)
                    .Select(a => (int)a.AnswerID)
                    .FirstOrDefault();

                bool? reviewed = db.Grades
                    .Where(g => g.AnswerID == answID)
                    .Select(g => g.ReviewedByAdmin)
                    .FirstOrDefault();

                if (reviewed.HasValue && reviewed.Value)
                {
                    TxtBox_Grade.IsEnabled = false;
                    TxtBox_Comment.IsEnabled = false;
                }
            }
        }
    }

}
