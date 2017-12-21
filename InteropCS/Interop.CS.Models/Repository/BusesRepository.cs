using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.Repository
{
    public class BusesRepository : IBusesRepository
    {
        private readonly IUnitOfWork _uow;
        public BusesRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //Опис: Методот ги вчитува сите записи од базата за Басови
        //Излезни параметри: Листа од сите Басови
        public IEnumerable<Buses> GetBuses()
        {
            return _uow.Context.Buses.ToList();
        }

        //Опис: Методот пребарува Url за запис во базата за Басови и притоа филтрира според влезниот параметар
        //Влезни параметри: Код за Бас
        //Излезни параметри: Url за конкретен запис со тој код за бас
        public string GetBusUrl(string busCode)
        {
            var bus = _uow.Context.Buses.Find(busCode);
            return bus.Url;
        }

        //Опис: Методот креира нов бас во базата за Басови
        //Прво се проверува дали постои веќе таков бас во база, и ако постои, се јавува грешка
        //Влезни параметри: Објект од класата Buses
        public void CreateBus(Buses bus)
        {
            var busExist = _uow.Context.Buses.Find(bus.Code);
            if (busExist != null)
            {
                throw new DuplicateBusException(bus);
            }

            UrlAttribute.IsValid(bus.Url);

            try
            {
                _uow.Context.Buses.Add(bus);
                _uow.Context.SaveChanges();
            }

            catch (DbUpdateException ex)
            {
                SqlException s = ex.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    throw new DuplicateBusException(bus);
                }
            }
        }
        //Опис: Методот вчитува податоци од базата за Басови, и притоа иг сортира според влезните параметри
        //Влезни параметри: индекс на страна, број на записи по страна
        //Излезни параметри: Листа од басови (индекс на страна, број на записи по страна, вкупен број на записи, записи)
        public PagedCollection<Buses> GetBusesPaged(int pageIndex, int itemPerPage)
        {

            var items =
                _uow.Context.Buses.ToList();

            var totalSize = items.Count();

            items = items.Skip((pageIndex - 1) * itemPerPage).Take(itemPerPage).ToList();

            return new PagedCollection<Buses>(pageIndex, itemPerPage, totalSize, items);
        }

        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
