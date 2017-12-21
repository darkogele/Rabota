using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IAKNCPlanDocProduction
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNCPlanDocProduction
    {
        [OperationContract]
        AKNDocOutput GetCPlanDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);   
    }
}
