using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs.AdminLogic
{
    public partial class WelcomeAdminWindow
    {
        private void LoadQuestions()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT QuestionID, QuestionText, AnswerText FROM StudentQuestions";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    QuestionsDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void UpdateAnswer()
        {
            try
            {
                DataRowView selectedRow = (DataRowView)QuestionsDataGrid.SelectedItem;
                if (selectedRow != null)
                {
                    int questionId = Convert.ToInt32(selectedRow["QuestionID"]);
                    string answerText = AnswerTextBox.Text;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query =
                            "UPDATE StudentQuestions SET AnswerText = @AnswerText, DateAnswered = GETDATE() WHERE QuestionID = @QuestionID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@AnswerText", answerText);
                        command.Parameters.AddWithValue("@QuestionID", questionId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Answer submitted successfully.");
                            AnswerTextBox.Text = ""; // Очищення текстового поля після відправки відповіді
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit answer.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a question to submit an answer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
