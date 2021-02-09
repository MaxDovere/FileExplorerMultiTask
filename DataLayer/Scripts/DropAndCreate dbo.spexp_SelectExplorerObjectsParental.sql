USE [explorer]
GO
 
/****** Oggetto: SqlProcedure [dbo].[spexp_SelectExplorerObjectsParental] Data script: 09/02/2021 00:48:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spexp_SelectExplorerObjectsParental];


GO

CREATE PROCEDURE [dbo].[spexp_SelectExplorerObjectsParental]
	@id	INT							= 0              
	,@Keyid UNIQUEIDENTIFIER		= ''
	,@Fullpath NVARCHAR(1024)		= ''
AS

	IF(LEN(@Fullpath) > 0)
	BEGIN

		SELECT
			[id]              
			,[Keyid]              
			,[Keyparentid]
			,[Fullpath]          
		FROM [dbo].[ExplorerObjectsParental]
		WHERE 
			[Fullpath] = @Fullpath 
	END
	ELSE
	BEGIN
		IF(LEN(@Keyid) > 0)
		BEGIN
			SELECT
				[id]              
				,[Keyid]              
				,[Keyparentid]
				,[Fullpath]          
			FROM [dbo].[ExplorerObjectsParental]
			WHERE 
				[Keyid] = @Keyid
		END
		ELSE
		BEGIN
			IF(LEN(@id) > 0)
			BEGIN
				SELECT
					[id]              
					,[Keyid]              
					,[Keyparentid]
					,[Fullpath]          
				FROM [dbo].[ExplorerObjectsParental]
				WHERE 
					[id] = @id
			END
			ELSE
			BEGIN
				SELECT
					[id]              
					,[Keyid]              
					,[Keyparentid]
					,[Fullpath]          
				FROM [dbo].[ExplorerObjectsParental]
			END
		END
	END
