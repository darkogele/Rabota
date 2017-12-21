using System.Collections.Generic;
using System.ServiceModel;
using MetaService.Models;

namespace Interop.ExternalCC.MetaService
{
    [ServiceContract]
    public interface IExternalCCMetaService
    {
        [OperationContract]
        List<ProviderDTO> GetProviders(string participantCode);
        [OperationContract]
        List<string> GetServices(string providerId, string participantCode);
        [OperationContract]
        string GetService(string providerId, string serviceId, string callType, string participantCode);
    }

}
