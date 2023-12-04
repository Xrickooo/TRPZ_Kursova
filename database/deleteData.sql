USE AIS_Dekanat;

DELETE FROM StudentQuestions;
DELETE FROM Users;
DELETE FROM Grades;
DELETE FROM Schedule;
DELETE FROM Professors;
DELETE FROM Subjects;
DELETE FROM Students;
DELETE FROM Groups;
DELETE FROM Faculties;
DELETE FROM Administrators;

TRUNCATE TABLE Schedule;
TRUNCATE TABLE Grades;
TRUNCATE TABLE StudentQuestions;
TRUNCATE TABLE Users;
