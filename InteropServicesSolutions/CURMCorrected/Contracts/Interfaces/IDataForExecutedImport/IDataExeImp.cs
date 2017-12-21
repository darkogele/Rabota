using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.DataAccessLibrary.DataForExecutedImport;
using Contracts.Models.DataForExecutedImport;

namespace Contracts.Interfaces.IDataForExecutedImport
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IDataExeImp
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        List<ExecutedImportOutput> GetDataExeImp(ExecutedImportInput input);
    }
}
