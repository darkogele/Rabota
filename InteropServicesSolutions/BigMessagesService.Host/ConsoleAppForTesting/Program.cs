using System;
using System.IO;
using System.ServiceModel;
using System.Text;
using ConsoleAppForTesting.BigDataService;

namespace ConsoleAppForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var service = new BigDataClient();
                //using (var fileStream = new FileStream("d:\\temp\\bigfile.pdf", FileMode.Create))
                //{
                //    service.GetLargeObject().CopyTo(fileStream);
                //}
                var output = service.GetLargeObjectDoc("doc");
                if (output.HasDocument)
                {
                    Console.WriteLine("ima dokument.");
                }
                Console.ReadLine();
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Finished. Error FaultException occured:");
                //File.WriteAllText("C:\\Users\\Administrator\\Desktop\\TempKPDocError.txt", ex.Message, Encoding.Unicode);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Finished. Error occured:");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
