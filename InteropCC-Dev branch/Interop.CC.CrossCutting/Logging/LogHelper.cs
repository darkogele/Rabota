using System;

namespace Interop.CC.CrossCutting.Logging
{
    public static class LogHelper
    {
        public static void WriteInNLoc(string actionName, string controllerName, string errors, string nameNLogFile = "", string logLevel = "Error")
        {
            //using (var logger = LoggingFactory.GetNLogger())
            //{
            //    if (logLevel == "Error")
            //        logger.Error(String.Format("Error on {0} controller, {1} action.", controllerName, actionName), errors);
            //    if (logLevel == "Info")
            //        logger.Info(String.Format("Info on {0} controller, {1} action", controllerName, actionName), errors);
            //}
        }

        public static void WriteInNLoc(string message, Exception e, string logLevel = "Error", string infoMessage = "", string optionalData = "")
        {
            //using (var logger = LoggingFactory.GetNLogger())
            //{

            //    if (logLevel == "Error")
            //    {
            //        var m = string.Format("Message:{0}", message);
            //        logger.Error(m, e);
            //    }
            //    if (logLevel == "Info")
            //    {
            //        var i = string.Format("InfoMessage:{0}", infoMessage);
            //        logger.Info(i);
            //    }
            //}
        }

        public static void WriteInNLog(string actionName, string controllerName, string errors, string nameNLogFile, string logLevel = "Error")
        {
            //using (var logger = LoggingFactory.GetNLogger(nameNLogFile))
            //{
            //    if (logLevel == "Error")
            //        logger.Error(String.Format("Error on {0} controller, {1} action.", controllerName, actionName), errors);
            //    if (logLevel == "Info")
            //        logger.Info(String.Format("Info on {0} controller, {1} action.", controllerName, actionName), errors);
            //}
        }
    }
}