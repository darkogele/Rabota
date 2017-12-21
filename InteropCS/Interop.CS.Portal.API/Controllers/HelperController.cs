using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Interop.CS.Models.RepositoryContracts;
using Microsoft.Ajax.Utilities;

namespace Interop.CS.Portal.API.Controllers
{
    public class HelperController : ApiController
    {
        private readonly IParticipantRepository _participantsRepository;
        private readonly IAccessMappingRepository _accessMappingRepository;
        private readonly IServiceRepository _serviceRepository;
        public HelperController(IParticipantRepository participantsRepository, IAccessMappingRepository accessMappingRepository, IServiceRepository serviceRepository)
        {
            _participantsRepository = participantsRepository;
            _accessMappingRepository = accessMappingRepository;
            _serviceRepository = serviceRepository;
        }
        public List<KeyValuePair<string, string>> GetParticipants()
        {
            var participants = _participantsRepository.GetParticipants();
            var consumers = new Dictionary<string, string>();
            var mims = new[] { "MIM1$$", "MIM2$$" };
            foreach (var participant in participants)
            {
                bool participantContainsMim = mims.Any(m => participant.Code.Contains(m));
                if (participantContainsMim)
                {
                    participant.Code = participant.Code.Remove(0, 6);
                }
                consumers.Add(participant.Code, participant.Name);
            }
            return consumers.ToList();
        }

        public List<KeyValuePair<string, string>> GetConsumersByAccMap(string institution)
        {
            var consumers = new Dictionary<string, string>();
            var consumersByAccMapp =
                _accessMappingRepository.GetAccessMappings()
                    .Where(accessMapping => accessMapping.ConsumerCode == institution || accessMapping.ProviderCode == institution)
                    .DistinctBy(accessMapping => accessMapping.ConsumerCode).Select(accessMapping => accessMapping.ConsumerCode).ToList();

            if (consumersByAccMapp.Any())
            {
                foreach (var consumer in consumersByAccMapp)
                {
                    consumers.Add(consumer, _participantsRepository.GetParticipantName(consumer));
                }
            }
            return consumers.ToList();
        }

        public List<KeyValuePair<string, string>> GetProvidersByAccMap(string institution, string consumer)
        {
            var providers = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(consumer) && consumer != institution)
            {
                providers.Add(institution, _participantsRepository.GetParticipantName(institution));
            }
            else
            {
                var providersByAccMapp =
                _accessMappingRepository.GetAccessMappings()
                    .Where(accessMapping => accessMapping.ConsumerCode == institution || accessMapping.ProviderCode == institution)
                    .DistinctBy(accessMapping => accessMapping.ProviderCode).Select(accessMapping => accessMapping.ProviderCode).ToList();

                if (providersByAccMapp.Any())
                {
                    foreach (var provider in providersByAccMapp)
                    {
                        providers.Add(provider, _participantsRepository.GetParticipantName(provider));
                    }
                }
            }

            return providers.ToList();
        }

        [System.Web.Http.HttpGet]
        public List<KeyValuePair<string, string>> GetConsumerForProviderAccMap(string provider, string institution)
        {
            var consumers = new Dictionary<string, string>();
            if (provider == institution)
            {
                var allConsumersForInsitution =
                    _accessMappingRepository.GetAccessMappings()
                        .Where(accessMapping => accessMapping.ProviderCode == institution)
                        .DistinctBy(accessMapping => accessMapping.ConsumerCode)
                        .Select(accessMapping => accessMapping.ConsumerCode)
                        .ToList();
                if (allConsumersForInsitution.Any())
                {
                    foreach (var consumerForInsitution in allConsumersForInsitution)
                    {
                        consumers.Add(consumerForInsitution, _participantsRepository.GetParticipantName(consumerForInsitution));
                    }
                }
            }
            else
            {
                consumers.Add(institution, _participantsRepository.GetParticipantName(institution));
            }
            return consumers.ToList();
        }

        [System.Web.Http.HttpGet]
        public List<KeyValuePair<string, string>> GetConsumerByProvider(string provider)
        {
            var mims = new[] { "MIM1$$", "MIM2$$" };
            bool providerContainsMim = mims.Any(m => provider.Contains(m));
            if (providerContainsMim)
            {
                provider = provider.Remove(0, 6);
            }

            var consumersForProvider = _accessMappingRepository.GetAccessMappings().Where(x => x.ProviderCode == provider).DistinctBy(x => x.ConsumerCode).ToList();
            var consumers = new Dictionary<string, string>();
            if (consumersForProvider.Any())
            {
                foreach (var consumerForProvider in consumersForProvider)
                {
                    string consumerCodeFromAccessMappAccordingToMim = null;
                    if (consumerForProvider.ConsumerBusCode == "MIM1")
                    {
                        consumerCodeFromAccessMappAccordingToMim = "MIM1$$" + consumerForProvider.ConsumerCode;
                    }
                    if (consumerForProvider.ConsumerBusCode == "MIM2")
                    {
                        consumerCodeFromAccessMappAccordingToMim = "MIM2$$" + consumerForProvider.ConsumerCode;
                    }
                    if (!string.IsNullOrEmpty(consumerCodeFromAccessMappAccordingToMim))
                    {
                        var participantName = _participantsRepository.GetParticipantNameByCode(consumerCodeFromAccessMappAccordingToMim);
                        consumers.Add(consumerForProvider.ConsumerCode, participantName);
                    }
                }
            }
            return consumers.ToList();
        }

        [System.Web.Http.HttpGet]
        public List<KeyValuePair<string, string>> GetProvidersByConsumerAccMap(string consumer, string institution)
        {
            var providers = new Dictionary<string, string>();
            if (consumer != institution)
            {
                providers.Add(institution, _participantsRepository.GetParticipantName(institution));
            }
            else
            {
                var allProvidersForInstitution =
                    _accessMappingRepository.GetAccessMappings()
                        .Where(accessMapping => accessMapping.ConsumerCode == consumer).DistinctBy(accessMapping => accessMapping.ProviderCode).Select(accessMapping => accessMapping.ProviderCode).ToList();
                if (allProvidersForInstitution.Any())
                {
                    foreach (var providerForInstitution in allProvidersForInstitution)
                    {
                        providers.Add(providerForInstitution, _participantsRepository.GetParticipantName(providerForInstitution));
                    }
                }
            }
            return providers.ToList();
        }

        [System.Web.Http.HttpGet]
        public List<KeyValuePair<string, string>> GetProviders(string consumer)
        {
            var mims = new[] { "MIM1$$", "MIM2$$" };
            bool consumerContainsMim = mims.Any(m => consumer.Contains(m));

            if (consumerContainsMim)
            {
                consumer = consumer.Remove(0, 6);
            }

            var accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer).ToList();
            var providers = new Dictionary<string, string>();
            foreach (var accessMapping in accessMappings)
            {
                string providerCodeFromAccessMappAccordingToMim = accessMapping.ProviderBusCode + "$$" + accessMapping.ProviderCode;
                if (!string.IsNullOrEmpty(providerCodeFromAccessMappAccordingToMim))
                {
                    if (!providers.ContainsKey(accessMapping.ProviderCode))
                    {
                        var participantName = _participantsRepository.GetParticipantNameByCode(providerCodeFromAccessMappAccordingToMim);
                        providers.Add(accessMapping.ProviderCode, participantName);
                    }
                }
            }
            return providers.ToList();
        }

        [System.Web.Http.HttpGet]
        public List<string> GetServiceMethods(string service)
        {
            var serviceMethods = new List<string>();
            if (!string.IsNullOrEmpty(service))
            {
                var methodsFromMapping = _accessMappingRepository.GetAccessMappings().Where(x => x.ServiceCode == service).DistinctBy(x => x.MethodCode).ToList();
                if (methodsFromMapping.Any())
                {
                    foreach (var method in methodsFromMapping)
                    {
                        serviceMethods.Add(method.MethodCode);
                    }
                }
            }
            return serviceMethods;
        }

        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetServicesByAccMap(string institution, string provider, string consumer)
        {
            var services = new List<string>();
            var servicesTemp = new List<SelectListItem>();
            var allServices = _serviceRepository.GetServices().ToList();

            //var municipalities = ConfigurationManager.AppSettings["MunicipalitiesKey"];

            string municipalities = ConfigurationManager.AppSettings["MunicipalitiesKey"] ?? "Municipalities";

            if (!string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
            {
                var accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer && x.ProviderCode == provider && x.ServiceCode != municipalities);
                if (accessMappings.Any())
                {
                    foreach (var accessMapping in accessMappings)
                    {
                        if (!services.Contains(accessMapping.ServiceCode))
                        {
                            var lst = new SelectListItem
                            {
                                Text = allServices.Where(x => x.Code == accessMapping.ServiceCode).FirstOrDefault().Name,
                                Value = accessMapping.ServiceCode
                            };
                            servicesTemp.Add(lst);
                            //services e ostaveno da se polni zaradi contains, bidejki vo ovoj contains string sporeduvame, inaku ke treba selectlistitem
                            services.Add(accessMapping.ServiceCode);
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(consumer) && !string.IsNullOrEmpty(provider))
                {
                    var accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ProviderCode == provider && x.ServiceCode != municipalities);
                    if (accessMappings.Any())
                    {
                        foreach (var accessMapping in accessMappings)
                        {
                            if (!services.Contains(accessMapping.ServiceCode))
                            {
                                var lst = new SelectListItem
                                {
                                    Text =
                                        allServices.Where(x => x.Code == accessMapping.ServiceCode)
                                            .FirstOrDefault()
                                            .Name,
                                    Value = accessMapping.ServiceCode
                                };
                                servicesTemp.Add(lst);
                                services.Add(accessMapping.ServiceCode);
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
                {
                    var accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer && x.ServiceCode != municipalities);
                    if (accessMappings.Any())
                    {
                        foreach (var accessMapping in accessMappings)
                        {
                            if (!services.Contains(accessMapping.ServiceCode))
                            {
                                var lst = new SelectListItem
                                {
                                    Text =
                                        allServices.Where(x => x.Code == accessMapping.ServiceCode)
                                            .FirstOrDefault()
                                            .Name,
                                    Value = accessMapping.ServiceCode
                                };
                                servicesTemp.Add(lst);
                                services.Add(accessMapping.ServiceCode);
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(provider) && string.IsNullOrEmpty(consumer))
                {
                    var servicesFromAccMap = _accessMappingRepository.GetAccessMappings().Where(accessMapping => (accessMapping.ConsumerCode == institution || accessMapping.ProviderCode == institution) && accessMapping.ServiceCode != municipalities).DistinctBy(accessMapping => accessMapping.ServiceCode).Select(accessMapping => accessMapping.ServiceCode).ToList();
                    if (servicesFromAccMap.Any())
                    {
                        foreach (var service in servicesFromAccMap)
                        {
                            if (!services.Contains(service))
                            {
                                var lst = new SelectListItem
                                {
                                    Text = allServices.Where(x => x.Code == service).FirstOrDefault().Name,
                                    Value = service
                                };
                                servicesTemp.Add(lst);
                                services.Add(service);
                            }
                        }
                    }
                }
            }
            return servicesTemp;
        }

        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetServiceMethodsByAccMap(string service, string provider, string consumer, string institution)
        {
            var serviceMethods = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(service))
            {
                //    var methodsFromMapping = _accessMappingRepository.GetAccessMappings().Where(x => x.ServiceCode == service).DistinctBy(x => x.MethodCode).ToList();
                //    if (methodsFromMapping.Any())
                //    {
                //        foreach (var method in methodsFromMapping)
                //        {
                //            serviceMethods.Add(method.MethodCode);
                //        }
                //    }
                if (!string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
                {
                    //vo distinctby e dodadeno vnatre .Split('/').Last() zaradi metodi so link i bez sto se
                    //da se dodade isActive proverka???
                    var methodsFromMapping = _accessMappingRepository.GetAccessMappings().Where(x => x.ServiceCode == service && x.ConsumerCode == consumer && x.ProviderCode == provider).DistinctBy(x => x.MethodCode).ToList();
                    if (methodsFromMapping.Any())
                    {
                        foreach (var method in methodsFromMapping)
                        {
                            var lst = new SelectListItem();
                            lst.Text = method.MethodCode;
                            lst.Value = method.MethodCode;
                            serviceMethods.Add(lst);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(consumer) && !string.IsNullOrEmpty(provider))
                    {
                        var methodsFromMapping = _accessMappingRepository.GetAccessMappings().Where(x => x.ServiceCode == service && x.ProviderCode == provider).DistinctBy(x => x.MethodCode).ToList();
                        if (methodsFromMapping.Any())
                        {
                            foreach (var method in methodsFromMapping)
                            {
                                var lst = new SelectListItem();
                                lst.Text = method.MethodCode;
                                lst.Value = method.MethodCode;
                                serviceMethods.Add(lst);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
                    {
                        var methodsFromMapping = _accessMappingRepository.GetAccessMappings().Where(x => x.ServiceCode == service && x.ConsumerCode == consumer).DistinctBy(x => x.MethodCode).ToList();
                        if (methodsFromMapping.Any())
                        {
                            foreach (var method in methodsFromMapping)
                            {
                                var lst = new SelectListItem();
                                lst.Text = method.MethodCode;
                                lst.Value = method.MethodCode;
                                serviceMethods.Add(lst);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(provider) && string.IsNullOrEmpty(consumer))
                    {
                        //da se dodade isActive proverka???
                        var methodsFromMapping = _accessMappingRepository.GetAccessMappings().Where(x => x.ServiceCode == service && (x.ProviderCode == institution || x.ConsumerCode == institution)).DistinctBy(x => x.MethodCode).ToList();
                        if (methodsFromMapping.Any())
                        {
                            foreach (var method in methodsFromMapping)
                            {
                                var lst = new SelectListItem();
                                lst.Text = method.MethodCode;
                                lst.Value = method.MethodCode;
                                serviceMethods.Add(lst);
                            }
                        }
                    }
                }

            }
            return serviceMethods;
        }
        [System.Web.Http.HttpGet]
        public List<string> GetServices(string provider = "", string consumer = "")
        {
            bool consumerContainsMim = false;
            bool providerContainsMim = false;
            var services = new List<string>();
            var mims = new[] { "MIM1$$", "MIM2$$" };
            if (!string.IsNullOrEmpty(consumer))
            {
                consumerContainsMim = mims.Any(m => consumer.Contains(m));
            }
            if (!string.IsNullOrEmpty(provider))
            {
                providerContainsMim = mims.Any(m => provider.Contains(m));
            }

            if (consumerContainsMim)
            {
                consumer = consumer.Remove(0, 6);
            }
            if (providerContainsMim)
            {
                provider = provider.Remove(0, 6);
            }
            if (!string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
            {
                var accessMappings =
                    _accessMappingRepository.GetAccessMappings()
                        .Where(x => x.ConsumerCode == consumer && x.ProviderCode == provider);
                if (accessMappings.Any())
                {
                    foreach (var accessMapping in accessMappings)
                    {
                        if (!services.Contains(accessMapping.ServiceCode))
                        {
                            services.Add(accessMapping.ServiceCode);
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(consumer) && !string.IsNullOrEmpty(provider))
                {
                    var accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ProviderCode == provider);
                    if (accessMappings.Any())
                    {
                        foreach (var accessMapping in accessMappings)
                        {
                            if (!services.Contains(accessMapping.ServiceCode))
                            {
                                services.Add(accessMapping.ServiceCode);
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
                {
                    var accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer);
                    if (accessMappings.Any())
                    {
                        foreach (var accessMapping in accessMappings)
                        {
                            if (!services.Contains(accessMapping.ServiceCode))
                            {
                                services.Add(accessMapping.ServiceCode);
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(provider) && string.IsNullOrEmpty(consumer))
                {
                    var allServices = _serviceRepository.GetServices();
                    if (allServices.Any())
                    {
                        foreach (var service in allServices)
                        {
                            if (!services.Contains(service.Code))
                            {
                                services.Add(service.Code);
                            }
                        }
                    }
                }
            }
            return services;
        }
    }
}
