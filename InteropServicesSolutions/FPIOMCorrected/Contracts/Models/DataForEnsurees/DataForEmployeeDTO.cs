using System.Xml.Serialization;

namespace Contracts.Models.DataForEnsurees
{
    [XmlType("Osigurenik")]
    public class DataForEmployeeDTO
    {
        [XmlAttribute("EMBG")]
        public string EMBG { get; set; }

        [XmlElement("ime")]
        public string Name { get; set; }

        [XmlElement("prezime")]
        public string Surname { get; set; }

        [XmlElement("vkupen_staz_GGMMDD")]
        public string WorkExperience { get; set; }
    }
}
