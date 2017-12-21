using System.Xml.Serialization;

namespace Helpers.Models
{
    public class MimBody
    {
        [XmlAttribute]
        public string id { get; set; }
        public string Message { get; set; }
    }
}
