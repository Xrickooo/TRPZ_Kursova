using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs
{
    public class StudentQuestion
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public DateTime DateAsked { get; set; }
        public string AnswerText { get; set; }
        public DateTime? DateAnswered { get; set; }
    }
    
    public partial class AskQuestionWindow : Window
    {
        private const string ConnectionString = @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";
        private readonly int studentID;

        public ObservableCollection<StudentQuestion> Questions { get; set; }

        public AskQuestionWindow(int studentId)
        {
            InitializeComponent();
            DataContext = this;
            studentID = studentId;
            Questions = new ObservableCollection<StudentQuestion>();
            LoadStudentQuestions();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string questionText = QuestionTextBox.Text;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string insertQuery = "INSERT INTO StudentQuestions (StudentID, AdministratorID, QuestionText, DateAsked) " +
                                     "VALUES (@StudentID, @AdministratorID, @QuestionText, @DateAsked)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@AdministratorID", DBNull.Value); 
                command.Parameters.AddWithValue("@QuestionText", questionText);
                command.Parameters.AddWithValue("@DateAsked", DateTime.Now);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Question submitted successfully!");
                        LoadStudentQuestions();
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit question.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        
        private void LoadStudentQuestions()
        {
            try
            {
                Questions.Clear();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT QuestionID, QuestionText, DateAsked, AnswerText, DateAnswered FROM StudentQuestions WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int questionID = Convert.ToInt32(reader["QuestionID"]);
                        string questionText = reader["QuestionText"].ToString();
                        DateTime dateAsked = Convert.ToDateTime(reader["DateAsked"]);
                        string answerText = reader["AnswerText"].ToString();
                        DateTime? dateAnswered = reader["DateAnswered"] as DateTime?;

                        StudentQuestion question = new StudentQuestion
                        {
                            QuestionID = questionID,
                            QuestionText = questionText,
                            DateAsked = dateAsked,
                            AnswerText = answerText,
                            DateAnswered = dateAnswered
                        };

                        Questions.Add(question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}


