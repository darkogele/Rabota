using System;
using System.Collections.Generic;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.RepositoryContracts
{
    public interface IMessageLogsRepository
 {
        IEnumerable<MessageLog> GetMessageLogs();
        MessageLog GetMessageLogById(long messageLogId);
        MessageLogPairsViewModelDetails GetMessageLogByTransactionId(Guid transactionId);
        void InsertMessageLog(MessageLog messageLog);
        PagedCollection<MessageLogPairsViewModel> GetMessageLogsPaged(int pageIndex, int itemsPerPage, string filterConsumer, string filterProvider, string filterService, string filterMethod, bool? filterTransactionSuccess, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol);
        List<MessageLog> GetMessageLogsByDate(DateTime createdDate);
        List<MessageLogExcelDTO> GetFilteredMessageLogs(string filterConsumer, string filterProvider, string filterService, string filterMethod, bool? filterTransactionSuccess, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol);
        void UpdateMessageLog(long messageLogId, bool isCorrect);
        void UpdateMessageLog(string dir, string timestampToken, string serviceMethod, string publicKey, string messageLogId);

 }
}
