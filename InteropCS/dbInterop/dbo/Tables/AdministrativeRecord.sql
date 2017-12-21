CREATE TABLE [dbo].[AdministrativeRecord] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ItemNumber]              BIGINT         NOT NULL,
    [DateReceived]            DATETIME       NOT NULL,
    [DateEntered]             DATETIME       NOT NULL,
    [ApplicantNameAddress]    NVARCHAR (MAX) NULL,
    [ElectronicDBName]        NVARCHAR (MAX) NULL,
    [ElectronicDBTypeVersion] NVARCHAR (MAX) NULL,
    [DataType]                NVARCHAR (MAX) NULL,
    [LegislationData]         NVARCHAR (MAX) NULL,
    [AuthorizedPersonData]    NVARCHAR (MAX) NULL,
    [Note]                    NVARCHAR (MAX) NULL,
    [OptionalField1]          NVARCHAR (MAX) NULL,
    [OptionalField2]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AdministrativeRecord] PRIMARY KEY CLUSTERED ([Id] ASC)
);

