using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHandlerHelper.Model
{
    public class MimAdditionalHeader
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string ProviderEndpointUrl { get; set; }
        public string ExternalEndpointUrl { get; set; }
        public string WebServiceUrl { get; set; }
    }
}
