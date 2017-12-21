using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.FPIOM.Implementations
{
    public class YWEInsuranceData
    {
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyName { get; set; }
        public string EDB { get; set; }
        public string EMB { get; set; }
        public string StartData { get; set; }
        public string EndDate { get; set; }
        public int WeeklyHours { get; set; }
    }
}
