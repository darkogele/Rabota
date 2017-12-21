using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class AnnualRevenuesMONOutputDTO : BasicPrintInfoDTO
    {
        public AnnualRevenuesMONDTO AnnualRevenuesMON { get; set; }
        public string Message { get; set; }
        public string MonXml { get; set; }
        public string MonPdf { get; set; }
    }
    public class AnnualRevenuesMONDTO
    {
        public string EDB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Year { get; set; }
        public string Licniprim_Bruto { get; set; }
        public string Licniprim_Neto { get; set; }
        public string Prihodi_Svd { get; set; }
        public string Prihodi_Zem { get; set; }
        public string Prihodi_Imot { get; set; }
        public string Prihodi_Avtor { get; set; }
        public string Prihodi_Kapital { get; set; }
        public string Prihodi_Kapdob { get; set; }
        public string Prihodi_Igri { get; set; }
        public string Prihodi_Drugo { get; set; }
        public string Zabeleska { get; set; }
    }
}
