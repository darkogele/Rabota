using System;

namespace Exceptions
{
    public class UrlSegmentException : Exception
    {
        public UrlSegmentException()
        { }

        public UrlSegmentException(String message)
            : base(message)
        {

        }
        public UrlSegmentException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
