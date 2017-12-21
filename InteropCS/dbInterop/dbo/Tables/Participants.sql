CREATE TABLE [dbo].[Participants] (
    [Code]      NVARCHAR (128) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Uri]       NVARCHAR (MAX) NOT NULL,
    [IsActive]  BIT            NOT NULL,
    [PublicKey] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Participants] PRIMARY KEY CLUSTERED ([Code] ASC)
);



