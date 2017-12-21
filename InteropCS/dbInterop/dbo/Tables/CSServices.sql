CREATE TABLE [dbo].[CSServices] (
    [Code]            NVARCHAR (128) NOT NULL,
    [ParticipantCode] NVARCHAR (128) NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [Wsdl]            NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.CSServices] PRIMARY KEY CLUSTERED ([Code] ASC, [ParticipantCode] ASC),
    CONSTRAINT [FK_dbo.CSServices_dbo.Participants_ParticipantCode] FOREIGN KEY ([ParticipantCode]) REFERENCES [dbo].[Participants] ([Code]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ParticipantCode]
    ON [dbo].[CSServices]([ParticipantCode] ASC);

