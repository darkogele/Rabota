using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Internal.Kibs;
using Interop.CC.Handler.Internal.NinjectConfig;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Newtonsoft.Json;
using Ninject;

namespace Interop.CC.Handler.Internal
{
    public class InteropCCApiHandlerInternal : IHttpHandler
    {
        private readonly IMessageLogsRepository _messageLogsRepository;
        private readonly ISoapFaultRepository _soapFaultRepository;
        private readonly ILogger _logger;

        public InteropCCApiHandlerInternal()
        {
            using (IKernel kernel = new StandardKernel(new WCFNinject()))
            {
                _messageLogsRepository = kernel.Get<IMessageLogsRepository>();
                _soapFaultRepository = kernel.Get<ISoapFaultRepository>();
                _logger = kernel.Get<ILogger>();
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            _logger.Info("Dosol vo InteropCCApiHandlerInternal");

            var splittedRawUrl = context.Request.RawUrl.Split('/');
            var lastFromRawUrl = splittedRawUrl.Last();
            _logger.Info("context.Request.RawUrl: ", context.Request.RawUrl);
            _logger.Info("lastFromRawUrl: ", lastFromRawUrl);
            _logger.Info("splittedRawUrl: ", splittedRawUrl.ToString());
            DateTime dateFromContext;
            string[] format = { "dd.MM.yyyy" };
            DateTime.TryParseExact(lastFromRawUrl, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFromContext);

            //TODO:tuka smeni go vo datetime.today za testiranje
            _logger.Info("datecontext Day: " + dateFromContext.Day + "datecontext month: " + dateFromContext.Month);

            var messageLogs = _messageLogsRepository.GetMessageLogsByDate(new DateTime(dateFromContext.Year, dateFromContext.Month, dateFromContext.Day)); /* new DateTime(2016, 11, 04)*/
            _logger.Info("messageLogs by date count e: " + messageLogs.Count);

            var soapFaults = _soapFaultRepository.GetSoapFaultsByDate(new DateTime(dateFromContext.Year, dateFromContext.Month, dateFromContext.Day)); /*new DateTime(2016, 04, 08)*/
            _logger.Info("soapfaults by date count e: " + soapFaults.Count);


            //var joinedLogsAndFaults = messageLogs.FullOuterJoin(soapFaults, messageLog => messageLog.TransactionId, soapFault => soapFault.TransactionId,
            //    (messageLog, soapFault, transactionId) => new { messageLog, soapFault, transactionId }).ToList();

            var joinedLogsAndFaults = messageLogs.GroupJoin(soapFaults, messageLog => messageLog.TransactionId,
                soapFault => soapFault.TransactionId, (ml, sf) => new StatisticLogsFaults { MessageLog = ml, SoapFault = sf.FirstOrDefault(), TransactionId = ml.TransactionId }).ToList();

            _logger.Info("joinedLogsAndFaults count e: " + joinedLogsAndFaults.Count());

            if (joinedLogsAndFaults.Any())
            {
                var statiticLogsFaultsList = new List<StatisticLogsFaults>();
                foreach (var joinedLogsAndFault in joinedLogsAndFaults)
                {
                    statiticLogsFaultsList.Add(new StatisticLogsFaults
                    {
                        MessageLog = joinedLogsAndFault.MessageLog ?? new MessageLog(),
                        SoapFault = joinedLogsAndFault.SoapFault ?? new SoapFault(),
                        TransactionId = joinedLogsAndFault.TransactionId
                    });

                }
                var serializedMessageLogs = JsonConvert.SerializeObject(statiticLogsFaultsList);
                _logger.Info("serializedMessageLogs: " + serializedMessageLogs);
                context.Response.Write(serializedMessageLogs);
            }

            //if (messageLogs.Any())
            //{
            //    //foreach (var messageLog in messageLogs)
            //    //{
            //    //    _messageLogsRepository.UpdateMessageLog(messageLog.Id, CheckAndGetMessageLogTimeStamp(messageLog.TokenTimestamp));
            //    //}

            //    var serializedMessageLogs = JsonConvert.SerializeObject(messageLogs);
            //    context.Response.Write(serializedMessageLogs);
            //}

        }

        public bool IsReusable
        {
            get { return false; }
        }

        public bool CheckAndGetMessageLogTimeStamp(string tokenTimestamp)
        {
            var oWS = new wsTSATest();
            oWS.Url = "https://wstsatest.kibs.mk/wsTSATest.asmx";
            var kibsCertificationPath = WebConfigurationManager.AppSettings["KIBSCertificationPath"];
            var kibsCertificationPassword = WebConfigurationManager.AppSettings["KIBSCertificationPassword"];
            var cer = new X509Certificate2(kibsCertificationPath, kibsCertificationPassword);
            oWS.ClientCertificates.Add(cer);
            byte[] temp_backToBytes = Convert.FromBase64String(tokenTimestamp);
            var response = oWS.funCheckTS_Bytes(temp_backToBytes);
            oWS.Dispose();
            if (response.strFailureInfo == "")
                return true;
            return false;
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