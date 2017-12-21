using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Interop.CC.Models.DTO.Institutions
{
    [XmlType("Osigurenik")]
    public class DataForEmployeeDTO : BasicPrintInfoDTO
    {
        [XmlAttribute("EMBG")]
        public string EMBG { get; set; }

        [XmlElement("ime")]
        public string Name { get; set; }

        [XmlElement("prezime")]
        public string Surname { get; set; }

        [XmlElement("vkupen_staz_GGMMDD")]
        public string WorkExperience { get; set; }

        public string FilePdfName { get; set; }
        public string FileXMLName { get; set; }

    }
}
