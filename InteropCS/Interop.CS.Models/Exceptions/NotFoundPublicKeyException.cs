using System;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден јавен клуч
   public class NotFoundPublicKeyException : Exception
    {
       private readonly string _participantCode;
       public NotFoundPublicKeyException(string participantcode)
        {
            _participantCode = participantcode;
        }
       public override string Message
       {
           get
           {
               return String.Format(Resources.NotFoundPublicKeyException, _participantCode);
           }
       }
    }
}
