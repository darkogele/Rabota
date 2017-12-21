using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
                                Console.WriteLine("You can find the created document inside C drive then  LogsFromConsoleApps and then OutputResults have a nice day sir/mam !");
                                File.WriteAllText("C:\\LogsFromConsoleApps\\OutputResults\\DocumentImotenList.txt", data.Message);
                                File.WriteAllBytes("C:\\LogsFromConsoleApps\\OutputResults\\KopijaKatastarskiPlan.pdf", data.Document);

                            }
                            catch (FaultException ex)
                            {
                                var fileName = "C:\\LogsFromConsoleApps\\Errors\\FaultDocumentKopijaKatastraskiPlan.txt";
                                File.WriteAllText(fileName, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(FaultExeption when call to PRODUCTION service AKNCPlanDocProduction {0})", ex.Message);
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                var fileName = "C:\\LogsFromConsoleApps\\Errors\\ExceptionDocumentKopijaKatastraskiPlan.txt";
                                File.WriteAllText(fileName, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(Exception when call to PRODUCTION service AKNCPlanDocProduction {0})", ex.Message);
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
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
