using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ICRMServiceTest
    {
        [OperationContract]
        string GetTekovnaSostojba(string param);
    }
}
