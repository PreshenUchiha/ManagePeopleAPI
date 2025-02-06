CREATE OR ALTER PROCEDURE GetPersonById
	@PersonId INT
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