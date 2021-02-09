USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_DeleteExplorerObjectsParental] Data script: 09/02/2021 00:46:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_DeleteExplorerObjectsParental];


GO



CREATE PROCEDURE [dbo].[spexp_DeleteExplorerObjectsParental]
	@id int = 0
AS
	IF(@id = 0)
	BEGIN
		TRUNCATE TABLE [dbo].[ExplorerObjectsParental]
	END
	ELSE
	BEGIN
		DELETE FROM [dbo].[ExplorerObjectsParental]
		WHERE id = @id

	END


