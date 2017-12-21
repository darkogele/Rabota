using System;

namespace Exceptions
{
    public class BizTalkException : Exception
    {
          public BizTalkException()
        { }

        public BizTalkException(String message)
            : base(message)
        {

        }
        public BizTalkException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
