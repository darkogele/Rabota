CREATE TABLE [dbo].[SoapFaults] (
    [TransactionId] UNIQUEIDENTIFIER NOT NULL,
    [Code]          NVARCHAR (MAX)   NOT NULL,
    [SubCode]       NVARCHAR (MAX)   NOT NULL,
    [Reason]        NVARCHAR (MAX)   NOT NULL,
    [Details]       NVARCHAR (MAX)   NOT NULL,
    [DateOccured]   DATETIME         NOT NULL,
    [DateCreated]   DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.SoapFaults] PRIMARY KEY CLUSTERED ([TransactionId] ASC)
);

