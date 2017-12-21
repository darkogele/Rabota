using System.ServiceModel;
using Implementations.Models;

namespace Implementations.Contracts.BigData
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IBigData
    {
        [OperationContract]
        DocumentModel GetLargeDoc(string type);
    }
}
