using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IStatusForRegularStudent
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ISRegStudent
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetStuS(string EMBG);
    }
}
