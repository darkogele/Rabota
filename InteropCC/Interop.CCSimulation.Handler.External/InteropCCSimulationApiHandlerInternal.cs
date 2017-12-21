using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CCSimulation.Handler.Internal.NinjectConfig;
using Newtonsoft.Json;
using Ninject;

namespace Interop.CCSimulation.Handler.Internal
{
    public class InteropCCSimulationApiHandlerInternal : IHttpHandler
    {
         private readonly IMessageLogsRepository _messageLogsRepository;
         private readonly ISoapFaultRepository _soapFaultRepository;

         public InteropCCSimulationApiHandlerInternal()
        {
            using (IKernel kernel = new StandardKernel(new WCFNinject()))
            {
                _messageLogsRepository = kernel.Get<IMessageLogsRepository>();
                _soapFaultRepository = kernel.Get<ISoapFaultRepository>();
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            //TODO:tuka smeni go vo datetime.today za testiranje
            IEnumerable<MessageLog> messageLogs = _messageLogsRepository.GetMessageLogsByDate(new DateTime(2016, 02, 01) /*DateTime.Today*/);

            IEnumerable<SoapFault> soapFaults = _soapFaultRepository.GetSoapFaultsByDate(new DateTime(2016, 02, 01));

            var joinedLogsAndFaults = messageLogs.FullOuterJoin(soapFaults, messageLog => messageLog.TransactionId, soapFault => soapFault.TransactionId,
                (messageLog, soapFault, transactionId) => new { messageLog, soapFault, transactionId }).ToList();

            if (joinedLogsAndFaults.Any())
            {
                var statiticLogsFaultsList = new List<StatisticLogsFaults>();
                foreach (var joinedLogsAndFault in joinedLogsAndFaults)
                {
                    statiticLogsFaultsList.Add(new StatisticLogsFaults
                    {
                        MessageLog = joinedLogsAndFault.messageLog ?? new MessageLog(),
                        SoapFault = joinedLogsAndFault.soapFault ?? new SoapFault(),
                        TransactionId = joinedLogsAndFault.transactionId
                    });
                }
                var serializedMessageLogs = JsonConvert.SerializeObject(statiticLogsFaultsList);
                context.Response.Write(serializedMessageLogs);
            }
        }

        public bool IsReusable
        {
            get { return false; }
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