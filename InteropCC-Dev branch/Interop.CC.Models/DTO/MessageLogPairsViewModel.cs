using System;

namespace Interop.CC.Models.DTO
{
    public class MessageLogPairsViewModel
    {
        public Guid TransactionId { get; set; }

        public string Consumer { get; set; }

        public string ConsumerName { get; set; }

        public string Provider { get; set; }
        public string ProviderName { get; set; }

        public string RoutingTokenName { get; set; }

        public string RoutingToken { get; set; }

        public string Service { get; set; }

        public string ServiceMethod { get; set; }

        public DateTime? RequestTimestamp { get; set; }

        public DateTime? ResponseTimestamp { get; set; }
        public bool HasResponse { get; set; }
        public bool HasRequest { get; set; }
    }
}
