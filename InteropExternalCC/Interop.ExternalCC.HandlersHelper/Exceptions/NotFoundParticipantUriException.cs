using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.ExternalCC.HandlersHelper.Exceptions
{
    public class NotFoundParticipantUriException : Exception
    {
        private string _participantCode;

        public NotFoundParticipantUriException(string participantCode)
        {
            _participantCode = participantCode;
        }

        public override string Message
        {
            get { return String.Format(Resources.Properties.Resources.NotFoundParticipantUri, _participantCode); }
        }
    }
}
