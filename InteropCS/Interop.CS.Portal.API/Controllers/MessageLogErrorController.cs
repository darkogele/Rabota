using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;

namespace Interop.CS.Portal.API.Controllers
{
    [Authorize]
    public class MessageLogErrorController : ApiController
    {
        private readonly IMessageLogRepository _messageLogRepository;
        private readonly ILogger _logger;
        private readonly IMessageLogStatisticRepository _messageLogStatisticRepository;
        public MessageLogErrorController(IMessageLogRepository messageLogRepository, ILogger logger, IMessageLogStatisticRepository messageLogStatisticRepository)
        {
            _messageLogRepository = messageLogRepository;
            _logger = logger;
            _messageLogStatisticRepository = messageLogStatisticRepository;
        }
        private HttpResponseMessage SetResponseMessage(string message)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        [HttpPost]
        public HttpResponseMessage CreateMessageLogForResponse()
        {
            var msgForResponse = string.Empty;
            var allMesasgeLogs = _messageLogRepository.GetAndCreateMissingMesageLogs();
            if (allMesasgeLogs.Count > 0)
            {
                foreach (var mLog in allMesasgeLogs)
                {
                    msgForResponse += mLog + "; ";
                }
                return SetResponseMessage(msgForResponse);
            }
            return SetResponseMessage("Не постојат такви случаи.");
        }

        [HttpPost]
        public HttpResponseMessage CreateMessageLogStatistic()
        {
            var msgForResponse = string.Empty;
            var allMesasgeLogs = _messageLogRepository.GetAndCreateMissingMesageLogsInStatistic();

            if (allMesasgeLogs.Count > 0)
            {
                foreach (var mlNeedToBeCreated in allMesasgeLogs)
                {
                    try
                    {
                        _messageLogStatisticRepository.InsertMessageLogStatistic(new MessageLogStatistic
                        {
                            Consumer = mlNeedToBeCreated.Consumer,
                            Provider = mlNeedToBeCreated.Provider,
                            RoutingToken = mlNeedToBeCreated.RoutingToken,
                            Service = mlNeedToBeCreated.Service,
                            ServiceMethod = mlNeedToBeCreated.ServiceMethod,
                            TransactionId = mlNeedToBeCreated.TransactionId,
                            Dir = mlNeedToBeCreated.Dir,
                            CallType = mlNeedToBeCreated.CallType,
                            PublicKey = mlNeedToBeCreated.PublicKey,
                            Status = mlNeedToBeCreated.Status,
                            MimeType = mlNeedToBeCreated.MimeType,
                            Timestamp = mlNeedToBeCreated.Timestamp,
                            CreateDate = mlNeedToBeCreated.CreateDate,
                            Signature = mlNeedToBeCreated.Signature,
                            CorrelationId = mlNeedToBeCreated.CorrelationId,
                            ParticipantUri = "CS",
                            TokenTimestamp = mlNeedToBeCreated.TokenTimestamp,
                            IsCorrect = mlNeedToBeCreated.IsCorrect,
                            ParticipantCode = "CS",
                            ConsumerName = mlNeedToBeCreated.ConsumerName,
                            RoutingTokenName = mlNeedToBeCreated.RoutingTokenName
                        });

                        msgForResponse += mlNeedToBeCreated.TransactionId + "; ";
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Nastanata e greska pri racno kreiranje vo MessageLogStatistic, greshkata e: " + ex);
                        throw new Exception(ex.Message);
                    }
                }

                return SetResponseMessage(msgForResponse);
            }
            //if (allMesasgeLogs.Count > 0)
            //{
            //    foreach (var mLog in allMesasgeLogs)
            //    {
            //        msgForResponse += mLog + "; ";
            //    }
            //    return SetResponseMessage(msgForResponse);
            //}
            return SetResponseMessage("Не постојат такви случаи.");
        }
    }
}
