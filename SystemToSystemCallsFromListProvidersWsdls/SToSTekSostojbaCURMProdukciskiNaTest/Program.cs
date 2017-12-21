using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Implementations.Production;

namespace SToSTekSostojbaCURMProdukciskiNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CURM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Tekovna sostojba (CURM) - produkciski");

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
                                var client = new TekovnaSostojbaCURMProdukciska.CRM_TS_CURMClient();
                                var certUser = ConfigurationManager.AppSettings["UploadCertUser"];
                                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                                store.Open(OpenFlags.ReadOnly);

                                var certificate = store.Certificates
                                     .Find(X509FindType.FindBySubjectName, certUser, false)
                                     .OfType<X509Certificate2>()
                                     .First();

                                var embs = ConfigurationManager.AppSettings["TekovnaSostojbaCURMSystemToSystemEMBS"];
                                var xml = Helper.CRM_TS_CURM_Prod_Request(embs);
                                var param = Helper.SignXml(xml, certificate);


                                var output = client.Get_TS_CURM(param);
                                var xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(output);
                                Console.WriteLine();
                                Console.WriteLine("Service result: ");
                                Console.WriteLine();
                                Console.WriteLine(xmlDoc.InnerXml);
                                Console.WriteLine();

                                var pathToResultFile = ConfigurationManager.AppSettings["PathToResultFile"];
                                File.WriteAllText(pathToResultFile, xmlDoc.InnerXml, Encoding.Unicode);

                                var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
                                var outputDTO = new TekovnaSostojbaCURMProducDTO();
                                var attr = "";

                                var info = new List<CU11>();
                                var owners = new List<CU12>();
                                var actors = new List<CU13>();
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
                                            var serializer = new XmlSerializer(typeof(CU11));
                                            var temp = serializer.Deserialize(stringReader) as CU11;
                                            info.Add(temp);
                                        }
                                        outputDTO.Message = "OK";
                                    }
                                    if (Items[1].HasChildNodes)
                                    {
                                        for (int i = 0; i < Items[1].ChildNodes.Count; i++)
                                        {
                                            var stringReader = new StringReader(Items[1].ChildNodes[i].OuterXml);
                                            var serializer = new XmlSerializer(typeof(CU12));
                                            var temp = serializer.Deserialize(stringReader) as CU12;
                                            owners.Add(temp);
                                        }
                                    }
                                    if (Items[2].HasChildNodes)
                                    {
                                        for (int i = 0; i < Items[2].ChildNodes.Count; i++)
                                        {
                                            var stringReader = new StringReader(Items[2].ChildNodes[i].OuterXml);
                                            var serializer = new XmlSerializer(typeof(CU13));
                                            var temp = serializer.Deserialize(stringReader) as CU13;
                                            actors.Add(temp);
                                        }
                                    }

                                    outputDTO.Info = info;
                                    outputDTO.Actors = actors;
                                    outputDTO.Owners = owners;
                                }
                                foreach (var cu11 in outputDTO.Info)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Uspeshno povikuvanje na servisot.");
                                    Console.WriteLine();
                                    Console.WriteLine("Owner info: " + cu11.LEFullName + "");
                                    Console.WriteLine();
                                }
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                var pathToFaultExceptionFile = ConfigurationManager.AppSettings["PathToFaultExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToFaultExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("FaultException when Call to PRODUCTION service CRM_TS_CURM(). {0}", ex.Message);
                            }
                            catch (Exception ex)
                            {
                                var pathToExceptionFile = ConfigurationManager.AppSettings["PathToExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("Exception when Call to PRODUCTION service CRM_TS_CURM(): {0}", ex.Message);
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
