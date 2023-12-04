using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs
{
    public partial class ScheduleStudentWindow : Window
    {
        private string connectionString = @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";
        private int studentID;
        private string selectedDay;

        public ScheduleStudentWindow(int studentID, string selectedDay)
        {
            InitializeComponent();
            this.studentID = studentID;
            this.selectedDay = selectedDay;
            DisplayStudentScheduleByDay(studentID, selectedDay);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayStudentScheduleByDay(studentID, selectedDay);
        }

        private void DisplayStudentScheduleByDay(int studentID, string dayOfWeek)
        {
            string query = @"
                SELECT s.FirstName, s.LastName, su.SubjectName, sch.DayOfWeek, sch.Time, p.FirstName AS ProfessorFirstName, p.LastName AS ProfessorLastName
                FROM Students s
                INNER JOIN Groups g ON s.GroupID = g.GroupID
                INNER JOIN Schedule sch ON g.GroupID = sch.GroupID
                INNER JOIN Subjects su ON sch.SubjectID = su.SubjectID
                INNER JOIN Professors p ON sch.ProfessorID = p.ProfessorID
                WHERE s.StudentID = @StudentID AND sch.DayOfWeek = @DayOfWeek
            ";

            List<string> scheduleInfo = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        scheduleInfo.Add("You are free today!");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            string info = $"Student: {reader["FirstName"]} {reader["LastName"]}\n" +
                                          $"Subject: {reader["SubjectName"]}\n" +
                                          $"Day: {reader["DayOfWeek"]}, Time: {reader["Time"]}\n" +
                                          $"Professor: {reader["ProfessorFirstName"]} {reader["ProfessorLastName"]}\n";
                            scheduleInfo.Add(info);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    scheduleInfo.Add("Error: " + ex.Message);
                }
            }

            ScheduleListView.ItemsSource = scheduleInfo;
        }
    }
}




