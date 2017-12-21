using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ITest_TS_UJP
    {
        [OperationContract]
        string Get_TS_UJP(string param);
    }
}
