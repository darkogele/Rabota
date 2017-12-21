using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHandlerHelper.Model;

namespace CSHandlerHelper.Contracts
{
    public interface IRequestHelper
    {
        UrlSegment GetUrlSegments(string url);
        string IsSoapRequest(NameValueCollection headers, ref bool isSoapRequest);
        string GetSoapHeader(NameValueCollection headers);
        string GetSoapBody(Stream inputStream);
        string GetServiceUrl(MimHeader mimMessageHeaderMimMessage);
        string GetSoapMethodName(string soapBody);
    }
}
