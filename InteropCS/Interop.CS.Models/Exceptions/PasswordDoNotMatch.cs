using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка дека лозинката е грешна
    public class PasswordDoNotMatch : Exception
    {
        public PasswordDoNotMatch()
        {
           
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.PasswordDoNotMatch);
            }
        }
    }
}
