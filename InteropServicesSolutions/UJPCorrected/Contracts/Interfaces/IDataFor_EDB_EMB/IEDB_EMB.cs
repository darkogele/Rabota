using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Models.DataFor_EDB_EMB;

namespace Contracts.Interfaces.IDataFor_EDB_EMB
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IEDB_EMB
    {
        //[OperationContract]
        //[FaultContract(typeof(InteropFault))]
        //List<EDB_EMB_Output> GetEDB(string edb);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        List<EDB_EMB_Output> GetEMB(string emb);
    }
}
