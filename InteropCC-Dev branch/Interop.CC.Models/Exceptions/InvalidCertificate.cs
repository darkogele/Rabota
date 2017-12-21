using System;
using System.Security.Cryptography;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за прикачување на невалиден сертификат
    public class InvalidCertificate : CryptographicException
    {
        private readonly string _password;

        public InvalidCertificate(string password)
        {
            _password = password;
        }
        public override string Message
        {
            get
            {
                return String.Format(Resources.InvalidCertificate, _password);
            }
        }
    }
}