using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен дупликат учесник
    public class DuplicateParticipantException : Exception
    {
        private readonly Participant _participant;

        public DuplicateParticipantException(Participant participant)
        {
            _participant = participant;
        }
        
        public override string Message
        {
            get
            {
                return String.Format(Resources.DuplicateParticipant, _participant.Code);
            }
        }

    }
}
