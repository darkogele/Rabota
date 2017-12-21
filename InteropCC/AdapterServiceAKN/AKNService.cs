using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AKNService: IAKNService
    {
        public AKNOriginalService.dzgr GetPropertyList(string username, string password, string opstina, string katastarskaOpstina, string brImotenList)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
             ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
            var AKNClient = new AKNOriginalService.Service_MACEDONIAN_CADASTRESoapClient();
            var output = AKNClient.VRATIPODATOCI_STRUKTURA_DZGR(username,password,opstina,katastarskaOpstina,brImotenList);
            return output;
        }
    }
}
