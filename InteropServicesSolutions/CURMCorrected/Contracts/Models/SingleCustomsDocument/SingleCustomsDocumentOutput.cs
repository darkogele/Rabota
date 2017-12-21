using System.Collections.Generic;

namespace Contracts.Models.SingleCustomsDocument
{
    public class SingleCustomsDocumentOutput
    {
        public SCDGeneralData GeneralData { get; set; }
        public List<SCDItemData> ItemData { get; set; }
        public SCDExporterData ExporterData { get; set; }
        public SCDImporterData ImporterData { get; set; }
    }
}
