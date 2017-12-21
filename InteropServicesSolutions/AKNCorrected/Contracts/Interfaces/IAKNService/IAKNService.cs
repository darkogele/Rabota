using System.ServiceModel;
using Contracts.DTO_s.AKNService;

namespace Contracts.Interfaces.IAKNService
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNService
    {
        [OperationContract]
        dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);

        [OperationContract]
        ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
    }
}
