using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.MetaService.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.MetaService.Models;

namespace Interop.CS.MetaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CSMetaService : ICSMetaService
    {
        private readonly IServiceRepository _servicesRepository;
        private readonly IAccessMappingRepository _accessMappingRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly ICSRepoFactory _csRepoFactory;
        private readonly IBusesRepository _busesRepository;
        private readonly ILogger _logger;

        public CSMetaService(IServiceRepository servicesRepository,IAccessMappingRepository accessMappingRepository,
            IParticipantRepository participantRepository, ICSRepoFactory csRepoFactory, IBusesRepository busesRepository, ILogger logger)
        {
            _servicesRepository = servicesRepository;
            _accessMappingRepository = accessMappingRepository;
            _participantRepository = participantRepository;
            _csRepoFactory = csRepoFactory;
            _busesRepository = busesRepository;
            _logger = logger;
        }

        public void RegisterService(CSService service)
        {
            var csRepoFactory = _csRepoFactory.GetMetaServiceHelper();
            csRepoFactory.InsertService(_servicesRepository, _participantRepository, service);
        }

        public void UnRegisterService(string serviceId)
        {
            if (!String.IsNullOrEmpty(serviceId))
            {
                
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public List<ProviderCSDTO> GetProviders(string consumerId)
        {
            var csRepoFactory = _csRepoFactory.GetMetaServiceHelper();
            return csRepoFactory.GetProviders(_accessMappingRepository, _participantRepository, _busesRepository, consumerId);
        }

        public List<SelectListItem> GetServices(string providerId, string consumerId)
        {
            var csRepoFactory = _csRepoFactory.GetMetaServiceHelper();
            var services = csRepoFactory.GetServices(_accessMappingRepository, _busesRepository, _servicesRepository, providerId, consumerId);
            _logger.Info("Servisite se: " + services.Count);
            return services;
        }
        
        public string GetService(string providerId, string consumerId, string serviceId, string callType)
        {
            var csRepoFactory = _csRepoFactory.GetMetaServiceHelper();
            return csRepoFactory.GetService(_accessMappingRepository, _servicesRepository, _busesRepository, providerId, consumerId, serviceId);
        }

        public List<string> ListConsumers(string providerId, string serviceId)
        {
            var csRepoFactory = _csRepoFactory.GetMetaServiceHelper();
            return csRepoFactory.ListConsumers(_accessMappingRepository, providerId, serviceId);
        }

        public List<string> GetServiceRoles(string consumerId, List<string> providersCodes)
        {
            var csRepoFactory = _csRepoFactory.GetMetaServiceHelper();
            return csRepoFactory.GetServiceRoles(_accessMappingRepository, _servicesRepository, providersCodes, consumerId);
        }
    }
}
