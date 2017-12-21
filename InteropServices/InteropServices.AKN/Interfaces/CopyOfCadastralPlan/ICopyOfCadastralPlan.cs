using System.ServiceModel;

namespace InteropServices.AKN.Interfaces
{
    [ServiceContract]
    public interface ICopyOfCadastralPlan
    {
        [OperationContract]
        string Get(string department, string municipality, string number);
    }
}
