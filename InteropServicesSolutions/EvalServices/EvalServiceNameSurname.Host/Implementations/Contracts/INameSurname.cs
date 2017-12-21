using System.ServiceModel;

namespace Implementations.Contracts
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface INameSurname
    {
        [OperationContract]
        string GetNameSurname(string name, string surname);
    }
}
