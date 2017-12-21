using InteropServices.UJP.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org")]
    interface IEDB_EMB
    {
        [OperationContract]
        List<EDB_EMB_Output> GetEDB(string input);
        [OperationContract]
        List<EDB_EMB_Output> GetEMB(string input);
    }
}
