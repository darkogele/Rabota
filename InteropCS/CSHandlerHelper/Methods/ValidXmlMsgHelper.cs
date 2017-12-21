using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using CSHandlerHelper.Contracts;
using CSHandlerHelper.Exceptions;
using Interop.CS.CrossCutting;

namespace CSHandlerHelper.Methods
{
    public class ValidXmlMsgHelper : IValidXmlMsgHelper
    {
        public List<ValidationEventArgs> Exceptions = new List<ValidationEventArgs>();
        public void ValidateXml(string mimMsg)
        {
            if (mimMsg != "")
            {
                ValidateXmlAgainstXsd(PrepareForValidation(mimMsg));
                if (Exceptions.Count > 0)
                    throw new XmlValidationException(Exceptions);
            }
        }

        public void ValidateXmlAgainstXsd(string xmlDoc)
        {
            XDocument xDoc = XDocument.Parse(xmlDoc);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, AppSettings.Get<string>("MIMMessageXSDLocation"));

            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationEventHandler += settings_ValidationEventHandler;
            settings.CheckCharacters = false;

            using (XmlReader validatingReader = XmlReader.Create(new StringReader(xDoc.ToString()), settings))
            {
                while (validatingReader.Read()) { /* just loop through document */ }
            }
        }

        public void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
            {
                Exceptions.Add(e);
            }
        }
        public string PrepareForValidation(string mimMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(mimMsg);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("mioa", "http://mioa.gov.mk/interop/mim/v1");
            var signatureNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:Signature", ns);
            var headerNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:MIMHeader", ns);
            var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:MIMBody", ns);
            headerNode.RemoveAllAttributes();
            bodyNode.RemoveAllAttributes();
            signatureNode.ParentNode.RemoveChild(signatureNode);
            return doc.OuterXml;
        }
    }
}
