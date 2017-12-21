using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.DataForAnnualRevenues
{
    public class AnnualRevenuesKKKSOutput
    {
        public string EDB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Year { get; set; }
        public string Pospl_mmgg { get; set; }
        public string Pospl_Bruto { get; set; }
        public string Pospl_Neto { get; set; }
        public string Mes6_Broj { get; set; }
        public string Mes6_Bruto { get; set; }
        public string Mes6_Neto { get; set; }
        public string Vk_Bruto { get; set; }
        public string Vk_Neto { get; set; }
        public string Drugo_Bruto { get; set; }
        public string Drugo_Neto { get; set; }
        public string Zabeleska { get; set; }
    }
}
