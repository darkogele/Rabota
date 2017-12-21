CREATE TABLE [dbo].[AccessMappings] (
    [ProviderCode]    NVARCHAR (128) NOT NULL,
    [ConsumerCode]    NVARCHAR (128) NOT NULL,
    [ServiceCode]     NVARCHAR (128) NOT NULL,
    [MethodCode]      NVARCHAR (128) NOT NULL,
    [ProviderBusCode] NVARCHAR (128) DEFAULT ('') NOT NULL,
    [ConsumerBusCode] NVARCHAR (128) DEFAULT ('') NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_AccessMappings_IsActive] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.AccessMappings] PRIMARY KEY CLUSTERED ([ProviderCode] ASC, [ProviderBusCode] ASC, [ConsumerCode] ASC, [ConsumerBusCode] ASC, [ServiceCode] ASC, [MethodCode] ASC)
);







