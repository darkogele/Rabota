using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.CC.CrossCutting;

namespace ExternalCCConsoleTestRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var numberOfRequests = AppSettings.Get<int>("NumberOfRequests");
                for (int i = 0; i <= numberOfRequests; i++)
                {
                    var mim2Client = new M1M2ServiceRef.M1M2ServiceSoapClient();
                    var person = new M1M2ServiceRef.Person();
                    person.FirstName = "Dijana";
                    person.SurName = "Kostovska";
                    mim2Client.FullName(person);

                    //Calling methods from MIM2 service
                    var callGetM1M2 = AppSettings.Get<bool>("CallMethodGetM1M2");
                    var callGetM1M2ById = AppSettings.Get<bool>("CallMethodGetM1M2ById");

                    //Call method GetM1M2
                    if (callGetM1M2)
                    {
                        var responseM1M2 = mim2Client.GetM1M2("1402986470039", null, new DateTime(2014, 1, 1));
                        foreach (var m1M2 in responseM1M2)
                        {
                            Console.WriteLine("GetM1M2: " + m1M2.CompanyName + " " + m1M2.CompanyAddress);
                        }
                    }

                    //Call method GetM1M2ById
                    if (callGetM1M2ById)
                    {
                        var responseGetM1M2ById = mim2Client.GetM1M2ById(1);
                        Console.WriteLine("GetM1M2ById: " + responseGetM1M2ById.CompanyName);
                    }

                    Console.WriteLine("Uspesen povik");
                    Console.ReadLine();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("error:" + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
