using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.ICRM_TS_CURM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ICRM_TS_CURM
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string Get_TS_CURM(string param);
    }
}
