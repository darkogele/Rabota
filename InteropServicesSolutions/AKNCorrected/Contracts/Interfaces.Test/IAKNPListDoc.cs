using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.Test
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IAKNPListDoc
    {
        [OperationContract]
        AKNDocOutput GetPListDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEMB);
    }
}
