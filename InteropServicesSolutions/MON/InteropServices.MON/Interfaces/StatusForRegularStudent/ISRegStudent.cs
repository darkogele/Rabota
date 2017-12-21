using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MON.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ISRegStudent
    {
        [OperationContract]
        string GetStuS(string EMBG);
    }
}
