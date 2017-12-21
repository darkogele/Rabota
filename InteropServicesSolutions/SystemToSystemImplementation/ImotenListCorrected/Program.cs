using System;
using System.IO;
using System.ServiceModel;

namespace ImotenListCorrected
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CallServiceAndGetResult

            try
            {
                var client = new ImotenListTest.PropertyListClient();
                var output = client.GetPropertyList("mio", "katastarservis", "1", "1", "1");

                File.WriteAllText("C:\\ImotenListTestResult.txt",output.message);
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
