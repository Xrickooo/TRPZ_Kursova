using System;
using System.Data.SqlClient;
using System.Windows;

namespace TRPZ_Kurs;

public partial class EditPersonalDataWindow : Window
{
    private int studentID;
    private int userID;
    
    private string connectionString = @"Data Source=.\SQLExpress;Initial Catalog=AIS_Dekanat;Integrated Security=True;";
    public EditPersonalDataWindow(int studentID,int userID)
    {
        this.studentID = studentID;
        this.userID = userID;
        InitializeComponent();
    }
    
    private void SaveChanges_Click(object sender, RoutedEventArgs e)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            
            if (!string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                string updatePhoneQuery = "UPDATE Students SET Phone = @Phone WHERE StudentID = @StudentID";
                SqlCommand updatePhoneCommand = new SqlCommand(updatePhoneQuery, connection);
                updatePhoneCommand.Parameters.AddWithValue("@Phone", PhoneTextBox.Text);
                updatePhoneCommand.Parameters.AddWithValue("@StudentID", studentID);
                updatePhoneCommand.ExecuteNonQuery();
            }
            
            if (!string.IsNullOrEmpty(EmailTextBox.Text))
            {
                string updateEmailQuery = "UPDATE Students SET Email = @Email WHERE StudentID = @StudentID";
                SqlCommand updateEmailCommand = new SqlCommand(updateEmailQuery, connection);
                updateEmailCommand.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                updateEmailCommand.Parameters.AddWithValue("@StudentID", studentID);
                updateEmailCommand.ExecuteNonQuery();
                
                string updateUsersEmailQuery = "UPDATE Users SET Username = @Email WHERE UserID = @UserID";
                SqlCommand updateUsersEmailCommand = new SqlCommand(updateUsersEmailQuery, connection);
                updateUsersEmailCommand.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                updateUsersEmailCommand.Parameters.AddWithValue("@UserID", userID);
                updateUsersEmailCommand.ExecuteNonQuery();
            }
            
            if (!string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                string updatePasswordQuery = "UPDATE Users SET Password = @Password WHERE UserID = @UserID";
                SqlCommand updatePasswordCommand = new SqlCommand(updatePasswordQuery, connection);
                updatePasswordCommand.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
                updatePasswordCommand.Parameters.AddWithValue("@UserID", userID);
                updatePasswordCommand.ExecuteNonQuery();
            }
            
            if (!string.IsNullOrEmpty(AddressTextBox.Text))
            {
                string updateAddressQuery = "UPDATE Students SET Address = @Address WHERE StudentID = @StudentID";
                SqlCommand updateAddressCommand = new SqlCommand(updateAddressQuery, connection);
                updateAddressCommand.Parameters.AddWithValue("@Address", AddressTextBox.Text);
                updateAddressCommand.Parameters.AddWithValue("@StudentID", studentID);
                updateAddressCommand.ExecuteNonQuery();
            }

            MessageBox.Show("Changes saved successfully!");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
}

}
