using System.Collections.Generic;

namespace Interop.CS.Models.Models
{
    public class CSService
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Wsdl { get; set; }
        public string ParticipantCode { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
