using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Interop.CC.CrossCutting;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Portal.API.Helpers;

namespace Interop.CC.Portal.API.Controllers
{
    [System.Web.Http.Authorize]
    public class AccessMappingController : ApiController
    {
        private readonly IServiceRepository _serviceRepository;

        public AccessMappingController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetOwnServices()
        {
            var ownServices = _serviceRepository.GetServices();
            var lstOwnServices = ownServices.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Code
            }).ToList();

            return lstOwnServices;
        }

        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.HttpGet]
        public List<string> GetServiceMethods(string service)
        {
            var wsdl = _serviceRepository.GetWSDL(service);
            List<string> methods = WsdlParsingHelper.WsdlGetMethodNames(wsdl);
            return methods.Distinct().ToList();
        }

        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetOtherParticipants(string institution)
        {
            var consumers = new List<SelectListItem>();
            var participantsFromCs1 = RequestToCsHelper.MakeRequestToCs<Dictionary<string, string>>(AppSettings.Get<string>("ApiCSUrl") + "Participant/GetParticipants", "GET");
            foreach (var participant in participantsFromCs1)
            {
                if (participant.Key.ToLower().Contains("mim"))
                {
                    consumers.Add(new SelectListItem
                    {
                        Value = participant.Key.Remove(0, 6),
                        Text = participant.Value
                    });
                }
            }
            if (consumers.Any(x => x.Value.Contains(institution)))
            {
                consumers = consumers.Where(x => x.Value != institution).ToList();
            }
            return consumers;
        }

        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.HttpPost]
        public CreateAccessMappingResult CreateAccessMapping(AccessMapping accessMapping)
        {
            accessMapping.ConsumerBusCode = "MIM1";
            accessMapping.ProviderBusCode = "MIM1";
            try
            {
                var createMapping = RequestToCsHelper.MakeRequestToCsCreateAccessMapping(AppSettings.Get<string>("ApiCSUrl") + "AccessMapping/CreateAccessMapping", accessMapping);
                return createMapping;
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message);
            }
        }

       
        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.HttpGet]
        public PagedCollection<AccessMappingModelDTO> GetAccessMappingsPaged(int pageIndex, int itemsPerPage, string providerCode)// AccessMapping[] accessMappings //string searchConsumerCode, string searchServiceCode,bool? isActive
        {
            try
            {
                var ownAccessMappings = RequestToCsHelper.MakeRequestToCs<IEnumerable<AccessMappingModelDTO>>(AppSettings.Get<string>("ApiCSUrl") + "AccessMapping/GetOwnAccessMappings?providerCode=" + providerCode, "GET");
                var accessMappings = ownAccessMappings as AccessMappingModelDTO[] ?? ownAccessMappings.ToArray();
                if (accessMappings.Any())
                {
                    var totalSize = accessMappings.Count();
                    var items = accessMappings.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage);
                    return new PagedCollection<AccessMappingModelDTO>(pageIndex, itemsPerPage, totalSize, items.ToList());
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message);
            }
            return new PagedCollection<AccessMappingModelDTO>();
        }

        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        [System.Web.Http.HttpPut]
        public CreateAccessMappingResult ChangeAccessMappingActivity(string providerCode, string consumerCode, string serviceCode, string serviceMethod, string providerBusCode, string consumerBusCode, bool activity)
        {
            try
            {
                var response = RequestToCsHelper.MakeRequestToCs<CreateAccessMappingResult>(AppSettings.Get<string>("ApiCSUrl") + "AccessMapping/ChangeActivityAccessMapping?accessMappingProviderCode=" + providerCode + "&accessMappingConsumerCode=" + consumerCode + "&accessMappingServiceCode=" + serviceCode + "&accessMappingMethodCode=" + serviceMethod + "&accessMappingProviderBusCode=" + providerBusCode + "&accessMappingConsumerBusCode=" + consumerBusCode + "&accessMappingIsActive=" + activity, "PUT");
                return response;
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message);
            }
        }
    }
}
