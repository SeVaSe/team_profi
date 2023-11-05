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
using team_profi.Pages.UserPages;

namespace team_profi.Pages.DopPages
{
    /// <summary>
    /// Логика взаимодействия для AssigmentAnswerCreate.xaml
    /// </summary>
    public partial class AssigmentAnswerCreate : Page
    {
        public AssigmentAnswerCreate()
        {
            InitializeComponent();
            DateTime dateTime = DateTime.Now;
            TxtBl_DateTask.Text = dateTime.ToString("dd.MM.yyyy");
        }

        private void SendTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int studID = 999;
                int assigID = 999;
                string nameStud = DataGetIDStudentClass.GetName();
                string nameAssig = DataGetIDAssigmentClass.GetName();
                

                using (var db = new TeamProfiBDEntities())
                {
                    MessageBox.Show("0");
                    var users = db.Users
                        .AsNoTracking()
                        .Where(u => u.Login == nameStud)
                        .ToList();
                    MessageBox.Show("1");
                    foreach (var user in users)
                    {
                        studID = user.UserID;
                    }

                    MessageBox.Show("2");
                    var assigs = db.Assignments
                        .AsNoTracking()
                        .Where(u => u.Topic == nameAssig)
                        .ToList();
                    MessageBox.Show(nameAssig);
                    foreach (var assig in assigs)
                    {
                        assigID = assig.AssigID;
                    }
                    MessageBox.Show("3");



                }
                MessageBox.Show("4");
                var answerItem = new Answers()
                {
                    AssignmentID = assigID,
                    StudentID = studID,
                    AnswerText = TxtBox_Descr.Text,
                    SubmissionDate = TxtBl_DateTask.Text
                };
                MessageBox.Show(studID.ToString());
                MessageBox.Show(assigID.ToString());
                MessageBox.Show("5");
                using (var db = new TeamProfiBDEntities())
                {
                    db.Answers.Add(answerItem);
                    db.SaveChanges();
                }

                NavigationService.Navigate(new TasksUserPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ошибка: {ex.Message}. Подробности: {ex.InnerException?.Message}");
            }

        }

        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksUserPage());
        }
    }
}
