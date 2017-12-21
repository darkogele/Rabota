using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class ListOfChangesCU : IListOfChangesCU
    {
        public string GetListOfChangesCU(string param)
        {
            var CRM = new ProductionTSOld.XmlWebServiceSoapClient();
            var header = new ProductionTSOld.XmlSoapHeader();
            string output = CRM.ProcessSignedRequest(ref header, param);
            return output;
        }

    }
}
