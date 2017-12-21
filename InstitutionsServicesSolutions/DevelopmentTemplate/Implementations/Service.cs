using System.ServiceModel;
using Contracts;
using Contracts.DataAccessLibrary;

namespace Implementations
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : IService
    {
        public string GetData()
        {
            var client = new MockServiceReference.MockServiceProdClient();
            return client.GetEnvName_ProdMethod();
        }

        public CompositeType GetDataUsingDataContract()
        {
            var client = new MockServiceReference.MockServiceProdClient();
            return new CompositeType
            {
                BoolValue = true,
                StringValue = client.GetEnvName_ProdMethod()
            };
        }
    }
}
