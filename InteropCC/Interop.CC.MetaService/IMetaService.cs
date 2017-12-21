using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.MetaService.Models;
using Interop.CC.Models.Models;

namespace Interop.CC.MetaService
{
    [ServiceContract]
    public interface IMetaService
    {
        [OperationContract]
        void RegisterService(Service service);
        [OperationContract]
        void UnRegisterService(string serviceId);
        [OperationContract]
        List<ProviderCCDTO> GetProviders();
        [OperationContract]
        List<SelectListItem> GetServices(string providerId);
        [OperationContract]
        string GetService(string providerId, string serviceId, string callType);
        [OperationContract]
        List<string> ListConsumers(string serviceId);
        [OperationContract]
        string CheckStateByTransactionId(string transactionId);
        [OperationContract]
        string GetMessageByTransactionId(string transactionId);
        [OperationContract]
        void PostMessage(string transactionId,string message);

        [OperationContract]
        List<string> GetServiceRolesAfterGetProvider(string loggedUser, string[] providersCodes);

        [OperationContract]
        List<string> CreateOrDeleteServicesRoles(List<string> serviceRolesFromGetProviders, string loggedUser);

    }
}
