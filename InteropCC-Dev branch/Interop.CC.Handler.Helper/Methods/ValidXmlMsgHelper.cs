using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Interop.CC.CrossCutting;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Exceptions;

namespace Interop.CC.Handler.Helper.Methods
{
    public class ValidXmlMsgHelper:IValidXmlMsgHelper
    {
        // Опис: листа од грешки настанати при комуникација во системот
        public List<ValidationEventArgs> Exceptions = new List<ValidationEventArgs>();

        // Опис: Метод кој ја валидира ХМL пораката
        // Влезни параметри: податочна вредност mimMsg
        // Излезни параметри: /
        public void ValidateXml(string mimMsg)
        {
            if (mimMsg != "")
            {
               ValidateXmlAgainstXsd(PrepareForValidation(mimMsg));
                if (Exceptions.Count > 0)
                    throw new XmlValidationException(Exceptions);
            }
        }

        // Опис: Метод кој ја валидира ХМL пораката во однос на XSD шема
        // Влезни параметри: податочна вредност xmlDoc
        // Излезни параметри: /
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

        // Опис: Метод за додавање на грешки во рамки на листата грешки
        // Влезни параметри: object sender, ValidationEventArgs e
        // Излезни параметри: /
        public void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
            {
                Exceptions.Add(e);
            }
        }

        // Опис: Метод за валидирање на MIM XML пораката
        // Влезни параметри: податочна вредност mimMsg
        // Излезни параметри: XML во форма на податочен тип string
        public string PrepareForValidation(string mimMsg)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(mimMsg);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("mioa", "http://mioa.gov.mk/interop/mim/v1");
            //var signatureNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:Signature", ns);
            var headerNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:MIMHeader", ns);
            var bodyNode = (XmlElement)doc.DocumentElement.SelectSingleNode("//mioa:MIMBody", ns);
            headerNode.RemoveAllAttributes();
            bodyNode.RemoveAllAttributes();
            //signatureNode.ParentNode.RemoveChild(signatureNode);
            return doc.OuterXml;
        }
    }
}
