using System.ServiceModel;

namespace MockService
{
    [ServiceContract]
    public interface IMockServiceTest
    {
        [OperationContract]
        string GetEnvironmentName_TestMethod();
    }
}
