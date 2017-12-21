using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConsumer.EvalServiceReference;

namespace ClientConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientEval = new EvalServiceClient("CustomBinding_IEvalService");

            var eval = new Eval
            {
                Comments = "InterOP",
                Id = Guid.NewGuid().ToString(),
                Submitter = "Korvus",
                TimeSubmitted = DateTime.Now
            };

            try
            {
                clientEval.SubmitEval(eval);
                Console.WriteLine("uspesno!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
