using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Interop.CS.Models.Models
{
    [Serializable]
    public class MessageLogStatisticToDisplayInDocs
    {
        [DisplayName("Институции")]
        public List<Service> ListServices { get; set; } 
    }

    [Serializable]
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public bool isSuccessfull { get; set; }

        [DisplayName("Грешки кај институции")]
        public string ErrorInstitution { get; set; }
        public int NumberOfSuccesfull { get; set; }
        public int NumberOfUnSuccesfull { get; set; }

    }
    [Serializable]
    public class Provider
    {
        public string ProviderName { get; set; }
        public int Count { get; set; }
        public List<Transaction> ListTransactions { get; set; }
        public int SuccesfullCalls { get; set; }
        public int UnSuccesfullCalls { get; set; }
        public string PointOfError { get; set; }

    }
    [Serializable]
    public class Consumer
    {
        public string ConsumerName { get; set; }
        public List<Provider> ListProviders { get; set; }

    }
     [Serializable]
    public class Service
    {
        [DisplayName("Сервис")]
        public string ServiceName { get; set; }
        public List<Consumer> ListConsumers { get; set; }
        public int TotalCalledTimes { get; set; }
    }
   
}
