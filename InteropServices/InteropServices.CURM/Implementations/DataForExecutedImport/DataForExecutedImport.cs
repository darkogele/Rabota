﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.CURM.Interfaces;

namespace InteropServices.CURM.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DataForExecutedImport:IDataForExecutedImport
    {
        public string Get(string department, string municipality, string number)
        {
            return "Test 1 CURM - " + DateTime.Now;
        }
    }
}
