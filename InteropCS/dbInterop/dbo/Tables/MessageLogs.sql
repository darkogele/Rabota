CREATE TABLE [dbo].[MessageLogs] (
    [Id]                             BIGINT           IDENTITY (1, 1) NOT NULL,
    [Consumer]                       NVARCHAR (MAX)   NOT NULL,
    [Provider]                       NVARCHAR (MAX)   NULL,
    [RoutingToken]                   NVARCHAR (MAX)   NOT NULL,
    [Service]                        NVARCHAR (MAX)   NOT NULL,
    [ServiceMethod]                  NVARCHAR (MAX)   NOT NULL,
    [TransactionId]                  UNIQUEIDENTIFIER NOT NULL,
    [Dir]                            NVARCHAR (50)    NOT NULL,
    [CallType]                       NVARCHAR (50)    NOT NULL,
    [PublicKey]                      NVARCHAR (MAX)   NULL,
    [Status]                         NVARCHAR (50)    NULL,
    [MimeType]                       NVARCHAR (100)   NULL,
    [Timestamp]                      DATETIME         NOT NULL,
    [CreateDate]                     DATETIME         NOT NULL,
    [Signature]                      NVARCHAR (MAX)   NOT NULL,
    [CorrelationId]                  NVARCHAR (MAX)   NULL,
    [TokenTimestamp]                 NVARCHAR (MAX)   NULL,
    [IsCorrect]                      BIT              NULL,
    [IsInteropTestCommunicationCall] BIT              CONSTRAINT [DF_MessageLogs_IsInteropTestCommunicationCall] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.MessageLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);





