USE [explorer]
GO
 
/****** Oggetto: Scalar Function [dbo].[fns_ExplorerPathParent] Data script: 09/02/2021 00:46:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP FUNCTION [dbo].[fns_ExplorerPathParent];


GO
CREATE FUNCTION [dbo].[fns_ExplorerPathParent]
(
	@fullpath nvarchar(1024) = ''
)
RETURNS nvarchar(1024)
AS
BEGIN
declare @path_table table (id int IDENTITY(1,1), value nvarchar(1024))
declare @fullpathparent nvarchar(1024)

		INSERT INTO @path_table
		SELECT  
			value  
		FROM 
			STRING_SPLIT(@fullpath, '\')		

		DELETE FROM @path_table WHERE ID = (SELECT MAX(ID) FROM @path_table)

		--SELECT * FROM @path_table
		SELECT @fullpathparent = COALESCE(@fullpathparent + '\', '') + value FROM @path_table		
	
	RETURN @fullpathparent 
END
