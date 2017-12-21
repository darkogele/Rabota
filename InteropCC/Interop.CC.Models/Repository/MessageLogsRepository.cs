using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters;
using System.Web.WebPages;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;
using System.Linq.Dynamic;
using Newtonsoft.Json;

namespace Interop.CC.Models.Repository
{
    public class MessageLogsRepository : IMessageLogsRepository
    {
        private readonly IUnitOfWork _uow;

        // Опис: Конструктор на MessageLogsRepository модулот 
        // Влезни параметри: модел IUnitOfWork
        public MessageLogsRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Опис: Методот врши вчитување на сите логови од база
        // Влезни параметри: /
        // Излезни параметри: IEnumerable<MessageLog> 
        public IEnumerable<MessageLog> GetMessageLogs()
        {
            return _uow.Context.MessageLogs.ToList();
        }

        // Опис: Методот врши вчитување на лог од база според идентификациски број
        // Влезни параметри: податочна вредност messageLogId
        // Излезни параметри: MessageLog 
        public MessageLog GetMessageLogById(long messageLogId)
        {
            var message = _uow.Context.MessageLogs.Find(messageLogId);

            if (message == null)
            {
                throw new NotFoundMessageLogException(messageLogId);
            }

            return message;
        }

        // Опис: Методот врши вчитување на лог од база според трансакциски идентификациски број
        // Влезни параметри: податочна вредност transactionId
        // Излезни параметри: MessageLog 
        public MessageLogPairsViewModelDetails GetMessageLogByTransactionId(Guid transactionId)
        {
            var messages = _uow.Context.MessageLogs.Where(x => x.TransactionId == transactionId);

            if (messages == null)
            {
                throw new NotFoundMessageLogTransactionIdException(transactionId);
            }

            var request = messages.FirstOrDefault(x => x.Dir == "Request");
            var response = messages.FirstOrDefault(x => x.Dir == "Response");

            var soapFault = _uow.Context.SoapFaults.FirstOrDefault(x => x.TransactionId == transactionId);
            var messageLogDetails = new MessageLogPairsViewModelDetails();
            var requestDetail = new MessageLogDetails
                {
                    Id = request.Id,
                    Consumer = request.Consumer,
                    ConsumerName = string.Empty,
                    Provider = request.Provider,
                    ProviderName = string.Empty,
                    RoutingToken = request.RoutingToken,
                    RoutingTokenName = string.Empty,
                    Service = request.Service,
                    ServiceMethod = request.ServiceMethod,
                    TransactionId = request.TransactionId,
                    Dir = request.Dir,
                    CallType = request.CallType,
                    PublicKey = request.PublicKey,
                    Status = request.Status,
                    MimeType = request.MimeType,
                    Timestamp = request.Timestamp,
                    TokenTimestamp = request.TokenTimestamp,
                    CreateDate = request.CreateDate,
                    Signature = request.Signature,
                    CorrelationId = request.CorrelationId,
                    IsCorrect = request.IsCorrect,
                    IsInteropTestCommunicationCall = request.IsInteropTestCommunicationCall
                };
            messageLogDetails.Request = requestDetail;
            if (response != null)
            {
                var responseDetail = new MessageLogDetails
                {
                    Id = response.Id,
                    Consumer = response.Consumer,
                    ConsumerName = string.Empty,
                    Provider = response.Provider,
                    ProviderName = string.Empty,
                    RoutingToken = response.RoutingToken,
                    RoutingTokenName = string.Empty,
                    Service = response.Service,
                    ServiceMethod = response.ServiceMethod,
                    TransactionId = response.TransactionId,
                    Dir = response.Dir,
                    CallType = response.CallType,
                    PublicKey = response.PublicKey,
                    Status = response.Status,
                    MimeType = response.MimeType,
                    Timestamp = response.Timestamp,
                    TokenTimestamp = response.TokenTimestamp,
                    CreateDate = response.CreateDate,
                    Signature = response.Signature,
                    CorrelationId = response.CorrelationId,
                    IsCorrect = response.IsCorrect,
                    IsInteropTestCommunicationCall = response.IsInteropTestCommunicationCall
                };
                messageLogDetails.Response = responseDetail;
            }
            if (soapFault != null)
                messageLogDetails.ErrorMessage = soapFault.Reason;
            return messageLogDetails;
        }

        // Опис: Методот врши внесување на лог во база
        // Влезни параметри: MessageLog messageLog
        // Излезни параметри: / 
        public void InsertMessageLog(MessageLog messageLog)
        {
            var messagelog = _uow.Context.MessageLogs.Find(messageLog.Id);
            if (messagelog != null)
            {
                throw new DuplicateMessageLogException(messageLog);
            }

            try
            {
                _uow.Context.MessageLogs.Add(messageLog);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                SqlException s = ex.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    throw new DuplicateMessageLogException(messageLog);
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
            finally
            {
                if (_uow.Context != null)
                    _uow.Context.Dispose();
            }

        }

        // Опис: Методот вчитува податоци од база за приказ на Логови, притоа филтрира и сортира според влезните параматери
        // Влезни параметри: индекс за страна, број на записи по страна, консумер, провајдер, сервис, метод, успешност на трансакција, дата од, дата до, насока за сортирање, колона за сортирање 
        // Излезни параметри: излезна листа од Логови (индекс за страна, број на записи по страна вкупен број на записи, записи)

        public PagedCollection<MessageLogPairsViewModel> GetMessageLogsPaged(int pageIndex, int itemsPerPage, string filterConsumer, string filterProvider, string filterService, string filterMethod, bool? filterTransactionSuccess, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {

            if (String.IsNullOrEmpty(filterConsumer))
            {
                filterConsumer = String.Empty;
            }
            if (String.IsNullOrEmpty(filterProvider))
            {
                filterProvider = String.Empty;
            }
            if (String.IsNullOrEmpty(filterService))
            {
                filterService = String.Empty;
            }
            if (String.IsNullOrEmpty(filterMethod))
            {
                filterMethod = String.Empty;
            }

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "RequestTimestamp";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "desc";
            }

            var municipalities = "";
            if (ConfigurationManager.AppSettings["MunicipalitiesKey"] != null)
            {
                municipalities = ConfigurationManager.AppSettings["MunicipalitiesKey"];
            }
            else
            {
                municipalities = "Municipalities";
            }

            var messageLogsDb = new List<MessageLogPairsViewModel>();
            var mLogsFromRealCommunication = _uow.Context.MessageLogs.Where(x => x.IsInteropTestCommunicationCall.Equals(false) && x.Service != municipalities);
            foreach (var group in mLogsFromRealCommunication.GroupBy(x => x.TransactionId))
            {
                var request = group.FirstOrDefault(x => x.Dir == "Request");
                var response = group.FirstOrDefault(x => x.Dir == "Response");
                if (request != null)
                {
                    var itemToAdd = new MessageLogPairsViewModel
                    {
                        HasRequest = true,
                        TransactionId = request.TransactionId,
                        Consumer = request.Consumer,
                        RoutingToken = request.RoutingToken,
                        Service = request.Service,
                        ServiceMethod = request.ServiceMethod,
                        RequestTimestamp = request.Timestamp
                    };
                    if (response != null)
                    {
                        itemToAdd.HasResponse = true;
                        itemToAdd.Provider = response.Provider;
                        itemToAdd.ResponseTimestamp = response.Timestamp;
                    }
                    messageLogsDb.Add(itemToAdd);
                }
            }


            IEnumerable<MessageLogPairsViewModel> filteredItems;

            if (fromDate == null && toDate == null)
            {
                if (filterService != "" && filterMethod != "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service == filterService && x.ServiceMethod == filterMethod.Split('/').Last());
                }
                else if (filterService != "" && filterMethod == "")
                {
                    filteredItems =
                        messageLogsDb.Where(x =>
                                x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                                x.Service == filterService && x.ServiceMethod.Contains(filterMethod));
                }
                else if (filterService == "" && filterMethod != "")
                {
                    filteredItems =
                        messageLogsDb.Where(
                            x =>
                                x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                                x.Service.Contains(filterService) && x.ServiceMethod == filterMethod);
                }
                else
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod));
                }
            }
            else if (fromDate == null)
            {
                var dateTo = toDate.Value.AddDays(1);

                if (filterService != "" && filterMethod != "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service == filterService && x.ServiceMethod == filterMethod &&
                            x.ResponseTimestamp <= dateTo);
                }
                else if (filterService != "" && filterMethod == "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service == filterService && x.ServiceMethod.Contains(filterMethod) &&
                            x.ResponseTimestamp <= dateTo);
                }
                else if (filterService == "" && filterMethod != "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService) && x.ServiceMethod == filterMethod &&
                            x.ResponseTimestamp <= dateTo);
                }
                else
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) &&
                            x.ResponseTimestamp <= dateTo);
                }
            }
            else if (toDate == null)
            {
                if (filterService != "" && filterMethod != "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service == filterService && x.ServiceMethod == filterMethod &&
                             x.RequestTimestamp >= fromDate);
                }
                else if (filterService != "" && filterMethod == "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service == filterService && x.ServiceMethod.Contains(filterMethod) &&
                             x.RequestTimestamp >= fromDate);
                }
                else if (filterService == "" && filterMethod != "")
                {
                    filteredItems =
                        messageLogsDb.Where(
                            x => x.Consumer.Contains(filterConsumer) &&
                                 x.RoutingToken.Contains(filterProvider) &&
                                 x.Service.Contains(filterService) && x.ServiceMethod == filterMethod &&
                                 x.RequestTimestamp >= fromDate);
                }
                else
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) &&
                             x.RequestTimestamp >= fromDate);
                }
            }
            else
            {
                var dateTo = toDate.Value.AddDays(1);

                if (filterService != "" && filterMethod != "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service == filterService && x.ServiceMethod.Contains(filterMethod.Split('/').Last()) &&
                             x.RequestTimestamp >= fromDate && x.RequestTimestamp <= dateTo);
                }
                else if (filterService != "" && filterMethod == "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service == filterService && x.ServiceMethod.Contains(filterMethod.Split('/').Last()) &&
                             x.RequestTimestamp >= fromDate && x.RequestTimestamp <= dateTo);
                }
                else if (filterService == "" && filterMethod != "")
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod.Split('/').Last()) &&
                             x.RequestTimestamp >= fromDate && x.RequestTimestamp <= dateTo);
                }
                else
                {
                    filteredItems =
                    messageLogsDb.Where(
                        x => x.Consumer.Contains(filterConsumer) &&
                             x.RoutingToken.Contains(filterProvider) &&
                             x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod.Split('/').Last()) &&
                             x.RequestTimestamp >= fromDate && x.RequestTimestamp <= dateTo);
                }

            }

            if (filterTransactionSuccess.HasValue)
            {
                if (filterTransactionSuccess.Value)
                {
                    filteredItems = filteredItems.Where(x => x.HasRequest && x.HasResponse);
                }
                else
                {
                    filteredItems = filteredItems.Where(x => !x.HasRequest || !x.HasResponse);
                }
            }

            if (sortDir == "desc")
            {
                filteredItems = filteredItems.OrderBy(sortCol + " descending");
            }
            else if (sortDir == "asc")
            {
                filteredItems = filteredItems.OrderBy(sortCol + " ascending");
            }
            else
            {
                filteredItems = filteredItems.OrderBy(x => x.RequestTimestamp);
            }

            var temp = filteredItems.ToList();
            foreach (var item in temp)
            {
                if (item.ServiceMethod != "")
                {
                    item.ServiceMethod = item.ServiceMethod.Split('/').Last();
                }
            }

            List<MessageLogPairsViewModel> displayItems = temp.ToList();

            var totalSize = displayItems.Count();

            var pagedItems = displayItems.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            return new PagedCollection<MessageLogPairsViewModel>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }

        // Опис: Методот врши вчитување на сите логови од база во филтрирана форма 
        // Влезни параметри: податочни вредности filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol
        // Излезни параметри: List<MessageLogExcelDTO>
        public List<MessageLogExcelDTO> GetFilteredMessageLogs(string filterConsumer, string filterProvider, string filterService, string filterMethod, bool? filterTransactionSuccess, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            if (String.IsNullOrEmpty(filterConsumer))
                filterConsumer = string.Empty;
            if (String.IsNullOrEmpty(filterProvider))
                filterProvider = string.Empty;
            if (String.IsNullOrEmpty(filterService))
                filterService = string.Empty;
            if (String.IsNullOrEmpty(filterMethod))
                filterMethod = string.Empty;

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "Consumer";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }

            //var municipalities = ConfigurationManager.AppSettings["MunicipalitiesKey"];
            string municipalities = ConfigurationManager.AppSettings["MunicipalitiesKey"] ?? "Municipalities";

            var messageLogsDb = new List<MessageLogExcelDTO>();
            var mLogsFromRealCommunication = _uow.Context.MessageLogs.Where(x => x.IsInteropTestCommunicationCall.Equals(false) && x.Service != municipalities);
            foreach (var group in mLogsFromRealCommunication
                .GroupJoin(_uow.Context.SoapFaults, msg => msg.TransactionId, sf => sf.TransactionId, (msg, sf) => new { msg, sf })
                .GroupBy(x => x.msg.TransactionId))
            {
                var request = group.FirstOrDefault(x => x.msg.Dir == "Request");
                var response = group.FirstOrDefault(x => x.msg.Dir == "Response");
                if (request != null)
                {
                    var itemToAdd = new MessageLogExcelDTO
                    {
                        HasResponse = "Не",
                        TransactionId = request.msg.TransactionId,
                        Consumer = request.msg.Consumer,
                        RoutingToken = request.msg.RoutingToken,
                        Service = request.msg.Service,
                        ServiceMethod = request.msg.ServiceMethod,
                        RequestTimestamp = request.msg.Timestamp.ToString(),
                        //Error = request.sf.Select(x => x.Reason).ToString()
                    };
                    if (response != null)
                    {
                        itemToAdd.HasResponse = "Да";
                        itemToAdd.ServiceMethod = response.msg.ServiceMethod;
                        itemToAdd.Provider = response.msg.Provider;
                        itemToAdd.ResponseTimestamp = response.msg.Timestamp.ToString();
                    }
                    messageLogsDb.Add(itemToAdd);
                }
            }

            IEnumerable<MessageLogExcelDTO> filteredItems;
            if (fromDate == null && toDate == null)
            {
                filteredItems = messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) &&
                            x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService)
                    );
            }
            else if (fromDate == null)
            {
                var dateTo = toDate.Value.AddDays(1);
                filteredItems = messageLogsDb
                    .Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) &&
                            x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService) &&
                            x.ResponseTimestamp.AsDateTime() <= dateTo
                    );
            }
            else if (toDate == null)
            {
                filteredItems = messageLogsDb
                    .Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) &&
                            x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService) &&
                            x.RequestTimestamp.AsDateTime() >= fromDate
                    );
            }
            else
            {
                var dateTo = toDate.Value.AddDays(1);
                filteredItems =
                    messageLogsDb.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Service.Contains(filterService) &&
                            x.RequestTimestamp.AsDateTime() >= fromDate && x.ResponseTimestamp.AsDateTime() <= dateTo);
            }

            if (filterTransactionSuccess.HasValue)
            {
                if (filterTransactionSuccess.Value)
                {
                    filteredItems = filteredItems.Where(x => x.ResponseTimestamp != null).ToList();
                }
                else
                {
                    filteredItems = filteredItems.Where(x => x.ResponseTimestamp == null).ToList();
                }
            }

            if (sortDir == "asc")
            {
                filteredItems = filteredItems.OrderBy(sortCol);
            }
            else if (sortDir == "desc")
            {
                filteredItems = filteredItems.OrderBy(sortCol + " descending");
            }
            else
            {
                filteredItems = filteredItems.OrderBy(x => x.RequestTimestamp);
            }

            return filteredItems.ToList();
        }

        // Опис: Методот врши вчитување на логови од база според датум на креирање
        // Влезни параметри: податочна вредност createdDate
        // Излезни параметри: IEnumerable<MessageLog> 
        public List<MessageLog> GetMessageLogsByDate(DateTime createdDate)
        {
            var allMessageLogs = _uow.Context.MessageLogs;
            //messageLog => messageLog.CreateDate.Date == createdDate.Date

            //IMPORTANT!!! VAKA NE MI GI ZEMASE LOGOVITE OD ODREDEN DATUM LOKALNO
            //var messageLogsFromToday = allMessageLogs.Where(x => DbFunctions.TruncateTime(x.CreateDate) == createdDate.Date && x.IsInteropTestCommunicationCall.Equals(false)).ToList();

            var messageLogsFromToday = from messageLog in allMessageLogs
                where DbFunctions.TruncateTime(messageLog.CreateDate) == DbFunctions.TruncateTime(createdDate.Date)
                select messageLog;

            return messageLogsFromToday.ToList();
        }

        // Опис: Методот врши измена на лого во база
        // Влезни параметри: податочна вредност messageLogId, isCorrect
        // Излезни параметри: / 
        public void UpdateMessageLog(long messageLogId, bool isCorrect)
        {
            var messageLog = _uow.Context.MessageLogs.FirstOrDefault(ml => ml.Id == messageLogId);
            if (messageLog != null)
            {
                messageLog.IsCorrect = isCorrect;
                _uow.Context.SaveChanges();
            }
        }

        public void UpdateMessageLog(string dir, string timestampToken, string serviceMethod, string publicKey, string messageLogId)
        {
            var messageLog = _uow.Context.MessageLogs.FirstOrDefault(ml => ml.TransactionId == new Guid(messageLogId) && ml.Dir == dir);
            if (messageLog != null)
            {
                messageLog.TokenTimestamp = timestampToken;
                if (!string.IsNullOrEmpty(serviceMethod) && !string.IsNullOrEmpty(publicKey))
                {
                    messageLog.ServiceMethod = serviceMethod;
                    messageLog.PublicKey = publicKey;
                }
                _uow.Context.SaveChanges();
            }
        }
    }
}
