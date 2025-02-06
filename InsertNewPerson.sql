USE [ManagePeopleDb]
GO

CREATE OR ALTER PROCEDURE InsertNewPerson
	@PersonId INT OUTPUT,
	@Name NVARCHAR(25),
	@Surname NVARCHAR(15),
	@IdNumber NVARCHAR(15)
AS
BEGIN

    SET NOCOUNT ON;

	INSERT INTO [Persons]([Name], [Surname], [IdNumber])
	VALUES (@Name, @Surname, @IdNumber);

	SET @PersonId = SCOPE_IDENTITY();

END
GO