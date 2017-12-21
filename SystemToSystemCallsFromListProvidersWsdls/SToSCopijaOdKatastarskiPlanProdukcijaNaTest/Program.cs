using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace SToSCopijaOdKatastarskiPlanProdukcijaNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1.Kopija od katastarski plan - produkciski");

            while (true)
            {
                Console.WriteLine("Enter value: ");
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
                                Console.WriteLine("Call to PRODUCXTION service AKNCPlanDocProduction - GetCPlanDoc");
                                var client = new KopijaKatastarskiPlan.AKNCPlanDocProductionClient();

                                var opstina = ConfigurationManager.AppSettings["opstina"];
                                var katastarskaOpstina = ConfigurationManager.AppSettings["katastarskaOpstina"];
                                var showEmb = Convert.ToBoolean(ConfigurationManager.AppSettings["showEmb"]);
                                var brojParcela = ConfigurationManager.AppSettings["brojParcela"];

                                var data = client.GetCPlanDoc(opstina, katastarskaOpstina, string.Empty, brojParcela,  showEmb);
                                Console.WriteLine("You can find the created document in 'C:\\LogsFromConsoleApps\\OutputResults\\KopijaKatastarskiPlan.pdf'");
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
                                Console.WriteLine("FaultExeption when call to PRODUCTION service AKNCPlanDocProduction: {0}", ex.Message);
                            }
                            catch (Exception ex)
                            {
                                var pathToExceptionFile = ConfigurationManager.AppSettings["PathToExceptionFile"];
                                Console.WriteLine();
                                File.WriteAllText(pathToExceptionFile, ex.Message, Encoding.Unicode);
                                Console.WriteLine("Exception when call to PRODUCTION service AKNCPlanDocProduction: {0}", ex.Message);
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
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
