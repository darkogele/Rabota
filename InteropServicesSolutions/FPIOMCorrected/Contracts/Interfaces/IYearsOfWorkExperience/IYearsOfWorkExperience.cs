using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.Models.YearsOfWorkExperience;

namespace Contracts.Interfaces.IYearsOfWorkExperience
{
    [ServiceContract(Namespace = "http://interop.org/")]
    public interface IYearsOfWorkExperience
    {
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        byte[] GetYWExpReport(string embg);
        [OperationContract]
        [FaultContract(typeof(InteropFault))]
        YearsOfWorkExperienceOutput GetYWExpXML(string embg);
    }
}
