using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.CRRM.Interfaces;

namespace InteropServices.CRRM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataFromTekovnaSostojbaForAKN : IDataFromTekovnaSostojbaForAKN
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test new 1 CRRM - " + DateTime.Now;
        }
    }
}
