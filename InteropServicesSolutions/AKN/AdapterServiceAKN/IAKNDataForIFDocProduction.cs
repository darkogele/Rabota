using System.ServiceModel;

namespace AdapterServiceAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNDataForIFDocProduction
    {
        [OperationContract]
        AKNDocOutput GetIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
    }
}
