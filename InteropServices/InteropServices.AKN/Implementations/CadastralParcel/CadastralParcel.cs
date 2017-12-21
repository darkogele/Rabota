﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.AKN.Interfaces;

namespace InteropServices.AKN.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CadastralParcel:ICadastralParcel
    {
        public string Get(string department, string municipality, string number)
        {
            //call internal service

            return "Test 3 AKN - " + DateTime.Now;
        }
    }
}
