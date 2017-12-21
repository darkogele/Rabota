using System.ServiceModel;

namespace AdapterServiceAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IAKNCPlanDocProduction
    {
        [OperationContract]
        AKNDocOutput GetCPlanDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB);
    }
}
