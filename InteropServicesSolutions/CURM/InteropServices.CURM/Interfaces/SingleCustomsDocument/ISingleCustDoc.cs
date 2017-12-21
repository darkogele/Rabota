using InteropServices.CURM.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.CURM.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ISingleCustDoc
    {
        [OperationContract]
        SingleCustomsDocumentOutput GetSingleCustDoc(int year, long EDBOfShippingCompany, int NumberOfCustomsOffice, int regNumber);
        [OperationContract]
        List<CustomsOfficeNumbers> GetCustomsOfficeNumbers();
    }
}
