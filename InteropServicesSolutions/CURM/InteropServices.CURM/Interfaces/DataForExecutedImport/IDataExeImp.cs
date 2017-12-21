using InteropServices.CURM.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.CURM.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IDataExeImp
    {
        [OperationContract]
        List<ExecutedImportOutput> GetDataExeImp(ExecutedImportInput input);
    }
}
