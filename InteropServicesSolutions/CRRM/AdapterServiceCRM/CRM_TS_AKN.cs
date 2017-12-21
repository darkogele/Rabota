using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AdapterServiceCRM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class CRM_TS_AKN : ICRM_TS_AKN
    {
        public string Get_TS_AKN(string param)
        {
            var CRM = new ProductionTSOld.XmlWebServiceSoapClient();
            ProductionTSOld.XmlSoapHeader header = new ProductionTSOld.XmlSoapHeader();
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
