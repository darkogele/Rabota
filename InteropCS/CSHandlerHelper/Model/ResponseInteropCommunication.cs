using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSHandlerHelper.Model
{
    public class ResponseInteropCommunication
    {
        public string Response { get; set; }
        public string MimeType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
    }
}
