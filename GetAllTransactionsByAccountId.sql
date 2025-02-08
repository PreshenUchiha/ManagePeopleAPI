USE [ManagePeopleDb];
GO

CREATE OR ALTER PROCEDURE [dbo].[GetAllTransactionsByAccountId]
    @AccountId INT
AS
BEGIN

    SET NOCOUNT ON;

    SELECT 
       [TransactionId]
      ,[AccountId]
      ,[TransactionDate]
      ,[CaptureDate]
      ,[Amount]
      ,[description]
    FROM [dbo].[Transactions]
    WITH (NOLOCK)
    WHERE
        AccountId =2

END
GO