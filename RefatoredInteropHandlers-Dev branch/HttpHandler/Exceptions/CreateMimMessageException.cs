using System;

namespace Exceptions
{
    public class CreateMimMessageException : Exception
    {
        public CreateMimMessageException()
        { }

        public CreateMimMessageException(String message)
            : base(message)
        {

        }
        public CreateMimMessageException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
