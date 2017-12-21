using System.ServiceModel;

namespace Contracts.Interfaces.ITSProduction
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ITSProduction
    {
        [OperationContract]
        string GetTSProduction(string embs);
    }
}
