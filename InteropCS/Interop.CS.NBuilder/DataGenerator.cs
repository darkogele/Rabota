using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FizzWare.NBuilder;
using Interop.CS.Models.Models;

namespace Interop.CS.NBuilder
{
    // Класа со методи за генерирање на тестни податоци со NBuilder
    public class DataGenerator
    {
        //Опис:  Метод за генерирање на тестни податоци за учесници
        //Влезни параметри: број на учесници
        public IEnumerable<Participant> GenerateParticipants(int numberOfParticipants)
        {
            var list = Builder<Participant>.CreateListOfSize(numberOfParticipants).Build().ToList();
            return list;
        }

        //Опис: Метод за генерирање на тестни податоци за сервиси
        //Влезни параметри: број на сервиси
        public IEnumerable<CSService> GenerateServices(int numberOfServices)
        {
            var list = Builder<CSService>.CreateListOfSize(numberOfServices).Build().ToList();
            return list;
        }

        //Опис: Метод за генерирање на тестни податоци за пристапна листа
        //Влезни параметри: број на записи од пристапната листа
        public IEnumerable<AccessMapping> GenerateAccessMappings(int numberOfAccessMappings)
        {
            var list = Builder<AccessMapping>.CreateListOfSize(numberOfAccessMappings).Build().ToList();
            return list;
        }

        //Опис: Метод за генерирање на тестни податоци за логови
        //Влезни параметри: број на логови
        public IEnumerable<MessageLog> GenerateMessageLogs(int numberOfMessageLogs)
        {
            var list = Builder<MessageLog>.CreateListOfSize(numberOfMessageLogs).Build().ToList();
            return list;
        }

        //Опис: Метод за генерирање на тестни податоци за еден учесник
        public Participant GenerateOneParticipant()
        {
            return Builder<Participant>.CreateNew().Build();
        }

        //Опис: Метод за генерирање на тестни податоци за еден сервис
        public CSService GenerateOneService()
        {
            return Builder<CSService>.CreateNew().Build();
        }
        //Опис: Метод за генерирање на тестни податоци за еден запис од пристапната листа
        //Влезни параметри: код за корисник, код за провајдер, код за сервис
        public AccessMapping GenerateOneAccessMapping(string consumerCode, string providerCode, string serviceCode)
        {
            if ((consumerCode != null) && (providerCode != null) && (serviceCode != null))
            {
                return
                    Builder<AccessMapping>.CreateNew()
                    .With(x => x.ConsumerCode = consumerCode)
                    .And(x => x.ProviderCode = providerCode)
                    .And(x => x.ServiceCode = serviceCode)
                    .Build();
            }
            return Builder<AccessMapping>.CreateNew().Build();
        }

        //Опис: Метод за генерирање на тестни податоци за еден запис од пристапната листа
        //Влезни параметри: код за корисник, код за провајдер, код за сервис, код за метод, код за бас на провајдер, код за бас на корисник, активност
        public AccessMapping GenerateOneAccessMapping(string consumerCode, string providerCode, string serviceCode,
            string methodCode, string providerBusCode, string consumerBusCode, bool isActive)
        {
            return
                Builder<AccessMapping>.CreateNew()
                    .With(x => x.ConsumerCode = consumerCode)
                    .And(x => x.ProviderCode = providerCode)
                    .And(x => x.ServiceCode = serviceCode)
                    .And(x => x.MethodCode = methodCode)
                    .And(x => x.ProviderBusCode = providerBusCode)
                    .And(x => x.ConsumerBusCode = consumerBusCode)
                    .And(x => x.IsActive = isActive)
                    .Build();
        }

        //Опис: Метод за генерирање на тестни податоци за еден лог
        public MessageLog GenerateOneMessageLog()
        {
            return Builder<MessageLog>.CreateNew().Build();
        }

        //Опис: Метод за генерирање на тестни податоци за еден запис од пристапната листа
        public AccessMapping GenerateOneAccessMapping()
        {
            return Builder<AccessMapping>.CreateNew().Build();
        }
    }
}
