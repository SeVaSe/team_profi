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
using static team_profi.Pages.UserPages.AnswerUserPage;

namespace team_profi.Pages.DopPages
{
    /// <summary>
    /// Логика взаимодействия для AnswerUserInfoPage.xaml
    /// </summary>
    public partial class AnswerUserInfoPage : Page
    {
        

        public AnswerUserInfoPage(AnswerTeachViewModel viewModel)
        {
            InitializeComponent();

            TxtBl_AnswerDate.Text = viewModel.SubmissionDate;
            TxtBl_AnswerName.Text = viewModel.Topic;
            TxtBox_Grade.Text = viewModel.Grade.ToString().Substring(0, 1);
            TxtBl_AnswerText.Text = viewModel.AnswerText;
            TxtBL_Comment.Text = viewModel?.Comment ?? "Вашу работу пока не проверили и не прокомментировали";

        }
    }
}
