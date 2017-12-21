using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IDataForEnsurers
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IDataForEnsurers
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetDataForEnsurees(string embg);
    }
}
