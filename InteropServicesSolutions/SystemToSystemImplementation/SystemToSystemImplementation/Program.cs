using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;
using SystemToSystemImplementation.Helpers;

namespace SystemToSystemImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Preparation

            var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
            var xml = Helper.CRM_XML_Test_Request("6646123");
            var param = Helper.SignXml(xml, certificate);

            #endregion

            #region CallServiceAndGetResult

            try
            {
                //client
                var client = new SToSRef.CRMServiceClient();

                //call service methods
                var result = client.GetTekovnaSostojba(param);

                //get result 
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                xmlDoc.Save("C:\\ServiceResult.xml");
                Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                Console.ReadLine();
            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                Console.ReadLine();
            }

            #endregion

        }
    }
}
