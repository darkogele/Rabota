using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Exceptions;
using Helpers.Contracts;

namespace Helpers.Implementations
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
        public string PrepareForValidation(string mimMsg)
        {
            var doc = new XmlDocument();
            doc.LoadXml(mimMsg);
            var ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("mioa", "http://mioa.gov.mk/interop/mim/v1");
            var headerNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:MIMHeader", ns);
            var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:MIMBody", ns);
            headerNode.RemoveAllAttributes();
            bodyNode.RemoveAllAttributes();
            return doc.OuterXml;
        }

        public void ValidateXmlAgainstXsd(string xmlDoc)
        {
            XDocument xDoc = XDocument.Parse(xmlDoc);
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(null, ConfigurationManager.AppSettings["MIMMessageXSDLocation"]);

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
    }
}
