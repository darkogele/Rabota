using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;

namespace SystemToSystemImplementation.Helpers
{
    public class Helper
    {
        public static XmlDocument CRM_XML_Test_Request(string embg)
        {
            var xdoc = new XDocument(
                new XElement("CrmRequest",
                    new XAttribute("ProductName", "LEOSSCurrentView"),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID"))),
                    new XElement("Parameters", new XAttribute("TemplateName", "CVLECourt"),
                        new XElement("Parameter", embg, new XAttribute("Name", "@LEID")))
                    )
                );

            var xmldoc = new XmlDocument();
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

            var signedXml = new SignedXml(xmlDoc);
            var xmlSignature = signedXml.Signature;
            signedXml.SigningKey = rsaKey;
            var reference = new Reference();
            reference.Uri = "";
            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            xmlSignature.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            return xmlDoc.InnerXml;
        }
    }
}
