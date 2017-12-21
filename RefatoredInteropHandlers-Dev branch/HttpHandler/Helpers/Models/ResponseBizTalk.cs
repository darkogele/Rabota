using System.Net;

namespace Helpers.Models
{
    public class ResponseInteropCommunication
    {
        public string Response { get; set; }
        public string MimeType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
    }
}
