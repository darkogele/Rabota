using System;
using System.Diagnostics;
using System.Linq;
using Exceptions.Models;

namespace Exceptions
{
    public class Helper
    {
        public static ExceptionInfo GetExceptionAdditionalInfo(Exception exception)
        {
            var st = new StackTrace(exception, true);
            StackFrame frame = st.GetFrame(0);
            string fileName = frame.GetFileName();
            if (!string.IsNullOrEmpty(fileName))
            {
                var s = fileName.Split('\\');
                fileName = s.Last();
            }

            int line = frame.GetFileLineNumber();
            string methodName = frame.GetMethod().Name;

            return  new ExceptionInfo
            {
                FileErrorOccured = fileName,
                LineErrorOccured = line,
                MethodErrorOccurred = methodName
            };
        }
    }
}
