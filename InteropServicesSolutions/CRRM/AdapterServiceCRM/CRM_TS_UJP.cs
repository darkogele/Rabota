using System;
using System.IO;
using System.ServiceModel;

namespace AdapterServiceCRM
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class CRM_TS_UJP : ICRM_TS_UJP
    {
        public string Get_TS_UJP(string param)
        {
            var CRM = new ProductionTSOld.XmlWebServiceSoapClient();
            var header = new ProductionTSOld.XmlSoapHeader();
            string output = "";
            try
            {
                output = CRM.ProcessSignedRequest(ref header, param);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                throw ex;
            }
            return output;
        }

        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream;
            DirectoryInfo logDirInfo;
            FileInfo logFileInfo;

            string logFilePath = "C:\\Logs\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }   
    }
}
