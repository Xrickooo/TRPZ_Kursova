using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TRPZ_Kurs.ProfessorLogic
{
    public partial class ScheduleProfessorWindow : Window
    {
        private string connectionString =
            @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";

        private int professorId;
        private string selectedDay;

        public ScheduleProfessorWindow(int professorId, string selectedDay)
        {
            InitializeComponent();
            this.professorId = professorId;
            this.selectedDay = selectedDay;
            LoadDaysOfWeek();
            DisplayProfessorScheduleByDay(professorId, selectedDay);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayProfessorScheduleByDay(professorId, selectedDay);
        }

        private void LoadDaysOfWeek()
        {
            List<string> daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
            DayDropDown.ItemsSource = daysOfWeek;
        }

        private void DisplayProfessorScheduleByDay(int professorId, string dayOfWeek)
        {
            string query = @"
                SELECT p.FirstName AS ProfessorFirstName, p.LastName AS ProfessorLastName, su.SubjectName, sch.DayOfWeek, sch.Time, g.GroupName
                FROM Professors p
                INNER JOIN Schedule sch ON p.ProfessorID = sch.ProfessorID
                INNER JOIN Subjects su ON sch.SubjectID = su.SubjectID
                INNER JOIN Groups g ON sch.GroupID = g.GroupID
                WHERE p.ProfessorID = @ProfessorID AND sch.DayOfWeek = @DayOfWeek
            ";

            List<string> scheduleInfo = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProfessorID", professorId);
                command.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        scheduleInfo.Add("No schedule found for this professor.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            string info = $"Professor: {reader["ProfessorFirstName"]} {reader["ProfessorLastName"]}\n" +
                                          $"Subject: {reader["SubjectName"]}\n" +
                                          $"Day: {reader["DayOfWeek"]}, Time: {reader["Time"]}\n" +
                                          $"Group: {reader["GroupName"]}\n";
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

        private void DayDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedDay = DayDropDown.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedDay))
            {
                DisplayProfessorScheduleByDay(professorId, selectedDay);
            }
        }
    }
}


