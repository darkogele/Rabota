using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Microsoft.Ajax.Utilities;

namespace Interop.CS.Portal.API.Controllers
{
    [System.Web.Http.Authorize]
    public class AccessMappingController : ApiController
    {
        private readonly IAccessMappingRepository _accessMappingRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger _logger;

        public AccessMappingController()
        {
                
        }
        public AccessMappingController(IAccessMappingRepository accessMappingRepository, IParticipantRepository participantRepository, IServiceRepository serviceRepository, ILogger logger)
        {
            _accessMappingRepository = accessMappingRepository;
            _participantRepository = participantRepository;
            _serviceRepository = serviceRepository;
            _logger = logger;
        }

        //Опис: Методот прави повик до методот GetAccessMappings од AccessMappingRepository
        [System.Web.Http.HttpGet]
        public IEnumerable<AccessMapping> GetAccessMappingList()
        {
            return _accessMappingRepository.GetAccessMappings();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        public IQueryable<AccessMappingCCDTO> GetOwnAccessMappings(string providerCode)
        {
            var ownAccessMappings = _accessMappingRepository.GetOwnAccessMappings(providerCode);
            return ownAccessMappings;
        }
        //TEST
        public List<SelectListItem> GetAccessMappingServices()
        {
            var accessMappings = _accessMappingRepository.GetAccessMappings().DistinctBy(accessMapping => accessMapping.ServiceCode);
            var allServices = _serviceRepository.GetServices().ToList();
            var servicesList = new List<SelectListItem>();
            foreach (var accMap in accessMappings)
            {
                var lst = new SelectListItem();
                lst.Text = allServices.Where(x => x.Code == accMap.ServiceCode).FirstOrDefault().Name;
                lst.Value = accMap.ServiceCode;
                servicesList.Add(lst);
            }
            return servicesList;
        }
        //TEST
        //Опис: Методот прави повик до методот GetAccessMapping од AccessMappingRepository
        //Влезни параметри: Код за провајдер за запис во пристапната листа, код за корисник за запис во пристапната листа, код за сервис, код за метод
        [System.Web.Http.HttpGet]
        public AccessMapping GetAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethod)
        {
            try
            {
                return _accessMappingRepository.GetAccessMapping(accessMappingProviderCode, accessMappingConsumerCode, accessMappingServiceCode, accessMappingMethod);
            }
            catch (NotFoundAccessMappingException ex)
            {
                throw new HttpException(ex.Message);
            }
            
        }

        //Опис: Методот прави повик до методот CreateAccessMapping од AccessMappingRepository
        //Влезни параметри: објект од класата AccessMapping
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPost]
        //[System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        public CreateAccessMappingResult CreateAccessMapping(AccessMapping accessMapping)
        {
            try
            {
                return _accessMappingRepository.CreateAccessMapping(accessMapping);
            }
            catch (DuplicateAccessMappingException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        
       //Опис: Методот прави повик до методот ActivateAccessMapping од AccessMappingRepository
        //Влезни параметри: Код за провајдер за запис во пристапната листа, код за корисник за запис во пристапната листа, код за сервис, код за метод, бас код на корисник, активност на записот од пристапната листа
       [System.Web.Http.HttpPut]
       [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
      
        public void ActivateAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethodCode, string accessMappingProviderBusCode, string accessMappingConsumerBusCode, bool accessMappingIsActive)
        {
            try
            {
                _accessMappingRepository.ActivateAccessMapping(accessMappingProviderCode, accessMappingConsumerCode, accessMappingServiceCode, accessMappingMethodCode, accessMappingProviderBusCode, accessMappingConsumerBusCode, accessMappingIsActive);
            }
            catch (NotFoundAccessMappingException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот се повикува од страна на Комуникациски клиент по префрлувањето на целата логика секоја пристапна листа да се креира на страна на Комуникациски клиент
        //Во тој момент и активирањето и деактивирањето на пристапните листи оди на страната на Комуникацискиот клиент
        //Влезни параметри: Код за провајдер за запис во пристапната листа, код за корисник за запис во пристапната листа, код за сервис, код за метод, бас код на корисник, активност на записот од пристапната листа
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpPut]
        public CreateAccessMappingResult ChangeActivityAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethodCode, string accessMappingProviderBusCode, string accessMappingConsumerBusCode, bool accessMappingIsActive)
        {
            try
            {
                return _accessMappingRepository.ChangeActivityAccessMapping(accessMappingProviderCode, accessMappingConsumerCode, accessMappingServiceCode, accessMappingMethodCode, accessMappingProviderBusCode, accessMappingConsumerBusCode, accessMappingIsActive);
            }
            catch (NotFoundAccessMappingException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот прави повик до методот GetAccessMappingsPaged од AccessMappingRepository
        //Влезни параметри: индекс за страна, број на записи по страна, код за провајдер, код за корисник, код за сервис, и параметар за активност
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public PagedCollection<AccessMapping> GetAccessMappingListPaged(int pageIndex, int itemsPerPage, string searchProviderCode, string searchConsumerCode, string searchServiceCode, bool? isActive)
        {
            var accessMappingPaged = _accessMappingRepository.GetAccessMappingsPaged(pageIndex, itemsPerPage, searchProviderCode, searchConsumerCode, searchServiceCode, isActive);
            return accessMappingPaged;
        }
        public Dictionary<string, string> GetProviders()
        {
            var temp = new Dictionary<string,string>();
            var providers = _accessMappingRepository.GetProviders();
            foreach (var prov in providers)
            {
                var bus = prov.Value;
                var providerName = _participantRepository.GetParticipantNameByCode(bus+"$$"+prov.Key.ToUpper());
                if (!string.IsNullOrEmpty(providerName))
                {
                    temp.Add(prov.Key, providerName);
                }
            }

            return temp;
        }
        public Dictionary<string, string> GetConsumers()
        {
            var temp = new Dictionary<string, string>();
            var consumers = _accessMappingRepository.GetConsumers();
            foreach (var con in consumers)
            {
                var bus = con.Value;
                var consumerName = _participantRepository.GetParticipantNameByCode(bus+"$$"+con.Key.ToUpper());
                if (!string.IsNullOrEmpty(consumerName))
                {
                    temp.Add(con.Key, consumerName);
                }
            }

            return temp;
        }
    }
}
