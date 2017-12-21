using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.ExternalCC.Resources.Properties;

namespace Interop.ExternalCC.HandlersHelper.Exceptions
{
    public class NotFoundParticipantCodeException : Exception
    {

        public NotFoundParticipantCodeException()
        {

        }

        public override string Message
        {
            get { return String.Format(Resources.Properties.Resources.NotFoundParticipantCode); }
        }
    }
}
