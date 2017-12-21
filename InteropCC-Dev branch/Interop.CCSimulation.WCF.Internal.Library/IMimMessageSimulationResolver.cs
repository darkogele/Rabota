using System.ServiceModel;
using Interop.CC.Handler.Helper.SOAP;

namespace Interop.CCSimulation.WCF.Internal.Library
{
    [ServiceContract]
    public interface IMimMessageSimulationResolver
    {
        [OperationContract]
        SoapMessage SentMimMessage(SoapMessage mimMessage, string action);
    }
}
