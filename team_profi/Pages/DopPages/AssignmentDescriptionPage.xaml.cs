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

namespace team_profi.Pages.DopPages
{
    /// <summary>
    /// Логика взаимодействия для AssignmentDescriptionPage.xaml
    /// </summary>
    public partial class AssignmentDescriptionPage : Page
    {
        private Assignments _assignment;

        public AssignmentDescriptionPage(Assignments assignment)
        {
            InitializeComponent();
            _assignment = assignment;
            TxtBl_Topic.Text = assignment.TaskDescription;
        }
    }
}
