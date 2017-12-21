using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class DataForExecutedExportDTO : BasicPrintInfoDTO
    {
        public List<ExecutedExportDTO> ExecutedExportList { get; set; }
        public string Message { get; set; }
        public string FileXMLName { get; set; }
        public string FilePDFName { get; set; }
    }

    public class ExecutedExportDTO
    {
        public string EDB { get; set; }
        public double ExportAmount { get; set; }
        public int ExportMonth { get; set; }
        public int ExportYear { get; set; }
        
    }
}
