using System;
using System.IO;
using System.ServiceModel;

namespace KopijaParcelaDocumentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CallServiceAndGetResult

            try
            {
                var client = new ParcelaDocTest.AKNCPlanDocClient();
                var output = client.GetCPlanDoc("25", "997", "", "1057", false);

                File.WriteAllBytes("C:\\TempCPlanDocTest.pdf", output.Document);
                File.WriteAllText("C:\\ParcelaDocResult.txt", output.Message);

                //Console.WriteLine(output.Message);
                Console.ReadLine();

            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                File.WriteAllText("C:\\ParcelaDocResult.txt", faultException.Message);
                Console.ReadLine();
            }

            #endregion
        }
    }
}
