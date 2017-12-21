using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org")]
    interface IBondsDataForCalcAndPayOfContributions
    {
        [OperationContract]
        byte[] GetOU_AVRM(string Date);
        [OperationContract]
        byte[] GetOU_FZOM(string Date);
        [OperationContract]
        byte[] GetOU_FPIOM(string Date);
    }
}
