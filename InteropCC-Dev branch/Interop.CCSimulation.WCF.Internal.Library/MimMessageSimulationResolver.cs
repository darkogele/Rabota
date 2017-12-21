using System;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Methods;
using Interop.CC.Handler.Helper.SOAP;

namespace Interop.CCSimulation.WCF.Internal.Library
{
    public class MimMessageSimulationResolver : IMimMessageSimulationResolver
    {
        public SoapMessage SentMimMessage(SoapMessage mimMessage, string action)
        {
            #region Log SOAP Request Message

            var logSoap = AppSettings.Get<bool>("LogSoap");
            if (logSoap)
            {
                var nameLogerError = "Request_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLogerError))
                {
                    logger.Info(mimMessage.Body.MimBody.Message.ToString());
                }
            }

            #endregion

            #region Log MIM Message
            MimMsgHelper.LogMimMessage(mimMessage);
            #endregion

            //var urlToHostedApp = RequestHelper.GetServiceUrl(mimMessage.Header);
            var urlToHostedApp = "";//temporrary
            string mimeType;
            var response = SoapRequestHelper.Execute(mimMessage.Body.MimBody.Message.ToString(), action, urlToHostedApp, out  mimeType);

            var mimMsgResponse = MimMsgHelper.CreateMimResponseMsg(mimMessage, response, mimeType, mimMessage.Header.CryptoHeader.Key, mimMessage.Header.CryptoHeader.InitializationVector);


            #region Log MIM Message
            MimMsgHelper.LogMimMessage(mimMsgResponse);
            #endregion

            #region Log SOAP Response Message

            if (logSoap)
            {
                var nameLogerError = "Response_" + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLogerError))
                {
                    logger.Info(mimMsgResponse.Body.MimBody.Message.ToString());
                }
            }

            #endregion

            return mimMsgResponse;
        }
    }
}
