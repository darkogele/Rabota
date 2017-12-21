using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InteropServices.UJP.Implementations
{
    [DataContract]
    public class ExciseBondsInput
    {
        [DataMember]
        public long EDB { get; set; }
        [DataMember]
        public string Number { get; set; }
    }
}
