using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Text;

namespace SToSImotenListProdukciskiNaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----CRRM TEST OKOLINA------");
            Console.WriteLine("System-to-system, od WSDL fajl prevzemen od Lista provajderi");
            Console.WriteLine("1. Imoten list - produkciski");

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
                            Console.WriteLine("Call to PRODUCTION service AKNPopertyListProduction - GetPropertyList");
                            var client = new ImotenListProdukciski.PropertyListClient();

                            var opshtina = ConfigurationManager.AppSettings["Opshtina"];
                            var katastarskaOpshtina = ConfigurationManager.AppSettings["KatastarskaOpshtina"];
                            var brojImotenList = ConfigurationManager.AppSettings["BrojImotenList"];

                            var data = client.GetPropertyList("mio", "katastarservis", opshtina, katastarskaOpshtina, brojImotenList);
                            File.WriteAllText("C:\\LogsFromConsoleApps\\OutputResults\\PropertyListProduction.txt", data.message);
                        }
                        catch (FaultException ex)
                        {
                            var filename = "C:\\LogsFromConsoleApps\\Errors\\FaultPropertyListProduction.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("FaultException when Call to PRODUCTION service PropertyList. {0}", ex.Message);
                            Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            var filename = "C:\\LogsFromConsoleApps\\Errors\\ExceptionPropertyListProduction.txt";
                            File.WriteAllText(filename, ex.Message, Encoding.Unicode);

                            Console.WriteLine("Exception when Call to PRODUCTION service PropertyList(): {0}", ex.Message);
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
