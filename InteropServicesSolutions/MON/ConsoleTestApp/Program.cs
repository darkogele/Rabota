using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new ConsoleTestApp.DataForRegularStudentService.DataForRegularStudentClient();
            //try
            //{
            //    var output = client.GetStudentData("1810997495034");
            //    Console.WriteLine(output);
            //}
            //catch (System.Exception excep){ Console.WriteLine(excep); }
            var wsdlURL = "http://192.168.18.2/MONServices/DataForRegularStudent.svc?wsdl";
            if (wsdlURL.EndsWith("?wsdl"))
                wsdlURL = wsdlURL.Replace("?wsdl", "?singlewsdl");
            Console.WriteLine(wsdlURL);
            Console.ReadLine();
        }
    }
}
