using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Models.DataFor_EDB_EMB;

namespace Contracts.Interfaces.IDataForEDB
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IDataForEDB
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        List<EDB_EMB_Output> GetEDB(string emb);

    }
}
