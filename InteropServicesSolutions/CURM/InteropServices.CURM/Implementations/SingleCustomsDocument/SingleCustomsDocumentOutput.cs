using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.CURM.Implementations
{
    public class SingleCustomsDocumentOutput
    {
        public SCDGeneralData GeneralData { get; set; }
        public List<SCDItemData> ItemData { get; set; }
        public SCDExporterData ExporterData { get; set; }
        public SCDImporterData ImporterData { get; set; }


    }
}
