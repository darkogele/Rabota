using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.ExternalCC.HandlersHelper.Model
{
    public class MimAdditionalHeader
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string ProviderEndpointUrl { get; set; }
        public string ExternalEndpointUrl { get; set; }
        public string WebServiceUrl { get; set; }
        public string ConsumerBusCode { get; set; }
    }
}
