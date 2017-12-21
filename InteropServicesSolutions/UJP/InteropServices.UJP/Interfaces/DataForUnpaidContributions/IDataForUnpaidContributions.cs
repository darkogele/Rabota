using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org")]
    interface IDataForUnpaidContributions
    {
        [OperationContract]
        byte[] GetNP_AVRM(string Date);
        [OperationContract]
        byte[] GetNP_FZOM(string Date);
        [OperationContract]
        byte[] GetNP_FPIOM(string Date);
    }
}
