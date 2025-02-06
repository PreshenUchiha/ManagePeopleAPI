USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE GetAllPersons
AS
BEGIN

    SET NOCOUNT ON;
    
    SELECT 
        [PersonId],
        [Name],
        [Surname],
        [IdNumber]
    FROM [dbo].Persons WITH (NOLOCK)

END
GO