using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
   public class CadastralParcelDTO : BasicPrintInfoDTO
    {
       public List<ParcelAttributes> AttributesList { get; set; }
       public string Message { get; set; }
       public string CadastralParcelPDF { get; set; }
       public string CadastralParcelXML { get; set; }
    }

    public class ParcelAttributes
    {
        public string Municipality { get; set; }
        public string CadastralMunicipality { get; set; }
        public string PropertyList { get; set; }
        public string PartNumber { get; set; }
        public int Object { get; set; }
        public string Location { get; set; }
        public string Culture { get; set; }
        public long Area { get; set; }
        public string Pravo { get; set; }
    }
}
