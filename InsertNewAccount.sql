USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE InsertNewAccount
	@AccountId	 INT OUTPUT,
	@PersonId INT,
	@AccountNumber VARCHAR(50),
	@OutstandingBalance Money
	
AS
BEGIN

    SET NOCOUNT ON;

	INSERT INTO [ManagePeopleDb].[dbo].[Accounts] (PersonId, AccountNumber, OutstandingBalance)
	VALUES (@PersonId, @AccountNumber, @OutstandingBalance);

	SET @AccountId = SCOPE_IDENTITY();

END
GO

