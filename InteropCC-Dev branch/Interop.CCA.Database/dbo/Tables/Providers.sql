CREATE TABLE [dbo].[Providers] (
    [PublicKey]    NVARCHAR (MAX) NOT NULL,
    [RoutingToken] NVARCHAR (128) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_dbo.Providers] PRIMARY KEY CLUSTERED ([RoutingToken] ASC)
);

