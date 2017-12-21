using InteropServices.MVR.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MVR.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ForeignersPermitForPermanentResidence : IForeignersPermitForPermanentResidence
    {
        public ForeignersPermitOutput GetForeignersPermit(string EMBG)
        {
            ForeignersPermitOutput output = new ForeignersPermitOutput();
            //TODO business logic implementation
            return output;
        }
    }
}
