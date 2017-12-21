CREATE TABLE [dbo].[Services] (
    [Code]     NVARCHAR (128) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [Endpoint] NVARCHAR (100) NOT NULL,
    [Wsdl]     NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Services] PRIMARY KEY CLUSTERED ([Code] ASC)
);

