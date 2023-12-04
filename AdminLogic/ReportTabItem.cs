using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TRPZ_Kurs.AdminLogic;

public partial class WelcomeAdminWindow
{
    private void GenerateStudentReport_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Отримати всіх студентів з їхніми оцінками та предметами
                string query =
                    "SELECT Students.FirstName, Students.LastName, Groups.GroupName, Subjects.SubjectName, Grades.Grade " +
                    "FROM Students " +
                    "JOIN Groups ON Students.GroupID = Groups.GroupID " +
                    "JOIN Grades ON Students.StudentID = Grades.StudentID " +
                    "JOIN Subjects ON Grades.SubjectID = Subjects.SubjectID";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                ReportsDataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TabControl tabControl = sender as TabControl;
        if (tabControl != null)
        {
            // Получаем выбранную вкладку
            TabItem selectedTab = tabControl.SelectedItem as TabItem;

            if (selectedTab != null)
            {
                if (selectedTab.Name == "ReportsTab") // Замените "ReportsTab" на имя вашей вкладки "Reports"
                {
                    LoadQuestionsButton.Visibility = Visibility.Collapsed; // Скрыть кнопку
                }
                else if
                    (selectedTab.Name ==
                     "SubjectsManageTab") // Замените "ReportsTab" на имя вашей вкладки "Reports"
                {
                    LoadQuestionsButton.Visibility = Visibility.Collapsed; // Скрыть кнопку
                }
                else
                {
                    LoadQuestionsButton.Visibility = Visibility.Visible; // Показать кнопку
                }
            }
        }
    }
}