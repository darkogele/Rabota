using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AKNDesktopApplication
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

            [XmlElement("Iznos_Opz")]
            public string OpzAmount { get; set; }

            [XmlElement("Iznos_Penzija")]
            public string PensionAmount { get; set; }

            [XmlElement("Datum_Valuta")]
            public string CurrencyDate { get; set; }

            [XmlElement("Status_Penzija")]
            public string PensionStatus { get; set; }
        }
    
}
