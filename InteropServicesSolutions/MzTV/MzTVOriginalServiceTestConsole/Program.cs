using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzTVOriginalServiceTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
                var client = new MZTVRef.InteropWebServiceSoapClient();
                MZTVRef.InteropInputViewModel input = new MZTVRef.InteropInputViewModel();
                input.ArchiveNumber = "УП 221/2014";
                input.ConstructionTypeId = 2;
                input.GetDocuments = false;
                input.MunicipalityId = 108;
                input.SendDate = new DateTime(2014, 09, 11);
                var output = client.GetRequestDetails(input);

                Console.WriteLine("uspesno");
                Console.WriteLine("ConstructionAddress" + output.ConstructionAddress);
                Console.WriteLine("ConstructionDescription" + output.ConstructionDescription);
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Greska" + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
