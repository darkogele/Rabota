using System;

namespace Interop.CS.Models.DTO
{
    public class MessageLogStatisticDTO
    {
        public long Id { get; set; }
        public Guid TransactionId { get; set; }
        public String Consumer { get; set; }
        public String Provider { get; set; }
        public String ConsumerName { get; set; }
        public String RoutingTokenName { get; set; }
        public String RoutingToken { get; set; }
        public String Service { get; set; }
        public String ServiceMethod { get; set; }
        public String Dir { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CreateDate { get; set; }
        public String ParticipantUri { get; set; }
        public string IsSuccessfull { get; set; }
        public int Count { get; set; }
    }
}
