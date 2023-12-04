using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs
{
    public partial class GradesWindow : Window
    {
        private string connectionString = @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";
        private readonly int studentID;

        public GradesWindow(int studentID)
        {
            InitializeComponent();
            this.studentID = studentID;

            DisplayGrades();
        }

        private void DisplayGrades()
        {
            string query = @"
                SELECT su.SubjectName, g.Grade
                FROM Grades g
                INNER JOIN Subjects su ON g.SubjectID = su.SubjectID
                WHERE g.StudentID = @StudentID
            ";

            List<string> gradeInfo = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        gradeInfo.Add("No grades available.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            string info = $"Subject: {reader["SubjectName"]}, Grade: {reader["Grade"]}";
                            gradeInfo.Add(info);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    gradeInfo.Add("Error: " + ex.Message);
                }
            }

            GradesListView.ItemsSource = gradeInfo;
        }
    }
}