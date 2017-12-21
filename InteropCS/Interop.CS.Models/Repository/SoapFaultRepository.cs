using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Interop.CS.Models.DTO;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.UoW;
using System.Linq.Dynamic;

namespace Interop.CS.Models.Repository
{
    public class SoapFaultRepository : ISoapFaultRepository
    {
        private readonly IUnitOfWork _uow;

        public SoapFaultRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //Опис: Методот вчитува податоци од базата за Грешки
        //Излезни параметри: Листа од сите записи од базата за Грешки
        public IEnumerable<SoapFault> GetSoapFaultMessages()
        {
            return _uow.Context.SoapFaults.ToList();
        }

        //public SoapFault GetSoapFaultMessageBy(string code)
        //{
        //    var soapExists = _uow.Context.SoapFaults.Find(code);

        //    if (soapExists == null)
        //    {
        //        //throw new NotFoundServiceException(code);
        //    }

        //    return soapExists;
        //}

        //Опис: Методот додава нов запис во базата за Грешки, но прво се бара дали веќе постои запис со Id за трансакција за лог
        //Ако таков запис постои, се јавува грешка
        //Влезни параметри: Објект од класата SoapFault
        public void InsertSoapFault(SoapFault soapFault)
        {
            SoapFault soapExists = _uow.Context.SoapFaults.Find(soapFault.TransactionId);

            if (soapExists != null)
            {
                //throw new DuplicateServiceException(service);
            }

            try
            {
                _uow.Context.SoapFaults.Add(soapFault);
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo
            }
        }

        //Опис: Методот вчитува податоци од базата за Грешки, притоа филтрира и сортира според влезните параметри
        //Влезни параметри: индекс за страна, број на записи по страна, трансакција, дата од (за дата на настанување) и дата до (за дата на настанување)
        //Излезни параметри: Листа од записи од базата за Грешки (индекс на страна, број на записи по страна, вкупен број на записи, записи)
        public PagedCollection<SoapFault> GetSoapFaultsPaged(int pageIndex, int itemsPerPage, Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol)
        {
            //IQueryable<SoapFault> items ;
            IQueryable<SoapFault> items = null;
           
            if (fromDateSoap == null && toDateSoap == null && filterTransaction == null)
            {
                items = _uow.Context.SoapFaults;
            }
            if (fromDateSoap == null && toDateSoap == null && filterTransaction != null)
            {
                items = _uow.Context.SoapFaults.Where(x => x.TransactionId == filterTransaction);
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults.Where(x => x.DateOccured <= dateTo);
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults.Where(x => x.TransactionId == filterTransaction && x.DateOccured <= dateTo);
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction == null)
            {
                items = _uow.Context.SoapFaults.Where(x => x.DateOccured >= fromDateSoap);
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction != null)
            {
                items = _uow.Context.SoapFaults.Where(x => x.TransactionId == filterTransaction && x.DateOccured >= fromDateSoap);
            }
            else if (fromDateSoap != null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults.Where(x => x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo);
            }

            else if (fromDateSoap != null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                      _uow.Context.SoapFaults.Where(
                          x => x.TransactionId == filterTransaction &&

                              x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo);
            }

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "Name";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }

            if (sortDir == "asc")
            {
                items = items.OrderBy(sortCol);
            }
            else if (sortDir == "desc")
            {
                items = items.OrderBy(sortCol + " descending");
            }
            else
            {
                items = items.OrderBy(x => x.TransactionId);
            }

            var pagedItems = items.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            var totalSize = items.Count();
            return new PagedCollection<SoapFault>(pageIndex, itemsPerPage, totalSize, pagedItems);
        }

        //Опис: Методот ги филтрира записите од базата за Грешки според влезните параметри
        //Влезни параметри: Трансакција, дата од (за дата на настанување) и дата до (за дата на настанување)
        //Излезни параметри: Листа од исфилтрирани записи од базата за Грешки
        public IEnumerable<SoapFaultExcelDTO> GetFilteredSoapFaults(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol)
        {

            List<SoapFault> items = null;
            if (fromDateSoap == null && toDateSoap == null && filterTransaction == null)
            {
                items = _uow.Context.SoapFaults.ToList();
            }
            if (fromDateSoap == null && toDateSoap == null && filterTransaction != null)
            {
                items = _uow.Context.SoapFaults.Where(x => x.TransactionId == filterTransaction).ToList();
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults.Where(x => x.DateOccured <= dateTo).ToList();
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults.Where(x => x.TransactionId == filterTransaction && x.DateOccured <= dateTo).ToList();
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction == null)
            {
                items = _uow.Context.SoapFaults.Where(x => x.DateOccured >= fromDateSoap).ToList();
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction != null)
            {
                items = _uow.Context.SoapFaults.Where(x => x.TransactionId == filterTransaction && x.DateOccured >= fromDateSoap).ToList();
            }
            else if (fromDateSoap != null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults.Where(x => x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo).ToList();
            }

            else if (fromDateSoap != null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);

                items =
                      _uow.Context.SoapFaults.Where(
                          x => x.TransactionId == filterTransaction &&

                              x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo).ToList();
            }

            var itemsDto = new List<SoapFaultExcelDTO>();

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "Name";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }
            
            var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
            foreach (var soapFaultExcel in items)
            {
                itemsDto.Add(new SoapFaultExcelDTO
                {
                    Code = soapFaultExcel.Code,
                    DateCreated = soapFaultExcel.DateCreated.ToString("dd.MM.yyyy hh:mm:ss", macCultureInfo),
                    DateOccured = soapFaultExcel.DateOccured.ToString("dd.MM.yyyy hh:mm:ss", macCultureInfo),
                    Reason = soapFaultExcel.Reason,
                    SubCode = soapFaultExcel.SubCode,
                    TransactionId = soapFaultExcel.TransactionId
                });
            }

            if (sortDir == "asc")
            {
                itemsDto = itemsDto.OrderBy(sortCol).ToList();
            }
            else if (sortDir == "desc")
            {
                itemsDto = itemsDto.OrderBy(sortCol + " descending").ToList();
            }
            else
            {
                itemsDto = itemsDto.OrderBy(x => x.TransactionId).ToList();
            }

            return itemsDto;
        }

        public IEnumerable<SoapFault> GetSoapFaultsByDate(DateTime createdDate)
        {
            var allSoapFaults = _uow.Context.SoapFaults.ToList();
            var soapFaultsFromToday = allSoapFaults.Where(soapFault => soapFault.DateCreated.Date == createdDate.Date).ToList();
            return soapFaultsFromToday;
        }

        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
