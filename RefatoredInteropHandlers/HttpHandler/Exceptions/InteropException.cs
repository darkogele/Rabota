using System;

namespace Exceptions
{
    public class InteropException : Exception
    {
        public InteropException()
        { }

        public InteropException(String message)
            : base(message)
        {

        }
        public InteropException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
