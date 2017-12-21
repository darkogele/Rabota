using System;
using System.Configuration;

namespace LoadTestClient
{
    public class LogItemMessage
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class LogItemModel
    {
        public LogItemMessage Response { get; set; }
        public LogItemMessage Request { get; set; }
        public bool WasCallSuccessful { get; set; }

        public LogItemModel()
        {
            Request = new LogItemMessage();
            Response = new LogItemMessage();
        }

        public int CallDuration
        {
            get { return (int) Math.Round((Response.TimeStamp - Request.TimeStamp).TotalMilliseconds); }
        }

        public override string ToString()
        {
            var result = "OK";
            if (!WasCallSuccessful)
            {
                result = "ERROR";
            }
            return "RequestID: " + Request.Id + " " + Request.TimeStamp.ToString("hh:mm:ss.fff tt") + " - " +
                   "ResponseID: " + Response.Id + " " + Response.TimeStamp.ToString("hh:mm:ss.fff tt") + " Result: " + result + " Total: " + CallDuration + "ms.";
        }
    }
}
