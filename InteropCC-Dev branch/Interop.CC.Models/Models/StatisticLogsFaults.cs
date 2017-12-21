using System;

namespace Interop.CC.Models.Models
{
    public class StatisticLogsFaults
    {
        public StatisticLogsFaults()
        {
            MessageLog = new MessageLog();
            SoapFault = new SoapFault();
        }
        public MessageLog MessageLog { get; set; }
        public SoapFault SoapFault { get; set; }
        public Guid TransactionId { get; set; }
       
    }
}
