using System;
using System.IO;
using System.ServiceModel;

namespace ParcelaCorrected
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CallServiceAndGetResult

            try
            {
                var client = new ParcelaTest.CadastrialParcelClient();
                var output = client.GetCParcel("mio", "katastarservis", "1", "1", "2");
                File.WriteAllText("C:\\ParcelaTestResult.txt", output.message);
                Console.WriteLine(output.message);
                Console.ReadLine();
            }
            catch (FaultException faultException)
            {
                Console.WriteLine("Service fault: {0}", faultException.Message);
                Console.ReadLine();
            }

            #endregion
        }
    }
}
