using System.ServiceModel;

namespace MockService
{
    [ServiceContract]
    public interface IMockServiceProd
    {
        [OperationContract]
        string GetEnvName_ProdMethod();
    }
}
