using Contracts.DataAccessLibrary;
using System.ServiceModel;

namespace Contracts.Interfaces.IAKNDataForIFDocProduction
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNDataForIFDocProduction
    {
        [OperationContract]
        AKNDocOutput GetIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);
    }
}
