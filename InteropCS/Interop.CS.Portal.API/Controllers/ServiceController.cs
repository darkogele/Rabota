using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;

namespace Interop.CS.Portal.API.Controllers
{
    [Authorize]
    public class ServiceController : ApiController
    {
        private readonly IServiceRepository _servicesRepository;
        private readonly IParticipantRepository _participantRepository;

        public ServiceController()
        {

        }

        public ServiceController(IServiceRepository servicesRepository, IParticipantRepository participantRepository)
        {
            _servicesRepository = servicesRepository;
            _participantRepository = participantRepository;
        }

        //Опис: Методот прави повик до методот GetServices од ServiceRepository
        [HttpGet]
        public IEnumerable<CSService> GetServiceList()
        {
            return _servicesRepository.GetServices();
        }

        //Опис: Методот прави повик до методот GetService од ServiceRepository
        //Влезни параметри: код за сервис, код на учесник за сервис
        [HttpGet]
        public CSService GetService(string serviceCode, string serviceParticipantCode)
        {
            try
            {
                return _servicesRepository.GetService(serviceCode, serviceParticipantCode);
            }
            catch (NotFoundCSServiceException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот прави повик до методот GetServicesPaged од ServiceRepository
        //Влезни параметри: индекс на страна, број на записи по страна
        [HttpGet]
        public PagedCollection<ServiceDTO> GetServiceListPaged(int pageIndex, int itemsPerPage,string providerCode,string pickProvider)
        {
            var servicesPaged = _servicesRepository.GetServicesPaged(pageIndex, itemsPerPage,providerCode,pickProvider);
            return servicesPaged;
        }

        //Опис: Методот прави повик до методот GetServices од ServiceRepository
        //Се врши проверка дали сервисите имаат ист код за учесник како влезниот параметар
        //Влезни параметри: код на учесник
        public IEnumerable<CSService> GetListByParticipantCode(string participantCode)
        {
            var lstService = new List<CSService>();
            if (!string.IsNullOrEmpty(participantCode))
            {
                var codeForParticipant = _participantRepository.GetParticipantCode(participantCode);
                if (!string.IsNullOrEmpty(codeForParticipant))
                {
                    lstService = _servicesRepository.GetServices().Where(x => x.ParticipantCode == codeForParticipant).ToList();
                }
            }

            return lstService;
        }

        //Опис: Методот прави повик до методот GetWSDL од ServiceRepository
        //Влезни параметри: код за провајдер, код за сервис
        public string GetWSDL(string providerCode, string serviceCode)
        {
            try
            {
                _servicesRepository.GetWSDL(providerCode, serviceCode);
            }
            catch (NotFoundCSServiceException ex)
            {
                throw new HttpException(ex.Message);
            }
            return _servicesRepository.GetWSDL(providerCode, serviceCode);
        }
        //Опис: Методот прави повик до методот GetWSDL од ServiceRepository
        //Од таму се зема WSDL, притоа филтрирајќи по влезните параметри
        //Влезни параметри: код за провајдер, код за сервис
        //Излезни параметри: Листа од методи за добиенот WSDL
        public List<string> GetMethodsForWsdl(string providerCode, string serviceCode)
        {
            var methods = new List<string>();
            var participantName = _participantRepository.GetParticipantCodeByName(providerCode);
            if (!string.IsNullOrEmpty(participantName))
            {
                var wsdl = _servicesRepository.GetWSDL(providerCode, serviceCode);
                methods = WsdlParsingHelper.WsdlGetMethodNames(wsdl);
            }
            return methods;
        }
        public List<string> GetMethodsForWsdlAccMap(string providerCode, string serviceCode)
        {
            var methods = new List<string>();
            if (!string.IsNullOrEmpty(providerCode))
            {
                var wsdl = _servicesRepository.GetWSDLAccMap(providerCode, serviceCode);
                methods = WsdlParsingHelper.WsdlGetMethodNames(wsdl);
            }
            return methods;
        }
        [AllowAnonymous]
        [HttpGet]
        public Dictionary<string, string> GetServices()
        {
            var servicesDict = new Dictionary<string, string>();
            var services = _servicesRepository.GetServices();
            foreach (var service in services)
            {
                servicesDict.Add(service.Code, service.Name);
            }
            return servicesDict;
        } 
    }
}
