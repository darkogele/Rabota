using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.UoW;

namespace Interop.CS.Models.Repository
{
    public class AccessMappingRepository : IAccessMappingRepository, IDisposable
    {
        private readonly IUnitOfWork _uow;
        public AccessMappingRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //Опис: Методот вчитува податоци од база за приказ на Пристапна Листа
        //Излезни параметри: Сите записи од Пристапна Листа
        public IEnumerable<AccessMapping> GetAccessMappings()
        {
            try
            {
                return _uow.Context.AccessMappings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Опис: Методот вчитува податок од базата за приказ на Пристапна Листа, и притоа филтрира според влезните параметри
        //Влезни параметри: Код за провајдер за запис во пристапната листа, код за корисник за запис во пристапната листа, код за сервис и код за метод
        //Излезни параметри: Објект од класата AccessMapping
        public AccessMapping GetAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethod)
        {
            AccessMapping accessMapping = _uow.Context.AccessMappings.Find(accessMappingConsumerCode, accessMappingProviderCode, accessMappingServiceCode, accessMappingMethod);

            if (accessMapping == null)
            {
                throw new NotFoundAccessMappingException(accessMappingConsumerCode, accessMappingProviderCode, accessMappingServiceCode);
            }

            return accessMapping;
        }

        //Опис: Методот пребарува запис филтриран според влезниот параметар
        //Влезни параметри: Објект од класата AccessMapping
        //Излезни параметри: Објект од класата AccessMapping
        private AccessMapping GetAccessMapping(AccessMapping accessMapping)
        {
            if (string.IsNullOrEmpty(accessMapping.ProviderCode)) accessMapping.ProviderCode = "";
            if (string.IsNullOrEmpty(accessMapping.ConsumerCode)) accessMapping.ConsumerCode = "";
            if (string.IsNullOrEmpty(accessMapping.ServiceCode)) accessMapping.ServiceCode = "";
            if (string.IsNullOrEmpty(accessMapping.MethodCode)) accessMapping.MethodCode = "";
            if (string.IsNullOrEmpty(accessMapping.ProviderBusCode)) accessMapping.ProviderBusCode = "";
            if (string.IsNullOrEmpty(accessMapping.ConsumerBusCode)) accessMapping.ConsumerBusCode = "";
            return
                _uow.Context.AccessMappings.FirstOrDefault(
                    x =>
                        x.ProviderCode == accessMapping.ProviderCode && x.ConsumerCode == accessMapping.ConsumerCode &&
                        x.ServiceCode == accessMapping.ServiceCode && x.MethodCode.Contains(accessMapping.MethodCode) &&
                        x.ProviderBusCode == accessMapping.ProviderBusCode &&
                        x.ConsumerBusCode == accessMapping.ConsumerBusCode);
        }

        //Опис: Методот креира нов запис во базата за Пристапна листа според влезниот параметар
        //Влезни параметри: Објект од класата AccessMapping
        public CreateAccessMappingResult CreateAccessMapping(AccessMapping accessMapping)
        {
            var providerCode = accessMapping.ProviderCode; //"MIM2$$AKN";
            var consumerCode = accessMapping.ConsumerCode; //"MIM1$$AKN";
            if (providerCode.Contains("MIM1$$") || providerCode.Contains("MIM2$$"))
            {
                accessMapping.ProviderCode = providerCode.Remove(0, 6);
            }
            if (consumerCode.Contains("MIM1$$") || consumerCode.Contains("MIM2$$"))
            {
                accessMapping.ConsumerCode = consumerCode.Remove(0, 6);
            }

            AccessMapping _accessMapping = GetAccessMapping(accessMapping);

            if (_accessMapping != null)
            {
                if (_accessMapping.IsActive)
                {
                    throw new DuplicateAccessMappingException(_accessMapping);
                }
                try
                {
                    _accessMapping.IsActive = true;
                    _uow.Context.SaveChanges();
                }
                catch
                {
                    throw new NotFoundAccessMappingException(_accessMapping.ConsumerCode, _accessMapping.ProviderCode,
                        _accessMapping.ServiceCode);
                }
                return new CreateAccessMappingResult
                {
                    IsSaved = true,
                    IsActivated = true
                };
            }

            try
            {
                _uow.Context.AccessMappings.Add(accessMapping);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException s = ex.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    throw new DuplicateAccessMappingException(accessMapping);
                }
            }
            return new CreateAccessMappingResult
            {
                IsSaved = true,
                IsActivated = false
            };
        }

        
        //Опис: Методот активира запис во базата за Пристапна листа, кој се пребарува според влезните параметри
        //Влезни параметри: Код за провајдер за запис во пристапната листа, код за корисник за запис во пристапната листа, код за сервис, код за метод, бас код на корисник, активност на записот од пристапната листа
        //Код за бас за провајдер, код за бас за корисник, и код за активност
        public void ActivateAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethod, string accessMappingProviderBusCode, string accessMappingConsumerBusCode, bool accessMappingIsActive = false)
        {

            AccessMapping accessMapping = GetAccessMapping(new AccessMapping
                {
                    ProviderCode = accessMappingProviderCode,
                    ConsumerCode = accessMappingConsumerCode,
                    ServiceCode = accessMappingServiceCode,
                    MethodCode = accessMappingMethod,
                    ProviderBusCode = accessMappingProviderBusCode,
                    ConsumerBusCode = accessMappingConsumerBusCode
                });

            if (accessMapping == null)
            {
                throw new NotFoundAccessMappingException(accessMappingConsumerCode, accessMappingProviderCode, accessMappingServiceCode);
            }
            accessMapping.IsActive = accessMappingIsActive;

            try
            {
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotFoundAccessMappingException(accessMappingConsumerCode, accessMappingProviderCode, accessMappingServiceCode);
            }
        }

        public CreateAccessMappingResult ChangeActivityAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode,
            string accessMappingServiceCode, string accessMappingMethod, string accessMappingProviderBusCode,
            string accessMappingConsumerBusCode, bool accessMappingIsActive)
        {
            AccessMapping accessMapping = GetAccessMapping(new AccessMapping
            {
                ProviderCode = accessMappingProviderCode,
                ConsumerCode = accessMappingConsumerCode,
                ServiceCode = accessMappingServiceCode,
                MethodCode = accessMappingMethod,
                ProviderBusCode = accessMappingProviderBusCode,
                ConsumerBusCode = accessMappingConsumerBusCode
            });

            if (accessMapping == null)
            {
                throw new NotFoundAccessMappingException(accessMappingConsumerCode, accessMappingProviderCode, accessMappingServiceCode);
            }

            accessMapping.IsActive = accessMappingIsActive;

            try
            {
                _uow.Context.SaveChanges();
                return new CreateAccessMappingResult
                {
                    IsSaved = true,
                    IsActivated = accessMappingIsActive
                };
            }
            catch (Exception)
            {
                throw new NotFoundAccessMappingException(accessMappingConsumerCode, accessMappingProviderCode, accessMappingServiceCode);
            }
        }

        //Опис: Методот вчитува податоци од база за приказ на Пристапна листа, и притоа филтрира и сортира според влезните параметри
        //Влезни параметри: индекс за страна, број на записи по страна, код за провајдер, код за корисник, код за сервис, и параметар за активност
        //Излезни параметри: листа на записи од базата за Пристапна Листа(индекс за страна, број на записи по страна, вкупен број на записи, записи)
        public PagedCollection<AccessMapping> GetAccessMappingsPaged(int pageIndex, int itemPerPage, string searchProviderCode, string searchConsumerCode, string searchServiceCode, bool? isActive)
        {
            if (String.IsNullOrEmpty(searchProviderCode))
            {
                searchProviderCode = "";
            }
            if (String.IsNullOrEmpty(searchConsumerCode))
            {
                searchConsumerCode = "";
            }
            if (String.IsNullOrEmpty(searchServiceCode))
            {
                searchServiceCode = "";
            }

            //var services = _uow.Context.Services;


            IQueryable<AccessMapping> queriedItems = null;

            if (!String.IsNullOrEmpty(searchProviderCode))
            {
                if (!String.IsNullOrEmpty(searchConsumerCode))
                {
                    if (!String.IsNullOrEmpty(searchServiceCode))
                    {
                        queriedItems =
                           _uow.Context.AccessMappings.Where(
                               x =>
                                   x.ProviderCode == searchProviderCode && x.ConsumerCode == searchConsumerCode &&
                                   x.ServiceCode == searchServiceCode);
                    }
                    else
                    {
                        queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode == searchProviderCode && x.ConsumerCode == searchConsumerCode &&
                                    x.ServiceCode.Contains(searchServiceCode));
                    }
                }
                else if (!String.IsNullOrEmpty(searchServiceCode))
                {
                    queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode == searchProviderCode && x.ConsumerCode.Contains(searchConsumerCode) &&
                                    x.ServiceCode == searchServiceCode);
                }
                else
                {
                    queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode == searchProviderCode && x.ConsumerCode.Contains(searchConsumerCode) &&
                                    x.ServiceCode.Contains(searchServiceCode));
                }
            }
            else if (!String.IsNullOrEmpty(searchConsumerCode))
            {
                if (!String.IsNullOrEmpty(searchServiceCode))
                {
                    queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode.Contains(searchProviderCode) && x.ConsumerCode == searchConsumerCode &&
                                    x.ServiceCode == searchServiceCode);
                }
                else
                {
                    queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode.Contains(searchProviderCode) && x.ConsumerCode == searchConsumerCode &&
                                    x.ServiceCode.Contains(searchServiceCode));
                }
            }
            else if (!String.IsNullOrEmpty(searchServiceCode))
            {
                queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode.Contains(searchProviderCode) && x.ConsumerCode.Contains(searchConsumerCode) &&
                                    x.ServiceCode == searchServiceCode);
            }
            else
            {
                queriedItems =
                            _uow.Context.AccessMappings.Where(
                                x =>
                                    x.ProviderCode.Contains(searchProviderCode) &&
                                    x.ConsumerCode.Contains(searchConsumerCode) &&
                                    x.ServiceCode.Contains(searchServiceCode));
            }


            if (isActive.HasValue)
            {
                queriedItems = isActive.Value ? queriedItems.Where(x => x.IsActive) : queriedItems.Where(x => !x.IsActive);
            }

            //var resultItemsNew = new List<AccessMappingDTO>();
            var resultItems = queriedItems.ToList();

            //foreach (var accessMapping in resultItems)
            //{
            //    var item = new AccessMappingDTO
            //    {
            //        ProviderCode = accessMapping.ProviderCode,
            //        ProviderBusCode = accessMapping.ProviderBusCode,
            //        ConsumerCode = accessMapping.ConsumerCode,
            //        ConsumerBusCode = accessMapping.ConsumerBusCode,
            //        ServiceCode = accessMapping.ServiceCode,
            //        MethodCode = accessMapping.MethodCode.Split('/').Last(),
            //        IsActive = accessMapping.IsActive,
            //        ServiceName = services.Where(serv => serv.Code.ToLower() == accessMapping.ServiceCode.ToLower())
            //            .FirstOrDefault()
            //            .Name
            //    };

            //    resultItemsNew.Add(item);
            //}

            var totalSize = resultItems.Count();

            resultItems = resultItems.OrderBy(x => x.ProviderCode).Skip((pageIndex - 1) * itemPerPage).Take(itemPerPage).ToList();

            return new PagedCollection<AccessMapping>(pageIndex, itemPerPage, totalSize, resultItems);
        }
        public Dictionary<string, string> GetProviders()
        {
            var temp = new Dictionary<string, string>();
            var providers = _uow.Context.AccessMappings.Select(x => new { x.ProviderCode, x.ProviderBusCode }).Distinct().ToList();
            foreach (var prov in providers)
            {
                temp.Add(prov.ProviderCode, prov.ProviderBusCode);
            }

            return temp;
        }

        public Dictionary<string, string> GetConsumers()
        {
            var temp = new Dictionary<string, string>();
            var consumers = _uow.Context.AccessMappings.Select(x => new { x.ConsumerCode, x.ConsumerBusCode }).Distinct().ToList();
            foreach (var con in consumers)
            {
                temp.Add(con.ConsumerCode, con.ConsumerBusCode);
            }

            return temp;
        }

        public IQueryable<AccessMappingCCDTO> GetOwnAccessMappings(string providerCode)
        {
            var ccAccessMappings = new List<AccessMappingCCDTO>();
            var ownAccessMappings = _uow.Context.AccessMappings.Where(x => x.ProviderCode == providerCode.Trim().ToUpper()).ToList();
            foreach (var accessMapping in ownAccessMappings)
            {
                var consumerExistAsParticipant = _uow.Context.Participants.Any(x => x.Code.Contains(accessMapping.ConsumerCode));
                var providerExistAsParticipant = _uow.Context.Participants.Any(x => x.Code.Contains(accessMapping.ProviderCode));
                var serviceExist = _uow.Context.Services.Any(x => x.Code == accessMapping.ServiceCode);

                string consumerName = !consumerExistAsParticipant ? accessMapping.ConsumerCode : _uow.Context.Participants.FirstOrDefault(x => x.Code.Contains(accessMapping.ConsumerCode)).Name;
                string providerName = !providerExistAsParticipant ? accessMapping.ProviderCode : _uow.Context.Participants.FirstOrDefault(x => x.Code.Contains(accessMapping.ProviderCode)).Name;
                string serviceName = !serviceExist ? accessMapping.ServiceCode : _uow.Context.Services.FirstOrDefault(x => x.Code == accessMapping.ServiceCode).Name;

                var accessMappingDto = new AccessMappingCCDTO
                {
                    ConsumerCode = accessMapping.ConsumerCode,
                    ConsumerBusCode = accessMapping.ConsumerBusCode,
                    ConsumerName = consumerName,
                    ProviderCode = accessMapping.ProviderCode,
                    ProviderBusCode = accessMapping.ProviderBusCode,
                    ProviderName = providerName,
                    IsActive = accessMapping.IsActive,
                    MethodCode = accessMapping.MethodCode,
                    ServiceCode = accessMapping.ServiceCode,
                    ServiceName = serviceName
                };
                ccAccessMappings.Add(accessMappingDto);
            }
            return ccAccessMappings.AsQueryable();
        }

        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
