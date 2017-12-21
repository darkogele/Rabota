CREATE TABLE [dbo].[AdministrativeService] (
    [Id]                                   INT            IDENTITY (1, 1) NOT NULL,
    [ItemNumber]                           BIGINT         NOT NULL,
    [DateReceived]                         DATETIME       NOT NULL,
    [DateEntered]                          DATETIME       NOT NULL,
    [ApplicantNameAddress]                 NVARCHAR (MAX) NULL,
    [AdministrativeServiceName]            NVARCHAR (MAX) NULL,
    [AdministrativeServiceDesc]            NVARCHAR (MAX) NULL,
    [LegislationForAdminService]           NVARCHAR (MAX) NULL,
    [LegislationForAdminServicePayment]    NVARCHAR (MAX) NULL,
    [AdminServicePaymentAmount]            NVARCHAR (MAX) NULL,
    [AdminServiceSubmissionLeagalDeadline] NVARCHAR (MAX) NULL,
    [DeliveringSpecialFormData]            NVARCHAR (MAX) NULL,
    [PreviousSubmittedDocDependencyData]   NVARCHAR (MAX) NULL,
    [ElectronicDBTypeVersion]              NVARCHAR (MAX) NULL,
    [AuthorizedPersonData]                 NVARCHAR (MAX) NULL,
    [Note]                                 NVARCHAR (MAX) NULL,
    [OptionalField1]                       NVARCHAR (MAX) NULL,
    [OptionalField2]                       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AdministrativeService] PRIMARY KEY CLUSTERED ([Id] ASC)
);

