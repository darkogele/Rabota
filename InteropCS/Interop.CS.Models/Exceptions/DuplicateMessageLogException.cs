using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен дупликат лог
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
