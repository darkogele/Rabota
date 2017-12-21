using System;
using System.Security.Cryptography;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен невалиден јавен клуч за сертификат
    public class InvalidPublicKeyForCertificate : CryptographicException
    {
        private readonly string _password;

        public InvalidPublicKeyForCertificate(string password)
        {
            _password = password;
        }
        public override string Message
        {
            get
            {
                return String.Format(Resources.InvalidPublicKeyForCertificate, _password);
            }
        }
    }
}