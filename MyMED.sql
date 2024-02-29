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
