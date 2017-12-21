using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;

namespace Contracts.Interfaces.IDataForUnpaidContributions
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IDataForUnpaidContributions
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetNP_AVRM(string date);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetNP_FZOM(string date);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetNP_FPIOM(string date);
    }
}
