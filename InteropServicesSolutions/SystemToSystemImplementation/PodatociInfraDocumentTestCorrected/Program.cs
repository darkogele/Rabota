using System;
using System.IO;
using System.ServiceModel;

namespace PodatociInfraDocumentTestCorrected
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CallServiceAndGetResult

            try
            {
                var client = new InfraOjectDocTest.AKNDataForIFDocClient();
                var output = client.GetDataForIFDoc("25", "167", "55736", "", false);

                File.WriteAllBytes("C:\\TempIfDocTest.pdf", output.Document);
                File.WriteAllText("C:\\InfraDocResult.txt", output.Message);

                //Console.WriteLine(output.Message);
                Console.ReadLine();

            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                File.WriteAllText("C:\\InfraDocResult.txt", faultException.Message);
                Console.ReadLine();
            }

            #endregion

        }
    }
}
