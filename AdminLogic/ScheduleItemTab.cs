using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs.AdminLogic;

public partial class WelcomeAdminWindow
{
    private void LoadSchedule()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Schedule";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                ScheduleDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void AddSchedule()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query =
                    "IF NOT EXISTS (SELECT * FROM Schedule WHERE DayOfWeek = @DayOfWeek AND Time = @Time) " +
                    "INSERT INTO Schedule (DayOfWeek, Time, GroupID, SubjectID, ProfessorID) " +
                    "VALUES (@DayOfWeek, @Time, @GroupID, @SubjectID, @ProfessorID)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DayOfWeek", DayOfWeekComboBox.Text);
                command.Parameters.AddWithValue("@Time", TimeTextBox.Text);
                command.Parameters.AddWithValue("@GroupID", Convert.ToInt32(GroupIDComboBox.Text));
                command.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(SubjectIDComboBox.Text));
                command.Parameters.AddWithValue("@ProfessorID", Convert.ToInt32(ProfessorIDComboBox.Text));

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Schedule added successfully.");
                    LoadSchedule();
                }
                else
                {
                    MessageBox.Show("Failed to add schedule. Schedule at that time already exists.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }


    private void DeleteSchedule()
    {
        try
        {
            DataRowView selectedRow = (DataRowView)ScheduleDataGrid.SelectedItem;
            if (selectedRow != null)
            {
                int scheduleId = Convert.ToInt32(selectedRow["ScheduleID"]);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Schedule WHERE ScheduleID = @ScheduleID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ScheduleID", scheduleId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Schedule deleted successfully.");
                        LoadSchedule();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete schedule.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a schedule to delete.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }


    private void LoadDaysOfWeek()
    {
        List<string> daysOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        DayOfWeekComboBox.ItemsSource = daysOfWeek;
    }



    private void LoadGroupIDs()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT GroupID FROM Groups";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int groupId = Convert.ToInt32(reader["GroupID"]);
                    GroupIDComboBox.Items.Add(groupId);
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void LoadSubjectIDs()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SubjectID FROM Subjects";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int subjectId = Convert.ToInt32(reader["SubjectID"]);
                    SubjectIDComboBox.Items.Add(subjectId);
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void LoadProfessorIDs()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query =
                    "SELECT ProfessorID FROM Professors";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int professorId = Convert.ToInt32(reader["ProfessorID"]);
                    ProfessorIDComboBox.Items.Add(professorId);
                }

                reader.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }
}