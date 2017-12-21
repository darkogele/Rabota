using System.Collections.Generic;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.DataAccessLibrary.DataForExecutedExport;
using Contracts.Models.DataForExecutedExport;

namespace Contracts.Interfaces.IDataForExecutedExport
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IDataExeExp
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        List<ExecutedExportOutput> GetDataExeExp(ExecutedExportInput input);
    }
}
