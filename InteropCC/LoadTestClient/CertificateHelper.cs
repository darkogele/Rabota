using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;

namespace LoadTestClient
{
    public static class CertificateHelper
    {
        public static string GetSignedCertificate(string edb)
        {
            X509Certificate2 certificate = new X509Certificate2(ConfigurationManager.AppSettings["CertificatePath"], ConfigurationManager.AppSettings["CertificatePassword"]);
            var xml = CreateCRM_XML(edb);
            return SignXml(xml, certificate);
        }

        private static XmlDocument CreateCRM_XML(string edb)
        {
            XDocument xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LEOSSCurrentView"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLECourt"),
                        new XElement("Parameter", edb, new XAttribute("Name", "@LEID")))
                    )
                );
            XmlDocument xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }

        public static string SignXml(XmlDocument xmlDoc, X509Certificate2 cert)
        {
            var rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (rsaKey == null)
                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(xmlDoc);
            var xmlSignature = signedXml.Signature;
            signedXml.SigningKey = rsaKey;
            Reference reference = new Reference {Uri = ""};
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            xmlSignature.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            var xmlDigitalSignature = signedXml.GetXml();
            if (xmlDoc.DocumentElement != null)
                xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            return xmlDoc.InnerXml;
        }
    }
}
