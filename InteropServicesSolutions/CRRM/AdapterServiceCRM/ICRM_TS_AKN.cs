using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ICRM_TS_AKN
    {
        [OperationContract]
        string Get_TS_AKN(string param);
       
    }
}
