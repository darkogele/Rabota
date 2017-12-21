using System;
using TestAKNAdapterForParcel.AKNPListDocTest;
using TestAKNAdapterForParcel.OriginalAKNService;

namespace TestAKNAdapterForParcel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing Cadastral parcel
            try
            {
                //Testing throught out adapter
                var client = new AKNAdapterService.AKNServiceClient();
                var output = client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");


                //Testing with real institution service, without our adapter
                //var client = new Service_MACEDONIAN_CADASTRESoapClient();
                //var output = client.ReturnParcela_7("mio", "katastarservis", "1", "1", "2");

                Console.WriteLine(output);
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Se sluci greska vo client.GetCadastrialParcel: " + exception);
                Console.ReadLine();
            }
        }
    }
}
