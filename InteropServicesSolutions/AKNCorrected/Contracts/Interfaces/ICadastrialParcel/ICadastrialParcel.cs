using System.ServiceModel;
using Contracts.DTO_s.AKNService;

namespace Contracts.Interfaces.ICadastrialParcel
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ICadastrialParcel
    {
        [OperationContract]
        ATRparceli GetCParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
    }
}
