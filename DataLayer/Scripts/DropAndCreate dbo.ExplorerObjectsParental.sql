USE [explorer]
GO
 
/****** Oggetto: Table [dbo].[ExplorerObjectsParental] Data script: 09/02/2021 00:45:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[ExplorerObjectsParental];


GO
CREATE TABLE [dbo].[ExplorerObjectsParental] (
    [id]           INT              IDENTITY (1, 1) NOT NULL,
    [keyparentid]  UNIQUEIDENTIFIER NULL,
    [keyid]        UNIQUEIDENTIFIER NOT NULL,
    [fullpath]     NVARCHAR (1024)  NOT NULL,
    [creationTime] DATETIME2         NOT NULL default(getdate()),
    [modifierTime] DATETIME2         NULL,
    [deletedTime]  DATETIME2         NULL
);


