using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CrossCutting.Logging
{
    public interface ILogger: IDisposable   
    {
        #region Public Properties
        int Category
        {
            get;
            set;
        }

        int EventId
        {
            get;
            set;
        }
        #endregion

        #region Info
        void Info(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        #endregion Info

        #region Warn
        void Warn(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        #endregion Warn

        #region Debug
        void Debug(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        #endregion Debug

        #region Trace
        void Trace(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        #endregion Trace

        #region Error
        void Error(string message, string exception, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        void Error(string message, Exception x, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        void Error(Exception x, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        #endregion Error

        #region Fatal
        void Fatal(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        void Fatal(string message, Exception ex, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        void Fatal(Exception x, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0);
        #endregion Fatal

        void AddVariable<T>(Expression<Func<T>> variable);
        void ClearVariables();
    }
    public class LoggingFactory
    {
        public static ILogger GetNLogger()
        {
            return new NLogger();
        }

        public static ILogger GetNLogger(string logger)
        {
            return new NLogger(logger);
        }
    }
}
