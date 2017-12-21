CREATE TABLE [dbo].[SoapFault] (
    [TransactionId] UNIQUEIDENTIFIER NOT NULL,
    [Code]          NVARCHAR (MAX)   NULL,
    [SubCode]       NVARCHAR (MAX)   NULL,
    [Reason]        NVARCHAR (MAX)   NULL,
    [Details]       NVARCHAR (MAX)   NULL,
    [DateOccured]   DATETIME         NOT NULL,
    [DateCreated]   DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.SoapFault] PRIMARY KEY CLUSTERED ([TransactionId] ASC)
);

