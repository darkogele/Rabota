using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org")]
    interface IDataForPaidContributionsByEmployeeAndPayer
    {
        [OperationContract]
        byte[] GetPP_AVRM(string Date, int AditionalFile);
        [OperationContract]
        byte[] GetPP_FZOM(string Date, int AditionalFile);
        [OperationContract]
        byte[] GetPP_FPIOM(string Date, int AditionalFile);
    }
}
