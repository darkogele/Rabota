using System;
using System.ComponentModel;

namespace Interop.CS.Models.DTO
{
    public class SoapFaultExcelDTO
    {
        [DisplayName("Трансакција")]
        public Guid TransactionId { get; set; }

        [DisplayName("Код")]
        public string Code { get; set; }

        [DisplayName("Субкод")]
        public string SubCode { get; set; }

        [DisplayName("Причина")]
        public string Reason { get; set; }

         [DisplayName("Датум на настанување")]
        public string DateOccured { get; set; }

        [DisplayName("Датум на креирање")]
        public string DateCreated { get; set; }
    }
}
