CREATE TABLE [dbo].[AdministrativeServiceLog] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ChangeDate]              DATETIME       NOT NULL,
    [UserName]                NVARCHAR (MAX) NULL,
    [PerformedActivity]       NVARCHAR (MAX) NULL,
    [AdministrativeServiceId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.AdministrativeServiceLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AdministrativeServiceLog_dbo.AdministrativeService_AdministrativeServiceId] FOREIGN KEY ([AdministrativeServiceId]) REFERENCES [dbo].[AdministrativeService] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AdministrativeServiceId]
    ON [dbo].[AdministrativeServiceLog]([AdministrativeServiceId] ASC);

