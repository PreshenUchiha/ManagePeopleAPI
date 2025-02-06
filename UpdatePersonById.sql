USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE UpdatePersonById
	@PersonId INT,
	@Name NVARCHAR(25),
	@Surname NVARCHAR(50),
	@IdNumber NVARCHAR(15)
AS
BEGIN

    SET NOCOUNT ON;
	UPDATE Persons
	SET 
	[Name]= @Name,
	[Surname]= @Surname, 
	[IdNumber]= @IdNumber
	WHERE [PersonId]= @PersonId

END
GO