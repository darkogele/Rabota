using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.CURM.Implementations
{
    public class ExecutedImportOutput
    {
        public string EDB { get; set; }
        public double ImportAmount { get; set; }
        public double ImportTaxAmount { get; set; }
        public int ImportMonth { get; set; }
        public int ImportYear { get; set; }
    }
}
