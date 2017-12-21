using System.Runtime.Serialization;

namespace BusSyncMockWCFService.DAL
{
    [DataContract]
    public class Service
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string ParticipantCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Wsdl { get; set; }
    }
}