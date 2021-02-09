USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_UpdateExplorerObjectsParental] Data script: 09/02/2021 00:48:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_UpdateExplorerObjectsParental];


GO



CREATE PROCEDURE [dbo].[spexp_UpdateExplorerObjectsParental]
	@keyparentid uniqueidentifier = null,
	@keyid uniqueidentifier = null,
	@fullpath NVARCHAR(1024) = ''
AS

	UPDATE [dbo].[ExplorerObjectsParental] SET
		[keyparentid]	= @keyparentid 
		,[fullpath]		= @fullpath
	WHERE 
		keyid = @keyid

RETURN 0
