using System.Security.Cryptography;
using System.Xml;
using Helpers.Models;

namespace Helpers.Contracts
{
    public interface IMimMsgHelper
    {
        SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionId);
        OwnCertificate LoadOwnCertificate();
        RsaWithRsaParameterKey GetPrivateKey(RSAParameters privateKeyRsaParams);
        PublicKeyClass GetPublicKeyForProvider(string routingToken);
        EncryptedPacket EncryptSoapBody(string original, RsaWithRsaParameterKey keys);
        XmlDocument CreateMimXmlMsg(SoapMessage mimMsg);
        string CreateMimSignedXmlMsg(XmlDocument soapmsg, OwnCertificate ownCert);
        string DecryptSoapBody(byte[] encryptedData, byte[] sessionKey, byte[] iVector, RsaWithRsaParameterKey rsaParams);
        SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string mimeType);
        RsaWithRsaParameterKey GetPublicKeyFromString(string certString);
        void LogInitialMimMessage(SoapMessage mimMsg);
    }
}
