using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using TRPZ_Kurs.ProfessorLogic;

namespace TRPZ_Kurs
{
    public partial class WelcomeProfessorWindow : Window
    {
        private int professorId;
        private int userId;

        public WelcomeProfessorWindow(int professorId, int userId)
        {
            InitializeComponent();
            this.professorId = professorId;
            this.userId = userId;
        }

        private void ShowSchedule_Click(object sender, RoutedEventArgs e)
        {
            string selectedDay = ((ComboBoxItem)DayOfWeekComboBox.SelectedItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedDay))
            {
                ScheduleProfessorWindow scheduleWindow = new ScheduleProfessorWindow(professorId, selectedDay);
                scheduleWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a day of the week.");
            }
        }

        private void ManageSchedule_Click(object sender, RoutedEventArgs e)
        {
            ManageSchedule manageSchedulePage = new ManageSchedule(professorId);
            manageSchedulePage.Show();
        }

        private void GradeStudents_Click(object sender, RoutedEventArgs e)
        {
            GradeStudentsWindow gradeWindow = new GradeStudentsWindow(professorId);
            gradeWindow.Show();
        }

        

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authWindow = new AuthenticationWindow();
            authWindow.Show();
            Close(); 
        }
    }
}