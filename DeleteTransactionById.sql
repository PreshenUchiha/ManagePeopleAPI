GO

CREATE OR ALTER PROCEDURE DeleteTransactionById
    @TransactionId INT
AS
BEGIN

    SET NOCOUNT ON;

    BEGIN TRY;

    BEGIN TRANSACTION;
    
        DELETE FROM [ManagePeopleDb].[dbo].[Transactions]
        
        WHERE 
            TransactionId = @TransactionId;

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