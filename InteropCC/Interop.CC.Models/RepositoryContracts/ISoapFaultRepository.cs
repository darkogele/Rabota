using System;
using System.Collections.Generic;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.RepositoryContracts 
{
 public interface  ISoapFaultRepository
 {
     IEnumerable<SoapFault> GetSoapFaultMessages();
     //SoapFault GetSoapFaultMessageBy(string code);
     void InsertSoapFault(SoapFault soapFault);
     PagedCollection<SoapFault> GetSoapFaultsPaged(int pageIndex, int itemsPerPage, Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap);
     List<SoapFaultExcelDTO> GetFilteredSoapFaults(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap);
     List<SoapFault> GetSoapFaultsByDate(DateTime createdDate);
 }
}
