USE ManagePeopleDb;
GO

CREATE OR ALTER PROCEDURE DeletePersonById
@PersonId INT = 1
AS
BEGIN

    SET NOCOUNT ON;
    
    SELECT 
        [PersonId],
        [Name],
        [Surname],
        [IdNumber]
	FROM [dbo].Persons WITH (NOLOCK)
	WHERE PersonId = @PersonId;

END
GO