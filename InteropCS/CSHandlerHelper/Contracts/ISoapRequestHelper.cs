using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CSHandlerHelper.Model;
using CSHandlerHelper.SOAP;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models.Models;

namespace CSHandlerHelper.Contracts
{
    public interface ISoapRequestHelper
    {
        ResponseInteropCommunication Execute(string req, string contentType, string url);
        HttpWebRequest CreateWebRequest(string contentType, string url);
        SoapMessage UnwrapMimMessage(string mimMessage);
        SoapFaultMessage UnwrapSoapFaultMessage(string soapFaultMessage);
        bool ValidateSignature(string mimMessage, string publicKey, ILogger _logger);
        SoapFault CreateSoapFaultDB(Guid tId, string code, string subCode, string details, string reason);
        string BTsoapFault(string errorMessage);
    }
}
