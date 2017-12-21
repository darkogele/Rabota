using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.MzTV.Interfaces
{
    [ServiceContract]
    interface IMzTVAdapter
    {
        [OperationContract]
        MzTVOriginalService.InteropOutputViewModel ConsPerm(string archiveNumber, string constructionTypeId, string municipalityId, string sendDate = null, string getDocuments = null);
    }
}
