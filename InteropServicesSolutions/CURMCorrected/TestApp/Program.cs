using System;
using System.ServiceModel;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CURM Testing Application - enter 0 to stop");
            Console.WriteLine("1. PROD Adapter - GetDataExeExp()");
            Console.WriteLine("2. PROD Adapter - GetDataExeImp()");
            Console.WriteLine("3. PROD Adapter - GetSingleCustDoc()");
            Console.WriteLine("4. TEST Adapter - GetDataExeExpTest()");
            Console.WriteLine("5. TEST Adapter - GetDataExeImpTest()");
            Console.WriteLine("6. TEST Adapter - GetSingleCustDocTest()");

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
                                var productionClient = new DataExeExpProd.DataExeExpClient();
                                var input = new DataExeExpProd.ExecutedExportInput();

                                input.EDB = 5080012502950;

                                var data = productionClient.GetDataExeExp(input);

                                Console.WriteLine("Call to PROD service GetDataExeExp. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataExeExp(). " + ex.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                var productionClient = new DataExeImpProd.DataExeImpClient();
                                var input = new DataExeImpProd.ExecutedImportInput();

                                input.EDB = 5080012502950;

                                var data = productionClient.GetDataExeImp(input);

                                Console.WriteLine("Call to PROD service GetDataExeImp. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetDataExeImp(). " + ex.Message);
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                var productionClient = new SingleCustDocProd.SingleCustDocClient();

                                var data = productionClient.GetSingleCustDoc(2015, 4006008501630, 1010, 2726);

                                Console.WriteLine("Call to PROD service GetSingleCustDoc. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetSingleCustDoc(). " + ex.Message);
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                var productionClient = new DataExeExpTest.DataExeExpClient();
                                var input = new DataExeExpTest.ExecutedExportInput();

                                input.EDB = 5080012500000;

                                var data = productionClient.GetDataExeExp(input);

                                Console.WriteLine("Call to TEST service GetDataExeExpTEST. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataExeExpTEST(). " + ex.Message);
                            }
                            break;
                        }
                    case 5:
                        {
                            try
                            {
                                var productionClient = new DataExeImpTest.DataExeImpClient();
                                var input = new DataExeImpTest.ExecutedImportInput();

                                input.EDB = 5080012502950;

                                var data = productionClient.GetDataExeImp(input);

                                Console.WriteLine("Call to TEST service GetDataExeImpTEST. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetDataExeImpTEST(). " + ex.Message);
                            }
                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                var productionClient = new SingleCustDocTest.SingleCustDocClient();

                                var data = productionClient.GetSingleCustDoc(2015, 4006008501630, 1010, 2726);

                                Console.WriteLine("Call to TEST service GetSingleCustDocTEST. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetSingleCustDocTEST(). " + ex.Message);
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
