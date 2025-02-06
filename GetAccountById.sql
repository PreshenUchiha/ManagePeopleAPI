USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE GetAccountById
	@PersonId INT,
	@AccountId INT
AS
BEGIN

    SET NOCOUNT ON;
    
    SELECT 
       [AccountId]
      ,[PersonId]
      ,[AccountNumber]
      ,[OutstandingBalance]
	  FROM [ManagePeopleDb].[dbo].[Accounts]
	WITH (NOLOCK)
	WHERE 
		PersonId = @PersonId AND
		AccountId = @AccountId;

END
GO