using System;
using Interop.CC.Models.Models;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за внес на постоечки запис за лог
  public  class DuplicateMessageLogException : Exception
    {
        private readonly MessageLog _messageLog;

        public DuplicateMessageLogException(MessageLog messageLog)
        {
            _messageLog = messageLog;
            
        }

        public override string Message
        {
            get {
             return String.Format(Resources.DuplicateMessageLog, _messageLog.TransactionId);
        }
        }
    }
}
