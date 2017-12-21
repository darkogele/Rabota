using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceFPIOM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,  Namespace = "http://interop.org/")]
    public class FPIOMService: IFPIOMService
    {
        public string GetDataForRetired(string EMBG)
        {
            var FPIOMOriginal = new AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortTypeClient();
            var output = FPIOMOriginal.penzioner1s(EMBG);
            return output;
        }
        public string GetDataForEnsurees(string EMBG)
        {
            var FPIOMOriginal = new AdapterServiceFPIOM.FPIOMOriginalService.WSIO113PortTypeClient();
            var output = FPIOMOriginal.osigurenik1s(EMBG);
            return output;
        }
    }
}
