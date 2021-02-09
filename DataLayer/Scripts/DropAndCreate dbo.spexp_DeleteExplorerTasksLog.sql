USE [explorer]
GO 

/****** Oggetto: SqlProcedure [dbo].[spexp_DeleteExplorerTasksLog] Data script: 09/02/2021 00:46:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_DeleteExplorerTasksLog];


GO



CREATE PROCEDURE [dbo].[spexp_DeleteExplorerTasksLog]
	@keyid UNIQUEIDENTIFIER = NULL
AS
	IF(@keyid IS NULL)
	BEGIN
		TRUNCATE TABLE [dbo].[ExplorerTasksLog] 
	END
	ELSE
	BEGIN
		UPDATE [dbo].[ExplorerTasksLog] SET
			[deletedTime] = getdate()
		WHERE keyid = @keyid
	END
