USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_InsertExplorerTasksLog] Data script: 05/02/2021 20:43:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_InsertExplorerTasksLog];


GO


CREATE PROCEDURE [dbo].[spexp_InsertExplorerTasksLog]
	@Keyid UNIQUEIDENTIFIER = NULL,
	@HResult INT = 0,
	@Message NVARCHAR(1024) = ''
AS
DECLARE @ID INT = 0

	INSERT INTO [dbo].[ExplorerTasksLog]
		([Keyid]
		,[HResult]
		,[Message])
	VALUES
		(@Keyid
		,@HResult
		,@Message)

	SELECT @ID = SCOPE_IDENTITY()

	SELECT @ID as returnValue

