using System.ServiceModel;

namespace AdapterServiceAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IAKNService
    {
        [OperationContract]
        dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);

        [OperationContract]
        ATRparceli GetCadastrialParcel(string username, string password, string opstina, string katastarskaOpstina, string brParcela);
    }
}
