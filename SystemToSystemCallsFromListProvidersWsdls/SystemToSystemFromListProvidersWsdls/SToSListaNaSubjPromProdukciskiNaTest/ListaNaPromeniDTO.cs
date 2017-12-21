using System.Xml.Serialization;

namespace SToSListaNaSubjPromProdukciskiNaTest
{
    public class ListaNaPromeniDTO
    {
        public ListaNaPromenInfo Info { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class ListaNaPromenInfo
    {
        [XmlElement("InfoMessage")]
        public string InfoMessage { get; set; }

        [XmlElement("ListType")]
        public string ListType { get; set; }

        [XmlElement("DateFrom")]
        public string DateFrom { get; set; }
       
    }
}
