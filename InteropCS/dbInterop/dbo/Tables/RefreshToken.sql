﻿CREATE TABLE [dbo].[RefreshToken] (
    [Id]              NVARCHAR (128) NOT NULL,
    [Subject]         NVARCHAR (50)  NOT NULL,
    [ClientId]        NVARCHAR (50)  NOT NULL,
    [IssuedUtc]       DATETIME       NOT NULL,
    [ExpiresUtc]      DATETIME       NOT NULL,
    [ProtectedTicket] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.RefreshToken] PRIMARY KEY CLUSTERED ([Id] ASC)
);

