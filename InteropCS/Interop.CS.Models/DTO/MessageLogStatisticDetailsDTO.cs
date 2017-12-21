using System.Collections.Generic;
using Interop.CS.Models.DTO;

namespace Interop.CS.Models.Models
{
    public class MessageLogStatisticDetailsDTO
    {
        public List<MessageLogStatisticDTO> LogParticipantX { get; set; }
        public List<MessageLogStatisticDTO> LogParticipantY { get; set; }
        public List<MessageLogStatisticDTO> LogCS { get; set; }
        public int DirParticipantX { get; set; }
        public int DirParticipantY { get; set; }
        public int DirCS { get; set; }
    }
}
