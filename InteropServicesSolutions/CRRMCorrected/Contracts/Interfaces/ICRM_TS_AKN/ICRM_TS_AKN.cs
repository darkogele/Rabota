using System.ServiceModel;
using Contracts.DataAccessLibrary;

namespace Contracts.Interfaces.ICRM_TS_AKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface ICRM_TS_AKN
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        string Get_TS_AKN(string param);
    }
}
