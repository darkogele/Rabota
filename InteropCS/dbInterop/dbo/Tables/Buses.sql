CREATE TABLE [dbo].[Buses] (
    [Code] NVARCHAR (128) NOT NULL,
    [Url]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Buses] PRIMARY KEY CLUSTERED ([Code] ASC)
);

