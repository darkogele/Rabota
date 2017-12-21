using System;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен дупликат корисник
    public class DuplicateUserException : Exception
    {
        private readonly UserModelDTO _user;

        public DuplicateUserException(UserModelDTO user)
        {
            _user = user;
        }
        
        public override string Message
        {
            get
            {
                return String.Format(Resources.DuplicateUserException, _user.Username);
            }
        }

    }
}
