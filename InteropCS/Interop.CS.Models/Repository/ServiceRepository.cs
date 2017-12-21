using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;

namespace Interop.CS.Models.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IUnitOfWork _uow;
        public ServiceRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //Опис: Методот вчитува податоци од база за приказ на Сервиси
        //Излезни параметри: Листа од сите сервиси
        public IEnumerable<CSService> GetServices()
        {
            return _uow.Context.Services.ToList();
        }

        //Опис: Методот вчитува податок од база за приказ на Сервиси, и притоа филтрира според влезните параметри
        //Пред се, се пребарува дали постои таков сервис со тие влезни параметри, и ако не постои, се јавува грешка
        //Влезни параметри: код за учесник кај одреден сервис, код за сервис
        //Излезни параметри: Објект од класата Сервис
        public CSService GetService(string serviceParticipantCode, string serviceCode)
        {

            var service = _uow.Context.Services.Find(serviceCode, serviceParticipantCode);
            if (service == null)
            {
                throw new NotFoundCSServiceException(serviceParticipantCode, serviceCode);
            }
            return service;
        }

        //Опис: Методот креира нов сервис
        //Влезни параметри: Објект од класата Сервис
        public void CreateService(CSService service) 
        {
            var serviceExists = _uow.Context.Services.Find(service.Code, service.ParticipantCode);
            if (serviceExists != null)
            {
                throw new DuplicateCSServiceException(service);
            }

            try
            {
                _uow.Context.Services.Add(service);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException s = ex.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    throw new DuplicateCSServiceException(service);
                }
            }
        }

        //Опис: Методот вчитува податоци од база за приказ на Сервиси,притоа сортира според влезните параметри
        //Влезни параметри: индекс за страна, број на записи по страна
        //Излезни параметри: Листа од сервиси (индекс за страна, број на записи по страна, вкупен број на записи, записи)
        public PagedCollection<ServiceDTO> GetServicesPaged(int pageIndex, int itemPerPage, string providerCode,string pickProvider)
        {
            if (String.IsNullOrEmpty(providerCode))
            {
                providerCode = "";
            }
            if (String.IsNullOrEmpty(pickProvider))
            {
                pickProvider = "";
            }

            var servicesDb = _uow.Context.Services.Where(s => s.Name.Contains(providerCode));
            var participantsDb = _uow.Context.Participants.Where(p => p.Code.Contains(pickProvider));

            //var query = from s in servicesDb
            //            join p in participantsDb on s.ParticipantCode equals p.Code
            //            select new ServiceDTO

            
            //var items = _uow.Context.Services.Select(x => new ServiceDTO
            //var splitter = new string ["$$"]; .Split(splitter,StringSplitOptions.None)
            
            var items = servicesDb.Join(participantsDb,
                s => s.ParticipantCode,
                p => p.Code,
                (s, p) => new ServiceDTO { ParticipantCode = p.Name, Code = p.Code, Name = s.Name, ServiceCode = s.Code}).ToList();

            //var splitChar = new string[] { "$$" };
            //foreach (var item in items)
            //{
            //    item.Code = item.Code.Split(splitChar, StringSplitOptions.None).First();
            //}

            
            var filteredItems = items.OrderBy(s => s.Name).Skip((pageIndex - 1) * itemPerPage).Take(itemPerPage).ToList();
                
            //    .Select//Select((s,p) => new ServiceDTO
            //{
            //    Name = s.Name,
            //    Code = s.Code,
            //    ParticipantCode = s.Name
            //}).Where(s => s.ParticipantCode.Contains(providerCode)).Where(s => s.ParticipantCode.Contains(pickProvider)).OrderBy(s => s.Name).Skip((pageIndex - 1) * itemPerPage).Take(itemPerPage).ToList();
            var totalSize = items.Count();

            return new PagedCollection<ServiceDTO>(pageIndex, itemPerPage, totalSize, filteredItems);
        }
        //Опис: Методот пребарува запис од база за приказ на Сервиси според влезните параметри
        //Влезни параметри: код за провјадер, код за сервис
        //Излезни параметри: WSDL 
        public string GetWSDL(string providerCode, string serviceCode)
        {
            var service = _uow.Context.Services.Find(serviceCode, providerCode);

            if (service == null)
            {
                throw new NotFoundCSServiceException(providerCode, serviceCode);
            }

            return service.Wsdl;
        }
        public string GetWSDLAccMap(string providerName, string serviceCode)
        {
            var service = _uow.Context.Services.Find(serviceCode, providerName);

            if (service == null)
            {
                throw new NotFoundCSServiceException(providerName, serviceCode);
            }

            return service.Wsdl;
        }

        public string GetServiceName(string serviceCode)
        {
            var service = _uow.Context.Services.SingleOrDefault(s=>s.Code == serviceCode);
            if (service == null)
            {
                throw new NotFoundCSServiceException(string.Empty, serviceCode);
            }

            return service.Name;
        }
        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
