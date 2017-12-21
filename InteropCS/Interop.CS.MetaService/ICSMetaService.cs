using System.Web.Mvc;
using Interop.CS.MetaService.Models;
using Interop.CS.Models.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Interop.CS.MetaService
{
    [ServiceContract]
    public interface ICSMetaService
    {
        [OperationContract]
        void RegisterService(CSService service);

        [OperationContract]
        void UnRegisterService(string serviceId);

        [OperationContract]
        List<ProviderCSDTO> GetProviders(string consumerId);

        [OperationContract]
        List<SelectListItem> GetServices(string providerId, string consumerId);

        [OperationContract]
        string GetService(string providerId,string consumerId, string serviceId, string callType);

        [OperationContract]
        List<string> ListConsumers(string providerId, string serviceId );

        [OperationContract]
        List<string> GetServiceRoles(string consumerId, List<string> providersCodes);
    }
}
