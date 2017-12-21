using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IListOfSubjectsCU
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IListOfSubjectsCU
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetSubjectsCU(string param);
    }
}
