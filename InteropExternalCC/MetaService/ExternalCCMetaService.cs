using System.Collections.Generic;
using System.Linq;
using Interop.ExternalCC.CrossCutting.Logging;
using Interop.ExternalCC.MetaService.CSMetaServiceReference;
using MetaService;
using MetaService.Models;
using System.ServiceModel;

namespace Interop.ExternalCC.MetaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ExternalCCMetaService: IExternalCCMetaService
    {
        private readonly CSMetaServiceClient _csMetaServiceClient;
        private readonly ILogger _logger;

        public ExternalCCMetaService(ILogger logger)
        {
            _csMetaServiceClient = new CSMetaServiceClient();
            _logger = logger;
        }

        public List<ProviderDTO> GetProviders(string participantCode)
        {
            var providers = _csMetaServiceClient.GetProviders(participantCode).ToList();
            return providers.Select(providerCsDto => new ProviderDTO { Code = providerCsDto.Code, PublicKey = providerCsDto.PublicKey }).ToList();
        }

        public List<string> GetServices(string providerId, string participantCode)
        {
            return _csMetaServiceClient.GetServices(providerId, participantCode).ToList();
        }

        public string GetService(string providerId, string serviceId, string callType, string participantCode)
        {
            return "";
        }
        
    }
}
