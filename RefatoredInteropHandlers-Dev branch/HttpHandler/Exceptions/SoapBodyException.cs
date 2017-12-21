using System;

namespace Exceptions
{
    public class SoapBodyException:Exception
    {
        public String errorInfo { get; set; }

        public SoapBodyException()
        { }

        public SoapBodyException(String message) : base(message)
        {
            
        }
        public SoapBodyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
