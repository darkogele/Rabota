using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class EDB_EMB_OutputDTO : BasicPrintInfoDTO
    {
        public List<Edb_EmbDTO> Edb_EmbList { get; set; }
        public string Message { get; set; }
        public string EdbXml { get; set; }
        public string EdbPdf { get; set; }
    }
    public class Edb_EmbDTO
    {
        public string Edb { get; set; }
        public string Naziv { get; set; }
        public string Emb { get; set; }
        public string Ziro { get; set; }
        public string BankaZiro { get; set; }
        public string DatumPrijava { get; set; }
        public string PrijavaVid { get; set; }
        public string PrijavaStatus { get; set; }
        public string DejnostNace { get; set; }
        public string SedisteNaziv { get; set; }
        public string SedisteBroj { get; set; }
        public string SedisteUlica { get; set; }
        public string SedisteTelefon { get; set; }
        public string SedisteTelefax { get; set; }
    }
}
