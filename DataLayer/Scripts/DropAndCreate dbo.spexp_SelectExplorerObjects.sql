USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_SelectExplorerObjects] Data script: 09/02/2021 00:47:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_SelectExplorerObjects];


GO

CREATE PROCEDURE [dbo].[spexp_SelectExplorerObjects]
	@id	INT							= 0              
	,@Keyid UNIQUEIDENTIFIER		= ''
	,@Fullpath NVARCHAR(1024)		= ''
	,@Drive		NVARCHAR(3)			= ''
AS
DECLARE @KeyParentID uniqueidentifier = NULL
	, @fullpathparent NVARCHAR(1024) = ''

	IF(LEN(@Fullpath) > 0)
	BEGIN
		SELECT @fullpathparent = [dbo].[fns_ExplorerPathParent](@fullpath)

		SELECT @KeyParentID = keyid FROM [dbo].[ExplorerObjects] WHERE fullpath = @fullpathparent

		SELECT
			[id]              
			,[Keyid]              
			, @KeyParentID [Keyparentid]
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
			,[FileLastWriteTime]
		FROM [dbo].[ExplorerObjects]
		WHERE 
			[Fullpath] = @Fullpath 
	END
	ELSE
	BEGIN
		IF(LEN(@Keyid) > 0)
		BEGIN
			SELECT @fullpath = fullpath FROM [dbo].[ExplorerObjects] WHERE Keyid = @Keyid

			SELECT @fullpathparent = [dbo].[fns_ExplorerPathParent](@fullpath)

			SELECT @KeyParentID = keyid FROM [dbo].[ExplorerObjects] WHERE fullpath = @fullpathparent

			SELECT
				[id]              
				,[Keyid]              
				,@KeyParentID [Keyparentid]
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
				,[FileLastWriteTime]
			FROM [dbo].[ExplorerObjects]
			WHERE 
				[Keyid] = @Keyid
		END
		ELSE
		BEGIN
			IF(LEN(@id) > 0)
			BEGIN
				SELECT @fullpath = fullpath FROM [dbo].[ExplorerObjects] WHERE id = @id

				SELECT @fullpathparent = [dbo].[fns_ExplorerPathParent](@fullpath)

				SELECT @KeyParentID = keyid FROM [dbo].[ExplorerObjects] WHERE fullpath = @fullpathparent

				SELECT
					[id]              
					,[Keyid]              
					,@KeyParentID [Keyparentid]
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
					,[FileLastWriteTime]
				FROM [dbo].[ExplorerObjects]
				WHERE 
					[id] = @id
			END
			ELSE
			BEGIN
				SELECT
					[id]              
					,[Keyid]              
					,NULL [Keyparentid]
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
					,[FileLastWriteTime]
				FROM [dbo].[ExplorerObjects]
			END
		END
	END
