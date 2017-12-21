using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.DTO
{
    public class AccessMappingCCDTO
    {
        public string ProviderCode { get; set; }
        public string ProviderName { get; set; }
        public string ProviderBusCode { get; set; }
        public string ConsumerCode { get; set; }
        public string ConsumerName { get; set; }
        public string ConsumerBusCode { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string MethodCode { get; set; }
        public bool IsActive { get; set; }
    }
}
