using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.AKN.Interfaces;

namespace InteropServices.AKN.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataForInfrastructureFacilities : IDataForInfrastructureFacilities
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test new 2 AKN - " + DateTime.Now;
        }
    }
}
