USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_UpdateExplorerObjects] Data script: 09/02/2021 00:48:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_UpdateExplorerObjects];


GO

CREATE PROCEDURE [dbo].[spexp_UpdateExplorerObjects]
	@KeyId UNIQUEIDENTIFIER			= NULL
	,@Fullpath NVARCHAR(1024)		= ''
    ,@Drive		NVARCHAR(3)			= ''
	,@Name NVARCHAR(1024)			= ''
    ,@IsDirectory INT         		= 0
    ,@Length BIGINT					= 0
	,@Attributes NVARCHAR(255)  	= ''
    ,@DirectoryName NVARCHAR(102)	= ''
    ,@Extension NVARCHAR (50)    	= ''
    ,@FileCreationTime DATETIME2 	= null
    ,@FileLastAccessTime DATETIME2	= null
    ,@FileLastWriteTime DATETIME2 	= null
AS
DECLARE @KeyParentID uniqueidentifier = NULL
	, @fullpathparent NVARCHAR(1024) = ''

	UPDATE [dbo].[ExplorerObjects] SET
		[Fullpath]				= @Fullpath          
		,[Drive]				= @Drive
		,[Name]					= @Name
		,[IsDirectory]			= @IsDirectory
		,[Length]            	= @Length
		,[Attributes]        	= @Attributes
		,[DirectoryName]     	= @DirectoryName
		,[Extension]         	= @Extension
		,[FileCreationTime]  	= @FileCreationTime
		,[FileLastAccessTime]	= @FileLastAccessTime
		,[FileLastWriteTime] 	= @FileLastWriteTime
	WHERE
		KeyId = @KeyId
		
	SELECT @fullpathparent = [dbo].[fns_ExplorerPathParent](@fullpath)

	SELECT @KeyParentID = keyid FROM [dbo].[ExplorerObjects] WHERE fullpath = @fullpathparent

	EXECUTE [dbo].[spexp_UpdateExplorerObjectsParental] @KeyParentID, @KEYID, @fullpath		

RETURN 0
