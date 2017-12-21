using ClientToWCF.ESReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientToWCF.Helpers;

namespace ClientToWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new EvalServiceClient("WSHttpBinding_IEvalService");
            var cb = new MessageInspectorBehavior();

            // Add the custom behaviour to the list of service behaviours.
            client.Endpoint.Behaviors.Add(cb);

            cb.OnMessageInspected += (src, e) =>
            {
                //if (e.MessageInspectionType == eMessageInspectionType.Request) request = e.Message;
                //else response = e.Message;
            };

            var eval = new Eval
            {
                Comments = "InterOP",
                Id = Guid.NewGuid().ToString(),
                Submitter = "Daniel",
                TimeSubmitted = DateTime.Now
            };


            client.SubmitEval(eval);


            var allEvall = client.GetEvals();

            var test = 0;
        }
    }
}
