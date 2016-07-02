Create TABLE tbl_Database 
(
Data_ID int IDENTITY(1,1) PRIMARY KEY,
Data_Name varchar(255) NOT NULL UNIQUE,
Data_Value varchar(255)
);

