using InteropServices.UJP.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org")]
    interface IDataForAnnualRevenues
    {
        [OperationContract]
        AnnualRevenuesMTSPOutput GetAnnualRevenuesMTSP(string EDB, string year);
        [OperationContract]
        AnnualRevenuesMONOutput GetAnnualRevenuesMON(string EDB, string year);
        [OperationContract]
        AnnualRevenuesFZOOutput GetAnnualRevenuesFZO(string EDB, string year);
        [OperationContract]
        AnnualRevenuesKKKSOutput GetAnnualRevenuesKKKS(string EDB, string year);
    }
}
