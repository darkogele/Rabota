using System;
using System.ServiceModel;
using InteropServices.UJP.Interfaces;

namespace InteropServices.UJP.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TaxIdNumberOfSubject:ITaxIdNumberOfSubject
    {
         public string Get(string department, string municipality, string number)
         {
             return "Test 1 UJP - " + DateTime.Now;
         }
    }
}
