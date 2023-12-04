-- Створення бази данихCREATE DATABASE AIS_Dekanat;
-- Вибір бази даних
USE AIS_Dekanat;
-- Створення таблиці "Faculties"
CREATE TABLE Faculties (
    FacultyID INT PRIMARY KEY,    FacultyName NVARCHAR(50),
    Dean NVARCHAR(100));
-- Створення таблиці "Groups"
CREATE TABLE Groups (    GroupID INT PRIMARY KEY,
    GroupName NVARCHAR(50),    Course INT,
    FacultyID INT,    FOREIGN KEY (FacultyID) REFERENCES Faculties(FacultyID)
);
-- Створення таблиці "Students"
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),    DateOfBirth DATE,
    Address NVARCHAR(100),    Phone NVARCHAR(15),
    Email NVARCHAR(100),    GroupID INT,
    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID));
-- Створення таблиці "Subjects"
CREATE TABLE Subjects (    SubjectID INT PRIMARY KEY,
    SubjectName NVARCHAR(100),    Hours INT
);
-- Створення таблиці "Professors"
CREATE TABLE Professors (
    ProfessorID INT PRIMARY KEY,    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),    Title NVARCHAR(50)
);
CREATE TABLE Schedule (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    DayOfWeek NVARCHAR(20),
    Time TIME,
    GroupID INT,
    SubjectID INT,
    ProfessorID INT,
    FOREIGN KEY (GroupID) REFERENCES Groups(GroupID),
    FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
    FOREIGN KEY (ProfessorID) REFERENCES Professors(ProfessorID)
);

-- Створення таблиці "Grades"
CREATE TABLE Grades (
    GradeID INT PRIMARY KEY IDENTITY(1,1), -- Змінили тут
    StudentID INT,
    SubjectID INT,
    Grade INT,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
);
-- Створення таблиці "Administrators" для адміністраторів
CREATE TABLE Administrators (    AdministratorID INT PRIMARY KEY,
    FirstName NVARCHAR(50),    LastName NVARCHAR(50)
);
use AIS_Dekanat
CREATE TABLE StudentQuestions (
    QuestionID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    AdministratorID INT DEFAULT NULL,
    QuestionText NVARCHAR(MAX) DEFAULT '',
    DateAsked DATETIME DEFAULT GETDATE(),
    AnswerText NVARCHAR(MAX) DEFAULT '',
    DateAnswered DATETIME DEFAULT NULL,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (AdministratorID) REFERENCES Administrators(AdministratorID)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100),
    Password NVARCHAR(100),
    Role NVARCHAR(50) -- Припустимо, що це строкове значення ролі
);