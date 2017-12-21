using System.Xml.Serialization;

namespace SToSListaNaSubjektiProdukciskiNaTest
{
    public class ListaNaSubjektiDTO
    {
        public ListaNaSubjektiInfo Info { get; set; }
    }
    [XmlType("CrmResultItem")]
    public class ListaNaSubjektiInfo
    {
        [XmlElement("Leid")]
        public string Leid { get; set; }

        [XmlElement("UpdateType")]
        public string UpdateType { get; set; }

        [XmlElement("ListType")]
        public string ListType { get; set; }

        [XmlElement("InfoMessage")]
        public string InfoMessage { get; set; }

    }
}
