using System;
using Interop.CC.Models.DTO;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Models;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за внес на постоечки запис за корисник
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
