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
    public class CopyOfCadastralPlan : ICopyOfCadastralPlan
    {
        public string Get(string department, string municipality, string number)
        {
            //call internal service

            return "Test new 1 AKN - " + DateTime.Now;
        }
    }
}
