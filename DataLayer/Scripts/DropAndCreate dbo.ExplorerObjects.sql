USE [explorer]
GO
 
/****** Oggetto: Table [dbo].[ExplorerObjects] Data script: 04/02/2021 18:21:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[ExplorerObjects];


GO
CREATE TABLE [dbo].[ExplorerObjects] (
    [id]					INT	IDENTITY (1, 1) NOT NULL,
    [Keyid]					UNIQUEIDENTIFIER	NOT NULL default(NEWID()),
    [Fullpath]				NVARCHAR (1024)		NOT NULL,
    [Drive]					NVARCHAR (5)		NOT NULL,
    [Name]					NVARCHAR (1024)		NOT NULL,
    [IsDirectory]			INT					NULL,
	[Length]				BIGINT				NOT NULL,
	[Attributes]			NVARCHAR (255)		NULL,
	[DirectoryName]			NVARCHAR (1024)		NOT NULL,
	[Extension]				NVARCHAR (1024)		NOT NULL,
	[FileCreationTime]      DATETIME2			NULL,
	[FileLastAccessTime]	DATETIME2			NULL,
	[FileLastWriteTime]		DATETIME2			NULL,
    [creationTime]			DATETIME2			NOT NULL default(getdate()),
    [modifierTime]			DATETIME2			NULL,
    [deletedTime]			DATETIME2			NULL
);


