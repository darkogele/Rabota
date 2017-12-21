using System;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден сервис
   public class NotFoundCSServiceException : Exception
     {
        private readonly string _participantcode;
        private readonly string _code;

        public NotFoundCSServiceException(string participantcode, string code)
        {
            _participantcode = participantcode;
            _code = code;
           
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.NotFoundServiceException, _participantcode, _code);
            }
        }
    }
}
