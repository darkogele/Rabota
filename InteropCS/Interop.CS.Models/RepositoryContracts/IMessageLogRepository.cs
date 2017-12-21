using System;
using System.Collections.Generic;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IMessageLogRepository : IDisposable
    {
        IEnumerable<MessageLog> GetMessageLogs();
        MessageLog GetMessageLogById(long messageLogId);
        MessageLog GetMessageLogByTransactionId(Guid transactionId, string dir);
        void InsertMessageLog(MessageLog messageLog);
        PagedCollection<MessageLogDTO> GetMessageLogsPaged(int pageIndex, int itemsPerPage, string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol);
        List<MessageLogExcelDTO> GetFilteredMessageLogs(string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol);
        IEnumerable<MessageLog> GetMessageLogsByDate(DateTime createdDate);
        void UpdateMessageLog(long messageLogId, bool isCorrect);
        List<string> GetAndCreateMissingMesageLogs();
        List<MessageLogStatistic> GetAndCreateMissingMesageLogsInStatistic();
    }
}
