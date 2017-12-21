using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;
using TekovnaSostojbaAKNTestCorrected.Helpers;

namespace TekovnaSostojbaAKNTestCorrected
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Preparation

            //AKN za razlika od drugite institucii koristi poseben sertifikat za povikuvanje na Tekovna sostojba AKN duri i na test okolina
            //Ne moze da se dobijat podatoci so sertifikatot UserCU, kako kaj ostanatite institucii

            var certUser = ConfigurationManager.AppSettings["CertUserAKNSystemToSystem"];
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2 certificate = store.Certificates.Find(X509FindType.FindBySubjectName, certUser, false).OfType<X509Certificate2>().First();

            var xml = Helper.CRM_TS_AKN_Test_Request(ConfigurationManager.AppSettings["embs"]);
            var param = Helper.SignXml(xml, certificate);

            #endregion

            #region CallServiceAndGetResult

            try
            {
                var client = new TekovnaSostojbaAKNTest.CRM_TS_AKNClient();
                var output = client.Get_TS_AKN(param);

                //get result 
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(output);
                xmlDoc.Save("C:\\TekovnaSostojbaAKNResult.xml");
                Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                Console.ReadLine();

            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                File.WriteAllText("C:\\TekovnaSostojbaAKN.txt", faultException.Message);
                Console.ReadLine();
            }

            #endregion
        }
    }
}
