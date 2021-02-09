USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_UpdateExplorerTasks] Data script: 05/02/2021 21:21:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_UpdateExplorerTasks];


GO



CREATE PROCEDURE [dbo].[spexp_UpdateExplorerTasks]
	@Keyid UNIQUEIDENTIFIER = NULL
AS

	UPDATE [dbo].[ExplorerTasks] SET
		[modifierTime] = getdate()
		,actived = 0
	WHERE KeyId = @Keyid
		AND [deletedTime] IS NULL

	UPDATE [dbo].[ExplorerTasksLog] SET
		[modifierTime] = getdate()
	WHERE KeyId = @Keyid
		AND [deletedTime] IS NULL

RETURN 0
