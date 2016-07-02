CREATE TABLE tbl_User(
UserID int IDENTITY(1,1) PRIMARY KEY,
UserName varchar(255) NOT NULL, 
UserGender varchar(255) NOT NULL,
CONSTRAINT UserGenderContrain CHECK (UserGender = 'Male' OR UserGender = 'Female' OR UserGender = 'Other')
);

INSERT INTO tbl_Database(Data_Name, Data_value)
VALUES ('Latest_QueryNumber', 1);