using System.ServiceModel;

namespace AdapterServiceAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IAKNPListDocProduction
    {
        [OperationContract]
        AKNDocOutput GetPListDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
    }
}
