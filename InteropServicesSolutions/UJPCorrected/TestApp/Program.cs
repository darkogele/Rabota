using System;
using System.IO;
using System.ServiceModel;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UJP Testing Application - enter 0 to stop");
            Console.WriteLine("1. PROD Adapter - GetEDB()");
            Console.WriteLine("2. PROD Adapter - GetEMB()");
            Console.WriteLine("3. TEST Adapter - GetEDB()");
            Console.WriteLine("4. TEST Adapter - GetEMB()");
            Console.WriteLine("5. PROD Adapter - GetAnnualRevenuesFZO()");
            Console.WriteLine("6. PROD Adapter - GetAnnualRevenuesKKKS()");
            Console.WriteLine("7. PROD Adapter - GetDocOU_NP_PP()");
            Console.WriteLine("8. PROD Adapter - GetRegExcBonds()");
            Console.WriteLine("9. TEST Adapter - GetRegExcBonds()");

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
                                var productionClient = new DataForEDB.DataForEDBClient();
                                var data = productionClient.GetEDB("6325270");//6325270

                                //var filename = "C:\\Users\\dijana.kostovska\\Desktop\\LengthEDB.txt";
                                //File.WriteAllText(filename, data.Length.ToString());

                                Console.WriteLine("Call to PROD service GetEDB. Service result: {0}" + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetEDB(). {0}" + ex.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                var productionClient = new DataForEMB.DataForEMBClient();
                                var data = productionClient.GetEMB("4030008020265");

                                //var filename = "C:\\Users\\dijana.kostovska\\Desktop\\LengthEMB.txt";
                                //File.WriteAllText(filename, data.Length.ToString());

                                Console.WriteLine("Call to PROD service GetEMB. Service result: {0}", data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetEMB(). {0}", ex.Message);
                            }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                var testClient = new DataForEDBTest.DataForEDBClient();
                                var data = testClient.GetEDB("6325270");//6325270

                                const string filename = "C:\\LengthEDBTest.txt";
                                File.WriteAllText(filename, data.Length.ToString());

                                Console.WriteLine("Call to TEST service GetEDB. Service result: {0}" + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetEDB(). " + ex.Message);
                                const string filename = "C:\\LengthEDBTest.txt";
                                File.WriteAllText(filename, ex.Message);
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                var testClient = new DataForEMBTest.DataForEMBClient();
                                var data = testClient.GetEMB("4030008020265");//4030008020265

                                const string filename = "C:\\LengthEMBTest.txt";
                                File.WriteAllText(filename, data.Length.ToString());

                                Console.WriteLine("Call to TEST service GetEMB. Service result: {0}" + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to TEST service GetEMB(). " + ex.Message);
                                const string filename = "C:\\LengthEMBTest.txt";
                                File.WriteAllText(filename, ex.Message);
                            }
                            break;
                        }
                    case 5:
                        {
                            //try
                            //{
                            //    var productionClient = new DataForAnnualRevenues.DataForAnnualRevenuesClient();
                            //    var data = productionClient.GetAnnualRevenuesFZO("edb", "year");

                            //    Console.WriteLine("Call to PROD service GetAnnualRevenuesFZO. Service result: {0}", data);
                            //    Console.ReadLine();
                            //}
                            //catch (FaultException ex)
                            //{
                            //    Console.WriteLine("Call to PROD service GetAnnualRevenuesFZO(). {0}", ex.Message);
                            //}
                            break;
                        }
                    case 6:
                        {
                            //try
                            //{
                            //    var productionClient = new DataForAnnualRevenues.DataForAnnualRevenuesClient();
                            //    var data = productionClient.GetAnnualRevenuesKKKS("edb", "year");

                            //    Console.WriteLine("Call to PROD service GetAnnualRevenuesKKKS. Service result: {0}", data);
                            //    Console.ReadLine();
                            //}
                            //catch (FaultException ex)
                            //{
                            //    Console.WriteLine("Call to PROD service GetAnnualRevenuesKKKS(). {0}", ex.Message);
                            //}
                            break;
                        }
                    case 7:
                        {
                            //try
                            //{
                            //    var productionClient = new GetDoc_OU_NP_PP.GetDoc_OU_NP_PPClient();
                            //    var data = productionClient.GetDocOU_NP_PP("institution", "service", "date", "additionalInfo");

                            //    Console.WriteLine("Call to PROD service GetDocOU_NP_PP. Service result: {0}", data);
                            //    Console.ReadLine();
                            //}
                            //catch (FaultException ex)
                            //{
                            //    Console.WriteLine("Call to PROD service GetDocOU_NP_PP(). {0}", ex.Message);
                            //}
                            break;
                        }
                    case 8:
                        {
                            try
                            {
                                var productionClient = new RegExcBonds.RegExcBondsClient();
                                var input = new RegExcBonds.ExciseBondsInput();
                                input.EDB = 5080012502950;

                                var data = productionClient.GetRegExcBonds(input);

                                //var filename = "C:\\Users\\dijana.kostovska\\Desktop\\LengthRegExcBonds.txt";
                                //File.WriteAllText(filename, data.ExciseBonds.Length.ToString());

                                Console.WriteLine("Call to PROD service GetRegExcBonds. Service result: " + data);
                                Console.ReadLine();
                            }
                            catch (FaultException ex)
                            {
                                Console.WriteLine("Call to PROD service GetRegExcBonds(). " + ex.Message);
                            }
                            break;
                        }
                    case 9:
                    {
                        try
                        {
                            var testClient = new RegExcBondsTest.RegExcBondsClient();
                            var input = new RegExcBondsTest.ExciseBondsInput();
                            input.EDB = 5080012500000;//5080012502950

                            var data = testClient.GetRegExcBonds(input);

                            const string filename = "C:\\LengthRegExcBondsTest.txt";
                            File.WriteAllText(filename, data.ExciseBonds.Length.ToString());

                            Console.WriteLine("Call to TEST service GetRegExcBonds. Service result: " + data);
                            Console.ReadLine();
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine("Call to TEST service GetRegExcBonds(). " + ex.Message);
                            const string filename = "C:\\LengthRegExcBondsTest.txt";
                            File.WriteAllText(filename, ex.Message);
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
