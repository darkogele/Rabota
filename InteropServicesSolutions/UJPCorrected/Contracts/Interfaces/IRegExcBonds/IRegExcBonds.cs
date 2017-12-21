using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Models.RegExcBonds;

namespace Contracts.Interfaces.IRegExcBonds
{
    [ServiceContract(Namespace = "http://interop.org")]
    public interface IRegExcBonds
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        ExciseBondsOutput GetRegExcBonds(ExciseBondsInput input);
    }
}
