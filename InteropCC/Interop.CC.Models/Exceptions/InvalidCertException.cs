using System;
using Interop.CC.Models.Models;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за прикачување на невалиден сертификат
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
