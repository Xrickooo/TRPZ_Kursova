using System;
using System.Data.SqlClient;
using System.Windows;
using TRPZ_Kurs.AdminLogic;

namespace TRPZ_Kurs
{
    public partial class AuthenticationWindow : Window
    {
        private AuthenticationManager authManager = new AuthenticationManager();

        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            UserRole role;
            int studentID;
            int userId;

            bool isAuthenticated =
                authManager.AuthenticateUser(username, password, out role, out studentID, out userId);

            if (isAuthenticated && role == UserRole.Student)
            {
                WelcomeStudentWindow welcomeWindow = new WelcomeStudentWindow(studentID, userId);
                welcomeWindow.Show();

                Close();
            }
            else if (isAuthenticated && role == UserRole.Professor)
            {
                int professorID;
                bool isProfessorAuthenticated = authManager.GetProfessorID(username, out professorID);

                if (isProfessorAuthenticated)
                {
                    WelcomeProfessorWindow welcomeWindow = new WelcomeProfessorWindow(professorID, userId);
                    welcomeWindow.Show();

                    Close();
                }
            }
            else if (isAuthenticated && role == UserRole.Admin)
            {
                int adminID;
                bool isAdminAuthenticated = authManager.GetAdminID(username, out adminID);

                if (isAdminAuthenticated)
                {
                    WelcomeAdminWindow welcomeWindow = new WelcomeAdminWindow(adminID, userId, UserRole.Admin);
                    welcomeWindow.Show();

                    Close();
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }
    }


    public enum UserRole
    {
        Student,
        Professor,
        Admin
    }

    public class AuthenticationManager
    {
        private readonly string connectionString =
            @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";

        public bool AuthenticateUser(string username, string password, out UserRole role, out int studentID,
            out int userId)
        {
            string userRoleQuery = 
                "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
            
            string studentIDQuery =
                "SELECT s.StudentID, u.UserID FROM Students s INNER JOIN Users u ON s.Email = u.Username WHERE u.Username = @Username";
            string professorIDQuery =
                "SELECT p.ProfessorID, u.UserID FROM Professors p INNER JOIN Users u ON CONCAT(p.FirstName, p.LastName) = u.Username WHERE u.Username = @Username";
            string adminIDQuery =
                "SELECT a.AdministratorID, u.UserID FROM Administrators a INNER JOIN Users u ON CONCAT(a.FirstName, a.LastName) = u.Username WHERE u.Username = @Username";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user is an administrator
                using (var adminCommand = new SqlCommand(adminIDQuery, connection))
                {
                    adminCommand.Parameters.AddWithValue("@Username", username);

                    var adminResult = adminCommand.ExecuteScalar();
                    if (adminResult != null)
                    {
                        role = UserRole.Admin;
                        var adminReader = adminCommand.ExecuteReader();
                        if (adminReader.Read())
                        {
                            userId = (int)adminReader["UserID"];
                            studentID = -1;
                            return true;
                        }
                    }
                }

                // If not an admin, proceed with the previous logic for student and professor
                using (var roleCommand = new SqlCommand(userRoleQuery, connection))
                {
                    roleCommand.Parameters.AddWithValue("@Username", username);
                    roleCommand.Parameters.AddWithValue("@Password", password);

                    var result = roleCommand.ExecuteScalar();
                    if (result != null && Enum.TryParse((string)result, out role))
                    {
                        using (var idCommand = role == UserRole.Student
                                   ? new SqlCommand(studentIDQuery, connection)
                                   : new SqlCommand(professorIDQuery, connection))
                        {
                            idCommand.Parameters.AddWithValue("@Username", username);

                            var reader = idCommand.ExecuteReader();
                            if (reader.Read())
                            {
                                studentID = role == UserRole.Student
                                    ? (int)reader["StudentID"]
                                    : (int)reader["ProfessorID"];
                                userId = (int)reader["UserID"];
                                return true;
                            }
                        }
                    }
                }
            }

            role = UserRole.Student;
            studentID = -1;
            userId = -1;
            return false;
        }


        public bool GetProfessorID(string username, out int professorID)
        {
            string professorIDQuery =
                "SELECT p.ProfessorID FROM Professors p INNER JOIN Users u ON CONCAT(p.FirstName, p.LastName) = u.Username WHERE u.Username = @Username";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(professorIDQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        professorID = (int)result;
                        return true;
                    }
                }
            }

            professorID = -1;
            return false;
        }

        public bool GetAdminID(string username, out int adminID)
        {
            string adminIDQuery =
                "SELECT a.AdministratorID FROM Administrators a INNER JOIN Users u ON CONCAT(a.FirstName, a.LastName) = u.Username WHERE u.Username = @Username";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(adminIDQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        adminID = (int)result;
                        return true;
                    }
                }
            }

            adminID = -1;
            return false;
        }

    }
}





