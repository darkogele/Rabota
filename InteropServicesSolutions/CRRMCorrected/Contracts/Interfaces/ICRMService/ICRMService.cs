using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.ICRMService
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ICRMService
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetTekovnaSostojba(string param);
    }
}
