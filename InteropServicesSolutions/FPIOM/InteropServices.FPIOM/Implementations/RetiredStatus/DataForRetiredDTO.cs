using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InteropServices.FPIOM.Implementations
{
    [XmlType("Penzioner")]
    public class DataForRetiredDTO
    {
        
        
            [XmlAttribute("EMBG")]
            public string EMBG { get; set; }

            [XmlElement("Penziski_Broj")]
            public string PensionNumber { get; set; }

            [XmlElement("Ime_Prez")]
            public string NameSurname { get; set; }

            [XmlElement("Status_Penzija")]
            public string PensionStatus { get; set; }

            [XmlElement("Status_Penzija_opis")]
            public string PensionStatusDesc { get; set; }
        
    }
}
