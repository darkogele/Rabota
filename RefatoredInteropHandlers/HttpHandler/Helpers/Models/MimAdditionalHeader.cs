
namespace Helpers.Models
{
    public class MimAdditionalHeader
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string ProviderEndpointUrl { get; set; }
        public string ExternalEndpointUrl { get; set; }
        public string WebServiceUrl { get; set; }
        public string ConsumerBusCode { get; set; }
        public string RoutingTokenExternal { get; set; }
        public string TimeStampToken { get; set; }
        public bool IsInteropTestCommunicationCall { get; set; }
    }
    
}
