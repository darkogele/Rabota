using System;
using ConsumeAKNService.AKNService;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace ConsumeAKNService
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AdapterServiceCRMRef.CRMServiceClient();
            var output = client.GetTekovnaSostojba("6646123");

            //var client = new AdapterServiceFPIOMRef.FPIOMServiceClient();
            //var temp = client.GetDataForRetired("0606949469013");
            //var temp1 = client.GetDataForEnsurees("2811985469003");
            //var client = new AdapterServiceAKNRef.AKNServiceClient();

            //var output = client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");

            //var client = new AKNService.Service_MACEDONIAN_CADASTRESoapClient();

            //var retVal = client.ReturnOps_1();

            //if(retVal != null)
            //{
            //    foreach (opstina opstina in retVal.nizops)
            //    {
            //        Console.WriteLine(opstina.nazivopstina);
            //    }
                
            //    Console.WriteLine("Nema poveke opstini");
            //}

            //Console.ReadLine();

        }
       
    }
}
