USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE UpdateAccountById
	@AccountId	 INT,
	@PersonId INT,
	@AccountNumber VARCHAR(50),
	@OutstandingBalance Money
AS
BEGIN

    SET NOCOUNT ON;

	BEGIN TRY;

	BEGIN TRANSACTION;
    
		UPDATE [ManagePeopleDb].[dbo].[Accounts]
		SET
			[AccountNumber] = @AccountNumber,
			[OutstandingBalance] = @OutstandingBalance
		WHERE 
			[AccountId] = @AccountId AND
			[PersonId] = @PersonId;

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