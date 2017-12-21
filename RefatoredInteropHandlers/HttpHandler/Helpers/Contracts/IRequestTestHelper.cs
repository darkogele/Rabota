using System.Collections.Specialized;
using System.IO;
using System.Xml;
using Helpers.Models;

namespace Helpers.Contracts
{
    public interface IRequestTestHelper
    {
        UrlSegment GetUrlSegments(string url);
        string GetOnlySoapBody(Stream inputStream);
        string MakeWebRequest(string url);
        SoapFaultMessage UnwrapSoapFaultMessage(string soapFaultMessage);
        ResponseInteropCommunication Execute(string decryptedRequestBodyWrappedInSoapEnvelope, string contentType, string url);
        XmlDocument CreateFaultMessage(SoapFaultMessage soapFault);
        SoapFaultMessage CreateSoapFault(string code, string subCode, string mTime, string text);
        SoapMessage UnwrapMimMessage(string mimMessage);
        SoapMessage CreateMimResponseMsg(SoapMessage mimMsg, string mimeType);
        string GetSoapBody(Stream inputStream);
        string GetSoapHeader(NameValueCollection headers);
        string GetActionFromContentType(string contentType);
        string GetServiceUrl(MimHeader mimMessageHeaderMimMessage);
        string GetOnlySoapBodyFromString(string inputStream);
    }
}
