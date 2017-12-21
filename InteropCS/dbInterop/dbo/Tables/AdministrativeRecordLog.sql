CREATE TABLE [dbo].[AdministrativeRecordLog] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [ChangeDate]             DATETIME       NOT NULL,
    [UserName]               NVARCHAR (MAX) NULL,
    [PerformedActivity]      NVARCHAR (MAX) NULL,
    [AdministrativeRecordId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.AdministrativeRecordLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AdministrativeRecordLog_dbo.AdministrativeRecord_AdministrativeRecordId] FOREIGN KEY ([AdministrativeRecordId]) REFERENCES [dbo].[AdministrativeRecord] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_AdministrativeRecordId]
    ON [dbo].[AdministrativeRecordLog]([AdministrativeRecordId] ASC);

