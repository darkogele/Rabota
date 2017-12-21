﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MzTV.Interfaces
{
    [ServiceContract]
    interface IConstructionOrOccupancyPermit
    {
        [OperationContract]
        string Get(string department, string municipality, string number);
    }
}
