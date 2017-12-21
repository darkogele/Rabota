using System;
using System.ServiceModel;
using InteropServices.AKN.Interfaces;

namespace InteropServices.AKN.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CopyOfCadastralPlan : ICopyOfCadastralPlan
    {
        public string Get(string department, string municipality, string number)
        {
            //call internal service

            return "Test 1 AKN - " + DateTime.Now;
        }
    }
}
