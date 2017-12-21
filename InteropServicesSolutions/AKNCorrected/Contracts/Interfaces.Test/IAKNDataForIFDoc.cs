using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.Test
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNDataForIFDoc
    {
        [OperationContract]
        AKNDocOutput GetDataForIFDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEmb);
    }
}
