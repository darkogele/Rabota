using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string GetData();

        [OperationContract]
        CompositeType GetDataUsingDataContract();
    }
}
