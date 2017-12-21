using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.ITSProduction;


namespace Implementations.Implementations.TSProduction
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class TSProduction : ITSProduction
    {
        public string GetTSProduction(string embs)
        {
            string output = "";
            string param = "";
            var outDoc = new XmlDocument();
            var client = new Contracts.CRM_TS_AKNProduction.CRM_TS_AKNClient();

            try
            {
                if (String.IsNullOrEmpty(embs))
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Адаптерот на сервисот врати грешка.",
                        ErrorDetails = "Параметарот 'ЕМБ' е задолжителен!"
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }

                var xdoc = new XDocument(
                    new XElement("CrmRequest",
                   new XAttribute("ProductName", "LECViewForAKN"),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID"))),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID"))),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID"))),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID"))),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID"))),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID"))),
                   new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                       new XElement("Parameter", embs, new XAttribute("Name", "@LEID")))
                  )
               );
                var xmldoc = new XmlDocument();
                using (var xmlReader = xdoc.CreateReader())
                {
                    xmldoc.Load(xmlReader);
                }
                var certUser = WebConfigurationManager.AppSettings["MyCertificateUserProduction"];
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2 certificate = store.Certificates
                     .Find(X509FindType.FindBySubjectName, certUser, false)
                     .OfType<X509Certificate2>()
                     .First();
                param = SignXml(xmldoc, certificate);
            }
            catch (Exception e)
            {
                throw new Exception("Neuspesno kreiranje i potpisuvanje na XML template za povik!");
            }
            try
            {
                output = client.Get_TS_AKN(param);
            }
            catch (Exception e)
            {
                WriteLog(e.Message);
            }
            try
            {
                WriteLog(output);
                outDoc.LoadXml(output);
                WriteLog(outDoc.OuterXml);
                var signatureNode = (XmlElement)outDoc.DocumentElement.SelectSingleNode("//*[local-name()='Signature']");
                signatureNode.ParentNode.RemoveChild(signatureNode);
                WriteLog(outDoc.OuterXml);
            }
            catch (Exception e)
            {
                WriteLog(e.Message);
            }
            return outDoc.OuterXml;
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
        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            var logFilePath = "C:\\Logs\\";
            logFilePath = logFilePath + "LogProduction-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
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
    }
}
