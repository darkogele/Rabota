using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Models.DataFor_EDB_EMB;

namespace Contracts.Interfaces.IDataForEMB
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IDataForEMB
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        List<EDB_EMB_Output> GetEMB(string edb);
    }
}
