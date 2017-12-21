using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;
using TekovnaSostojbaSitePodatociTestCorrected.Helpers;

namespace TekovnaSostojbaSitePodatociTestCorrected
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Preparation

            var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
            var xml = Helper.CRM_XML_Test_Request(ConfigurationManager.AppSettings["embs"]);
            var param = Helper.SignXml(xml, certificate);

            #endregion

            #region CallServiceAndGetResult

            try
            {
                var client = new TekovnaSostojbaSitePodatociTest.CRMServiceClient();
                var output = client.GetTekovnaSostojba(param);

                //get result 
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(output);
                xmlDoc.Save("C:\\TekovnaSostojbaSitePodatociResult.xml");
                Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                Console.ReadLine();

            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                File.WriteAllText("C:\\TekovnaSostojbaSitePodatoci.txt", faultException.Message);
                Console.ReadLine();
            }

            #endregion
        }
    }
}
