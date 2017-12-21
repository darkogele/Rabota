using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;

namespace Implementations.Test.Helpers
{
    public class Helper
    {
        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "C:\\Logs\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }
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
        public static XmlDocument CRM_TS_CURM_Test_Request(string embg)
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
        public static XmlDocument ListOfSubjects_Test_Request(string embg, int updateType, int listType)
        {
            var xdoc = new XDocument(
                   new XElement("CrmRequest",
                       new XAttribute("ProductName", "CU_LEIDTableUpdate"),
                       new XElement("Parameters", new XAttribute("TemplateName", "LEIDTableUpdate"),
                           new XElement("Parameter", embg, new XAttribute("Name", "@LEID")),
                            new XElement("Parameter", updateType, new XAttribute("Name", "@UpdateType")),
                            new XElement("Parameter", listType, new XAttribute("Name", "@ListType")))
                       )
                   );


            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            return xmldoc;
        }
        public static XmlDocument ListOfChangesCU_Test_Request(string date, int listype)
        {
            var xdoc = new XDocument(
                   new XElement("CrmRequest",
                       new XAttribute("ProductName", "CU_LEIDListChanges"),
                       new XElement("Parameters", new XAttribute("TemplateName", "LEIDListChangesInfo"),
                            new XElement("Parameter", listype, new XAttribute("Name", "@ListType")),
                            new XElement("Parameter", date, new XAttribute("Name", "@DateFrom"))),
                      new XElement("Parameters", new XAttribute("TemplateName", "LEIDListChanges"),
                            new XElement("Parameter", listype, new XAttribute("Name", "@ListType")),
                            new XElement("Parameter", date, new XAttribute("Name", "@DateFrom")))
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
