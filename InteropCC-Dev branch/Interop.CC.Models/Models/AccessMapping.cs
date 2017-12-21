using System;

namespace Interop.CC.Models.Models
{
    [Serializable]
    public class AccessMapping
    {
        public string ProviderCode { get; set; }
        public string ProviderBusCode { get; set; }
        public string ConsumerCode { get; set; }
        public string ConsumerBusCode { get; set; }
        public string ServiceCode { get; set; }
        public string MethodCode { get; set; }
        public bool IsActive { get; set; }
    }
}
