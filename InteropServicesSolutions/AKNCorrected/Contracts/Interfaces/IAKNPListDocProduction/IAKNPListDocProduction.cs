using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.IAKNPListDocProduction
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNPListDocProduction
    {
        [OperationContract]
        AKNDocOutput GetPListDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);
    }
}
