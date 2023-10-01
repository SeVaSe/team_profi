using System;
using System.Collections.Generic;
using System.Data;
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

namespace team_profi.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string dateBirth = $"{TxtBox_Birth1.Text}.{TxtBox_Birth2.Text}.{TxtBox_Birth3.Text}";

            var newUser = new Users
            {
                Login = TxtBoxGmailUp.Text,
                FirstName = TxtBoxFirstnameUp.Text,
                LastName = TxtBoxLastnameUp.Text,
                Otchestvo = TxtBoxOtchestvoUp.Text,
                Password = TxtBoxPaswUp.Text,
                Role = "user",
                BirthYear = dateBirth,
                College = TxtBoxUchebUp.Text,
                RoleUsers = TxtBoxRoleUp.Text

            };
            using (var db = new TeamProfiBDEntities())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }

            TxtBoxGmailUp.Clear();
            TxtBoxFirstnameUp.Clear();
            TxtBoxLastnameUp.Clear();
            TxtBoxOtchestvoUp.Clear();
            TxtBoxPaswUp.Clear();
            TxtBox_Birth1.Clear();
            TxtBox_Birth2.Clear();
            TxtBox_Birth3.Clear();
            TxtBoxUchebUp.Clear();
            TxtBoxRoleUp.Clear();

        }
    }
}
