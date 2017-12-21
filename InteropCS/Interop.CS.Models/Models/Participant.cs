using System.Collections.Generic;

namespace Interop.CS.Models.Models
{
    public class Participant
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsActive { get; set; }
        public string PublicKey { get; set; }
    }
}
