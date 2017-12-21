using System;
using System.ComponentModel;

namespace Interop.CS.Models.DTO
{
    public class MessageLogExcelDTO
    {
        [DisplayName("Трансакција")]
        public Guid TransactionId { get; set; }

        [DisplayName("Корисник на услуга")]
        public String Consumer { get; set; }

        [DisplayName("Извор на услуга")]
        public String Provider { get; set; }

         [DisplayName("Токен на рутирање")]
        public String RoutingToken { get; set; }

        [DisplayName("Сервис")]
        public String Service { get; set; }

         [DisplayName("Метод")]
        public String ServiceMethod { get; set; }

        [DisplayName("Насока")]
        public String Dir { get; set; }

         [DisplayName("Временска ознака")]
        public String Timestamp { get; set; }

        [DisplayName("Време на креирање")]
        public String CreateDate { get; set; }

        [DisplayName("Токен временска ознака")]
        public String TokenTimestamp { get; set; }
    }
}
