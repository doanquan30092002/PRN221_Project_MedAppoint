CREATE TABLE Roles(
	RoleID int PRIMARY KEY Identity(1,1),
	RoleName VARCHAR(20),
);
CREATE TABLE Specialist(
	SpecialtyID int PRIMARY KEY Identity(1,1),
	SpecialtyName VARCHAR(50),
);

-- Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY Identity(1,1),
    Username NVARCHAR(255),
    Password VARCHAR(255),
    Email VARCHAR(255),
    Phone VARCHAR(10),
	SpecialtyID int, -- cua bac si
	Address NVARCHAR(255), -- cua bac si
	RoleID int,
	Avatar varchar(100),
	Gender bit,
	FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
);
CREATE TABLE UsersToSpecialist(
	UsersToSpecialistID int PRIMARY KEY Identity(1,1),
	UserID INT,
	SpecialtyID int ,
	
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (SpecialtyID) REFERENCES Specialist(SpecialtyID)
);

-- HealthInformation Table Quản lý thông tin sức khỏe cá nhân
CREATE TABLE HealthInformation (
    HealthInfoID INT PRIMARY KEY Identity(1,1),
    UserID INT,
    Allergies TEXT,
    Medications TEXT,
    Height float,
	weight float,
	BloodType varchar,
	HealthHistory ntext,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
-- lịch nghỉ của bác sĩ
Create Table DoctorDayOff(
	DoctorDayOffID int PRIMARY KEY Identity(1,1),
	DoctorID INT,
	StartDate Datetime,
	EndDate Datetime,
	FOREIGN KEY (DoctorID) REFERENCES Users(UserID),
);
-- Appointments Table
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY Identity(1,1),
    UserID INT,
    DoctorID INT,
    StartDate DATETIME,
	EndDate DATETIME,
    SpecialtyID int, -- sang màn thứ 2 mới chọn select
	Status VARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (DoctorID) REFERENCES Users(UserID),
	FOREIGN KEY (SpecialtyID) REFERENCES Specialist(SpecialtyID)
);


-- ElectronicMedicalRecords Table
CREATE TABLE ElectronicMedicalRecords (
    EMR_ID INT PRIMARY KEY Identity(1,1),
    AppointmentID INT,
    --MedicalInformation TEXT,
	TestResults TEXT,
    TreatmentPlans TEXT,
    LastUpdated DateTime,
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID),
);
-- Payments Table
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY Identity(1,1),
    UserID INT,
    AppointmentID INT,
    Amount DECIMAL(10, 2),
    PaymentDate TIMESTAMP,
	Message NText,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID)
);

-- DoctorReviews Table
CREATE TABLE DoctorReviews (
    ReviewID INT PRIMARY KEY Identity(1,1),
    UserID INT,
    DoctorID INT,
    Comment TEXT,
    ReviewDate DateTIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (DoctorID) REFERENCES Users(UserID)
);

-- Tạo 5 bản ghi cho bảng Roles -- sửa lại
INSERT INTO Roles (RoleName, IsDeleted) VALUES 
('Admin', 0),
('Customer', 0),
('Doctor', 0);

-- Tạo 5 bản ghi cho bảng Specialist -- ko cần
INSERT INTO Specialists (SpecialtyName, IsDeleted) VALUES 
('Cardiology', 0),
('Neurology', 0),
('Pediatrics', 0),
('Oncology', 0),
('Dermatology', 0);

-- Tạo 5 bản ghi cho bảng Users
INSERT INTO Users (Username, Password, Email, Phone, Address, RoleID, Avatar, Gender, IsDeleted) VALUES 
('admin', '123456', 'user1@example.com', '123456789', '123 Main St', 1, Null, 1, 0),
('user2', 'pass2', 'user2@example.com', '987654321', '456 Elm St', 2, Null, 0, 0),
('user3', 'pass3', 'user3@example.com', '654321789', '789 Oak St', 2, Null, 1, 0),
('user4', 'pass4', 'user4@example.com', '321987654', '101 Pine St', 3, Null, 0, 0),
('user5', 'pass5', 'user5@example.com', '456789123', '246 Maple St', 3, Null, 1, 0);

-- Tạo 5 bản ghi cho bảng UsersToSpecialist
INSERT INTO UsersToSpecialists (UserID, SpecialistID, IsDeleted) VALUES 
(4, 1, 0),
(4, 2, 0),
(4, 3, 0),
(5, 4, 0),
(5, 5, 0);

-- Tạo 5 bản ghi cho bảng HealthInformation
INSERT INTO HealthInformations (UserID, Allergies, Medications, Height, Weight, BloodType, HealthHistory, IsDeleted) VALUES 
(2, 'Nuts, Dust', 'Antihistamines', 162.5, 55.8, 'B', 'None', 0),
(3, 'Shellfish', 'Antibiotics, Antidepressants', 180.1, 75.6, 'A', 'Diabetes Type 2', 0),
(4, 'Latex', 'Chemotherapy', 169.8, 70.1, 'B', 'None', 0),
(5, 'None', 'None', 155.2, 48.9, 'O', 'None', 0);


-- Tạo 5 bản ghi cho bảng DoctorDayOff
INSERT INTO DoctorDayOffs (DoctorID, StartDate, EndDate, Reasons, IsDeleted) VALUES 
(4, '2024-03-04', '2024-03-06', 'Sick leave', 0),
(5, '2024-03-05', '2024-03-07', 'Professional development', 0);

-- Tạo 5 bản ghi cho bảng Appointments

INSERT INTO Appointments (UserID, DoctorID, StartDate, EndDate, SpecialistID, Status, IsDeleted) VALUES 
(3, 4, '2024-03-01 09:00:00', '2024-03-01 10:00:00', 2, 'Confirmed', 0),
(2, 4, '2024-03-02 10:00:00', '2024-03-02 11:00:00', 1, 'Confirmed', 0),
(3, 5, '2024-03-03 11:00:00', '2024-03-03 12:00:00', 5, 'Confirmed', 0),
(2, 4, '2024-03-04 12:00:00', '2024-03-04 13:00:00', 3, 'Confirmed', 0),
(3, 5, '2024-03-05 13:00:00', '2024-03-05 14:00:00', 4, 'Confirmed', 0);


-- Tạo 5 bản ghi cho bảng ElectronicMedicalRecords
INSERT INTO ElectronicMedicalRecords (AppointmentID, TestResults, TreatmentPlans, LastUpdated, IsDeleted) VALUES 
(1, 'Blood pressure: 120/80', 'Prescription: Antibiotics', '2024-03-01 10:00:00', 0),
(2, 'Blood pressure: 110/70', 'No further treatment needed', '2024-03-02 11:00:00', 0),
(3, 'Blood pressure: 130/85', 'Follow-up appointment scheduled', '2024-03-03 12:00:00', 0),
(4, 'Blood pressure: 140/90', 'Chemo regimen updated', '2024-03-04 13:00:00', 0),
(5, 'Blood pressure: 115/75', 'Routine check-up completed', '2024-03-05 14:00:00', 0);

-- Tạo 5 bản ghi cho bảng Payments
INSERT INTO Payments (UserID, AppointmentID, Amount, PaymentDate, Message, IsDeleted) VALUES 
(1, 1, 50.00, NULL, 'Momo', 0),
(2, 2, 0.00, NULL, 'Covered by insurance', 0),
(3, 3, 75.00, NULL, 'Momo', 0),
(4, 4, 0.00, NULL, 'Covered by insurance', 0),
(5, 5, 100.00, NULL, 'Momo', 0);

-- Tạo 5 bản ghi cho bảng DoctorReviews
INSERT INTO DoctorReviews (UserID, DoctorID, Comment, ReviewDate, IsDeleted) VALUES 
(2, 4, 'Great bedside manner', '2024-03-01 10:00:00', 0),
(2, 4, 'Very knowledgeable', '2024-03-02 11:00:00', 0),
(3, 5, 'Friendly and thorough', '2024-03-03 12:00:00', 0),
(3, 4, 'Professional and caring', '2024-03-04 13:00:00', 0),
(2, 5, 'Highly recommend', '2024-03-05 14:00:00', 0);