using System;

namespace Interop.CC.Models.Models
{
    public class SoapFault
    {
        public SoapFault()
        {
            DateCreated = DateTime.Now;
        }
        public Guid TransactionId { get; set; }
        public string Code { get; set; }
        public string SubCode { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public DateTime DateOccured { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
