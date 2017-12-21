using System.Runtime.Serialization;

namespace Contracts.DataAccessLibrary.InteropFault
{
    [DataContract]
    public class InteropFault
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorDetails { get; set; }
    }
}
