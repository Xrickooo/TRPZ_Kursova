-- Тригер після вставки у таблиці Students
CREATE TRIGGER InsertStudentUser
ON Students
AFTER INSERT
AS
BEGIN
    INSERT INTO Users (Username, Password, Role)
    SELECT Email, LEFT(CONVERT(NVARCHAR(255), CONCAT(SUBSTRING(Email, 1, 4), NEWID())), 8), 'Student'
    FROM inserted;
END;

-- Тригер після вставки у таблиці Professors
CREATE TRIGGER InsertProfessorUser
ON Professors
AFTER INSERT
AS
BEGIN
    INSERT INTO Users (Username, Password, Role)
    SELECT CONCAT(FirstName, LastName), LEFT(CONVERT(NVARCHAR(255), CONCAT(SUBSTRING(CONCAT(FirstName, LastName), 1, 4), NEWID())), 8), 'Professor'
    FROM inserted;
END;

-- Тригер після вставки у таблиці Administrators
CREATE TRIGGER InsertAdministratorUser
ON Administrators
AFTER INSERT
AS
BEGIN
    INSERT INTO Users (Username, Password, Role)
    SELECT CONCAT(FirstName, LastName), LEFT(CONVERT(NVARCHAR(255), CONCAT(SUBSTRING(CONCAT(FirstName, LastName), 1, 4), NEWID())), 8), 'Administrator'
    FROM inserted;
END;

CREATE TRIGGER InsertGradesOnScheduleInsert
ON Schedule
AFTER INSERT
AS
BEGIN
    -- Перевірка чи існують в Schedule відповідні записи для вставки в Grades
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO Grades (StudentID, SubjectID, Grade)
        SELECT DISTINCT s.StudentID, Schedule.SubjectID, 0
        FROM Students s
        INNER JOIN inserted ON inserted.GroupID = s.GroupID
        INNER JOIN Schedule ON Schedule.GroupID = s.GroupID AND Schedule.SubjectID = inserted.SubjectID
        LEFT JOIN Grades g ON g.StudentID = s.StudentID AND g.SubjectID = Schedule.SubjectID
        WHERE g.GradeID IS NULL;
    END
END;

CREATE TRIGGER UpdateGradesOnNewStudent
ON Students
AFTER INSERT
AS
BEGIN
    -- Оновлення оцінок для нових студентів
    INSERT INTO Grades (StudentID, SubjectID, Grade)
    SELECT DISTINCT inserted.StudentID, Schedule.SubjectID, 0
    FROM inserted
    INNER JOIN Schedule ON Schedule.GroupID = inserted.GroupID
    LEFT JOIN Grades g ON g.StudentID = inserted.StudentID AND g.SubjectID = Schedule.SubjectID
    WHERE g.GradeID IS NULL;
END;












 