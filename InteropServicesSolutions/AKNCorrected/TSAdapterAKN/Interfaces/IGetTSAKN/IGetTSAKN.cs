using System.ServiceModel;

namespace TSAdapterAKN.Interfaces.IGetTSAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IGetTSAKN
    {
        [OperationContract]
        string TekSostojba(string embs);
    }
}
