using System;
using System.ServiceModel;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MON Application for testing services - enter 0 to stop");
            Console.WriteLine("1. PROD Adapter - Data for regular student");
            Console.WriteLine("2. PROD Adapter - Status for regular student");

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
                            var productionClient = new DataForRegularStudent.DRegStudentClient();

                            try
                            {
                                var data = productionClient.GetStuD("1810997495034");
                                Console.WriteLine("Call to PROD service DataForRegularStudent. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service DataForRegularStudent. {0}", ex.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            var productionClient = new StatusForRegularStudent.SRegStudentClient();

                            try
                            {
                                var data = productionClient.GetStuS("1810997495034");
                                Console.WriteLine("Call to PROD service StatusForRegularStudent. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service StatusForRegularStudent. {0}", ex.Message);
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input value.");
                            Console.ReadLine();
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
