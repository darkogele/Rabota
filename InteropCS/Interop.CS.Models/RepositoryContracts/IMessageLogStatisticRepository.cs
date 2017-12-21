using System;
using System.Collections.Generic;
using System.Linq;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IMessageLogStatisticRepository:IDisposable
    {
        void InsertMessageLogStatistic(MessageLogStatistic messageLogStatistic);
        IEnumerable<MessageLogStatistic> GetMessageLogStatistics();
        IQueryable<MessageLogStatistic> GetMessageLogStatistics(string consumer, string provider, bool successfully, bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string service, string sortDir, string sortCol);
        MessageLogStatisticDetails GetMessageLogStatisticByTransactionId(Guid transactionId, string consumer, string routingToken);
        bool MessageLogStatisticExist(Guid transactionId, string dir, string participantCode);
        PagedCollection<MessageLogStatisticDTO> GetMessageLogsStatisticPaged(int pageIndex, int itemsPerPage);
    }
}
