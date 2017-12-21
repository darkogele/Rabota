using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IDataForRetired
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IDataForRetired
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string GetDataForRetired(string embg);
    }
}
