USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_DeleteExplorerTasks] Data script: 09/02/2021 00:46:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_DeleteExplorerTasks];


GO


CREATE PROCEDURE [dbo].[spexp_DeleteExplorerTasks]
	@id int = 0
	, @keyid UNIQUEIDENTIFIER = NULL

AS
	IF(@id = 0 AND @keyid IS NULL)
	BEGIN
		TRUNCATE TABLE [dbo].[ExplorerTasks] 
		EXECUTE [dbo].[spexp_DeleteExplorerTasksLog] @keyid
	END
	ELSE
	BEGIN
		UPDATE [dbo].[ExplorerTasks] SET
			[deletedTime] = getdate()
			, actived = 0
		WHERE id = @id OR keyid = @keyid
		
		IF(@keyid IS NULL)
			SELECT @keyid = keyid FROM [dbo].[ExplorerTasks] WHERE ID = @id  
		
		EXECUTE [dbo].[spexp_DeleteExplorerTasksLog] @keyid

	END 

	
RETURN 0
