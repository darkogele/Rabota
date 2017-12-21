﻿GO

--MERGE generated by 'sp_generate_merge' stored procedure, Version 0.93
--Originally by Vyas (http://vyaskn.tripod.com): sp_generate_inserts (build 22)
--Adapted for SQL Server 2008/2012 by Daniel Nolan (http://danere.com)

SET NOCOUNT ON

MERGE INTO [CSServices] AS Target
USING (VALUES
  ('ServiceA','BEN','ServiceA','/')
 ,('ServiceA','GEM','ServiceA','/')
 ,('ServiceA','KIT','ServiceA','/')
 ,('ServiceA','KNG','ServiceA','/')
 ,('ServiceA','KOR','ServiceA','/')
 ,('ServiceA','MAR','ServiceA','/')
 ,('ServiceA','MVR','ServiceA','/')
 ,('ServiceA','OKP','ServiceA','/')
 ,('ServiceA','TAR','ServiceA','/')
 ,('ServiceA','UJP','ServiceA','/')
 ,('ServiceA','ZAG','ServiceA','/')
 ,('ServiceA','ZDR','ServiceA','/')
 ,('ServiceA','ZEM','ServiceA','/')
 ,('ServiceB','BEN','ServiceB','/')
 ,('ServiceB','GEM','ServiceB','/')
 ,('ServiceB','KIT','ServiceB','/')
 ,('ServiceB','KNG','ServiceB','/')
 ,('ServiceB','KOR','ServiceB','/')
 ,('ServiceB','MAR','ServiceB','/')
 ,('ServiceB','MVR','ServiceB','/')
 ,('ServiceB','OKP','ServiceB','/')
 ,('ServiceB','TAR','ServiceB','/')
 ,('ServiceB','UJP','ServiceB','/')
 ,('ServiceB','ZAG','ServiceB','/')
 ,('ServiceB','ZDR','ServiceB','/')
 ,('ServiceB','ZEM','ServiceB','/')
) AS Source ([Code],[ParticipantCode],[Name],[Wsdl])
ON (Target.[Code] = Source.[Code] AND Target.[ParticipantCode] = Source.[ParticipantCode])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Wsdl], Target.[Wsdl]) IS NOT NULL OR NULLIF(Target.[Wsdl], Source.[Wsdl]) IS NOT NULL) THEN
 UPDATE SET
  [Name] = Source.[Name], 
  [Wsdl] = Source.[Wsdl]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Code],[ParticipantCode],[Name],[Wsdl])
 VALUES(Source.[Code],Source.[ParticipantCode],Source.[Name],Source.[Wsdl])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [CSServices]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[CSServices] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET NOCOUNT OFF
GO