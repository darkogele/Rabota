using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.DataForAnnualRevenues
{
    public class AnnualRevenuesFZOOutput
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
