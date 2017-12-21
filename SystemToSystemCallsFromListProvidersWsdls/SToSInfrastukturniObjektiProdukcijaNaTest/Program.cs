using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace SToSInfrastukturniObjektiProdukcijaNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Podatoci za infrastukturni Objekti - produkciski");


            while (true)
            {
                Console.WriteLine("Enter a value: ");
                int result;
                int.TryParse(Console.ReadLine(), out result);
                var userWantsToExot = false;

                switch (result)
                {
                    case 0:
                        {
                            userWantsToExot = true;
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                Console.WriteLine("Call to PRODUCTION service AKNDataForIFDocProduction - GetIFdoc");
                                var client = new InfrastukturniObjektiService.AKNDataForIFDocProductionClient();

                                var opstina = ConfigurationManager.AppSettings["opstina"];
                                var katastarskaOpstina = ConfigurationManager.AppSettings["katastarskaOpstina"];
                                var snowEmb = Convert.ToBoolean(ConfigurationManager.AppSettings["snowEmb"]);
                                var brImotenList = ConfigurationManager.AppSettings["brImotenList"];

                                var data = client.GetIFDoc(opstina, katastarskaOpstina, brImotenList, string.Empty, snowEmb);
                                Console.WriteLine("You can find the created document in 'C:\\LogsFromConsoleApps\\OutputResults\\InfrastukturniObjekti.pdf'");
                                var pathToResultFile = ConfigurationManager.AppSettings["PathToResultFile"];
                                var pathToPdfResultFile = ConfigurationManager.AppSettings["PathToPdfResultFile"];
                                File.WriteAllText(pathToResultFile, data.Message);
                                File.WriteAllBytes(pathToPdfResultFile, data.Document);
                                Console.WriteLine("Povikot kon servisot zavrshi.");
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                var pathToFaultExceptionFile = ConfigurationManager.AppSettings["PathToFaultExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToFaultExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("FaultExeption when call to PRODUCTION service AKNDataForIFDocProduction: {0}", ex.Message);
                            }
                            catch (Exception ex)
                            {
                                var pathToExceptionFile = ConfigurationManager.AppSettings["PathToExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("Exception when call to PRODUCTION service AKNDataForIFDocProduction: {0}", ex.Message);
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wong input value.");
                            break;
                        }

                }
                if (userWantsToExot)
                {
                    break;
                }
            }
            Console.WriteLine("Press anby key to Continue...");
            Console.ReadLine();
        }
    }
}
