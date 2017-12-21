using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;

namespace Interop.CC.Models.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IUnitOfWork _uow;

        // Опис: Конструктор на ServiceRepository модулот 
        // Влезни параметри: модел IUnitOfWork
        public ServiceRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Опис: Методот врши вчитување на сите сервиси од база
        // Влезни параметри: /
        // Излезни параметри: IEnumerable<Service> 
        public IEnumerable<Service> GetServices()
        {
            return _uow.Context.Services.ToList();
        }

        // Опис: Методот врши вчитување на сервис од база според код
        // Влезни параметри: податочна вредност code
        // Излезни параметри: Service 
        public Service GetServiceByCode(string code)
        {
            var serviceExists = _uow.Context.Services.Find(code);

            if (serviceExists == null)
            {
                throw new NotFoundServiceException(code);
            }

            return serviceExists;
        }

        // Опис: Методот врши внесување на сервис во база
        // Влезни параметри: Service service
        // Излезни параметри: / 
        public void InsertService(Service service)
        {
            Service serviceExists = _uow.Context.Services.Where(x => x.Code == service.Code).FirstOrDefault();

            if (serviceExists != null)
            {
                throw new DuplicateServiceException(service);
            }

            try
            {
                _uow.Context.Services.Add(service);
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo
            }
           
        }

        // Опис: Методот врши вчитување на сите сервиси од база во нумерирана форма
        // Влезни параметри: податочни вредности pageIndex, itemsPerPage, filterCode, filterName, sortDir, sortCol
        // Излезни параметри: PagedCollection<ServiceDTO>
        public PagedCollection<ServiceDTO> GetServicesPaged(int pageIndex, int itemPerPage, string filterCode, string filterName, string sortDir, string sortCol)
        {
            IReadOnlyCollection<ServiceDTO> pagedItems;
            IQueryable<ServiceDTO> items;
            if (String.IsNullOrEmpty(filterCode))
                filterCode = "";
            if (String.IsNullOrEmpty(filterName))
                filterName = "";

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "Name";
            }

            // Build lambda expression
            var param = Expression.Parameter(typeof(ServiceDTO), "x");
            var mySortExpression = Expression.Lambda<Func<ServiceDTO, object>>(Expression.Property(param, sortCol), param);

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }

            if (sortDir == "asc")
            {
                items =
                    _uow.Context.Services.
                    Where(
                        x =>
                            (x.Code.Contains(filterCode) || string.IsNullOrEmpty(filterCode)) &&
                            (x.Name.Contains(filterName) || string.IsNullOrEmpty(filterName)))
                        .Select(x => new ServiceDTO
                        {
                            Name = x.Name,
                            Code = x.Code,
                            Endpoint = x.Endpoint
                        }).OrderBy(mySortExpression);
            }
            else if (sortDir == "desc")
            {
                items =
                    _uow.Context.Services.
                        Where(
                            x =>
                                (x.Code.Contains(filterCode) || string.IsNullOrEmpty(filterCode)) &&
                                (x.Name.Contains(filterName) || string.IsNullOrEmpty(filterName)))
                        .Select(x => new ServiceDTO
                        {
                            Name = x.Name,
                            Code = x.Code,
                            Endpoint = x.Endpoint
                        }).
                        OrderByDescending(mySortExpression);
            }
            else
            {
                items =
                    _uow.Context.Services.
                        Where(
                            x =>
                                (x.Code.Contains(filterCode) || string.IsNullOrEmpty(filterCode)) &&
                                (x.Name.Contains(filterName) || string.IsNullOrEmpty(filterName)))
                        .Select(x => new ServiceDTO
                        {
                            Name = x.Name,
                            Code = x.Code,
                            Endpoint = x.Endpoint
                        }).
                        OrderBy(x => x.Name);
            }
            
            pagedItems = items.Skip((pageIndex - 1) * itemPerPage).Take(itemPerPage).ToList();

            var totalSize = items.Count();

            return new PagedCollection<ServiceDTO>(pageIndex, itemPerPage, totalSize, pagedItems);
        }
        // Опис: Методот врши вчитување на WSDL од база
        // Влезни параметри: податочна вредност serviceCode
        // Излезни параметри: податочен тип string
        public string GetWSDL(string serviceCode)
        {
            var serviceExists = _uow.Context.Services.Find(serviceCode);

            if (serviceExists == null)
            {
                throw new NotFoundServiceException(serviceCode);
            }

            return serviceExists.Wsdl;
        }

        // Опис: Методот врши бришење на сервис од база
        // Влезни параметри: Service service
        // Излезни параметри: / 
        public void DeleteService(Service service)
        {
            var serviceExist = _uow.Context.Services.Find(service.Code);

            if (serviceExist == null)
            {
                throw new NotFoundServiceException(service.Code);
            }

            try
            {
                Service serviceDelete = _uow.Context.Services.Find(service.Code);
                _uow.Context.Services.Remove(serviceDelete);
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }

    }
}
