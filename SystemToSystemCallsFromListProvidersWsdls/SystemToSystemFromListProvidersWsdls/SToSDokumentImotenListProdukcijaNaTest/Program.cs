using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SToSDokumentImotenListProdukcijaNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Dokument Imoten List - produkciski");

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
                                Console.WriteLine("Call to PRODUCTION service AKNPListDocProduction -  GetPListDoc");
                                var client = new DocumentImotenList.AKNPListDocProductionClient();

                                var opstina = ConfigurationManager.AppSettings["opstina"];
                                var katastarskaOpstina = ConfigurationManager.AppSettings["katastarskaOpstina"];
                                var showEmb = Convert.ToBoolean(ConfigurationManager.AppSettings["showEmb"]);
                                var brImotenList = ConfigurationManager.AppSettings["brImotenList"];


                                var data = client.GetPListDoc(opstina, katastarskaOpstina, brImotenList, string.Empty, showEmb);
                                File.WriteAllText("C:\\LogsFromConsoleApps\\OutputResults\\DocumentImotenList.txt", data.Message);
                                File.WriteAllBytes("C:\\LogsFromConsoleApps\\OutputResults\\DocumentImotenList.pdf", data.Document);

                            }

                            catch (FaultException ex)
                            {
                                var fileName = "C:\\LogsFromConsoleApps\\Errors\\FaultDocumentImotenList.txt";
                                File.WriteAllText(fileName, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(FaultExeption when call to PRODUCTION service AKNPListDocProduction {0})", ex.Message);
                                Console.ReadLine();
                            }

                            catch (Exception ex)
                            {
                                var filename = "C:\\LogsFromConsoleApps\\Errors\\ExceptionDocumentImotenList.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(Exepction when call to PRODUCTION service AKNPListDocProduction {0})", ex.Message);
                                Console.ReadKey();
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
            Console.WriteLine("Press any key to Continue...");
            Console.ReadKey();
        }
    }
}
