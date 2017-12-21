using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CSHandlerHelper.Contracts
{
    public interface IValidXmlMsgHelper
    {
        void ValidateXml(string mimMsg);
        void ValidateXmlAgainstXsd(string xmlDoc);
        void settings_ValidationEventHandler(object sender, ValidationEventArgs e);
    }
}
