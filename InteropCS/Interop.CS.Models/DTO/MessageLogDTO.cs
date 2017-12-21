using System;

namespace Interop.CS.Models.DTO
{
    public class MessageLogDTO
    {
        public Guid TransactionId { get; set; }

        public String Consumer { get; set; }

        public String Provider { get; set; }

        public String RoutingToken { get; set; }

        public String Service { get; set; }

        public String ServiceMethod { get; set; }

        public String Dir { get; set; }

        public DateTime Timestamp { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
