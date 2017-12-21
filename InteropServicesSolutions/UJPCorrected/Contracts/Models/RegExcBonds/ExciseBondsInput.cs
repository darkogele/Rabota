using System.Runtime.Serialization;

namespace Contracts.Models.RegExcBonds
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
