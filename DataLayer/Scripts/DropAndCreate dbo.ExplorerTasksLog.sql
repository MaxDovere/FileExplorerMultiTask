USE [explorer]
GO
 
/****** Oggetto: Table [dbo].[ExplorerTasksLog] Data script: 09/02/2021 00:45:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[ExplorerTasksLog];


GO
CREATE TABLE [dbo].[ExplorerTasksLog] (
    [id]           INT              IDENTITY (1, 1) NOT NULL,
    [KeyId]        UNIQUEIDENTIFIER NOT NULL,
    [HResult]      INT              NOT NULL,
    [Message]      NVARCHAR (1024)  NOT NULL,
    [creationTime] DATETIME2         NOT NULL default(getdate()),
    [modifierTime] DATETIME2         NULL,
    [deletedTime]  DATETIME2         NULL
);


