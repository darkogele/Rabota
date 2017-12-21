using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;
using TekovnaSostojbaUJPTestCorrected.Helpers;

namespace TekovnaSostojbaUJPTestCorrected
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Preparation

            var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
            var xml = Helper.CRM_TS_UJP_Test_Request(ConfigurationManager.AppSettings["embs"]);
            var param = Helper.SignXml(xml, certificate);

            #endregion

            #region CallServiceAndGetResult

            try
            {
                var client = new TekovnaSostojbaUJPTest.CRM_TS_UJPClient();
                var output = client.Get_TS_UJP(param);

                //get result 
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(output);
                xmlDoc.Save("C:\\TekovnaSostojbaUJPResult.xml");
                Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                Console.ReadLine();

            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                File.WriteAllText("C:\\TekovnaSostojbaUJP.txt", faultException.Message);
                Console.ReadLine();
            }

            #endregion
        }
    }
}
