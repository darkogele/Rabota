﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.FPIOM.Interfaces
{
    [ServiceContract]
    interface IM4_Form
    {
        [OperationContract]
        string Get(string department, string municipality, string number);
    }
}
