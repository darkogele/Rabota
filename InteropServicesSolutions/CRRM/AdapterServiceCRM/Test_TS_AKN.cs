using System;
using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class Test_TS_AKN : ITest_TS_AKN
    {
        public string Get_TS_AKN(string param)
        {
            var CRM = new CRMOriginalService.XmlWebServiceSoapClient(); ;
            var header = new CRMOriginalService.XmlSoapHeader();
            string output = "";
            try
            {
                output = CRM.ProcessSignedRequest(ref header, param);
            }
            catch (Exception ex)
            {
                CRM_TS_UJP.WriteLog(ex.Message);
                throw ex;
            }
            return output;
        }
    }
}
