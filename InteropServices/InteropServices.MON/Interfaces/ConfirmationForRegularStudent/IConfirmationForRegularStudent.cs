using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MON.Interfaces
{
    [ServiceContract]
    interface IConfirmationForRegularStudent
    {
        [OperationContract]
        string Get(string department, string municipality, string number);
    }
}
