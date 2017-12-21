/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:setvar ScriptsPath "D:\Projects\Interoperability\Interop\InteropCS\dbInterop\PostDeploymentScripts"


--:R $(ScriptsPath)\__MigrationHistory.sql
:R $(ScriptsPath)\AccessMappings.sql
:R $(ScriptsPath)\AspNetUsers.sql
:R $(ScriptsPath)\AspNetUserClaims.sql
:R $(ScriptsPath)\AspNetUserLogins.sql
:R $(ScriptsPath)\AspNetRoles.sql
:R $(ScriptsPath)\AspNetUserRoles.sql
:R $(ScriptsPath)\Client.sql
:R $(ScriptsPath)\Participants.sql
:R $(ScriptsPath)\CSServices.sql
:R $(ScriptsPath)\MessageLogs.sql
:R $(ScriptsPath)\RefreshToken.sql
:R $(ScriptsPath)\SoapFault.sql
--:R $(ScriptsPath)\MessageLogStatistic.sql

