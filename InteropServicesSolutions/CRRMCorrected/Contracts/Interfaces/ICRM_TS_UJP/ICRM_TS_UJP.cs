using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.ICRM_TS_UJP
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ICRM_TS_UJP
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string Get_TS_UJP(string param);
    }
}
