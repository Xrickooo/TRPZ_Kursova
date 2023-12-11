using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TRPZ_Kurs.AdminLogic
{
    public partial class WelcomeAdminWindow : Window
    {
        private string connectionString =
            @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";

        private int adminId;
        private int userId;
        private UserRole currentUserRole;
        
        public WelcomeAdminWindow(int userId, int adminId, UserRole currentUserRole)
        {
            this.userId = userId;
            this.adminId = adminId;
            this.currentUserRole = currentUserRole;
            InitializeComponent();
            Data_Load();
        }

        private void Data_Load()
        {
            LoadSubjects();
            LoadSchedule();
            LoadDaysOfWeek();
            LoadGroupIDs();
            LoadSubjectIDs();
            LoadProfessorIDs();
        }

        private void LoadQuestions_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestions();
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            UpdateAnswer(userId);
            LoadQuestions(); 
        }
        

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authWindow = new AuthenticationWindow();
            authWindow.Show();
            Close();
        }
        
        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            AddSchedule();
        }
        
        private void DeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            DeleteSchedule();
        }
    }
}


