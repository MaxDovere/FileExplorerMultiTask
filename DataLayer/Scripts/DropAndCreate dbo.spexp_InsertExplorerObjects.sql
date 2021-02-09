USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_InsertExplorerObjects] Data script: 05/02/2021 20:17:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_InsertExplorerObjects];


GO
CREATE PROCEDURE [dbo].[spexp_InsertExplorerObjects]
	@Keyid UNIQUEIDENTIFIER			= NULL
	,@Fullpath NVARCHAR(1024)		= ''
	,@Drive NVARCHAR(5)				= ''
    ,@Name NVARCHAR(1024)			= ''
    ,@IsDirectory INT         		= 0
    ,@Length BIGINT					= 0
	,@Attributes NVARCHAR(255)  	= ''
    ,@DirectoryName NVARCHAR(1024)	= ''
    ,@Extension NVARCHAR (1024)    	= ''
    ,@FileCreationTime DATETIME2 	= null
    ,@FileLastAccessTime DATETIME2	= null
    ,@FileLastWriteTime DATETIME2 	= null
AS
DECLARE @ID INT = 0
	, @KeyParentID uniqueidentifier = NULL
	, @fullpathparent NVARCHAR(1024) = ''

	IF(SELECT COUNT(*) FROM [dbo].[ExplorerObjects] WHERE [Fullpath] = @Fullpath) = 0
	BEGIN

		INSERT INTO [dbo].[ExplorerObjects]
			([Keyid]
			,[Fullpath]          
			,[Drive]
			,[Name]              
			,[IsDirectory]       
			,[Length]            
			,[Attributes]        
			,[DirectoryName]     
			,[Extension]         
			,[FileCreationTime]  
			,[FileLastAccessTime]
			,[FileLastWriteTime]) 
		VALUES
			(@Keyid
			 ,@Fullpath 
			 ,@Drive
			 ,@Name 
			 ,@IsDirectory 
			 ,@Length 
			 ,@Attributes 
			 ,@DirectoryName 
			 ,@Extension 
			 ,@FileCreationTime
			 ,@FileLastAccessTime
			 ,@FileLastWriteTime)

		SELECT @ID = SCOPE_IDENTITY()

		IF(@ID > 0)
		BEGIN
			
			IF(NOT @Keyid IS NULL)
			BEGIN 
				SELECT @fullpathparent = [dbo].[fns_ExplorerPathParent](@fullpath)

				SELECT @KeyParentID = keyid FROM [dbo].[ExplorerObjects] WHERE fullpath = @fullpathparent
				EXECUTE [dbo].[spexp_InsertExplorerObjectsParental] @KeyParentID, @Keyid, @fullpath
			END
		END
	END

	SELECT @ID as returnValue


