using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.MON.Interfaces;

namespace InteropServices.MON.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ConfirmationForRegularStudent:IConfirmationForRegularStudent
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test 1 MON - " + DateTime.Now;
        }
    }
}
