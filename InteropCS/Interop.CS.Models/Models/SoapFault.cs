using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.Models
{
   public class SoapFault
    {
       public SoapFault()
       {
           DateCreated = DateTime.Now;
        }
       [Key]
        public Guid TransactionId { get; set; }
        public string Code { get; set; }
        public string SubCode { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public DateTime DateOccured { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
