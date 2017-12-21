using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Implementations.Test.Helpers;

namespace SystemToSystemFromListProvidersWsdls
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CURM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Tekovna sostojba (site podatoci)");
            Console.WriteLine("2. Tekovna sostojba (CURM)");
            Console.WriteLine("3. Lista na subjekti od poseben interes");
            Console.WriteLine("4. Lista na subjekti od koi imale promena");

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
                                var client = new CURMTekovnaSostojbaSitePodatociTest.CRMServiceClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
                                //ako e potrebno da se dobie greska od servisot na CRM poradi nevaliden sertifikat vrednosta na sertifikatot postavi ja na var certificate = new X509Certificate2("C:\\TestServise1_VerbaS1MK_IE10.pfx", "123456");

                                var embs = ConfigurationManager.AppSettings["TekovnaSostojbaTestSystemToSystemEMBS"];
                                var xml = Helper.CRM_XML_Test_Request(embs);
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
                                    Console.WriteLine("FaultException when calling service: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Exception when calling service: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("FaultException when Call to TEST service CRMService_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception when Call to TEST service CRMService_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                var client = new CURMTekovnaSostojbaCURMTest.CRM_TS_CURMClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");

                                var embs = ConfigurationManager.AppSettings["TekovnaSostojbaCURMTestSystemToSystemEMBS"];
                                var xml = Helper.CRM_TS_CURM_Test_Request(embs);
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
                                    Console.WriteLine("FaultException when calling service: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Exception when calling service: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("(FaultException when Call to TEST service CRM_TS_CURM_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception when Call to TEST service CRM_TS_CURM_Test(): {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                var client = new CURMListaSubjektiPosebenInteresTest.ListOfSubjectsCUClient();
                                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
                                var embs = ConfigurationManager.AppSettings["ListaSubjektiTestSystemToSystemEMBS"];

                                var xml = Helper.ListOfSubjects_Test_Request(embs, 1, 1);
                                var param = Helper.SignXml(xml, certificate);

                                try
                                {
                                    var output = client.GetSubjectsCU(param);
                                    var xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(output);

                                    var filename = "C:\\ResultSubjectsCUTest.txt";
                                    File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                    Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                    Console.ReadLine();
                                }
                                catch (FaultException ex)
                                {
                                    var filename = "C:\\FaultSubjectsCUTest.txt";
                                    File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                    Console.WriteLine("FaultException when calling service: {0}", ex.Message);
                                    Console.ReadLine();
                                }
                                catch (Exception ex)
                                {
                                    var filename = "C:\\ExceptionSubjectsCUTest.txt";
                                    File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                    Console.WriteLine("Exception when calling service: {0}", ex.Message);
                                    Console.ReadLine();
                                }

                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("FaultException when Call to TEST service ListOfSubjectsCU_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception when Call to TEST service ListOfSubjectsCU_Test(). {0}", ex.Message);
                                Console.ReadLine();
                            }
                            break;
                        }
                    case 4:
                    {
                        try
                        {
                            var client = new CURMListaSubjektiKoiImalePromenaTest.ListOfChangesCUClient();

                            var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");
                            var xml = Helper.ListOfChangesCU_Test_Request("12.08.2016", 0);

                            var param = Helper.SignXml(xml, certificate);

                            try
                            {
                                var output = client.GetListOfChangesCU(param);
                                var xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(output);

                                var filename = "C:\\ResultCUTest.txt";
                                File.WriteAllText(filename, xmlDoc.InnerXml, Encoding.Unicode);

                                Console.WriteLine("Service result: {0}", xmlDoc.InnerXml);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                var filename = "C:\\FaultChangesCUTest.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("FaultException when calling service: {0}", ex.Message);
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                var filename = "C:\\ExceptionChangesCUTest.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("Exception when calling service: {0}", ex.Message);
                                Console.ReadLine();
                            }

                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine("FaultException when Call to TEST service ListOfChangesCU_Test(). {0}", ex.Message);
                            Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception when Call to TEST service ListOfChangesCU_Test(). {0}", ex.Message);
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
