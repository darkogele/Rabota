using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;

namespace Interop.CS.Portal.API.Controllers
{
    [Authorize]
    public class BusesController : ApiController
    {
        private readonly IBusesRepository _busesRepository;

        public BusesController()
        {
                
        }

        public BusesController(IBusesRepository busesRepository)
        {
            _busesRepository = busesRepository;
        }

        //Опис: Методот прави повик до CreateBus од BusesRepository
        //Влезни параметри: Објект од класата Buses
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public void CreateBus(Buses bus)
        {
            try
            {
                _busesRepository.CreateBus(bus);
            }
            catch (DuplicateBusException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот прави повик до GetBuses од BusesRepository
        //Се враќаат кодовите од сите басови
        [HttpGet]
        public List<string> GetBuses()
        {
            var buses = _busesRepository.GetBuses();
            return buses.Select(x => x.Code).ToList();
        }

        //Опис: Методот прави повик до GetBusesPaged од BusesRepository
        //Влезни параметри: индекс на страна, број на записи по страна
        [HttpGet]
        public PagedCollection<Buses> GetBusesPaged(int pageIndex, int itemsPerPage)
        {
            var busesPaged = _busesRepository.GetBusesPaged(pageIndex, itemsPerPage);
            return busesPaged;
        }
    }
}
