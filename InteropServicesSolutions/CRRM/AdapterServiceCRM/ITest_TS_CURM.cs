using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ITest_TS_CURM
    {
        [OperationContract]
        string Get_TS_CURM(string param);
    }
}
