USE [explorer]
GO
 
/****** Oggetto: Table [dbo].[ExplorerTasks] Data script: 09/02/2021 00:45:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[ExplorerTasks];


GO
CREATE TABLE [dbo].[ExplorerTasks] (
    [id]           INT              IDENTITY (1, 1) NOT NULL,
    [KeyId]        UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (1024)  NOT NULL,
    [Message]      NVARCHAR (1024)  NOT NULL,
    [ThreadNum]    INT              NULL,
    [NIterations]  INT              NULL,
    [Actived]      INT              NULL,
    [creationTime] DATETIME2         NOT NULL default(getdate()),
    [modifierTime] DATETIME2         NULL,
    [deletedTime]  DATETIME2         NULL,
);


