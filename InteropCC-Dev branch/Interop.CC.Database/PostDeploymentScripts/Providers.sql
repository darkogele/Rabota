﻿GO

--MERGE generated by 'sp_generate_merge' stored procedure, Version 0.93
--Originally by Vyas (http://vyaskn.tripod.com): sp_generate_inserts (build 22)
--Adapted for SQL Server 2008/2012 by Daniel Nolan (http://danere.com)

SET NOCOUNT ON

MERGE INTO [Providers] AS Target
USING (VALUES
  ('MIIDgTCCAmmgAwIBAgIIQzlM4IDizgcwDQYJKoZIhvcNAQEFBQAwLzERMA8GA1UEAwwIRVBvdHBp
czIxDTALBgNVBAoMBE1JT0ExCzAJBgNVBAYTAk1LMB4XDTE1MTAyMjA3MDMxMFoXDTE2MTAyMTA3
MDMxMFowLzERMA8GA1UEAwwIS09SVkNDQTMxDTALBgNVBAoMBE1JT0ExCzAJBgNVBAYTAk1LMIGf
MA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCAfuNiQSvRZOTXupzhdmdETRjF0EbEDpdtCOVQlmHX
vHaaq9jcXuIwOH112FDoQTeIqEFfZAKn6Kgmz6WfFQ0UlNQBMakM1IIihOyLSUkEk35/2q02AVMY
A+aCDVu3b+9GFKTtkzA1dIY91nIvlz2uIx47Hne9J+8Ecsm/X4uUxwIDAQABo4IBIzCCAR8wHQYD
VR0OBBYEFFYzOjxWvHvi0yv4gW39RRj83SzpMAwGA1UdEwEB/wQCMAAwHwYDVR0jBBgwFoAUiipk
d1rHf1ddQSHUVTkNaSUcEQMwgakGA1UdHwSBoTCBnjCBm6BkoGKGYGh0dHA6Ly8xMC40Ny4yMC4y
Mzc6ODA4MC9lamJjYS9wdWJsaWN3ZWIvd2ViZGlzdC9jZXJ0ZGlzdD9jbWQ9Y3JsJmlzc3Vlcj1D
Tj1FUG90cGlzMixPPU1JT0EsQz1NS6IzpDEwLzERMA8GA1UEAwwIRVBvdHBpczIxDTALBgNVBAoM
BE1JT0ExCzAJBgNVBAYTAk1LMA4GA1UdDwEB/wQEAwIFoDATBgNVHSUEDDAKBggrBgEFBQcDATAN
BgkqhkiG9w0BAQUFAAOCAQEAKl1t0LRerN7ZsRwI1wBqkZDqyOpXZIrwI49Egkp4eFdo2haE5OQf
rE21U/3Rznx3jCpCD0LPu+5wPyHwOan0TutAVsQ46RrORj5P5OMmj7zvxFEmXSm+pefMXI4SzS7J
Fxvv60OI+V3a/pZAYmOWPS1aO709hgHejWIP6Vto6ruBwi2aWaMEmGgzTBLw+Gy0Dpgg67UJhaEh
eoT3lZL+o+rf+/NmxRCbznZE1ff64l1geDcXAd3waL8VHK2ZLPWX1vbFVfUwTbyD440U0+4g/plS
yrT9O3ZXspbOhs1NpKrUhccp8Z14F5lofDcKMqr9GTmcMkmHokTkZq4btpwtWA==
','AKN')
 ,('MIIDgTCCAmmgAwIBAgIIWpzqNxZZ+dAwDQYJKoZIhvcNAQEFBQAwLzERMA8GA1UEAwwIRVBvdHBp
czIxDTALBgNVBAoMBE1JT0ExCzAJBgNVBAYTAk1LMB4XDTE1MTAyMjA3MDYzMVoXDTE2MTAyMTA3
MDYzMVowLzERMA8GA1UEAwwIS09SVkNDQjMxDTALBgNVBAoMBE1JT0ExCzAJBgNVBAYTAk1LMIGf
MA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCyaCnPZIDzyhrCGjYmWNsHuDEgZPKiH4hXrzKA8//C
90Ainm5Xg6/oUqqxCvCk1ALQ1bWf3LCeNtHwSvirytPYTi7yA6H7APd21RjnNcuG7M3zIw76E+j2
hzqqtF2kaNJHUEsbYGN85b/vguJJHSOggA6Zg8RsuFuTfxJ444BiCwIDAQABo4IBIzCCAR8wHQYD
VR0OBBYEFLFQZF4edfZ9BRKN1i2txdtJu21iMAwGA1UdEwEB/wQCMAAwHwYDVR0jBBgwFoAUiipk
d1rHf1ddQSHUVTkNaSUcEQMwgakGA1UdHwSBoTCBnjCBm6BkoGKGYGh0dHA6Ly8xMC40Ny4yMC4y
Mzc6ODA4MC9lamJjYS9wdWJsaWN3ZWIvd2ViZGlzdC9jZXJ0ZGlzdD9jbWQ9Y3JsJmlzc3Vlcj1D
Tj1FUG90cGlzMixPPU1JT0EsQz1NS6IzpDEwLzERMA8GA1UEAwwIRVBvdHBpczIxDTALBgNVBAoM
BE1JT0ExCzAJBgNVBAYTAk1LMA4GA1UdDwEB/wQEAwIFoDATBgNVHSUEDDAKBggrBgEFBQcDATAN
BgkqhkiG9w0BAQUFAAOCAQEAb+JVnS/Hxd9B4Myy2YMvJFiveU5K5mQo1N9x1CDRQCqBhI/wcCLn
jDAgp8HTCy95q4nx8WS5JbTjpbjdcx1yuvu7zWpu9NBR/nekoYV0fWyxYvPiha1r6b2Y8blcydnr
or0C2v+Bfg02KylNq29xyW+LjYt4WabVfRq1LXuYJ1jNabrzxFF9Jzek/nP4Ki/cqp6Jhxxll5Pp
eAu+UpEEfc3Nz+LeOOqPps3huaMeGWGkbWeQAz0YuEGBT3sgYPxlmbpfLosbTAgM6lPo8s6vcTr0
942pmV0lTUW8Bq9S7dTTfrHa2uNO7eF2Dto7UjChpfOUyVOOjkra5+trUol5nQ==
','PIOM')
) AS Source ([PublicKey],[RoutingToken])
ON (Target.[RoutingToken] = Source.[RoutingToken])
WHEN MATCHED AND (
	NULLIF(Source.[PublicKey], Target.[PublicKey]) IS NOT NULL OR NULLIF(Target.[PublicKey], Source.[PublicKey]) IS NOT NULL) THEN
 UPDATE SET
  [PublicKey] = Source.[PublicKey]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([PublicKey],[RoutingToken])
 VALUES(Source.[PublicKey],Source.[RoutingToken])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Providers]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Providers] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET NOCOUNT OFF
GO