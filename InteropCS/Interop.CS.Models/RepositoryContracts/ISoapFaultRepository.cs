using System;
using System.Collections.Generic;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Models;
using Interop.CS.Models.Helpers;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface ISoapFaultRepository : IDisposable
    {
        IEnumerable<SoapFault> GetSoapFaultMessages();
        //SoapFault GetSoapFaultMessageBy(string code);
        void InsertSoapFault(SoapFault soapFault);
        PagedCollection<SoapFault> GetSoapFaultsPaged(int pageIndex, int itemsPerPage, Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol);
        IEnumerable<SoapFaultExcelDTO> GetFilteredSoapFaults(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol);
        IEnumerable<SoapFault> GetSoapFaultsByDate(DateTime createdDate);
    }
}
