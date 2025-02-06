GO

CREATE OR ALTER PROCEDURE DeleteAccountById
	@PersonId INT,
    @AccountId INT
AS
BEGIN

    SET NOCOUNT ON;

    BEGIN TRY;

    BEGIN TRANSACTION;
    
        DELETE FROM [ManagePeopleDb].[dbo].[Transactions]
        WHERE           
            AccountId = @AccountId;
        
        DELETE FROM [ManagePeopleDb].[dbo].[Accounts]
        WHERE 
            PersonId = @PersonId AND
            AccountId = @AccountId;

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