USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE InsertNewTransaction
    @TransactionId INT OUTPUT,
	@AccountId INT,
	@TransactionDate DateTime,
	@CaptureDate DateTime,
	@Amount Money,	
	@Description VARCHAR(50)
	
	
AS
BEGIN

    SET NOCOUNT ON;

	INSERT INTO [dbo].[Transactions] (AccountId, TransactionDate, CaptureDate,Amount,[description])
	VALUES (@AccountId, @TransactionDate, @CaptureDate,@Amount,@Description);

	SET @TransactionId = SCOPE_IDENTITY();

END
GO

