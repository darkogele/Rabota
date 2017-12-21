using System;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден учесник
    public class NotFoundParticipantException : Exception
    {
        private readonly string _participantCode;

        public NotFoundParticipantException(string participantCode)
        {
            _participantCode = participantCode;
        }
        
        public override string Message
        {
            get
            {
                return String.Format(Resources.NotFoundParticipant, _participantCode);
            }
        }

    }
}
