using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    [ServiceContract]
    interface IAKNService
    {
        [OperationContract]
        AKNOriginalService.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList);
    }
}
