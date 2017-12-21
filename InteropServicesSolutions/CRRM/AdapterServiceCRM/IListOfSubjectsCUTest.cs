using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IListOfSubjectsCUTest
    {
        [OperationContract]
        string GetListOfSubjectsCU(string param);
    }
}
