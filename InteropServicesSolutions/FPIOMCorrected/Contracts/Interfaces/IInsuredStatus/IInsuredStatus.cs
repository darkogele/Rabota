using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IInsuredStatus
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IInsuredStatus
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetInsuredStatus(string embg);
    }
}
