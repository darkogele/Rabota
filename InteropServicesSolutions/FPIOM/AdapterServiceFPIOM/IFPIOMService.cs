using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceFPIOM
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IFPIOMService
    {
        [OperationContract]
        string GetDataForRetired(string EMBG);
        [OperationContract]
        string GetDataForEnsurees(string EMBG);
    }
}
