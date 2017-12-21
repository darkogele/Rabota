using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Institution Name Testing Application - enter 0 to stop");
            Console.WriteLine("1. TEST Adapter - GetData()");
            Console.WriteLine("2. TEST Adapter - GetDataUsingDataContract()");
            Console.WriteLine("3. PROD Adapter - GetData()");
            Console.WriteLine("4. PROD Adapter - GetDataUsingDataContract()");
            Console.WriteLine("5. Call to original service (TEST) - GetEnvironmentName_TestMethod()");
            Console.WriteLine("6. Call to original service (PROD) - GetEnvName_ProdMethod()");

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
                            var testClient = new TestAdapterServiceReference.ServiceClient();
                            Console.WriteLine("Call to Test - GetData. Result: {0}", testClient.GetData());
                            break;
                        }
                    case 2:
                        {
                            var testClient = new TestAdapterServiceReference.ServiceClient();
                            Console.WriteLine("Call to Test - GetDataUsingDataContract. Result: {0}", testClient.GetDataUsingDataContract().StringValue);
                            break;
                        }
                    case 3:
                        {
                            var productionClient = new ProductionAdapterServiceReference.ServiceClient();
                            Console.WriteLine("Call to Production - GetData. Result: {0}", productionClient.GetData());
                            break;
                        }
                    case 4:
                        {
                            var productionClient = new ProductionAdapterServiceReference.ServiceClient();
                            Console.WriteLine("Call to Production - GetDataUsingDataContract. Result: {0}", productionClient.GetDataUsingDataContract().StringValue);
                            break;
                        }
                    case 5:
                        {
                            var originalTestClient = new OriginalTestServiceReference.MockServiceTestClient();
                            Console.WriteLine("Call to original test service - GetEnvironmentName_TestMethod. Result: {0}", originalTestClient.GetEnvironmentName_TestMethod());
                            break;
                        }
                    case 6:
                        {
                            var originalProductionClient = new OriginalProductionServiceReference.MockServiceProdClient();
                            Console.WriteLine("Call to original production service - GetEnvName_ProdMethod. Result: {0}",
                                originalProductionClient.GetEnvName_ProdMethod());
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
