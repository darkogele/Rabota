using System;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден Id на трансакција за лог
    public class NotFoundMessageLogTransactionIdException : Exception
    {
        private readonly Guid _transactionId;
        private readonly string _dir;

       public NotFoundMessageLogTransactionIdException(Guid transactionId, string dir)
       {
           _transactionId = transactionId;
           _dir = dir;
       }
       public override string Message
       {
           get
           {
               return String.Format(Resources.NotFoundMessageLogTransactionId, _transactionId, _dir);
           }
       }
    }
}
