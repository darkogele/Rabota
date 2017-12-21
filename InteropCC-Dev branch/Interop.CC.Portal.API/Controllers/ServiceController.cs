using System.Web;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using System.Collections.Generic;
using System.Web.Http;
using Interop.CC.Models.RepositoryContracts;

namespace Interop.CC.Portal.API.Controllers
{
    [Authorize]
    public class ServiceController : ApiController
    {
        private readonly IServiceRepository _servicesRepository;

        // Опис: Конструктор на ServiceController модулот
        // Влезни параметри: IServiceRepository модел
        public ServiceController(IServiceRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        // Опис: Методот го повикува GetServices методот на ServiceRepository модулот
        // Влезни параметри: /
        // Излезни параметри: листа со податоци за Service моделот
        [Authorize]
        public IEnumerable<Service> GetServiceList()
        {
            return _servicesRepository.GetServices();
        }

        // Опис: Методот го повикува GetServiceListPaged методот на ServiceRepository модулот
        // Влезни параметри: број на страна, број на записи по страна, насока на сортирање, колона за сортирање, код за филтрирање, име за филтрирање
        // Излезни параметри: листа со податоци за ServiceDTO моделот
        [HttpGet]
        [Authorize]
        public PagedCollection<ServiceDTO> GetServiceListPaged(int pageIndex, int itemsPerPage, string sortDir, string sortCol, string filterCode = "", string filterName = "")
        {
            var servicesPaged = _servicesRepository.GetServicesPaged(pageIndex, itemsPerPage, filterCode, filterName, sortDir, sortCol);
            return servicesPaged;
        }

        // Опис: Методот го повикува GetWSDL методот на ServiceRepository модулот
        // Влезни параметри: код за сервис
        // Излезни параметри: WSDL за соодветен сервис
        public string GetWSDL(string serviceCode)
        {
            try
            {
                return _servicesRepository.GetWSDL(serviceCode);
            }
            catch (NotFoundServiceException ex)
            {
                throw new HttpException(ex.Message);
            }
            
        }
    }
}
