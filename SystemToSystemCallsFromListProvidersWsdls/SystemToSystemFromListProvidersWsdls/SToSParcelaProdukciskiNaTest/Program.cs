using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace SToSParcelaProdukciskiNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Parcela - produkciski");

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
                                Console.WriteLine("Call to PRODUCTION service AKNPropertyList - CadastralParcel");
                                var client = new ParcelaProdukciski.CadastrialParcelClient();

                                var opshtina = ConfigurationManager.AppSettings["Opshtina"];
                                var katastarskaOpshtina = ConfigurationManager.AppSettings["KatastarskaOpshtina"];
                                var brojkatastarskaParcela = ConfigurationManager.AppSettings["BrojkatastarskaParcela"];

                                var data = client.GetCParcel("mio", "katastarservis", opshtina, katastarskaOpshtina, brojkatastarskaParcela);
                                File.WriteAllText("C:\\LogsFromConsoleApps\\OutputResults\\ParcelaProduction.txt", data.message);
                            }

                            catch (FaultException ex)
                            {
                                var filename = "C:\\LogsFromConsoleApps\\Errors\\FaultParcelaProduction.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("(FaultException when Call to PRODUCTION service CadastralParcel. {0}", ex.Message);
                                Console.ReadLine();
                            }

                            catch (Exception ex)
                            {
                                var filename = "C:\\LogsFromConsoleApps\\Errors\\ExceptionParcelaProduction.txt";
                                File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                                Console.WriteLine("Exception when Call to PRODUCTION service CadastralParcel(): {0}", ex.Message);
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
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
