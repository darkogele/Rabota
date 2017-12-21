using System;

namespace Interop.CS.Models.DTO
{
    public class StatisticExcelDTO
    {
        public String Consumer { get; set; }
        public int SuccesfullCalls { get; set; }
        public int UnSuccesfullCalls { get; set; }
        public int Total { get; set; }
    }
}
