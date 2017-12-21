using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Newtonsoft.Json;

namespace Interop.CS.Portal.API.Controllers
{
    [Authorize]
    public class UncreatedStatisticsController : ApiController
    {
        private readonly IParticipantRepository _participantsRepository;
        private readonly IMessageLogStatisticRepository _messageLogStatisticRepository;
        private readonly IMessageLogRepository _messageLogRepository;
        private readonly ISoapFaultRepository _soapFaultRepository;
        private readonly ILogger _logger;
        private readonly IServiceRepository _serviceRepository;

        public UncreatedStatisticsController(IParticipantRepository participantsRepository, IMessageLogStatisticRepository messageLogStatisticRepository, IMessageLogRepository messageLogRepository, ISoapFaultRepository soapFaultRepository,
            ILogger logger, IServiceRepository serviceRepository)
        {
            _participantsRepository = participantsRepository;
            _messageLogStatisticRepository = messageLogStatisticRepository;
            _messageLogRepository = messageLogRepository;
            _soapFaultRepository = soapFaultRepository;
            _logger = logger;
            _serviceRepository = serviceRepository;
        }

        //Опис: Методот прави повик до GetParticipants од ParticipantRepository
        //Излезни параметри: Листа од кодови на сите учесници
        [HttpGet]
        public Dictionary<string,string> GetParticipants()
        {
            var participants = _participantsRepository.GetParticipants();
            var consumers = new Dictionary<string,string>();
            var splitChar = new string[] { "$$" };
            foreach (var participant in participants)
            {
                var partCode = participant.Code.Split(splitChar, StringSplitOptions.None).Last().ToUpper();
                consumers.Add(participant.Code, partCode);
            }
            consumers.Add("CS","CS");
            return consumers;
        }

        private HttpResponseMessage SetResponseMessage(string message)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        [HttpPost]
        public HttpResponseMessage CreateMessageLogStatistic(string selectedParticipant, DateTime forDate)
        {
            var msgForResponse = string.Empty;
            if (selectedParticipant.Equals("CS"))
            {
                var messageLogsAndFaultsFromCs = GetMessageLogsAndFaultsFromCs(Convert.ToDateTime(forDate)).ToList();
                if (messageLogsAndFaultsFromCs != null)
                {
                    if (messageLogsAndFaultsFromCs.Count() > 0)
                    {
                        //TODO:Ova odkomentiraj go
                        //     //foreach (var logAndFault in messageLogsAndFaultsFromCs)
                        //     //{
                        //     //    _messageLogRepository.UpdateMessageLog(logAndFault.MessageLog.Id, GetMessageLogCheckTimeStamp(logAndFault.MessageLog.TokenTimestamp));
                        //     //}
                        var msgSuccessCreatedData = CreateMessageLogStatistics(messageLogsAndFaultsFromCs, ConfigurationManager.AppSettings["CSAddress"], ConfigurationManager.AppSettings["CSAddress"], forDate);
                        foreach (var msg in msgSuccessCreatedData)
                        {
                            msgForResponse += msg + "; ";
                        }
                        return SetResponseMessage(msgForResponse);
                    }
                }
                
                var msgNoDataToCreate = "Не постојат логови за тој датум во CS" + ".";
                return SetResponseMessage(msgNoDataToCreate);
            }
            else
            {
                var participantUri = _participantsRepository.GetParticipant(selectedParticipant).Uri;
                var participantMessageLogsFromCc = GetParticipantMessageLogs(participantUri, forDate);
                if (participantMessageLogsFromCc != null)
                {
                    if (participantMessageLogsFromCc.Count() > 0)
                    {
                        var msgSuccessCreatedData = CreateMessageLogStatistics(participantMessageLogsFromCc, participantUri, selectedParticipant, forDate);

                        foreach (var msg in msgSuccessCreatedData)
                        {
                            msgForResponse += msg + "; ";
                        }

                        return SetResponseMessage(msgForResponse);
                    }
                }
                
                var msgNoDataToCreate = "Неуспешно: Не постојат логови за тој датум во " + selectedParticipant + ".";
                return SetResponseMessage(msgNoDataToCreate);
            }
        }

        private List<StatisticLogsFaults> GetParticipantMessageLogs(string uri, DateTime forDate)
        {
            var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
            return GetMessageLogsFromResponse(uri + "/logs/" + forDate.ToString("dd.MM.yyyy", macCultureInfo));
        }
        public List<StatisticLogsFaults> GetMessageLogsFromResponse(string url)
        {
            //_logger.Info(DateTime.Now + " --**-- Dosol do momentot koa kje treba da gagja na CC handler internal api i url-to mu e:" + url);
            string responseFromServer = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            //_logger.Info(DateTime.Now + " --**-- requestot mu e:" + url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse response = request.GetResponse();
            //_logger.Info(DateTime.Now + " --**-- responsot mu e:" + url);
            Stream dataStream = response.GetResponseStream();
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }

            if (!string.IsNullOrEmpty(responseFromServer))
            {
                return JsonConvert.DeserializeObject<List<StatisticLogsFaults>>(responseFromServer);
            }
            return new List<StatisticLogsFaults>();
        }

        private List<string> CreateMessageLogStatistics(IEnumerable<StatisticLogsFaults> participantMessageLogs, string participantUri, string participantCode, DateTime? forDate)
        {
            //_logger.Info(DateTime.Now + " --**--Pocnuva da zapisuva vo message log statistics tabelata");
            var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
            var msgForDisplay = new List<string>();
            bool msgFound = false;
            var allServices = _serviceRepository.GetServices().ToDictionary(s => s.Code, s => s.Name);
            foreach (var logsAndFaults in participantMessageLogs)
            {
                //_logger.Info(DateTime.Now + " --**--Za participant: " + participantUri);

                var messageLogStatisticExist = _messageLogStatisticRepository.MessageLogStatisticExist(logsAndFaults.TransactionId, logsAndFaults.MessageLog.Dir, participantCode);

                //_logger.Info(" --**--consumer,provider,routingtoken,service,servicemethod,transactionid,dir,calltype,publickey,status,mimetype,timestamp,createdate,signature,correlationid,participanturi,timestamp, iscorrect," +
                //               "participantcode,consumername,routingtokenname,faultcode,faultsubcode,faulktreason,faultdetails,faultdatecreated: " + logsAndFaults.MessageLog.Consumer + "/" +
                //               logsAndFaults.MessageLog.Provider + "/" + logsAndFaults.MessageLog.RoutingToken + "/" + logsAndFaults.MessageLog.Service + "/" + logsAndFaults.MessageLog.ServiceMethod
                //               + "/" + logsAndFaults.MessageLog.TransactionId + "/" + logsAndFaults.MessageLog.Dir + "/" + logsAndFaults.MessageLog.CallType + "/" + logsAndFaults.MessageLog.PublicKey
                //               + "/" + logsAndFaults.MessageLog.MimeType
                //               + "/" + logsAndFaults.MessageLog.Timestamp
                //               + "/" + logsAndFaults.MessageLog.CreateDate
                //               + "/" + logsAndFaults.MessageLog.Signature + "/" + logsAndFaults.MessageLog.CorrelationId
                //               + "/" + participantUri
                //               + "/" + logsAndFaults.MessageLog.TokenTimestamp
                //               + "/" + logsAndFaults.MessageLog.IsCorrect
                //               + "/" + participantCode
                //               + "/" + _participantsRepository.GetParticipant(logsAndFaults.MessageLog.Consumer).Name
                //                + "/" + _participantsRepository.GetParticipant(logsAndFaults.MessageLog.RoutingToken).Name
                //                + "/" + logsAndFaults.SoapFault.Code
                //                + "/" + logsAndFaults.SoapFault.SubCode
                //                + "/" + logsAndFaults.SoapFault.Reason
                //                + "/" + logsAndFaults.SoapFault.Details
                //                + "/" + logsAndFaults.SoapFault.DateCreated);

                if (!messageLogStatisticExist)
                {
                    try
                    {
                        //Ova e samo privremeno se duri ne se postavat Participant-tite od dva razlicni Bus-a da se so razlicen ParticipantCode
                        string makeConsumerWithMim1;
                        if (logsAndFaults.MessageLog.Consumer == "AVRM")
                        {
                            makeConsumerWithMim1 = "MIM2$$" + logsAndFaults.MessageLog.Consumer;
                        }
                        else
                        {
                            makeConsumerWithMim1 = logsAndFaults.MessageLog.Consumer;
                        }
                        var serviceCodeFromMessageLog = logsAndFaults.MessageLog.Service;
                        var serviceNameFromCode = allServices[serviceCodeFromMessageLog];
                        _messageLogStatisticRepository.InsertMessageLogStatistic(new MessageLogStatistic
                        {
                            Consumer = logsAndFaults.MessageLog.Consumer,
                            Provider = logsAndFaults.MessageLog.Provider,
                            RoutingToken = logsAndFaults.MessageLog.RoutingToken,
                            //Service = logsAndFaults.MessageLog.Service, //ova e service code
                            Service = serviceNameFromCode,
                            ServiceMethod = logsAndFaults.MessageLog.ServiceMethod,
                            TransactionId = logsAndFaults.MessageLog.TransactionId,
                            Dir = logsAndFaults.MessageLog.Dir,
                            CallType = logsAndFaults.MessageLog.CallType,
                            PublicKey = logsAndFaults.MessageLog.PublicKey,
                            Status = logsAndFaults.MessageLog.Status,
                            MimeType = logsAndFaults.MessageLog.MimeType,
                            Timestamp = logsAndFaults.MessageLog.Timestamp,
                            CreateDate = logsAndFaults.MessageLog.CreateDate,
                            Signature = logsAndFaults.MessageLog.Signature,
                            CorrelationId = logsAndFaults.MessageLog.CorrelationId,
                            ParticipantUri = participantUri,
                            TokenTimestamp = logsAndFaults.MessageLog.TokenTimestamp,
                            IsCorrect = logsAndFaults.MessageLog.IsCorrect,
                            ParticipantCode = participantCode,
                            //ConsumerName = _participantsRepository.GetParticipant(logsAndFaults.MessageLog.Consumer).Name,
                            ConsumerName = _participantsRepository.GetParticipantByBus(makeConsumerWithMim1).Name,
                            RoutingTokenName = _participantsRepository.GetParticipant(logsAndFaults.MessageLog.RoutingToken).Name,
                            FaultCode = logsAndFaults.SoapFault.Code,
                            FaultSubCode = logsAndFaults.SoapFault.SubCode,
                            FaultReason = logsAndFaults.SoapFault.Reason,
                            FaultDetails = logsAndFaults.SoapFault.Details,
                            FaultDateCreated = logsAndFaults.SoapFault.DateCreated
                        });
                        if (!msgFound)
                        {
                            msgForDisplay.Add("Успешно: Креиран е запис во табелата MessageLogStatistic за учесник " +
                                         participantCode +
                                         " за датум " + forDate.Value.ToString("dd.MM.yyyy", macCultureInfo) + ".");
                        }
                        msgFound = true;
                        //_logger.Info(DateTime.Now + " --**--Zapisal eden podatok za message logs vo statistic tabelata za participant " + participantCode);
                    }
                    catch (Exception exception)
                    {
                        _logger.Error(DateTime.Now + "Se slucila greska pri zapisuvanje vo MessageLogStatistic" + exception);
                        //throw exception;
                    }
                }
                else
                {
                    msgForDisplay.Add("Неуспешно: Записот за учесник " +
                                         participantCode +
                                         " и датум " + forDate.Value.ToString("dd.MM.yyyy", macCultureInfo) + " веќе постои. Трансакција: " + logsAndFaults.TransactionId + ", тип на повик: " + logsAndFaults.MessageLog.Dir + ", код на учесник: " + participantCode);
                }
            }
            return msgForDisplay;
        }
        private IEnumerable<StatisticLogsFaults> GetMessageLogsAndFaultsFromCs(DateTime createdDate)
        {
            IEnumerable<MessageLog> messageLogs = _messageLogRepository.GetMessageLogsByDate(createdDate);
            var statiticLogsFaultsList = new List<StatisticLogsFaults>();
            if (messageLogs.Any())
            {
                IEnumerable<SoapFault> soapFaults = _soapFaultRepository.GetSoapFaultsByDate(createdDate);

                var joinedLogsAndFaults = messageLogs.FullOuterJoin(soapFaults, messageLog => messageLog.TransactionId, soapFault => soapFault.TransactionId,
                   (messageLog, soapFault, transactionId) => new { messageLog, soapFault, transactionId }).ToList();

                
                if (joinedLogsAndFaults.Any())
                {

                    foreach (var joinedLogsAndFault in joinedLogsAndFaults)
                    {
                        statiticLogsFaultsList.Add(new StatisticLogsFaults
                        {
                            MessageLog = joinedLogsAndFault.messageLog ?? new MessageLog(),
                            SoapFault = joinedLogsAndFault.soapFault ?? new SoapFault(),
                            TransactionId = joinedLogsAndFault.transactionId
                        });
                    }
                }
            }
           
            return statiticLogsFaultsList;
        }

    }

    internal static class LinqExtensions
    {
        internal static IEnumerable<TResult> FullOuterJoin<TA, TB, TKey, TResult>(
        this IEnumerable<TA> a,
        IEnumerable<TB> b,
        Func<TA, TKey> selectKeyA,
        Func<TB, TKey> selectKeyB,
        Func<TA, TB, TKey, TResult> projection,
        TA defaultA = default(TA),
        TB defaultB = default(TB),
        IEqualityComparer<TKey> cmp = null)
        {
            cmp = cmp ?? EqualityComparer<TKey>.Default;
            var alookup = a.ToLookup(selectKeyA, cmp);
            var blookup = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            var join = from key in keys
                       from xa in alookup[key].DefaultIfEmpty(defaultA)
                       from xb in blookup[key].DefaultIfEmpty(defaultB)
                       select projection(xa, xb, key);

            return join;
        }
        internal static IEnumerable<TResult> FullOuterGroupJoin<TA, TB, TKey, TResult>(
        this IEnumerable<TA> a,
        IEnumerable<TB> b,
        Func<TA, TKey> selectKeyA,
        Func<TB, TKey> selectKeyB,
        Func<IEnumerable<TA>, IEnumerable<TB>, TKey, TResult> projection,
        IEqualityComparer<TKey> cmp = null)
        {
            cmp = cmp ?? EqualityComparer<TKey>.Default;
            var alookup = a.ToLookup(selectKeyA, cmp);
            var blookup = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            var join = from key in keys
                       let xa = alookup[key]
                       let xb = blookup[key]
                       select projection(xa, xb, key);

            return join;
        }
    }
}
