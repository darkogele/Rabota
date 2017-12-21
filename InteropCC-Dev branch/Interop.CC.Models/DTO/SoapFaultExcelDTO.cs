using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.DTO
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
