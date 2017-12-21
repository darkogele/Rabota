using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Implementations.Test.Helpers;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CRRM Testing Application - enter 0 to stop");
            Console.WriteLine("1. PROD Adapter - CRM_TS_AKN");
            Console.WriteLine("2. TEST Adapter - CRM_TS_AKN_Test");
            Console.WriteLine("3. PROD Adapter - CRM_TS_CURM");//produkciskite servisi ne se vo moznost da se testiraat zaradi sertifikat
            Console.WriteLine("4. TEST Adapter - CRM_TS_CURM_Test)");
            Console.WriteLine("5. PROD Adapter - CRM_TS_UJP");
            Console.WriteLine("6. TEST Adapter - CRM_TS_UJP_Test");
            Console.WriteLine("7. PROD Adapter - CRMService");
            Console.WriteLine("8. TEST Adapter - CRMService_Test");
            Console.WriteLine("9. PROD Adapter - ListOfChangesCU");
            Console.WriteLine("10. TEST Adapter - ListOfChangesCU_Test");
            Console.WriteLine("11. PROD Adapter - ListOfSubjectsCU");
            Console.WriteLine("12. TEST Adapter - ListOfSubjectsCU_Test");

            while (true)
            {
                Console.Write("Enter value: ");
                int result;
                int.TryParse(Console.ReadLine(), out result);
                var userWantsToExit = false;
                switch (result)
                {
                    case 0:
                        {
                            userWantsToExit = true;
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                var client = new CRM_TS_AKN_Prod.CRM_TS_AKNClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");//sertifikat treba da se smeni za ovaa institucija

                                var xml = Implementations.Helpers.Helper.CRM_TS_CURM_Prod_Request("4641655");
                                var param = Helper.SignXml(xml, certificate);
                                try
                                {
                                    var output = client.Get_TS_AKN(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service CRM_TS_AKN(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                var client = new CRM_TS_AKN_Test.CRM_TS_AKNClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");//sertifikat treba da se smeni za ovaa institucija

                                var xml = Helper.CRM_TS_AKN_Test_Request("4641655");
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.Get_TS_AKN(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service CRM_TS_AKN_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                var client = new CRM_TS_CURM_Prod.CRM_TS_CURMClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");

                                var xml = Implementations.Helpers.Helper.CRM_TS_CURM_Prod_Request("4641655");
                                var param = Helper.SignXml(xml, certificate);
                                try
                                {
                                    var output = client.Get_TS_CURM(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service CRM_TS_CURM(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                var client = new CRM_TS_CURM_Test.CRM_TS_CURMClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");

                                var xml = Helper.CRM_TS_CURM_Test_Request("6646123");
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.Get_TS_CURM(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service CRM_TS_CURM_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 5:
                        {
                            try
                            {
                                var client = new CRM_TS_UJP_Prod.CRM_TS_UJPClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");//sertifikat treba da se smeni za ovaa institucija

                                var xml = Helper.CRM_TS_UJP_Test_Request("6646123");
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.Get_TS_UJP(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service CRM_TS_UJP(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                var client = new CRM_TS_UJP_Test.CRM_TS_UJPClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");//sertifikat treba da se smeni za ovaa institucija

                                var xml = Helper.CRM_TS_UJP_Test_Request("6646123");
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.Get_TS_UJP(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service CRM_TS_UJP_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    //case 7:
                    //    {
                    //        try
                    //        {

                    //        }
                    //        catch (FaultException ex)
                    //        {
                    //            Console.WriteLine("Call to PROD service CRMService(). {0}", ex.Message);
                    //        }
                    //        break;
                    //    }
                    case 8:
                        {
                            try
                            {
                                var client = new CRMService_Test.CRMServiceClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
                                //ako e potrebno da se dobie greska od servisot na CRM poradi nevaliden sertifikat vrednosta na sertifikatot postavi ja na var certificate = new X509Certificate2("C:\\TestServise1_VerbaS1MK_IE10.pfx", "123456");
                                var xml = Helper.CRM_XML_Test_Request("6646123");
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.GetTekovnaSostojba(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);
                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service CRMService_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 9:
                        {
                            try
                            {
                                var client = new ListOfChangesCU_Prod.ListOfChangesCUClient();
                                var certificate = new X509Certificate2("C:\\Users\\developer\\Desktop\\UserCU.pfx", "CR!tst");

                                var xml = Implementations.Helpers.Helper.ListOfChangesCU_Prod_Request("12.07.2016", 1);
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.GetListOfChangesCU(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);

                                    var filename = "C:\\Users\\developer\\Desktop\\ResultCU.txt";
                                    File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (WebException ex)
                                {
                                    Console.WriteLine("first web exception :  " + ex.Message);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    Console.WriteLine("Service result: {0}", ex.Message);

                                    var filename = "C:\\Users\\developer\\Desktop\\FaultCU.txt";
                                    File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                    Console.ReadLine();
                                }

                            }
                            catch (WebException ex)
                            {
                                Console.WriteLine("web exception :  " + ex.Message);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service ListOfChangesCU_Prod(). " + ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 10:
                        {
                            try
                            {
                                var client = new ListOfChangesCU_Test.ListOfChangesCUClient();

                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
                                var xml = Helper.ListOfChangesCU_Test_Request("12.08.2016", 0);

                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.GetListOfChangesCU(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);

                                    //var filename = "C:\\Users\\developer\\Desktop\\ResultCU.txt";
                                    //File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    var filename = "C:\\Users\\developer\\Desktop\\FaultChangesCU.txt";
                                    File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }

                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service ListOfChangesCU_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 11:
                        {
                            try
                            {
                                var client = new ListOfSubjectsCU_Prod.ListOfSubjectsCUClient();
                                var certificate = new X509Certificate2("C:\\Users\\developer\\Desktop\\UserCU.pfx", "CR!tst");

                                var xml = Implementations.Helpers.Helper.ListOfSubjects_Prod_Request("4641655", 1, 1);
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.GetSubjectsCU(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);

                                    var filename = "C:\\Users\\developer\\Desktop\\ResultSubjectsCU.txt";
                                    File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    var filename = "C:\\Users\\developer\\Desktop\\FaultSubjectsCU.txt";
                                    File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }

                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service ListOfSubjectsCU(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 12:
                        {
                            try
                            {
                                var client = new ListOfSubjectsCU_Test.ListOfSubjectsCUClient();
                                var certificate = new X509Certificate2("C:\\Users\\developer\\Desktop\\UserCU.pfx", "CR!tst");

                                var xml = Helper.ListOfSubjects_Test_Request("6509134", 1, 1);
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.GetSubjectsCU(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);

                                    var filename = "C:\\Users\\developer\\Desktop\\ResultSubjectsCU.txt";
                                    File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    var filename = "C:\\Users\\developer\\Desktop\\FaultSubjectsCU.txt";
                                    File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", ex.Message);
                                    Console.ReadLine();
                                }

                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service ListOfSubjectsCU_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input value.");
                            break;
                        }
                }
                if (userWantsToExit)
                {
                    break;
                }
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
