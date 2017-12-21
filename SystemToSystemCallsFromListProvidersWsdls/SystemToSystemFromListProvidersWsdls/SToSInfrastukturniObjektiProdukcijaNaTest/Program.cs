using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SToSInfrastukturniObjektiProdukcijaNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Infrastukturni Objekti - produkciski");


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
                                Console.WriteLine("Call to PRODUCTIOn service AKNDataForIFDocProduction - GetIFdoc");
                                var client = new InfrastukturniObjektiService.AKNDataForIFDocProductionClient();

                                var opstina = ConfigurationManager.AppSettings["opstina"];
                                var katastarskaOpstina = ConfigurationManager.AppSettings["katastarskaOpstina"];
                                var snowEmb = Convert.ToBoolean(ConfigurationManager.AppSettings["snowEmb"]);
                                var brImotenList = ConfigurationManager.AppSettings["brImotenList"];

                                var data = client.GetIFDoc(opstina, katastarskaOpstina, brImotenList, string.Empty, snowEmb);
                                Console.WriteLine("You can find the created document inside C drive then  LogsFromConsoleApps then OutputResults have a nice day sir/mam !");
                                File.WriteAllText("C:\\LogsFromConsoleApps\\OutputResults\\InfrastukturniObjekti.txt", data.Message);
                                File.WriteAllBytes("C:\\LogsFromConsoleApps\\OutputResults\\InfrastukturniObjekti.pdf", data.Document);
;

                            }
                            catch (FaultException ex)
                            {
                                var fileName = "C:\\LogsFromConsoleApps\\Errors\\FaultInfrastukturniObjekti.txt";
                                File.WriteAllText(fileName, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(FaultExeption when call to PRODUCTION service  {0})", ex.Message);
                                Console.ReadLine();

                            }

                            catch (Exception ex)
                            {

                                var filename = "C:\\LogsFromConsoleApps\\Errors\\ExceptionInfrastukturniObjekti.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(Exepction when call to PRODUCTION service  {0})", ex.Message);
                                Console.ReadKey();
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
