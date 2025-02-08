USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE UpdateTransactionById
    @TransactionId INT,
	@AccountId INT,
	@TransactionDate DateTime,
	@CaptureDate DateTime,
	@Amount Money,	
	@Description VARCHAR(50)
AS
BEGIN

    SET NOCOUNT ON;

	BEGIN TRY;

	BEGIN TRANSACTION;
    
		UPDATE [ManagePeopleDb].[dbo].[Transactions]
		SET
			[TransactionDate]=@TransactionDate,
			[CaptureDate] = @CaptureDate,
			[Amount] = @Amount,
			[description] =  @Description
		WHERE 
		   [TransactionId] = @TransactionId AND
			[AccountId] = @AccountId;

	COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH;

		IF (@@TRANCOUNT > 0)
		BEGIN;
			ROLLBACK TRANSACTION;
		END;

		PRINT 'Error occurred in ' + ERROR_PROCEDURE() + ' ' + ERROR_MESSAGE();

	END CATCH;

END
GO 