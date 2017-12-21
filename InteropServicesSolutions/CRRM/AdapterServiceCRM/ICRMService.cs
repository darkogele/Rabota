using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ICRMService
    {
        [OperationContract]
        string GetTekovnaSostojba(string param);
    }
}
