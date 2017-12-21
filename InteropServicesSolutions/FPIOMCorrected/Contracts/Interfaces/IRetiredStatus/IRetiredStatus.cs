using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IRetiredStatus
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IRetiredStatus
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetRetiredStatus(string embg);
    }
}
