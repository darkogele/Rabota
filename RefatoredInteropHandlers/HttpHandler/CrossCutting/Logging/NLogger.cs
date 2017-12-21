using System.Globalization;
using System.Linq;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;


namespace CrossCutting.Logging
{
    public class NLogger: ILogger
    {
        //denes
        #region Private members
        private readonly Logger _logger;
        private int _category = 1;
        private int _eventId = 1;
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
        #endregion Private Members

        #region Properties
        public int Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public int EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                _eventId = value;
            }
        }
        #endregion Properties

        #region Constructors
        public NLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public NLogger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        public void Dispose()
        {
            if (LogManager.Configuration != null)
            {
                var targets = LogManager.Configuration.AllTargets;
                foreach (var target in targets)
                {
                    target.Dispose();
                }
            }

        }
        #endregion Constructors

        #region Info

        public void Info(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (_logger.IsInfoEnabled)
            {
                var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
                logEvent.Level = LogLevel.Info;
                Config(filePath, logEvent);
                _logger.Log(logEvent);
            }
        }

        #endregion Info

        #region Warning
        public void Warn(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsWarnEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Warn;
            _logger.Log(logEvent);
        }
        #endregion Warning

        #region Debug
        public void Debug(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsDebugEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Debug;
            _logger.Log(logEvent);
        }
        #endregion Debug

        #region Trace
        public void Trace(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsTraceEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Trace;
            _logger.Log(logEvent);
        }
        #endregion Debug

        #region Error
        public void Error(string message, string exception, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsErrorEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Error;
            logEvent.Properties.Add("Exception", BuildCustomExceptionString(exception));
            Config(filePath, logEvent);
            _logger.Log(logEvent);
        }

        public void Error(string message, Exception ex, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsErrorEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Error;
            logEvent.Properties.Add("Exception", BuildExceptionString(ex));
            Config(filePath, logEvent);
            _logger.Log(logEvent);
        }

        public void Error(Exception ex, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsErrorEnabled) return;
            var logEvent = CreateLogEvent(string.Empty, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Error;
            logEvent.Properties.Add("Exception", BuildExceptionString(ex));
            _logger.Log(logEvent);
        }
        #endregion Error

        #region Fatal
        public void Fatal(string message, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsFatalEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Fatal;
            _logger.Log(logEvent);
        }

        public void Fatal(Exception ex, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsFatalEnabled) return;
            var logEvent = CreateLogEvent(string.Empty, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Fatal;
            logEvent.Properties.Add("Exception", BuildExceptionString(ex));
            _logger.Log(logEvent);
        }

        public void Fatal(string message, Exception ex, [CallerMemberName] string memberName = "default", [CallerFilePath] string filePath = "default", [CallerLineNumber] int lineNumber = 0)
        {
            if (!_logger.IsFatalEnabled) return;
            var logEvent = CreateLogEvent(message, memberName, filePath, lineNumber);
            logEvent.Level = LogLevel.Fatal;
            logEvent.Properties.Add("Exception", BuildExceptionString(ex));
            _logger.Log(logEvent);
        }
        #endregion Fatal

        #region Public Methods
        public void AddVariable<T>(Expression<Func<T>> variable)
        {
            var expressionBody = (MemberExpression)variable.Body;
            _variables.Add(expressionBody.Member.Name, ((FieldInfo)expressionBody.Member).GetValue(((ConstantExpression)expressionBody.Expression).Value));
        }
        public void ClearVariables()
        {
            _variables.Clear();
        }
        #endregion

        #region Privat methods
        private string BuildExceptionString(Exception ex)
        {
            var str = new StringBuilder(Environment.NewLine);
            str.AppendLine("__Exception__");
            str.AppendFormatLine("Message:{0}", ex.Message);
            str.AppendFormatLine("Source:{0}", ex.Source);
            str.AppendFormatLine("Stack Trace:{0}", ex.StackTrace);
            str.AppendFormatLine("Target Site:{0}", ex.TargetSite);
            if (ex.Data.Count > 0)
            {
                str.AppendLine("___Exception Data___:");
                foreach (DictionaryEntry de in ex.Data)
                {
                    str.AppendFormatLine("Key: {0,-20} - Value: {1}", de.Key.ToString(), de.Value);
                }
                str.AppendLine("___Exception Data End___:");
            }
            str.AppendLine("__End Exception__");
            var innerExeption = ex.InnerException;
            var i = 1;
            while (innerExeption != null)
            {
                str.AppendLine(FormatInnerExeption(innerExeption, i++));
                innerExeption = innerExeption.InnerException;
            }
            return str.ToString();
        }

        private string FormatInnerExeption(Exception innerExeption, int i)
        {
            var innExeption = new StringBuilder();
            innExeption.Append('\t', i);
            innExeption.AppendLine("__Inner Exception__");
            innExeption.Append('\t', i);
            innExeption.AppendFormatLine("Message:{0}", innerExeption.Message);
            innExeption.Append('\t', i);
            innExeption.AppendFormatLine("Source:{0}", innerExeption.Source);
            innExeption.Append('\t', i);
            innExeption.AppendFormatLine("Stack Trace:{0}", innerExeption.StackTrace);
            innExeption.Append('\t', i);
            innExeption.AppendFormatLine("Target Site:{0}", innerExeption.TargetSite);
            if (innerExeption.Data.Count > 0)
            {
                innExeption.Append('\t', i);
                innExeption.AppendLine("___Exception Data___:");
                foreach (DictionaryEntry de in innerExeption.Data)
                {
                    innExeption.Append('\t', i);
                    innExeption.AppendFormatLine("Key: {0,-20} - Value: {1}", de.Key.ToString(), de.Value);
                }
                innExeption.Append('\t', i);
                innExeption.AppendLine("___Exception Data End___:");
            }
            innExeption.Append('\t', i);
            innExeption.Append("__End Inner Exception__");
            return innExeption.ToString();
        }

        private LogEventInfo CreateLogEvent(string message, string memberName, string filePath, int lineNumber)
        {
            var logEvent = new LogEventInfo(LogLevel.Info, _logger.GetType().FullName, message)
            {
                LoggerName = _logger.Name
            };
            logEvent.Properties.Add("memberName", memberName);
            logEvent.Properties.Add("filePath", filePath);
            logEvent.Properties.Add("lineNumber", lineNumber);
            logEvent.Properties.Add("eventId", _eventId);
            logEvent.Properties.Add("category", _category);
            if (_variables.Count > 0)
            {
                logEvent.Properties.Add("variables", ProcessVariables(_variables));
            }
            return logEvent;
        }

        private string ProcessVariables(Dictionary<string, object> variables)
        {
            var var = new StringBuilder();
            foreach (KeyValuePair<string, object> variable in variables)
            {
                var.AppendLine();
                var.AppendLine("**New variable**");
                var.Append(ProcessVariable(variable.Value, variable.Key));
                var.AppendLine("**End variable**");
            }
            return var.ToString();
        }

        private string ProcessVariable(object variable, string name = null)
        {
            var var = new StringBuilder();
            var t = variable.GetType();
            var list = variable as IList;
            var dict = variable as IDictionary;
            if (t.Name.Equals("DictionaryEntry"))
            {
                var varDE = CastVariable<DictionaryEntry>(variable);
                var.AppendFormatLine("item key: {0}, item value: {1}", ProcessVariable(varDE.Key), ProcessVariable(varDE.Value));
            }
            else if (t.IsValueType || t == typeof(String))
            {
                if (String.IsNullOrWhiteSpace(name))
                {
                    var.AppendFormat("{0}", variable);
                }
                else
                {
                    var.AppendFormatLine("Name: {0}({1}), Value: {2}", name, t.Name, variable);
                }
            }
            else if (t.IsArray)
            {
                var.AppendLine(ProcessArray(variable, name, t));
            }
            else if (list != null)
            {
                var.AppendLine(ProcessList(variable, name, t));
            }
            else if (dict != null)
            {
                var.AppendLine(ProcessDictionary(variable, name, t));
            }
            else if (t.IsClass)
            {
                var.AppendLine(ProcessClass(variable, name, t));
            }
            return var.ToString();
        }

        private string ProcessList(object variable, string name, Type t)
        {
            var var = new StringBuilder();

            var list = CastVariable<IList>(variable);

            var.AppendFormatLine("List name:{0};({1})<{2}>; length:{3}", name, t.Name, list.GetType().GetGenericArguments()[0].Name, list.Count);
            foreach (var item in list)
            {
                var.AppendLine(ProcessVariable(item));
            }
            return var.ToString();
        }

        private string ProcessDictionary(object variable, string name, Type t)
        {
            var var = new StringBuilder();

            var dict = CastVariable<IDictionary>(variable);

            var.AppendFormatLine("Dictionary name:{0};({1})<{2},{3}>; length:{4}", name, t.Name, dict.GetType().GetGenericArguments()[0].Name, dict.GetType().GetGenericArguments()[1].Name, dict.Count);
            foreach (var item in dict)
            {
                var.Append(ProcessVariable(item));
            }
            return var.ToString();
        }

        private string ProcessClass(object variable, string name, Type t)
        {
            var var = new StringBuilder();
            var.AppendFormatLine("Class name:{0}{1}", name, t.Name);
            FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (fis.Length > 0)
            {
                var.AppendLine("__Fields__");
                foreach (var fi in fis)
                {
                    var.Append(ProcessVariable(fi.GetValue(variable), fi.Name));
                }
                var.AppendLine("__End Fields__");
            }
            var pis = t.GetProperties();
            if (pis.Length > 0)
            {
                var.AppendLine("__Properties:__");
                foreach (var pi in pis)
                {
                    var.Append(ProcessVariable(pi.GetValue(variable, null), pi.Name));
                }
                var.AppendLine("__End Properties__");
            }
            var.AppendLine("__End Class__");
            return var.ToString();
        }

        private string ProcessArray(object variable, string name, Type t)
        {
            var var = new StringBuilder();
            var array = variable as Array;
            if (array != null)
            {
                var rank = array.Rank;
                var dimensions = new string[rank];
                for (int i = 0; i < rank; i++)
                {
                    dimensions.SetValue(array.GetLength(i).ToString(CultureInfo.InvariantCulture), i);
                }
                var dimStr = string.Join(",", dimensions);

                var.AppendFormatLine("Array name:{0}({1}), Count:{2}", name, t.Name, dimStr);

                foreach (var arrElement in array)
                {
                    var.AppendLine(ProcessVariable(arrElement));
                }
            }
            return var.ToString();
        }

        private static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            var expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        private static T CastVariable<T>(object input)
        {
            return (T)input;
        }

        // custom method for properties passed from NLogger.cs to NLog.config
        // it should be implemented everywhere we want to get log's fileName, direction, and className

        #endregion Private methods

        #region Private custom methods

        private void Config(string filePath, LogEventInfo logEvent)
        {
            logEvent.Properties.Add("ClassName", filePath.Split('\\').ToList().LastOrDefault());
        }

        private string BuildCustomExceptionString(string ex)
        {
            var str = new StringBuilder(Environment.NewLine);
            str.AppendLine("__Exception__");
            str.AppendFormatLine("Message:{0}", ex);
            str.AppendLine("__End Exception__");
            return str.ToString();
        }

        #endregion Private custom methods
    }
    public static class StringBuilderExtensions
    {
        public static void AppendFormatLine(this StringBuilder str, string format, params object[] args)
        {
            str.AppendFormat(format, args);
            str.AppendLine();
        }
    }
}
