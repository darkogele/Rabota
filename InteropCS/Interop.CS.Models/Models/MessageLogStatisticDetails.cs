using System.Collections.Generic;
using Interop.CS.Models.DTO;

namespace Interop.CS.Models.Models
{
    public class MessageLogStatisticDetails
    {
        public List<MessageLogStatistic> LogParticipantConsumer { get; set; }
        public List<MessageLogStatistic> LogParticipantProvider { get; set; }
        public List<MessageLogStatistic> LogCS { get; set; }
        public int DirParticipantConsumer { get; set; }
        public int DirParticipantProvider { get; set; }
        public int DirCS { get; set; }
    }
}
