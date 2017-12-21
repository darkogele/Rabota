using System;
using System.Diagnostics;
using System.Linq;
using CrossCutting.Logging;
using Exceptions;
using Helpers.Models;

namespace Helpers
{
    public class Helper
    {
        public static T ExecuteLogicAndLogException<T>(Func<T> someAction, TypeException exceptionType, ILogger logger)
        {
            try
            {
                return someAction.Invoke();
            }
            catch (Exception e)
            {
                //var trace = new StackTrace(e,true);
                //var stackFrames = trace.GetFrames();

                //if (stackFrames != null && stackFrames.Any())
                //{
                //    var stackTraceInfo = GetStackTraceInfo(stackFrames);

                //    if (stackTraceInfo != null)
                //    {
                //        logger.Error("Error in " + stackTraceInfo.FileName + ", method " + stackTraceInfo.Method, e.Message, "MakeCallAndLog_");

                //        if (exceptionType.Equals(TypeException.UrlSegmentException))
                //        {
                //            throw new Exception("Error in " + stackTraceInfo.FileName + ", method " + stackTraceInfo.Method, new UrlSegmentException(e.Message, e.InnerException));
                //            //throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
                //        }
                //        if (exceptionType.Equals(TypeException.SoapBodyException))
                //        {
                //            //throw new Exception("Error in " + stackTraceInfo.FileName + ", method " + stackTraceInfo.Method, new SoapBodyException(e.Message, e.InnerException));
                //            throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
                //        }
                //        if (exceptionType.Equals(TypeException.DefaultException))
                //        {
                //            //throw new Exception("Error in " + stackTraceInfo.FileName + ", method " + stackTraceInfo.Method, new Exception(e.Message, e.InnerException));
                //            throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
                //        }
                //    }
                    
                //}
                //logger.Error("Error occured. Error info: " + e.Message, "MakeCallAndLog_");
                ////TODO: Da se razmisli dali exceptions od implementacija samo da se logiraat, a drug exception message da mu se prikazuva na korisikot
                ////throw new Exception("Error occured. Error info: " + e.Message);

                //throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");




                //Bidejki vo RELEASE Stack Trace ne dava info za imeto na metodot i imeto na fajlot kade shto se slucila greskata mora vaka da se odi

                //var trace = new StackTrace(e, true);
                //var stackFrames = trace.GetFrames();


                //if (stackFrames != null && stackFrames.Any())
                //{
                //var stackTraceInfo = GetStackTraceInfo(stackFrames);

                //if (stackTraceInfo != null)
                //{
                //LogToLocalFile("Error in " + stackTraceInfo.FileName + ", method " + stackTraceInfo.Method);

                //logger.Error("Error in " + stackTraceInfo.FileName + ", method " + stackTraceInfo.Method, e.Message, "MakeCallAndLog_");
                

                //Comment for Dev branch
                logger.Error("ERROR DETAILS FROM STACKTRACE: " + e.StackTrace, e.Message, "MakeCallAndLog_");

                if (exceptionType.Equals(TypeException.UrlSegmentException))
                {
                    throw new Exception(exceptionType + " occured. Error during proccesing data. Error details: " + e.Message, new Exception(e.Message, e.InnerException));
                    //throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
                }
                if (exceptionType.Equals(TypeException.SoapBodyException))
                {
                    throw new Exception(exceptionType + " occured. Error during proccesing data. Error details: " + e.Message, new Exception(e.Message, e.InnerException));
                    //throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
                }
                if (exceptionType.Equals(TypeException.DefaultException))
                {
                    throw new Exception(exceptionType + " occured. Error during proccesing data. Error details: " + e.Message, new Exception(e.Message, e.InnerException));
                    //throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
                }
                //}

                //}
                logger.Error("Error occured. Error info: " + e.Message, "MakeCallAndLog_");
                //TODO: Da se razmisli dali exceptions od implementacija samo da se logiraat, a drug exception message da mu se prikazuva na korisikot
                //throw new Exception("Error occured. Error info: " + e.Message);

                throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
            }
        }
        public static StackTraceInfo GetStackTraceInfo(StackFrame[] stackFrames)
        {
            var stackInfo = new StackTraceInfo();
            var frame = stackFrames.FirstOrDefault(x => x.ToString().Contains("RequestTestHelper") || x.ToString().Contains("MimMsgHelper"));
            if (frame != null)
            {
                var stackFrameFromRequestHelper = frame;
                var fileWithError = stackFrameFromRequestHelper.GetFileName();
                var methodWithError = stackFrameFromRequestHelper.GetMethod().Name;

                var fileName = string.Empty;
                if (fileWithError != null)
                {
                    var splited = fileWithError.Split('\\');
                    fileName = splited.Last();

                }
                var start = methodWithError.IndexOf("<", StringComparison.Ordinal) + 1;
                var end = methodWithError.IndexOf(">", start, StringComparison.Ordinal);
                var methodName = methodWithError.Substring(start, end - start);

                stackInfo.FileName = fileName;
                stackInfo.Method = methodName;
            }
            return stackInfo;
        }
    }
}
