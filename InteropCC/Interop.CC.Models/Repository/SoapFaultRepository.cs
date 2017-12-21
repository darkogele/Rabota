using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;

namespace Interop.CC.Models.Repository
{
    public class SoapFaultRepository : ISoapFaultRepository
    {
        private readonly IUnitOfWork _uow;

        // Опис: Конструктор на SoapFaultRepository модулот 
        // Влезни параметри: модел IUnitOfWork
        public SoapFaultRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Опис: Методот врши вчитување на сите грешки од база
        // Влезни параметри: /
        // Излезни параметри: IEnumerable<SoapFault> 
        public IEnumerable<SoapFault> GetSoapFaultMessages()
        {
            return _uow.Context.SoapFaults.ToList();
        }

        // Опис: Методот врши внесување на грешка во база
        // Влезни параметри: SoapFault soapFault
        // Излезни параметри: / 
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
            finally
            {
                if (_uow.Context != null)
                    _uow.Context.Dispose();
            }
        }

        // Опис: Методот врши вчитување на сите грешки од база во нумерирана форма
        // Влезни параметри: податочни вредности pageIndex, itemsPerPage, filterTransaction, fromDateSoap, toDateSoap
        // Излезни параметри: PagedCollection<SoapFault>
        public PagedCollection<SoapFault> GetSoapFaultsPaged(int pageIndex, int itemsPerPage, Guid? filterTransaction,
            DateTime? fromDateSoap, DateTime? toDateSoap)
        {
            IQueryable<SoapFault> items = null;

            if (fromDateSoap == null && toDateSoap == null && filterTransaction == null)
            {
                items = _uow.Context.SoapFaults;
            }
            if (fromDateSoap == null && toDateSoap == null && filterTransaction != null)
            {
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.TransactionId == filterTransaction);
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items = _uow.Context.SoapFaults
                    .Where(x => x.DateOccured <= dateTo);
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.TransactionId == filterTransaction && x.DateOccured <= dateTo);
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction == null)
            {
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.DateOccured >= fromDateSoap);
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction != null)
            {
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.TransactionId == filterTransaction && x.DateOccured >= fromDateSoap);
            }
            else if (fromDateSoap != null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo);
            }

            else if (fromDateSoap != null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                    _uow.Context.SoapFaults.Where(
                        x => x.TransactionId == filterTransaction &&

                             x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo);
            }

            var pagedItems =
                items.OrderByDescending(x => x.DateCreated)
                    .Skip((pageIndex - 1)*itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
            var totalSize = items.Count();
            return new PagedCollection<SoapFault>(pageIndex, itemsPerPage, totalSize, pagedItems);
        }

        // Опис: Методот врши вчитување на сите грешки од база во филтрирана форма 
        // Влезни параметри: податочни вредности filterTransaction, fromDateSoap, toDateSoap
        // Излезни параметри: List<SoapFaultExcelDTO>
        public List<SoapFaultExcelDTO> GetFilteredSoapFaults(Guid? filterTransaction, DateTime? fromDateSoap,
            DateTime? toDateSoap)
        {

            IQueryable<SoapFault> items = null;
            if (fromDateSoap == null && toDateSoap == null && filterTransaction == null)
            {
                items = _uow.Context.SoapFaults;
            }
            if (fromDateSoap == null && toDateSoap == null && filterTransaction != null)
            {
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.TransactionId == filterTransaction);
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.DateOccured <= dateTo);
            }
            else if (fromDateSoap == null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.TransactionId == filterTransaction && x.DateOccured <= dateTo);
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction == null)
            {
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.DateOccured >= fromDateSoap);
            }
            else if (fromDateSoap != null && toDateSoap == null && filterTransaction != null)
            {
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.TransactionId == filterTransaction && x.DateOccured >= fromDateSoap);
            }
            else if (fromDateSoap != null && toDateSoap != null && filterTransaction == null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);
                items =
                    _uow.Context.SoapFaults
                        .Where(x => x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo);
            }

            else if (fromDateSoap != null && toDateSoap != null && filterTransaction != null)
            {
                var dateTo = toDateSoap.Value.AddDays(1);

                items =
                    _uow.Context.SoapFaults.Where(
                        x => x.TransactionId == filterTransaction &&
                    x.DateOccured >= fromDateSoap && x.DateOccured <= dateTo);
            }

            var itemsDto = new List<SoapFaultExcelDTO>();
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
            return itemsDto.OrderByDescending(x => x.DateCreated).ToList();
        }

        public List<SoapFault> GetSoapFaultsByDate(DateTime createdDate)
        {
            var allSoapFaults = _uow.Context.SoapFaults;
            //soapFault => soapFault.DateCreated.Date == createdDate.Date
            var soapFaultsFromToday = allSoapFaults.Where(x => DbFunctions.TruncateTime(x.DateCreated) == createdDate.Date).ToList();
            return soapFaultsFromToday;
        }
    }
}
