using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs.AdminLogic;

public partial class WelcomeAdminWindow
{
    private void LoadSubjects()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Subjects";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                SubjectManagerGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void AddSubject_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string yourSubjectID = SubjectIDTextBox.Text;
            string subjectName = SubjectNameTextBox.Text;
            int hours = int.Parse(HoursTextBox.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query =
                    "INSERT INTO Subjects (SubjectID, SubjectName, Hours) VALUES (@SubjectID, @SubjectName, @Hours)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectID",
                    yourSubjectID); // Потрібно вказати конкретне значення для SubjectID
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                command.Parameters.AddWithValue("@Hours", hours);


                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Subject added successfully.");
                    LoadSubjects();
                }
                else
                {
                    MessageBox.Show("Failed to add subject.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void DeleteSubject_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            DataRowView selectedRow = (DataRowView)SubjectManagerGrid.SelectedItem;
            if (selectedRow != null)
            {
                int subjectId = Convert.ToInt32(selectedRow["SubjectID"]);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Subjects WHERE SubjectID = @SubjectID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SubjectID", subjectId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Subject deleted successfully.");
                        LoadSubjects(); // Оновити DataGrid після видалення
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete subject.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to delete.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }
}