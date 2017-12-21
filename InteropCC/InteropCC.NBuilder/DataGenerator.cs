using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Interop.CC.Models.Models;

namespace InteropCC.NBuilder
{
    public class DataGenerator
    {
        public IEnumerable<MessageLog> GenerateMessageLogs(int numberOfMessageLogs)
        {
            var list = Builder<MessageLog>.CreateListOfSize(numberOfMessageLogs).Build().ToList();
            return list;
        }

        public IEnumerable<Service> GenerateServices(int numberOfServices)
        {
            var list = Builder<Service>.CreateListOfSize(numberOfServices).Build().ToList();
            return list;
        }

        public MessageLog GenerateOneParticipant(string participantCode)
        {
            return Builder<MessageLog>.CreateNew().Build();
        }

        public Service GenerateOneService()
        {
            return Builder<Service>.CreateNew().Build();
        }

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
