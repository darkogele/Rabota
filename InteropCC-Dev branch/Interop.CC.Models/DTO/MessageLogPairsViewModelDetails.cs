using Interop.CC.Models.Models;

namespace Interop.CC.Models.DTO
{
    public class MessageLogPairsViewModelDetails
    {
        public MessageLogDetails Request { get; set; }
        public MessageLogDetails Response { get; set; }
        public string ErrorMessage { get; set; }
    }
}
