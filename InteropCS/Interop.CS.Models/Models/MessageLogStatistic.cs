using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interop.CS.Models.Models
{
    public class MessageLogStatistic
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Key]
        [Column(Order = 1)] 
        public Guid TransactionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(100)]
        public string Dir { get; set; }

        [MaxLength(150)]
        public string ParticipantUri { get; set; }

        [MaxLength(100)]
        public string Consumer { get; set; }

        [MaxLength(100)]
        public string Provider { get; set; }

        [MaxLength(100)]
        public string RoutingToken { get; set; }

        [MaxLength(100)]
        public string Service { get; set; }

        [MaxLength(100)]
        public string ServiceMethod { get; set; }

        [MaxLength(100)]
        public string CallType { get; set; }
        public string PublicKey { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(50)]
        public string MimeType { get; set; }
        public DateTime Timestamp { get; set; }

        public string TokenTimestamp { get; set; }
        public DateTime CreateDate { get; set; }
        public string Signature { get; set; }

        [MaxLength(100)]
        public string CorrelationId { get; set; }
        public bool? IsCorrect { get; set; }

        [Key]
        [Column(Order = 3)]
        [MaxLength(100)]
        public string ParticipantCode { get; set; }

        [MaxLength(100)]
        public string ConsumerName { get; set; }

        [MaxLength(100)]
        public string RoutingTokenName { get; set; }

        [MaxLength(50)]
        public string FaultCode { get; set; }

        [MaxLength(50)]
        public string FaultSubCode { get; set; }

        [MaxLength(800)]
        public string FaultReason { get; set; }

        [MaxLength(200)]
        public string FaultDetails { get; set; }
        public DateTime? FaultDateCreated { get; set; }
    }
}
