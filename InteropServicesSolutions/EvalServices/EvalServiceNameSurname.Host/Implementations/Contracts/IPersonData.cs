using System.Collections.Generic;
using System.ServiceModel;
using Implementations.Models;

namespace Implementations.Contracts
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IPersonData
    {
        [OperationContract]
        List<Person> GetPersonData(bool getData);
    }
}
