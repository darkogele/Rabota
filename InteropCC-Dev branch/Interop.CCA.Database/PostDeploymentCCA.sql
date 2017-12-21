:setvar ScriptsPath "D:\Projects\Interoperability\Interop\InteropCC\Interop.CCA.Database\PostDeploymentScripts"

:R $(ScriptsPath)\AspNetUsers.sql
:R $(ScriptsPath)\AspNetRoles.sql
:R $(ScriptsPath)\AspNetUserRoles.sql
:R $(ScriptsPath)\Clients.sql
:R $(ScriptsPath)\Providers.sql
