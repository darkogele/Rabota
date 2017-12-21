using System;
using System.IO;
using System.ServiceModel;

namespace ImotenListDocumentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CallServiceAndGetResult

            try
            {
                var client = new ImotenListDocTest.AKNPListDocClient();
                var output = client.GetPListDoc("25", "997", "1024", "", false);

                File.WriteAllBytes("C:\\TempPListDocTest.pdf", output.Document);
                File.WriteAllText("C:\\ImotenListDocResult.txt", output.Message);

                Console.WriteLine(output.Message);
                Console.ReadLine();

            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                File.WriteAllText("C:\\ImotenListDocResult.txt", faultException.Message);
                Console.ReadLine();
            }

            #endregion
        }
    }
}
