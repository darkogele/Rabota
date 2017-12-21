using Contracts.DataAccessLibrary;
using System.ServiceModel;

namespace Contracts.Interfaces.Test
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNCPlanDoc
    {
        [OperationContract]
        AKNDocOutput GetCPlanDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);
    }
}
