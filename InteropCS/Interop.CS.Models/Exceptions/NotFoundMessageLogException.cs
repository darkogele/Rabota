using System;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден лог
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
