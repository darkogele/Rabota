using System;
using System.Xml.Serialization;

namespace Interop.CC.Handler.Helper.Model
{
    public class MimHeader
    {
        [XmlAttribute]
        public string id { get; set; }
        public string Consumer { get; set; }

        public string Provider { get; set; }

        public string RoutingToken { get; set; }

        public string Service { get; set; }

        public string ServiceMethod { get; set; }

        public string TransactionId { get; set; }

        public string Dir { get; set; }

        public string PublicKey { get; set; }

        public string MimeType { get; set; }

        public DateTime TimeStamp { get; set; }

        public string CorrelationID { get; set; }

        public MimSignature Signature { get; set; }

        public MimHeaderCallType CallType { get; set; }
    }

    public enum MimHeaderCallType
    {
        synchronous,
        asynchronous
    }
}
