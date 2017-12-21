using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IListOfSubjectsCU
    {
        [OperationContract]
        string GetSubjectsCU(string param);
    }
}
