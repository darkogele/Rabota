﻿using System;
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
using Helpers;
using TSAdapterAKN.Interfaces.IGetTSAKN;

namespace TSAdapterAKN.Implementations.GetTSAKNTest
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class TSAKNTest : IGetTSAKN
    {
        public string TekSostojba(string embs)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(embs))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот 'ЕМБС' е задолжителен!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (!string.IsNullOrEmpty(embs) && embs.Length != 7)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Вредноста на параметарот 'ЕМБС' е надвор од дозволениот опсег!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region LogicBeforeCallingService

                var outDoc = new XmlDocument();

                var xDoc = new XDocument(new XElement("CrmRequest",
                    new XAttribute("ProductName", "OSSLECView"),
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

                var xmlDoc = new XmlDocument();
                using (var xmlReader = xDoc.CreateReader())
                {
                    xmlDoc.Load(xmlReader);
                }

                var certUser = WebConfigurationManager.AppSettings["MyCertificateUserTest"];
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                var certificate = store.Certificates.Find(X509FindType.FindBySubjectName, certUser, false) .OfType<X509Certificate2>().First();
                string param = SignXml(xmlDoc, certificate);

                #endregion

                #region CallingInstitutionService

                var client = new CRM_TS_AKN_Test.CRM_TS_AKNClient();
                var output = client.Get_TS_AKN(param);
                outDoc.LoadXml(output);
                var signatureNode =
                    (XmlElement) outDoc.DocumentElement.SelectSingleNode("//*[local-name()='Signature']");
                signatureNode.ParentNode.RemoveChild(signatureNode);

                #endregion

                return outDoc.OuterXml;

            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (EndpointNotFoundException ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (Exception ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при повикување на сервисот на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
        }
        public static string SignXml(XmlDocument xmlDoc, X509Certificate2 cert)
        {
            var rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (rsaKey == null)
                throw new ArgumentException("Key");

            var signedXml = new SignedXml(xmlDoc);
            var XMLSignature = signedXml.Signature;
            signedXml.SigningKey = rsaKey;
            var reference = new Reference();
            reference.Uri = "";
            var env = new XmlDsigEnvelopedSignatureTransform();
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
            string logFilePath = "C:\\Logs\\";
            logFilePath = logFilePath + "LogTest-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            var logFileInfo = new FileInfo(logFilePath);
            var logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            FileStream fileStream = !logFileInfo.Exists ? logFileInfo.Create() : new FileStream(logFilePath, FileMode.Append);
            var log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }
    }
}