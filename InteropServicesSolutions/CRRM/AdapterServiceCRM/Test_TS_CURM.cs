using System;
using System.ServiceModel;

namespace AdapterServiceCRM
{
     [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class Test_TS_CURM : ITest_TS_CURM
    {
         public string Get_TS_CURM(string param)
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
