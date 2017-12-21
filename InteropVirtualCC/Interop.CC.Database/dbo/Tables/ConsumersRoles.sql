CREATE TABLE [dbo].[ConsumersRoles] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Consumer] NVARCHAR (20)  NOT NULL,
    [RoleId]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_ConsumersRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

