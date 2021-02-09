USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_DeleteExplorerObjects] Data script: 09/02/2021 00:46:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_DeleteExplorerObjects];


GO

CREATE PROCEDURE [dbo].[spexp_DeleteExplorerObjects]
	@id int = 0
AS
DECLARE @IDP INT = 0 
	, @KEYID uniqueidentifier = NULL
	, @fullpath NVARCHAR(1024) = ''

	IF(@id = 0)
	BEGIN
		TRUNCATE TABLE [dbo].[ExplorerObjects]
		EXECUTE [dbo].[spexp_DeleteExplorerObjectsParental] @id
	END
	ELSE
	BEGIN
		SELECT @keyid = keyid, @fullpath = fullpath 
		FROM [dbo].[ExplorerObjects]
		WHERE
			ID = @ID
	
		DELETE FROM [dbo].[ExplorerObjects]
		WHERE
			ID = @ID
		
		--SELECT @fullpathparent = [dbo].[fns_ExplorerPathParent](@fullpath)

		SELECT @IDP = ID 
		FROM [dbo].[ExplorerObjects]
		WHERE
			keyid = @keyid AND 
			fullpath = @fullpath
		
		IF(@IDP > 0) 
		BEGIN
			EXECUTE [dbo].[spexp_DeleteExplorerObjectsParental] @IDP
		END
	END
