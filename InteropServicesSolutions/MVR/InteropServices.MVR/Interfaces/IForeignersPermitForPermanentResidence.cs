using InteropServices.MVR.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MVR.Interfaces
{
    [ServiceContract]
    interface IForeignersPermitForPermanentResidence
    {
        [OperationContract]
        ForeignersPermitOutput GetForeignersPermit(string EMBG);
    }
}
