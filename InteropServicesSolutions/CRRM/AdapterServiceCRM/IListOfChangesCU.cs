using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IListOfChangesCU
    {
        [OperationContract]
        string GetListOfChangesCU(string param);
    }
}
