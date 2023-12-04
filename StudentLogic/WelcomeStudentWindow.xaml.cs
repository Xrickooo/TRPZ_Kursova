using System;
using System.Windows;
using System.Windows.Controls;

namespace TRPZ_Kurs
{
    public partial class WelcomeStudentWindow : Window
    {
        private int studentID; 
        private int userId; 

        public WelcomeStudentWindow(int studentID, int userId)
        {
            InitializeComponent();
            this.studentID = studentID;
            this.userId = userId;
        }

        private void ShowSchedule_Click(object sender, RoutedEventArgs e)
        {
            string selectedDay = ((ComboBoxItem)DayOfWeekComboBox.SelectedItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedDay))
            {
                ScheduleStudentWindow scheduleWindow = new ScheduleStudentWindow(studentID, selectedDay); 
                scheduleWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a day of the week.");
            }
        }
        
        private void ShowGrades_Click(object sender, RoutedEventArgs e)
        {
            GradesWindow gradesWindow = new GradesWindow(studentID);
            gradesWindow.Show();
        }
        
        private void AskQuestion_Click(object sender, RoutedEventArgs e)
        {
            AskQuestionWindow questionWindow = new AskQuestionWindow(studentID);
            questionWindow.ShowDialog();
        }
        
        private void EditPersonalData_Click(object sender, RoutedEventArgs e)
        {
            EditPersonalDataWindow editWindow = new EditPersonalDataWindow(studentID,userId);
            editWindow.ShowDialog();
        }
        
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authWindow = new AuthenticationWindow();
            authWindow.Show();
            Close(); 
        }


    }
}