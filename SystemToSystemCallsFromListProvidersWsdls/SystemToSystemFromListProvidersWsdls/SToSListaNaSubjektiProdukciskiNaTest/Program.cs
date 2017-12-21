using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Implementations.Production;

namespace SToSListaNaSubjektiProdukciskiNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CURM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Lista na subjekti od poseben interes - produkciski");

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
                                var client = new ListaNaSubjektiInteres.ListOfSubjectsCUClient();
                                var certUser = ConfigurationManager.AppSettings["UploadCertUser"];
                                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                                store.Open(OpenFlags.ReadOnly);

                                var certificate = store.Certificates
                                     .Find(X509FindType.FindBySubjectName, certUser, false)
                                     .OfType<X509Certificate2>()
                                     .First();

                                var embg = ConfigurationManager.AppSettings["ListaSubjektiInteresSystemToSystemEMBG"];
                                var xml = Helper.ListOfSubjects_Prod_Request(embg, 1, 1);
                                var param = Helper.SignXml(xml, certificate);


                                var output = client.GetSubjectsCU(param);
                                var xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(output);
                                Console.WriteLine();
                                Console.WriteLine("Service result: ");
                                Console.WriteLine();
                                Console.WriteLine(xmlDoc.InnerXml);
                                Console.WriteLine();

                                var pathToResultFile = ConfigurationManager.AppSettings["PathToResultFile"];
                                File.WriteAllText(pathToResultFile, xmlDoc.InnerXml, Encoding.Unicode);

                                var outputDTO = new ListaNaSubjektiDTO();
                                var info = new ListaNaSubjektiInfo();
                                var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
                                var attr = string.Empty;
                                if (crmResponse[0].Attributes["Message"] != null)
                                {
                                    attr = crmResponse[0].Attributes["Message"].Value;
                                    Console.WriteLine("Service error while calling service: {0}", attr);
                                    Console.ReadLine();
                                }
                                else
                                {
                                    var Items = xmlDoc.GetElementsByTagName("CrmResultItems");
                                    if (Items[0].HasChildNodes)
                                    {
                                        for (int i = 0; i < Items[0].ChildNodes.Count; i++)
                                        {
                                            var stringReader = new StringReader(Items[0].ChildNodes[i].OuterXml);
                                            var serializer = new XmlSerializer(typeof(ListaNaSubjektiInfo));
                                            var temp = serializer.Deserialize(stringReader) as ListaNaSubjektiInfo;
                                            info = temp;
                                        }
                                    }
                                    outputDTO.Info = info;
                                }
                                Console.WriteLine();
                                Console.WriteLine("Uspeshno povikuvanje na servisot.");
                                Console.WriteLine();
                                Console.WriteLine("Service message info: " + outputDTO.Info.InfoMessage + "");
                                Console.WriteLine();
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                var pathToFaultExceptionFile = ConfigurationManager.AppSettings["PathToFaultExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToFaultExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("Fault exception when Call to PRODUCTION service ListOfSubjects():  {0}", ex.Message);
                            }
                            catch (Exception ex)
                            {
                                var pathToExceptionFile = ConfigurationManager.AppSettings["PathToExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("Exception when Call to PRODUCTION service ListOfSubjects(): {0}", ex.Message);
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
