using System.ServiceModel;
using Interop.CC.Handler.Helper.SOAP;

namespace Interop.CC.WCF.Internal.Library
{
    [ServiceContract]
    public interface IMimMessageResolver
    {
        [OperationContract]
        SoapMessage SentMimMessage(SoapMessage mimMessage, string action);
    }
}
