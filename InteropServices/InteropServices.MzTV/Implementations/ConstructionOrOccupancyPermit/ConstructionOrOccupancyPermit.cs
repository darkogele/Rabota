﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.MzTV.Interfaces;

namespace InteropServices.MzTV.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ConstructionOrOccupancyPermit:IConstructionOrOccupancyPermit
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test 1 MzTV - " + DateTime.Now;
        }
    }
}
