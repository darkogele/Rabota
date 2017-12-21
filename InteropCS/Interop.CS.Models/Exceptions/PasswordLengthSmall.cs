using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка дека должината на лозинката е мала
    public class PasswordLengthSmall : Exception
    {
        public PasswordLengthSmall()
        {
           
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.PasswordLengthSmall);
            }
        }
    }
}
