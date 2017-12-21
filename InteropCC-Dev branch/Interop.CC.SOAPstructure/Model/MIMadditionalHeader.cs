using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.CC.SOAPstructure
{
    public class MIMadditionalHeader
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string ProviderEndpointUrl { get; set; }
        public string ExternalEndpointUrl { get; set; }
        public string WebServiceUrl { get; set; }
        
    }
    
}
