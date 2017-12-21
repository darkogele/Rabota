using System.Xml.Schema;

namespace Helpers.Contracts
{
    public interface IValidXmlMsgHelper
    {
        void ValidateXml(string mimMsg);
        void ValidateXmlAgainstXsd(string xmlDoc);
        void settings_ValidationEventHandler(object sender, ValidationEventArgs e);
    }
}
