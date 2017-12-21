using System.ServiceModel;
using Contracts.DTO_s.AKNService;

namespace Contracts.Interfaces.IPropertyList
{
     [ServiceContract(Namespace = "http://interop.org/")]
    public interface IPropertyList
    {
        [OperationContract]
        dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);
    }
}
