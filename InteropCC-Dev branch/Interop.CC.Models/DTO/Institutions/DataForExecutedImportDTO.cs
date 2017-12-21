using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class DataForExecutedImportDTO : BasicPrintInfoDTO
    {
        public List<ExecutedImportDTO> ExecutedImportList { get; set; }
        public string Message { get; set; }
        public string FileXMLNameImport { get; set; }
        public string FilePDFNameImport { get; set; }
    }
    public class ExecutedImportDTO
    {
        public string EDB { get; set; }
        public double ImportAmount { get; set; }
        public double ImportTaxAmount { get; set; }
        public int ImportMonth { get; set; }
        public int ImportYear { get; set; }
    }
}
