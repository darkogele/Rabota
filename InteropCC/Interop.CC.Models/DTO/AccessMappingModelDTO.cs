using System.Collections.Generic;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.DTO
{
    public class AccessMappingModelDTO
    {
        //public string[] Consumers { get; set; }
        //public string[] Services { get; set; }
        //public string[] Methods { get; set; }
        //public IEnumerable<AccessMapping> AccessMappings { get; set; } 

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
