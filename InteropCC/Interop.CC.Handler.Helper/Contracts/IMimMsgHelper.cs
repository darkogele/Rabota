using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Encryption;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Handler.Helper.SOAP;
using System.Security.Cryptography;

namespace Interop.CC.Handler.Helper.Contracts
{
    public interface IMimMsgHelper
    {
        SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionID);
        SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string mimeType);
        string CreateMimSignedXmlMsg(SoapMessage soapmsg, OwnCertificate ownCert, ILogger _logger);
        EncryptedPacket EncryptSoapBody(string original, RsaWithRsaParameterKey keys);
        string DecryptSoapBody(byte[] encryptedData, byte[] sessionKey, byte[] iVector, RsaWithRsaParameterKey rsaParams);
        OwnCertificate LoadOwnCertificate(ILogger _logger);
        PublicKeyClass GetPublicKeyForProvider(string routingToken);
        RsaWithRsaParameterKey GetPublicKeyFromString(string certString);
        RsaWithRsaParameterKey GetPrivateKey(RSAParameters privateKeyRsaParams);
        XmlDocument CreateMimXmlMsg(SoapMessage mimMsg);
        XmlDocument CreateFaultMessage(SoapFaultMessage soapFault);
        SoapFaultMessage CreateSoapFault(string code, string subCode, string mTime, string text);
        string SignXml(XmlDocument xmlDoc, X509Certificate2 cert, ILogger _logger);
        void LogMimMessage(SoapMessage mimMsg, ILogger _logger);
        //SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionID);
        void LogInitialMimMessage(SoapMessage mimMsg, ILogger _logger);

    }
    
}
