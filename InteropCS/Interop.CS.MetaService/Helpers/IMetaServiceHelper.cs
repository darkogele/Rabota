using System.Collections.Generic;
using System.Web.Mvc;
using Interop.CS.MetaService.Models;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;

namespace Interop.CS.MetaService.Helpers
{
    public interface IMetaServiceHelper
    {
        void InsertService(IServiceRepository serviceRepository, IParticipantRepository participantRepository, CSService cSservice);
        List<ProviderCSDTO> GetProviders(IAccessMappingRepository accessMappingRepository,IParticipantRepository participantRepository, IBusesRepository busesRepository, string consumerId);
        List<SelectListItem> GetServices(IAccessMappingRepository accessMappingRepository, IBusesRepository busesRepository, IServiceRepository servicesRepository, string providerId, string consumerId);
        string GetService(IAccessMappingRepository accessMappingRepository, IServiceRepository serviceRepository, IBusesRepository busesRepository, string providerId, string consumerId, string serviceId);
        List<string> ListConsumers(IAccessMappingRepository accessMappingRepository, string providerId, string serviceId);
        List<string> GetServiceRoles(IAccessMappingRepository accessMappingRepository, IServiceRepository servicesRepository, List<string> providersCodes, string consumerId);
    }
}
