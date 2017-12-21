using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace SToSTekSostojbaAKNProdukciskiNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----AKN TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od specificniot adapter napraven za potrebite na AKN, TSAdapter.AKN.Host.C");
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
                            Console.WriteLine("System to system call to help production adapter on AKN");

                            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true); //sertifikatot ne im e u red za toa go stavam ova za da go ignorira 

                            var client = new TekovnaSostojbaAKNProdukciski.GetTSAKNClient();
                            var embs = ConfigurationManager.AppSettings["TekovnaSostojbaAKNSystemToSystemEMBS"];
                            var data = client.TekSostojba(embs); //6325270

                            var xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(data);
                            Console.WriteLine();
                            Console.WriteLine("Service result: ");
                            Console.WriteLine();
                            Console.WriteLine(xmlDoc.InnerXml);

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
