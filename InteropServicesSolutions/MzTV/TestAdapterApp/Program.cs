using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAdapterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MzTVadapterservice.MzTVAdapterClient();
            var output = client.GetConstructionPermitData("УП 221/2014", "2", "108", "2014-09-11", "y");
        }
    }
}
