using System;
using System.Net;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Handler.Helper.SOAP;
using Interop.CC.Models.Models;
using Interop.CC.CrossCutting.Logging;

namespace Interop.CC.Handler.Helper.Contracts
{
    public interface ISoapRequestHelper
    {
        ResponseInteropCommunication Execute(string req, string contentType, string url, ILogger _logger);
        HttpWebRequest CreateWebRequest(string contentType, string url);
        SoapMessage UnwrapMimMessage(string mimMessage);
        SoapFaultMessage UnwrapSoapFaultMessage(string soapFaultMessage);
        bool ValidateSignature(string mimMessage, string publicKey , ILogger _logger);
        SoapFault CreateSoapFaultDB(Guid tId, string code, string subCode, string details, string reason);
        string BTsoapFault(string errorMessage);
    }
}
