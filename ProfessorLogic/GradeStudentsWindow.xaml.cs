using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TRPZ_Kurs.ProfessorLogic
{
    public partial class GradeStudentsWindow : Window
    {
        private string connectionString =
            @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";

        private int professorId;

        public GradeStudentsWindow(int professorId)
        {
            InitializeComponent();
            this.professorId = professorId;
            LoadGroups();
        }

        private void LoadGroups()
        {
            string query = @"
        SELECT g.GroupID, g.GroupName 
        FROM Groups g
        WHERE g.GroupID IN (
            SELECT s.GroupID 
            FROM Schedule s
            WHERE s.ProfessorID = @ProfessorID
        )
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProfessorID", professorId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        List<Group> groups = new List<Group>();

                        while (reader.Read())
                        {
                            groups.Add(new Group
                            {
                                GroupID = Convert.ToInt32(reader["GroupID"]),
                                GroupName = reader["GroupName"].ToString()
                            });
                        }

                        GroupComboBox.ItemsSource = groups;
                        GroupComboBox.DisplayMemberPath = "GroupName";
                        GroupComboBox.SelectedValuePath = "GroupID";
                    }
                    else
                    {
                        MessageBox.Show("No groups found for this professor.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupComboBox.SelectedItem != null)
            {
                int selectedGroupId = (int)GroupComboBox.SelectedValue;
                LoadStudents(selectedGroupId);
            }
        }

        private void LoadStudents(int groupId)
        {
            string query = @"
        SELECT s.StudentID, s.FirstName, s.LastName, g.SubjectID, sub.SubjectName, g.Grade
        FROM Students s
        LEFT JOIN Grades g ON s.StudentID = g.StudentID
        LEFT JOIN Subjects sub ON g.SubjectID = sub.SubjectID
        LEFT JOIN Schedule sch ON s.GroupID = sch.GroupID AND g.SubjectID = sch.SubjectID
        WHERE s.GroupID = @GroupID AND sch.ProfessorID = @ProfessorID
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GroupID", groupId);
                command.Parameters.AddWithValue("@ProfessorID", professorId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<StudentGrade> studentGrades = new List<StudentGrade>();

                    while (reader.Read())
                    {
                        studentGrades.Add(new StudentGrade
                        {
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            SubjectID = reader["SubjectID"] != DBNull.Value ? Convert.ToInt32(reader["SubjectID"]) : 0,
                            SubjectName = reader["SubjectName"] != DBNull.Value
                                ? reader["SubjectName"].ToString()
                                : string.Empty,
                            Grade = reader["Grade"] != DBNull.Value ? Convert.ToInt32(reader["Grade"]) : 0
                        });
                    }

                    StudentsDataGrid.ItemsSource = studentGrades;

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void StudentsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedCell = e.EditingElement as TextBox;
                if (editedCell != null)
                {
                    var editedGrade = editedCell.Text;

                    if (int.TryParse(editedGrade, out int newGrade))
                    {
                        var studentGrade = (StudentGrade)e.Row.Item;
                        studentGrade.Grade = newGrade;

                        UpdateStudentGrade(studentGrade.StudentID, studentGrade.SubjectID, newGrade);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid grade.");
                    }
                }
            }
        }

        private void UpdateStudentGrade(int studentId, int subjectId, int newGrade)
        {
            // Перевірка чи оцінка в діапазоні від 0 до 100
            if (newGrade >= 0 && newGrade <= 100)
            {
                string query = @"
            IF EXISTS (SELECT 1 FROM Grades WHERE StudentID = @StudentID AND SubjectID = @SubjectID)
            BEGIN
                UPDATE Grades
                SET Grade = @NewGrade
                WHERE StudentID = @StudentID AND SubjectID = @SubjectID
            END
            ELSE
            BEGIN
                INSERT INTO Grades (StudentID, SubjectID, Grade)
                VALUES (@StudentID, @SubjectID, @NewGrade)
            END
        ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    command.Parameters.AddWithValue("@SubjectID", subjectId);
                    command.Parameters.AddWithValue("@NewGrade", newGrade);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Grade updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update grade.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Grade should be between 0 and 100.");
            }
        }

    }
    public class StudentGrade
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Grade { get; set; }
    }

    public class Group
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }
}
