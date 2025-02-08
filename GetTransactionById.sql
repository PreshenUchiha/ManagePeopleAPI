USE [ManagePeopleDb];
GO

CREATE OR ALTER PROCEDURE [dbo].[GetTransactionById]
    @TransactionId INT
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
        TransactionId = @TransactionId;

END
GO