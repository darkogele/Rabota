using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TSAdapterAKN
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface ITSAdapterProduction
    {
         [OperationContract]
         string GetTSProduction(string EMBS);
    }
}
