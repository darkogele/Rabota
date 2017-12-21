using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Interfaces
{
     [ServiceContract]
    interface IBondsDataForCalcAndPayOfContributions
    {
         [OperationContract]
         string Get(string department, string municipality, string number);
    }
}
