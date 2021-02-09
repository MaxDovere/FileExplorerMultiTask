USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_InsertExplorerObjectsParental] Data script: 05/02/2021 20:43:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_InsertExplorerObjectsParental];


GO


CREATE PROCEDURE [dbo].[spexp_InsertExplorerObjectsParental]
	@keyparentid uniqueidentifier = null,
	@keyid uniqueidentifier = null,
	@fullpath NVARCHAR(1024) = ''
AS
DECLARE @ID INT = 0

	IF(SELECT COUNT(*) FROM [dbo].[ExplorerObjectsParental] WHERE [Fullpath] = @Fullpath) = 0
	BEGIN

		INSERT INTO [dbo].[ExplorerObjectsParental]
			([keyparentid]
			,[keyid]
			,[fullpath])
		VALUES
			(@keyparentid
			,@keyid
			,@fullpath)

		SELECT @ID = SCOPE_IDENTITY()
	END
	
	SELECT @ID as returnValue

