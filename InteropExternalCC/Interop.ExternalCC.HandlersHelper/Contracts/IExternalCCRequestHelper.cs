

using System.Collections.Generic;
using Interop.ExternalCC.HandlersHelper.SOAP;

namespace Interop.ExternalCC.HandlersHelper.HelperMethods
{
    public interface IExternalCCRequestHelper
    {
        string GetParticipantUri(string participantCode);
        string ResentExternalCCRequest(string soapAction, string soapBody, string url);
        string GetParticipantCode(string soapBody);
        Dictionary<string, string> GetAllParticipantsUri();
        SoapMessage UnwrapMimMessage(string mimMessage, string externalCode);
    }
}
