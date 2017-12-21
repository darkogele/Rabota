﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.FPIOM.Interfaces
{
    [ServiceContract(Namespace = "http://interop.org/")]
    interface IInsuredStatus
    {
        [OperationContract]
        string GetInsuredStatus(string EMBG);
    }
}