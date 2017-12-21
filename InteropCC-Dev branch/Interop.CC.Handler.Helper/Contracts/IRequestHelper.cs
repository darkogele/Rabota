using System.Collections.Specialized;
using System.IO;
using Interop.CC.Handler.Helper.Model;

namespace Interop.CC.Handler.Helper.Contracts
{
    public interface IRequestHelper
    {
        UrlSegment GetUrlSegments(string url);
        string IsSoapRequest(NameValueCollection headers, ref bool isSoapRequest);
        string GetSoapHeader(NameValueCollection headers);
        string GetSoapBody(Stream inputStream);
        string GetOnlySoapBody(Stream inputStream);
        string GetOnlySoapBodyFromString(string inputStream);
        string GetServiceUrl(MimHeader mimMessageHeaderMimMessage);
        string GetSoapMethodName(string soapBody);
    }
}
