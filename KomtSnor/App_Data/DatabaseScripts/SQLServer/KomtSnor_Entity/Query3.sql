-----------------------	Standard update -----------------------------
BEGIN
	DECLARE @owner varchar(255) = 'sven';
	DECLARE @QueryNumber varchar(255) = '3';

	UPDATE dbo.tbl_Database
	SET Data_Value = @QueryNumber
	WHERE Data_Name = 'Latest_QueryNumber';
	
END
-----------------------	Standard update -----------------------------

BEGIN

CREATE TABLE tbl_Login(
LoginID int IDENTITY(1,1) Primary Key,
LoginEmail varchar(255) NOT NULL,
LoginPassword varchar(255) NOT NULL,
LoginStatus varchar(255) NOT NULL,
LoginCreationDate date NOT NULL,
CONSTRAINT LoginStatusConstrain CHECK (LoginStatus = 'Enabled' OR LoginStatus = 'Disabled' OR LoginStatus = 'NoEmail')
)

END

BEGIN

EXEC sp_rename 'tbl_Database.Data_Name', 'DataName', 'COLUMN';

EXEC sp_rename 'tbl_Database.Data_Value', 'DataValue', 'COLUMN';

END

