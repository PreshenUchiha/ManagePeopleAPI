GO

CREATE OR ALTER PROCEDURE GetAllAccountsByPersonId
    @PersonId INT
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
    WHERE PersonId = @PersonId ;

END
GO