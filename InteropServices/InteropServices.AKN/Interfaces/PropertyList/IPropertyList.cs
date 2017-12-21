using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.AKN.Interfaces
{
     [ServiceContract]
    interface IPropertyList
    {
         [OperationContract]
         string Get(string department, string municipality, string number);
    }
}
