using System;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден запис во листата на логови баран по трансакциски идентификациски број
    public class NotFoundMessageLogTransactionIdException : Exception
    {
        private readonly Guid _transactionId;

       public NotFoundMessageLogTransactionIdException(Guid transactionId)
       {
           _transactionId = transactionId;
       }
       public override string Message
       {
           get
           {
               return String.Format(Resources.NotFoundMessageLogTransactionId, _transactionId);
           }
       }
    }
}
