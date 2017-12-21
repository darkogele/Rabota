using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InteropServices.MzTV.Interfaces;

namespace InteropServices.MzTV.Implementations
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MzTVAdapter : IMzTVAdapter
    {
        public MzTVOriginalService.InteropOutputViewModel ConsPerm(string archiveNumber, string constructionTypeId, string municipalityId, string sendDate = null, string getDocuments = null)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;//sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
            var MzTVClient = new MzTVOriginalService.InteropWebServiceSoapClient();
            MzTVOriginalService.InteropInputViewModel input = new MzTVOriginalService.InteropInputViewModel();
            string archNumber = archiveNumber;
            DateTime sDate = new DateTime();
            if (sendDate != null)
                sDate = DateTime.Parse(sendDate);
            int consTypreId = Int32.Parse(constructionTypeId);
            int munId = Int32.Parse(municipalityId);
            bool getDoc = false;
            if (getDocuments != null && getDocuments == "y")
                getDoc = true;
            input.ArchiveNumber = archNumber;
            if (sendDate != null)
                input.SendDate = sDate;
            input.ConstructionTypeId = consTypreId;
            input.MunicipalityId = munId;
            if (getDocuments != null)
                input.GetDocuments = getDoc;
            var output = MzTVClient.GetRequestDetails(input);
            return output;
        }
    }
}
