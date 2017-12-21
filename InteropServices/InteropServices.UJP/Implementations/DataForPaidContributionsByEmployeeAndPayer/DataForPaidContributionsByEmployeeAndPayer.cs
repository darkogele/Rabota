using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.UJP.Interfaces;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataForPaidContributionsByEmployeeAndPayer:IDataForPaidContributionsByEmployeeAndPayer
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test 4 UJP - " + DateTime.Now;
        }
    }
}
