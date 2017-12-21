using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IListOfChangesCUTest
    {
        [OperationContract]
        string GetListOfChangesCU(string param);
    }
}
