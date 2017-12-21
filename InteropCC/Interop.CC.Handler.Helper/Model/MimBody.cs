
using System.Xml.Serialization;
namespace Interop.CC.Handler.Helper.Model
{
    public class MimBody
    {
        [XmlAttribute]
        public string id { get; set; }
        public string Message { get; set; }
    }
}
