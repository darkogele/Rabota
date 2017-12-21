using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class ListOfSubjectsCU : IListOfSubjectsCU
    {
        public string GetSubjectsCU(string param)
        {
            var CRM = new ProductionTSOld.XmlWebServiceSoapClient();
            var header = new ProductionTSOld.XmlSoapHeader();
            string output = CRM.ProcessSignedRequest(ref header, param);
            return output;
        }

    }
}
