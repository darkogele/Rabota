using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Interop.CC.Models.Models;

namespace Interop.CC.NBuilder
{
    // Класа со методи за генерирање на тестни податоци со NBuilder
    public class DataGenerator
    {
        // Метод за генерирање на тестни податоци за логови
        public IEnumerable<MessageLog> GenerateMessageLogs(int numberOfMessageLogs)
        {
            var list = Builder<MessageLog>.CreateListOfSize(numberOfMessageLogs).Build().ToList();
            return list;
        }

        // Метод за генерирање на тестни податоци за сервиси
        public IEnumerable<Service> GenerateServices(int numberOfServices)
        {
            var list = Builder<Service>.CreateListOfSize(numberOfServices).Build().ToList();
            return list;
        }

        // Метод за генерирање на тестни податоци за провајдери
        public IEnumerable<Provider> GenerateProviders(int numberOfProviders)
        {
            var list = Builder<Provider>.CreateListOfSize(numberOfProviders).Build().ToList();
            return list;
        }

        // Метод за генерирање на тестни податоци за грешки
        public IEnumerable<SoapFault> GenerateSoapFaults(int numberOfSoapFaults)
        {
            var list = Builder<SoapFault>.CreateListOfSize(numberOfSoapFaults).Build().ToList();
            return list;
        }

        // Метод за генерирање на тестни податоци за лог
        public MessageLog GenerateOneMessageLog()
        {
            return Builder<MessageLog>.CreateNew().Build();
        }

        // Метод за генерирање на тестни податоци за сервис
        public Service GenerateOneService()
        {
            return Builder<Service>.CreateNew().Build();
        }

        // Метод за генерирање на тестни податоци за провајдер
        public Provider GenerateOneProvider()
        {
            return Builder<Provider>.CreateNew().Build();
        }

        // Метод за генерирање на тестни податоци за грешка
        public SoapFault GenerateOneSoapFault()
        {
            return Builder<SoapFault>.CreateNew().Build();
        }

        // Метод за генерирање на тестни податоци за сервис со конкретни влезни параметри
        // Влезни параметри: податочни вредности code, name, endpoint, wsdl
        public Service GenerateOneService(string code, string name, string endpoint, string wsdl)
        {
            if ((code != null) && (name != null) && (endpoint != null) && (wsdl != null))
            {
                return
                    Builder<Service>.CreateNew()
                        .With(x => x.Code = code)
                        .And(x => x.Name = name)
                        .And(x => x.Endpoint = endpoint)
                        .And(x => x.Wsdl = wsdl)
                        .Build();
            }
            return Builder<Service>.CreateNew().Build();
        }
    }
}
