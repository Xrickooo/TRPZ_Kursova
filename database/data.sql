use AIS_Dekanat
-- Створення 20 предметів у таблиці Subjects
INSERT INTO Subjects (SubjectID, SubjectName, Hours)
VALUES
    (1, 'Mathematics', 60),
    (2, 'Biology', 45),
    (3, 'Economics', 50),
    (4, 'Literature', 40),
    (5, 'Physics', 55),
    (6, 'Chemistry', 48),
    (7, 'History', 42),
    (8, 'Computer Science', 52),
    (9, 'Psychology', 47),
    (10, 'Sociology', 44),
    (11, 'Art', 38),
    (12, 'Geography', 46),
    (13, 'Music', 36),
    (14, 'Philosophy', 49),
    (15, 'Physical Education', 30),
    (16, 'Foreign Languages', 60),
    (17, 'Business Administration', 55),
    (18, 'Environmental Science', 48),
    (19, 'Engineering', 52),
    (20, 'Medicine', 60);
-- Додавання записів до таблиці Faculties
INSERT INTO Faculties (FacultyID, FacultyName, Dean)
VALUES
    (1, 'Faculty of Engineering', 'John Smith'),
    (2, 'Faculty of Medicine', 'Emily Johnson'),
    (3, 'Faculty of Business', 'Michael Davis'),
    (4, 'Faculty of Arts', 'Sarah Wilson'),
    (5, 'Faculty of Science', 'Robert Anderson');

-- Додавання записів до таблиці Groups
INSERT INTO Groups (GroupID, GroupName, Course, FacultyID)
VALUES
    (1, 'ENG101', 1, 1),
    (2, 'MED202', 2, 2),
    (3, 'BUS303', 3, 3),
    (4, 'ART404', 4, 4),
    (5, 'SCI505', 5, 5);

-- Додавання записів до таблиці Students
INSERT INTO Students (StudentID, FirstName, LastName, DateOfBirth, Address, Phone, Email, GroupID)
VALUES
    (1, 'Alice', 'Johnson', '1998-05-10', '123 Main St', '123-456-7890', 'alice@example.com', 1),
    (2, 'Bob', 'Smith', '1997-08-15', '456 Elm St', '987-654-3210', 'bob@example.com', 2),
    (3, 'Eva', 'Brown', '1999-02-20', '789 Oak St', '111-222-3333', 'eva@example.com', 3),
    (4, 'Max', 'Taylor', '1996-11-25', '101 Pine St', '444-555-6666', 'max@example.com', 4),
    (5, 'Sophia', 'Lee', '2000-07-05', '202 Maple St', '777-888-9999', 'sophia@example.com', 5),
	    (6, 'Oliver', 'Martinez', '1997-03-15', '123 Oak St', '555-111-2222', 'oliver@example.com', 2),
    (7, 'Charlotte', 'Garcia', '1998-07-25', '456 Elm St', '555-333-4444', 'charlotte@example.com', 3),
    (8, 'Mia', 'Lopez', '1999-12-10', '789 Pine St', '555-555-6666', 'mia@example.com', 4),
    (9, 'Amelia', 'Hernandez', '2000-01-20', '101 Cedar St', '555-777-8888', 'amelia@example.com', 5),
    (10, 'Harper', 'Gonzalez', '1996-05-05', '202 Maple St', '555-999-0000', 'harper@example.com', 1),
    (11, 'Liam', 'Rodriguez', '1997-09-12', '303 Oak St', '555-111-3333', 'liam@example.com', 2),
    (12, 'Evelyn', 'Perez', '1998-11-18', '404 Elm St', '555-222-4444', 'evelyn@example.com', 3),
    (13, 'Aiden', 'Sanchez', '1999-04-30', '505 Pine St', '555-333-5555', 'aiden@example.com', 4),
    (14, 'Lucas', 'Torres', '2000-08-06', '606 Cedar St', '555-444-6666', 'lucas@example.com', 5),
    (15, 'Avery', 'Ramirez', '1996-02-19', '707 Maple St', '555-555-7777', 'avery@example.com', 1),
    (16, 'Ella', 'Gomez', '1997-06-22', '808 Oak St', '555-666-8888', 'ella@example.com', 2),
    (17, 'Jackson', 'Diaz', '1998-10-28', '909 Elm St', '555-777-9999', 'jackson@example.com', 3),
    (18, 'Scarlett', 'Nguyen', '1999-12-02', '1010 Pine St', '555-888-0000', 'scarlett@example.com', 4),
    (19, 'Mason', 'Kim', '2000-04-14', '1111 Cedar St', '555-999-1111', 'mason@example.com', 5),
    (20, 'Luna', 'Singh', '1996-08-18', '1212 Maple St', '555-000-2222', 'luna@example.com', 1),
    (21, 'Mateo', 'Patel', '1997-01-21', '1313 Oak St', '555-111-3333', 'mateo@example.com', 2),
    (22, 'Penelope', 'Lee', '1998-07-03', '1414 Elm St', '555-222-4444', 'penelope@example.com', 3),
    (23, 'Riley', 'Wong', '1999-11-10', '1515 Pine St', '555-333-5555', 'riley@example.com', 4),
    (24, 'Grace', 'Ali', '2000-05-20', '1616 Cedar St', '555-444-6666', 'grace@example.com', 5),
    (25, 'Zoey', 'Kumar', '1996-03-30', '1717 Maple St', '555-555-7777', 'zoey@example.com', 1),
    (26, 'Asher', 'Chen', '1997-09-09', '1818 Oak St', '555-666-8888', 'asher@example.com', 2),
    (27, 'Ellie', 'Sharma', '1998-11-17', '1919 Elm St', '555-777-9999', 'ellie@example.com', 3),
    (28, 'Bennett', 'Rao', '1999-05-25', '2020 Pine St', '555-888-0000', 'bennett@example.com', 4),
    (29, 'Nova', 'Das', '2000-08-08', '2121 Cedar St', '555-999-1111', 'nova@example.com', 5),
    (30, 'Hudson', 'Amin', '1996-02-12', '2222 Maple St', '555-000-2222', 'hudson@example.com', 1);

-- Додавання записів до таблиці Professors
INSERT INTO Professors (ProfessorID, FirstName, LastName, Title)
VALUES
    (1, 'David', 'Miller', 'Professor'),
    (2, 'Emma', 'Wilson', 'Associate Professor'),
    (3, 'James', 'Brown', 'Assistant Professor'),
    (4, 'Olivia', 'Davis', 'Lecturer'),
    (5, 'Daniel', 'Garcia', 'Instructor');

-- Додавання записів до таблиці Schedule з 20 записами для п'яти груп, п'яти предметів та п'яти професорів
INSERT INTO Schedule (DayOfWeek, Time, GroupID, SubjectID, ProfessorID)
VALUES
    ('Monday', '08:00:00', 1, 1, 1),
    ('Tuesday', '10:00:00', 2, 2, 2),
    ('Wednesday', '12:00:00', 3, 3, 3),
    ('Thursday', '14:00:00', 4, 4, 4),
    ('Friday', '16:00:00', 5, 5, 5),
    ('Monday', '09:00:00', 2, 6, 1),
    ('Tuesday', '11:00:00', 3, 7, 2),
    ('Wednesday', '13:00:00', 4, 8, 3),
    ('Thursday', '15:00:00', 5, 9, 4),
    ('Friday', '17:00:00', 1, 10, 5),
    ('Monday', '10:00:00', 3, 11, 1),
    ('Tuesday', '12:00:00', 4, 12, 2),
    ('Wednesday', '14:00:00', 5, 13, 3),
    ('Thursday', '16:00:00', 1, 14, 4),
    ('Friday', '08:00:00', 2, 15, 5),
    ('Monday', '11:00:00', 4, 16, 1),
    ('Tuesday', '13:00:00', 5, 17, 2),
    ('Wednesday', '15:00:00', 1, 18, 3),
    ('Thursday', '17:00:00', 2, 19, 4),
    ('Friday', '09:00:00', 3, 20, 5);


-- Додавання записів до таблиці Administrators
INSERT INTO Administrators (AdministratorID, FirstName, LastName)
VALUES
    (1, 'Admin', 'Smith'),
    (2, 'Admin', 'Johnson'),
    (3, 'Admin', 'Williams'),
    (4, 'Admin', 'Brown'),
    (5, 'Admin', 'Davis');

-- Додавання записів до таблиці StudentQuestions
INSERT INTO StudentQuestions (StudentID, AdministratorID, QuestionText, DateAsked, AnswerText, DateAnswered)
VALUES
    (1, 1, 'I have a question about the course schedule.', '2023-01-15 09:00:00', 'Please come to my office for clarification.', '2023-01-16 10:30:00'),
    (2, 2, 'How can I improve my grades in Biology?', '2023-02-20 14:30:00', 'Attend extra study sessions for better understanding.', '2023-02-21 11:00:00'),
    (3, 3, 'I need help with the Economics assignment.', '2023-03-10 11:45:00', 'Sure, let’s schedule a meeting to discuss it.', '2023-03-11 13:00:00'),
    (4, 4, 'Is there any additional reading for the Literature class?', '2023-04-05 10:00:00', 'Yes, there are supplementary materials on the course webpage.', '2023-04-06 09:15:00'),
    (5, 5, 'Can I get extra resources for Physics?', '2023-05-12 13:20:00', 'Certainly, I will email you additional study materials.', '2023-05-13 15:45:00');
