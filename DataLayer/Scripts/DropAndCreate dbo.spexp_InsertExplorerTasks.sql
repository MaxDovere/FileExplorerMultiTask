USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_InsertExplorerTasks] Data script: 05/02/2021 20:34:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_InsertExplorerTasks];


GO

CREATE PROCEDURE [dbo].[spexp_InsertExplorerTasks]
	@Keyid UNIQUEIDENTIFIER = NULL,
	@name NVARCHAR(1024) = '',
	@Message NVARCHAR(1024) = '',
	@ThreadNum INT			= 0,
	@NIterations	INT		= 0,
	@actived INT = 0
AS
DECLARE @ID INT = 0

	INSERT INTO [dbo].[ExplorerTasks]
		([Keyid]
		,[name]
		,[Message]
		,[ThreadNum]
		,[NIterations]
		,[actived])
	VALUES
		(@Keyid
		,@name
		,@Message
		,@ThreadNum
		,@NIterations
		,@actived)

	SELECT @ID = SCOPE_IDENTITY()
	
	SELECT @ID as returnValue
