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

namespace SToSTekovnaSostojbaProdukciskiNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CURM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Tekovna sostojba (site podatoci) - produkciski");

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
                            var client = new TekovnaSostojbaOpshtaProdukciska.CRMServiceClient();
                            //ako e potrebno da se dobie greska od servisot na CRM poradi nevaliden sertifikat vrednosta na sertifikatot postavi ja na var certificate = new X509Certificate2("C:\\TestServise1_VerbaS1MK_IE10.pfx", "123456");

                            var certUser = ConfigurationManager.AppSettings["UploadCertUser"];
                            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                            store.Open(OpenFlags.ReadOnly);

                            var certificate = store.Certificates
                                .Find(X509FindType.FindBySubjectName, certUser, false)
                                .OfType<X509Certificate2>()
                                .First();


                            var embs = ConfigurationManager.AppSettings["TekovnaSostojbaSystemToSystemEMBS"];
                            var xml = Helper.CRM_XML_Request(embs);
                            var param = Helper.SignXml(xml, certificate);

                            var outputDTO = new TekovnaSostojbaDTO();
                            var info = new List<CVLEInfo>();
                            var units = new List<CVUnits>();
                            var actors = new List<CVActors>();
                            var owners = new List<CVOwners>();
                            var activities = new List<CVActivities>();
                            var membership = new List<CVMembership>();
                            var founding = new List<CVFounding>();
                            var court = new List<CVLECourt>();


                            var output = client.GetTekovnaSostojba(param);
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
                                        var serializer = new XmlSerializer(typeof (CVLEInfo));
                                        var temp = serializer.Deserialize(stringReader) as CVLEInfo;
                                        info.Add(temp);
                                    }
                                    outputDTO.Message = "OK";
                                }
                                if (Items[1].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[1].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[1].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVUnits));
                                        var temp = serializer.Deserialize(stringReader) as CVUnits;
                                        units.Add(temp);
                                    }
                                }
                                if (Items[2].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[2].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[2].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVActors));
                                        var temp = serializer.Deserialize(stringReader) as CVActors;
                                        actors.Add(temp);
                                    }
                                }
                                if (Items[3].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[3].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[3].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVOwners));
                                        var temp = serializer.Deserialize(stringReader) as CVOwners;
                                        owners.Add(temp);
                                    }
                                }
                                if (Items[4].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[4].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[4].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVActivities));
                                        var temp = serializer.Deserialize(stringReader) as CVActivities;
                                        activities.Add(temp);
                                    }
                                }
                                if (Items[5].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[5].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[5].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVMembership));
                                        var temp = serializer.Deserialize(stringReader) as CVMembership;
                                        membership.Add(temp);
                                    }
                                }

                                if (Items[6].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[6].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[6].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVFounding));
                                        var temp = serializer.Deserialize(stringReader) as CVFounding;
                                        founding.Add(temp);
                                    }
                                }
                                if (Items[7].HasChildNodes)
                                {
                                    for (int i = 0; i < Items[7].ChildNodes.Count; i++)
                                    {
                                        var stringReader = new StringReader(Items[7].ChildNodes[i].OuterXml);
                                        var serializer = new XmlSerializer(typeof (CVLECourt));
                                        var temp = serializer.Deserialize(stringReader) as CVLECourt;
                                        court.Add(temp);
                                    }
                                }
                                outputDTO.Info = info;
                                outputDTO.Units = units;
                                outputDTO.Actors = actors;
                                outputDTO.Owners = owners;
                                outputDTO.Activities = activities;
                                outputDTO.Membership = membership;
                                outputDTO.Founding = founding;
                                outputDTO.Court = court;
                            }

                            foreach (var cvOwnerse in outputDTO.Info)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Uspeshno povikuvanje na servisot.");
                                Console.WriteLine();
                                Console.WriteLine("Owner info: " + cvOwnerse.LEFullName + "");
                                Console.WriteLine();
                            }
                            Console.ReadLine();

                        }
                        catch (FaultException ex)
                        {
                            var pathToFaultExceptionFile = ConfigurationManager.AppSettings["PathToFaultExceptionFile"];
                            Console.WriteLine();
                            File.WriteAllText(pathToFaultExceptionFile, ex.Message, Encoding.Unicode);
                            Console.WriteLine("FaultException when Call to PRODUCTION service CRMService(). {0}", ex.Message);
                        }
                        catch (Exception ex)
                        {
                            var pathToExceptionFile = ConfigurationManager.AppSettings["PathToExceptionFile"];
                            Console.WriteLine();
                            File.WriteAllText(pathToExceptionFile, ex.Message, Encoding.Unicode);
                            Console.WriteLine("Exception when Call to PRODUCTION service CRMService(). {0}", ex.Message);
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
