using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CSHandlerHelper.Model;
using CSHandlerHelper.SOAP;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Encryption;

namespace CSHandlerHelper.Contracts
{
    public interface IMimMsgHelper
    {
        SoapMessage CreateMimRequestMsg(UrlSegment urlSegments, string transactionID, string soapAction, string soapBody, string sessionKey, string iVector, string soapMethodName, OwnCertificate ownCert);
        SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string soapBody, string mimeType, string sessionKey, string iVector, OwnCertificate ownCert);
        string CreateMimSignedXmlMsg(SoapMessage mimMsg, OwnCertificate ownCert, ILogger _logger);
        EncryptedPacket EncryptSoapBody(string original, RsaWithRsaParameterKey keys);
        string DecryptSoapBody(byte[] encryptedData, byte[] sessionKey, byte[] iVector, RsaWithRsaParameterKey rsaParams);
        OwnCertificate LoadOwnCertificate(ILogger _logger);
        RsaWithRsaParameterKey GetKeys(string routingToken, OwnCertificate cert);
        XmlDocument CreateMimXmlMsg(SoapMessage mimMsg);
        XmlDocument CreateFaultMessage(SoapFaultMessage soapFault);
        SoapFaultMessage CreateSoapFault(string code, string subCode, string mTime, string text);
        string SignXml(XmlDocument xmlDoc, X509Certificate2 cert, ILogger _logger);
        void LogMimMessage(SoapMessage mimMsg, ILogger _logger);
    }
    public class OwnCertificate
    {
        public X509Certificate2 Certificate { get; set; }
        public string PublicKey { get; set; }
        public RSAParameters PrivateKey { get; set; }
    }
}
