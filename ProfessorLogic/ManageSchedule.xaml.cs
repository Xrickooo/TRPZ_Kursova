using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TRPZ_Kurs.ProfessorLogic
{
    public partial class ManageSchedule : Window
    {
        private string connectionString =
            @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";

        private int professorId;

        public ManageSchedule(int professorId)
        {
            InitializeComponent();
            this.professorId = professorId;
            LoadDaysOfWeek(); 
            LoadProfessorSchedule();
        }

        private void LoadDaysOfWeek()
        {
            List<string> daysOfWeek = new List<string>
                { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday",};
            NewDayComboBox.ItemsSource = daysOfWeek;
        }

        private void LoadProfessorSchedule()
        {
            string query = @"
                SELECT ScheduleID, DayOfWeek, Time, SubjectName, GroupName
                FROM Schedule sch
                INNER JOIN Subjects su ON sch.SubjectID = su.SubjectID
                INNER JOIN Groups g ON sch.GroupID = g.GroupID
                WHERE sch.ProfessorID = @ProfessorID
            ";

            List<string> scheduleInfo = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProfessorID", professorId);

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
                            string info = $"ScheduleID: {reader["ScheduleID"]}\n" +
                                          $"Day: {reader["DayOfWeek"]}, Time: {reader["Time"]}\n" +
                                          $"Subject: {reader["SubjectName"]}\n" +
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

        private void UpdateProfessorSchedule(int scheduleId, string newDayOfWeek, string newTime)
        {
            string updateQuery = @"
                UPDATE Schedule
                SET DayOfWeek = @NewDayOfWeek, Time = @NewTime
                WHERE ScheduleID = @ScheduleID
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@NewDayOfWeek", newDayOfWeek);
                command.Parameters.AddWithValue("@NewTime", newTime);
                command.Parameters.AddWithValue("@ScheduleID", scheduleId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} rows updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating schedule: " + ex.Message);
                }
            }
        }

        private bool IsScheduleConflict(int professorId, string newDayOfWeek, string newTime)
        {
            string query = @"
        SELECT COUNT(*)
        FROM Schedule
        WHERE ProfessorID = @ProfessorID AND DayOfWeek = @NewDayOfWeek AND Time = @NewTime
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProfessorID", professorId);
                command.Parameters.AddWithValue("@NewDayOfWeek", newDayOfWeek);
                command.Parameters.AddWithValue("@NewTime", newTime);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0; 
            }
        }

        private void UpdateSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleListView.SelectedItem != null)
            {
                string selectedSchedule = ScheduleListView.SelectedItem.ToString();
                string[] scheduleDetails =
                    selectedSchedule.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (scheduleDetails.Length >= 4)
                {
                    string scheduleIdString = scheduleDetails[0].Substring(scheduleDetails[0].IndexOf(':') + 1).Trim();

                    if (int.TryParse(scheduleIdString, out int scheduleId))
                    {
                        string newDayOfWeek = NewDayComboBox.SelectedItem as string;
                        string newTime = NewTimeTextBox.Text;

                        if (!string.IsNullOrEmpty(newDayOfWeek) && !string.IsNullOrEmpty(newTime))
                        {
                            if (IsTimeFormatValid(newTime)) 
                            {
                                if (!IsScheduleConflict(professorId, newDayOfWeek, newTime))
                                {
                                    UpdateProfessorSchedule(scheduleId, newDayOfWeek, newTime);
                                    LoadProfessorSchedule(); 
                                }
                                else
                                {
                                    MessageBox.Show("There is already a schedule entry for this day and time.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter time in HH:DD:MM format.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select a new day of the week and enter a new time.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Schedule ID format.");
                    }
                }
            }
        }

        private bool IsTimeFormatValid(string time)
        {
            return TimeSpan.TryParseExact(time, "hh\\:mm\\:ss", null, out _);
        }


    }
}




