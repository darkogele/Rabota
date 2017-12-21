using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class DataForConstructionPermitDTO : BasicPrintInfoDTO
    {
        public string ArchiveDate { get; set; }
        public string ConstructionAddress { get; set; }
        public string ConstructionDescription { get; set; }
        public string ConstructionTypeName { get; set; }
        public List<DocumentsViewModel> Documents { get; set; }
        public string EffectDate { get; set; }
        public List<string> Investors { get; set; }
        public List<MunicipalitiesViewModel> Municipalities { get; set; }
        public string SendDate { get; set; }
        public string Status { get; set; }
        public string ConstructionPermitPDF { get; set; }
        public string ConstructionPermitXML { get; set; }
    }

    public class DocumentsViewModel
    {
        public byte[] ContentBytes { get; set; }
        public string FileName { get; set; }
    }
    public class MunicipalitiesViewModel 
    {
        public List<CadastreMunicipalitiesViewModel> CadastreMunicipalities { get; set; }
        public string MunicipalityName { get; set; }

    }
    public class CadastreMunicipalitiesViewModel
    {
        public string Ko { get; set; }
        public string Kp { get; set; }
    }
}
