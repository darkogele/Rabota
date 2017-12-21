using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Models.GetDoc_OU_NP_PP;

namespace Contracts.Interfaces.IGetDoc_OU_NP_PP
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IGetDoc_OU_NP_PP
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        DocOuNpPpOutputData GetDocOU_NP_PP(string institution, string service, string date, string additionalInfo);
    }
}
