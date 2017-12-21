using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;

namespace Contracts.Interfaces.IDataForPaidContributionsByEmployeeAndPayer
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IDataForPaidContributionsByEmployeeAndPayer
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetPP_AVRM(string date, int aditionalFile);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetPP_FZOM(string date, int aditionalFile);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetPP_FPIOM(string date, int aditionalFile);
    }
}
