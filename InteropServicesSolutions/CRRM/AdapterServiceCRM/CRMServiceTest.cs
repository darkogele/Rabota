using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class CRMServiceTest : ICRMServiceTest
    {
        public string GetTekovnaSostojba(string param)
        {
            var CRM = new CRMOriginalService.XmlWebServiceSoapClient();
            var header = new CRMOriginalService.XmlSoapHeader();
            string output = CRM.ProcessSignedRequest(ref header, param);
            return output;
        }

    }
}
