using System;

namespace Exceptions
{
    public class CertificateException : Exception
    {
        public CertificateException()
        {}

        public CertificateException(String message):base(message)
        {}
        public CertificateException(string message, Exception innerException):base(message, innerException)
        { }
    }
}
