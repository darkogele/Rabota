using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IListOfChangesCU
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IListOfChangesCU
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetListOfChangesCU(string param);
    }
}
