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
        private Answers _answers;
        private AnswerViewModel answViewModel;

        public AnswerAdminSend(AnswerViewModel answerViewModel)
        {
            InitializeComponent();
            TxtBl_AnswerFio.Text = answerViewModel.FIO;
            TxtBl_AnswerName.Text = answerViewModel.Topic;
            TxtBl_AnswerText.Text = answerViewModel.AnswerText;
            TxtBl_AnswerDate.Text = answerViewModel.SubmissionDate;

            answViewModel = answerViewModel;

            MessageBox.Show(DataDBControlClass.GetName());
            MessageBox.Show($"{answerViewModel.Topic} {answerViewModel.FIO} {answerViewModel.AnswerText} {answerViewModel.SubmissionDate}");
        }

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
                MessageBox.Show("Доступная оценка находится в диапозоне - от 1 до 5", "Примечание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            using (var db = new TeamProfiBDEntities())
            {
                try
                {
                    // условие для сравнения AnswerText
                    int answID = db.Answers
                        .Where(a => a.AnswerText == answViewModel.AnswerText)
                        .Select(a => (int)a.AnswerID) // преобразование в int? для обработки возможного null
                        .FirstOrDefault();

                    int studentID = db.Answers
                        .Where(a => a.AnswerID == answID)
                        .Select(a => a.StudentID)
                        .FirstOrDefault();

                    int teachID = db.Users
                        .Where(u => u.Login == nameTeach)
                        .Select(u => u.UserID)
                        .FirstOrDefault();

                    //MessageBox.Show($"IDansw: {answID}  IDteach: {teachID}  IDstud: {studentID}");

                    var grade = new Grades()
                    {
                        AnswerID = answID,
                        TeacherID = teachID,
                        Grade = gradeTxt,
                        Comment = commentTxt
                    };

                    var ratingStudents = new StudentRatings()
                    {
                        StudentID = studentID,
                        TotalPoints = gradeTxt
                    };

                    // Получаем текущее значение TotalPoints из базы данных
                    int currentTotalPoints = db.StudentRatings
                        .Where(s => s.StudentID == studentID)
                        .Select(s => s.TotalPoints)
                        .FirstOrDefault();

                    // Прибавляем новое значение к текущему TotalPoints
                    int updatedTotalPoints = currentTotalPoints + gradeTxt;

                    // Обновляем значение TotalPoints в базе данных
                    db.StudentRatings
                        .Where(s => s.StudentID == studentID)
                        .FirstOrDefault()
                        .TotalPoints = updatedTotalPoints;


                    db.Grades.Add(grade);
                    db.SaveChanges();
                    MessageBox.Show("Ответ был успешно отправлен", "Отправка ответа", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new AnswerAdminPage());
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка, попробуйте заново ввести значения и проверить их ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            TxtBox_Comment.Clear();
            TxtBox_Grade.Clear();

        }
    }
}
