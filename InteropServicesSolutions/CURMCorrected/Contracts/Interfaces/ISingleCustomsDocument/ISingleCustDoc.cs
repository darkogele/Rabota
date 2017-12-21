using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.Models.SingleCustomsDocument;

namespace Contracts.Interfaces.ISingleCustomsDocument
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ISingleCustDoc
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        SingleCustomsDocumentOutput GetSingleCustDoc(int year, long edbOfShippingCompany, int numberOfCustomsOffice, int regNumber);
        [OperationContract]
        List<CustomsOfficeNumbers> GetCustomsOfficeNumbers();
    }
}
