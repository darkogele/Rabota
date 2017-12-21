using System.ServiceModel;

namespace Contracts.Interfaces.Test
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ITSTest
    {
        [OperationContract]
        string GetTSTest(string embs);
    }
}
