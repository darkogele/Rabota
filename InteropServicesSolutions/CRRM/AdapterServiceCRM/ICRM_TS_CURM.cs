using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ICRM_TS_CURM
    {
        [OperationContract]
        string Get_TS_CURM(string param);
    }
}
