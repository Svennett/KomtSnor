-----------------------	Standard update -----------------------------
BEGIN
	DECLARE @owner varchar(255) = 'sven';
	DECLARE @QueryNumber varchar(255) = '4';

	UPDATE dbo.tbl_Database
	SET DataValue = @QueryNumber
	WHERE DataName = 'Latest_QueryNumber';
	
END
-----------------------	Standard update -----------------------------

BEGIN

ALTER TABLE tbl_Login
ADD UserID int NOT NULL


ALTER TABLE tbl_Login
ADD FOREIGN KEY (UserID)
REFERENCES tbl_User(UserID);


ALTER TABLE tbl_Login
ADD CONSTRAINT UserIDConstraint
FOREIGN KEY (UserID)
REFERENCES tbl_User(UserID);

END

BEGIN

CREATE TABLE tbl_Country(
CountryID int IDENTITY(1,1) PRIMARY KEY,
CountryName varchar(255) NOT NULL,
CountryCode varchar(255) NOT NULL,
CountryFlag image
)

END