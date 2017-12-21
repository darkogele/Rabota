using System;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден запис во листата на логови
   public class NotFoundMessageLogException : Exception
   {
       private readonly long _id;

       public NotFoundMessageLogException(long id)
       {
           _id = id;
       }
       public override string Message
       {
           get
           {
               return String.Format(Resources.NotFoundMessageLog, _id);
           }
       }
   }
}
