using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен невалиден сертификат
    public class InvalidCertException : Exception
    {
        private readonly string _certExt;

        public InvalidCertException(string certExt)
        {
            _certExt = certExt;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.InvalidCertExtension, _certExt);
            }
        }
    }
}
