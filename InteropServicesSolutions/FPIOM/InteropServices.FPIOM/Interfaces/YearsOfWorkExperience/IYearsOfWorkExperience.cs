using InteropServices.FPIOM.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.FPIOM.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IYearsOfWorkExperience
    {
        [OperationContract]
        byte[] GetYWExpReport(string EMBG);
        [OperationContract]
        YearsOfWorkExperienceOutput GetYWExpXML(string EMBG);
    }
}
