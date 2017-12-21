using System;

namespace Interop.CS.Models.Models
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
