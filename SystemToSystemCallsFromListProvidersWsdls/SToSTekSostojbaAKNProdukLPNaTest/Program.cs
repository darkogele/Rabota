using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Implementations.Production;

namespace SToSTekSostojbaAKNProdukLPNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----AKN TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista na provajderi, test portal na AKN");
            Console.WriteLine();
            Console.WriteLine("1. Tekovna sostojba(AKN) - produkciski");

            while (true)
            {
                Console.WriteLine();
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
                            var client = new TekovnaSostojbaAKNProdLP.CRM_TS_AKNClient();

                            var certUser = ConfigurationManager.AppSettings["UploadCertUser"];
                            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                            store.Open(OpenFlags.ReadOnly);

                            var certificate = store.Certificates
                                 .Find(X509FindType.FindBySubjectName, certUser, false)
                                 .OfType<X509Certificate2>()
                                 .First();

                            var embs = ConfigurationManager.AppSettings["TekovnaSostojbaAKNSystemToSystemEMBS"];
                            var xml = Helper.CRM_XML_AKN_Prod_Request(embs);
                            var param = Helper.SignXml(xml, certificate);

                            var output = client.Get_TS_AKN(param);
                            var xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(output);
                            Console.WriteLine();
                            Console.WriteLine("Service result: ");
                            Console.WriteLine();
                            Console.WriteLine(xmlDoc.InnerXml);
                            Console.WriteLine();

                            var pathToResultFile = ConfigurationManager.AppSettings["PathToResultFile"];
                            File.WriteAllText(pathToResultFile, xmlDoc.InnerXml, Encoding.Unicode);
                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            var pathToFaultExceptionFile = ConfigurationManager.AppSettings["PathToFaultExceptionFile"];
                            Console.WriteLine();
                            File.WriteAllText(pathToFaultExceptionFile, ex.Message, Encoding.Unicode);
                            Console.WriteLine("FaultException when Call to PRODUCTION service Tekovna sostojba(AKN): {0}", ex.Message);
                        }
                        catch (Exception ex)
                        {
                            var pathToExceptionFile = ConfigurationManager.AppSettings["PathToExceptionFile"];
                            Console.WriteLine();
                            File.WriteAllText(pathToExceptionFile, ex.Message, Encoding.Unicode);
                            Console.WriteLine("Exception when Call to PRODUCTION service Tekovna sostojba(AKN): {0}", ex.Message);
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
