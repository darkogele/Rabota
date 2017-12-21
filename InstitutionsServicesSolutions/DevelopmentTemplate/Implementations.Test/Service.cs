using System.ServiceModel;
using Contracts;
using Contracts.DataAccessLibrary;

namespace Implementations.Test
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : IService
    {
        public string GetData()
        {
            var client = new MockServiceReference.MockServiceTestClient();
            return client.GetEnvironmentName_TestMethod();
        }

        public CompositeType GetDataUsingDataContract()
        {
            var client = new MockServiceReference.MockServiceTestClient();
            return new CompositeType
            {
                BoolValue = true,
                StringValue = client.GetEnvironmentName_TestMethod()
            };
        }
    }
}
