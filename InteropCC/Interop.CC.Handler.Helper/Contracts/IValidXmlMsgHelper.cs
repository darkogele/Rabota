using System.Xml.Schema;

namespace Interop.CC.Handler.Helper.Contracts
{
    public interface IValidXmlMsgHelper
    {
        void ValidateXml(string mimMsg);
        void ValidateXmlAgainstXsd(string xmlDoc);
        void settings_ValidationEventHandler(object sender, ValidationEventArgs e);
    }
}
