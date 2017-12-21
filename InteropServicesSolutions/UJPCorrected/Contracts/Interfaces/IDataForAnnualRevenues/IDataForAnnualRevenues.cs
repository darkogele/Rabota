using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Models.DataForAnnualRevenues;

namespace Contracts.Interfaces.IDataForAnnualRevenues
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IDataForAnnualRevenues
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        AnnualRevenuesMTSPOutput AnnualRevenMTSP(string edb, string year);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        AnnualRevenuesMONOutput AnnualRevenMON(string edb, string year);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        AnnualRevenuesFZOOutput AnnualRevenFZO(string edb, string year);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        AnnualRevenuesKKKSOutput AnnualRevenKKKS(string edb, string year);
    }
}
