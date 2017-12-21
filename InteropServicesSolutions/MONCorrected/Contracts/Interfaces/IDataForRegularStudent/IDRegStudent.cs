using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IDataForRegularStudent
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IDRegStudent
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetStuD(string EMBG);
    }
}
