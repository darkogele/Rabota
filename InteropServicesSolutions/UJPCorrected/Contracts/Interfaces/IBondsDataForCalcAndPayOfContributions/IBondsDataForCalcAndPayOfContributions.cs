using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;

namespace Contracts.Interfaces.IBondsDataForCalcAndPayOfContributions
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IBondsDataForCalcAndPayOfContributions
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetOU_AVRM(string date);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetOU_FZOM(string date);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetOU_FPIOM(string date);
    }
}
