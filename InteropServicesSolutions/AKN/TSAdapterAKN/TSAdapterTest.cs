using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.Security.Cryptography;
using System.IO;

namespace TSAdapterAKN
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class TSAdapterTest : ITSAdapterTest
    {
        public string GetTSTest(string EMBS)
        {
            string output = "";
            XmlDocument outDoc = new XmlDocument();
            string param = "";
                var client = new Test_TS_AKN.Test_TS_AKNClient();

                try
                {
                    XDocument Xdoc = new XDocument(
                        new XElement("CrmRequest",
                       new XAttribute("ProductName", "OSSLECView"),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                           new XElement("Parameter", EMBS, new XAttribute("Name", "@LEID")))
                      )
                   );
                    System.Xml.XmlDocument Xmldoc = new System.Xml.XmlDocument();
                    using (var xmlReader = Xdoc.CreateReader())
                    {
                        Xmldoc.Load(xmlReader);
                    }
                    var certUser = WebConfigurationManager.AppSettings["MyCertificateUserTest"];
                    X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly);

                    X509Certificate2 certificate = store.Certificates
                         .Find(X509FindType.FindBySubjectName, certUser, false)
                         .OfType<X509Certificate2>()
                         .First();
                    param = SignXml(Xmldoc, certificate);
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
                catch(Exception e)
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

            SignedXml signedXml = new SignedXml(xmlDoc);
            var XMLSignature = signedXml.Signature;
            signedXml.SigningKey = rsaKey;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            XMLSignature.KeyInfo = keyInfo;
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

            string logFilePath = "C:\\Logs\\";
            logFilePath = logFilePath + "LogTest-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
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
