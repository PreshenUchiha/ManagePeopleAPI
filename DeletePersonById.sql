USE [ManagePeopleDb]
GO
/****** Object:  StoredProcedure [dbo].[DeletePersonById]    Script Date: 2025/02/10 13:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[DeletePersonById]
@PersonId INT 
AS
BEGIN

    SET NOCOUNT ON;
    
    DELETE
	FROM [dbo].Persons
	WHERE PersonId = @PersonId;

END
