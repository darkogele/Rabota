using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO.Institutions
{
    public class AnnualRevenuesFZOOutputDTO : BasicPrintInfoDTO
    {
        public AnnualRevenuesFZODTO AnnualRevenuesFZO { get; set; }
        public string Message { get; set; }
        public string Xmlfzo { get; set; }
        public string Pdffzo { get; set; }
    }
    public class AnnualRevenuesFZODTO
    {
        public string EDB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Year { get; set; }
        public string FZO_Bruto { get; set; }
        public string FZO_Neto { get; set; }
        public string Zabeleska { get; set; }
    }
}
