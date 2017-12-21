using System.Runtime.Serialization;

namespace BusSyncMockWCFService.DAL
{
    [DataContract]
    public class Participant
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Uri { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string PublicKey { get; set; }
    }
}