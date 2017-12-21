using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.CC.SOAPstructure
{
    public class MIMHeader
    {
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

        public string Signature { get; set; }

        public MIMHeaderCallType CallType { get; set; }
    }
    public enum MIMHeaderCallType
    {
        synchronous,
        asynchronous,
    }
}
