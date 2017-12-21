using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Interop.CC.Models.Models
{
    
    public class MessageLog
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Consumer { get; set; }
        public string Provider { get; set; }
        public string RoutingToken { get; set; }
        public string Service { get; set; }
        public string ServiceMethod { get; set; }
        public Guid TransactionId { get; set; }
        public string Dir { get; set; }
        public string CallType { get; set; }
        public string PublicKey { get; set; }
        public string Status { get; set; }
        public string MimeType { get; set; }
        public DateTime Timestamp { get; set; }
        public string TokenTimestamp { get; set; }
        public DateTime CreateDate { get; set; }
        public string Signature { get; set; }
        public string CorrelationId { get; set; }
        public bool? IsCorrect { get; set; }
        public bool IsInteropTestCommunicationCall { get; set; }
    }
}
