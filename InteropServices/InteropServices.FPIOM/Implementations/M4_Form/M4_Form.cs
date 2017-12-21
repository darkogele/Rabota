using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.FPIOM.Interfaces;

namespace InteropServices.FPIOM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class M4_Form:IM4_Form
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test 2 FPIOM - " + DateTime.Now;
        }
    }
}
