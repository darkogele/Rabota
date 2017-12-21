CREATE TABLE [dbo].[MessageLogStatistic] (
    [Id]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [Consumer]         NVARCHAR (100)   NULL,
    [Provider]         NVARCHAR (100)   NULL,
    [RoutingToken]     NVARCHAR (100)   NULL,
    [Service]          NVARCHAR (100)   NULL,
    [ServiceMethod]    NVARCHAR (100)   NULL,
    [TransactionId]    UNIQUEIDENTIFIER NOT NULL,
    [Dir]              NVARCHAR (100)   NOT NULL,
    [CallType]         NVARCHAR (100)   NULL,
    [PublicKey]        NVARCHAR (MAX)   NULL,
    [Status]           NVARCHAR (50)    NULL,
    [MimeType]         NVARCHAR (50)    NULL,
    [Timestamp]        DATETIME         NOT NULL,
    [CreateDate]       DATETIME         NOT NULL,
    [Signature]        NVARCHAR (MAX)   NULL,
    [CorrelationId]    NVARCHAR (100)   NULL,
    [ParticipantUri]   NVARCHAR (150)   NULL,
    [TokenTimestamp]   NVARCHAR (MAX)   NULL,
    [IsCorrect]        BIT              NULL,
    [ParticipantCode]  NVARCHAR (100)   NOT NULL,
    [FaultCode]        NVARCHAR (50)    NULL,
    [FaultSubCode]     NVARCHAR (50)    NULL,
    [FaultReason]      NVARCHAR (800)   NULL,
    [FaultDetails]     NVARCHAR (200)   NULL,
    [FaultDateOccured] DATETIME         NULL,
    [FaultDateCreated] DATETIME         NULL,
    [ConsumerName]     NVARCHAR (100)   NULL,
    [RoutingTokenName] NVARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [TransactionId] ASC, [Dir] ASC, [ParticipantCode] ASC)
);













