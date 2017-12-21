using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using NLog;
using System.Linq.Dynamic;

namespace Interop.CS.Models.Repository
{
    public class MessageLogRepository : IMessageLogRepository
    {
        private readonly IUnitOfWork _uow;

        public MessageLogRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //Опис: Методот вчитува податоци од базата за Логови
        //Излезни параметри: Листа од сите записи од Логови
        public IEnumerable<MessageLog> GetMessageLogs()
        {
            return _uow.Context.MessageLogs.ToList();
        }

        //Опис: Методот пребарува запис од базата за Логови според влезниот параметар
        //Влезни параметри: Id за лог
        //Излезни параметри: Објект од класата MessageLog
        public MessageLog GetMessageLogById(long messageLogId)
        {
            var message = _uow.Context.MessageLogs.Find(messageLogId);

            if (message == null)
            {
                throw new NotFoundMessageLogException(messageLogId);
            }

            return message;
        }

        //Опис: Методот пребарува запис од базата за Логови според влензите параметри
        //Влезни параметри: Id на трансакција, насока
        //Излезни параметри: Објект од класата MessageLog
        public MessageLog GetMessageLogByTransactionId(Guid transactionId, string dir)
        {
            var messageId = _uow.Context.MessageLogs.FirstOrDefault(x => x.TransactionId == transactionId && x.Dir == dir);

            if (messageId == null)
            {
                throw new NotFoundMessageLogTransactionIdException(transactionId, dir);
            }

            var message = _uow.Context.MessageLogs.Find(messageId.Id);

            return message;
        }

        //Опис: Методот креира нов запис во Логови
        //Влезни параметри: Објект од класата MessageLog
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
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        throw new DbUpdateException(validationError.ErrorMessage);
                    }
                }
            }

        }

        // Опис: Методот вчитува податоци од база за приказ на Логови, притоа филтрира и сортира според влезните параматери
        // Влезни параметри: индекс за страна, број на записи по страна, консумер, провајдер, насока, сервис, метод, дата од, дата до, насока за сортирање, колона за сортирање 
        // Излезни параметри: излезна листа од Логови (индекс за страна, број на записи по страна вкупен број на записи, записи)
        public PagedCollection<MessageLogDTO> GetMessageLogsPaged(int pageIndex, int itemsPerPage, string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            //var contex = new InteropContext();
            //var res = contex.MessageLogs.Count();
            //LogToLocalFile("res e:" + res);

            IQueryable<MessageLogDTO> items;

            if (String.IsNullOrEmpty(filterConsumer))
            {

                filterConsumer = String.Empty;
            }
            if (String.IsNullOrEmpty(filterProvider))
            {
                filterProvider = String.Empty;
            }
            if (String.IsNullOrEmpty(filterDir))
            {
                filterDir = String.Empty;
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
                sortCol = "Name";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }


            if (fromDate == null && toDate == null)
            {
                items =
                    _uow.Context.MessageLogs.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Dir.Contains(filterDir) && x.Service.Contains(filterService) &&
                            x.ServiceMethod.Contains(filterMethod)).Select(x => new MessageLogDTO
                            {
                                TransactionId = x.TransactionId,
                                Consumer = x.Consumer,
                                Provider = x.Provider,
                                RoutingToken = x.RoutingToken,
                                CreateDate = x.CreateDate,
                                Dir = x.Dir,
                                Service = x.Service,
                                ServiceMethod = x.ServiceMethod,
                                Timestamp = x.Timestamp
                            });

            }
            else if (fromDate == null && toDate != null)
            {
                var dateTo = toDate.Value.AddDays(1);
                items =
                    _uow.Context.MessageLogs.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Dir.Contains(filterDir) && x.Service.Contains(filterService) &&
                            x.ServiceMethod.Contains(filterMethod) && x.Timestamp <= dateTo).Select(x => new MessageLogDTO
                            {
                                TransactionId = x.TransactionId,
                                Consumer = x.Consumer,
                                Provider = x.Provider,
                                RoutingToken = x.RoutingToken,
                                CreateDate = x.CreateDate,
                                Dir = x.Dir,
                                Service = x.Service,
                                ServiceMethod = x.ServiceMethod,
                                Timestamp = x.Timestamp
                            });
            }
            else if (fromDate != null && toDate == null)
            {
                items =
                    _uow.Context.MessageLogs.Where(
                     x => x.Consumer.Contains(filterConsumer) &&
                            x.RoutingToken.Contains(filterProvider) && x.Dir.Contains(filterDir) &&
                            x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) &&
                            x.Timestamp >= fromDate).Select(x => new MessageLogDTO
                            {
                                TransactionId = x.TransactionId,
                                Consumer = x.Consumer,
                                Provider = x.Provider,
                                RoutingToken = x.RoutingToken,
                                CreateDate = x.CreateDate,
                                Dir = x.Dir,
                                Service = x.Service,
                                ServiceMethod = x.ServiceMethod,
                                Timestamp = x.Timestamp
                            });
            }
            else
            {
                var dateTo = toDate.Value.AddDays(1);
                items =
                      _uow.Context.MessageLogs.Where(
                          x => x.Consumer.Contains(filterConsumer) &&
                              x.RoutingToken.Contains(filterProvider) && x.Dir.Contains(filterDir) &&
                              x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) &&
                              x.Timestamp >= fromDate && x.Timestamp <= dateTo).Select(x => new MessageLogDTO
                              {
                                  TransactionId = x.TransactionId,
                                  Consumer = x.Consumer,
                                  Provider = x.Provider,
                                  RoutingToken = x.RoutingToken,
                                  CreateDate = x.CreateDate,
                                  Dir = x.Dir,
                                  Service = x.Service,
                                  ServiceMethod = x.ServiceMethod,
                                  Timestamp = x.Timestamp
                              });
            }

            if (sortDir == "asc")
            {
                items = items.OrderBy(sortCol);
            }
            else if (sortDir == "desc")
            {
                items = items.OrderBy(sortCol + " descending");
            }
            else
            {
                items = items.OrderBy(x => x.Timestamp);
            }

            List<MessageLogDTO> pagedItems = items.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            var totalSize = items.Count();
            return new PagedCollection<MessageLogDTO>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());

        }

        //Опис: Методот вчитува податоци од базата за Логови, притоа филтрира според влезните параметри
        //Влезни параметри по кои се филтрира: корисник, провајдер, насока, сервис, метод, датум од (за временски печат),датум до (за временски печат)
        //Излезни параметри: Листа од исфилтрирани записи од базата за Логови, во опаѓачки редослед според временската ознака
        public List<MessageLogExcelDTO> GetFilteredMessageLogs(string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            List<MessageLog> items;
            var itemsDto = new List<MessageLogExcelDTO>();
            if (String.IsNullOrEmpty(filterConsumer))
                filterConsumer = "";
            if (String.IsNullOrEmpty(filterProvider))
                filterProvider = "";
            if (String.IsNullOrEmpty(filterDir))
                filterDir = "";
            if (String.IsNullOrEmpty(filterService))
                filterService = "";
            if (String.IsNullOrEmpty(filterMethod))
                filterMethod = "";


            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "Name";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }


            if (fromDate == null && toDate == null)
            {
                items = _uow.Context.MessageLogs
                    .Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) &&
                            x.RoutingToken.Contains(filterProvider) &&
                            x.Dir.Contains(filterDir) &&
                            x.Service.Contains(filterService) &&
                            x.ServiceMethod.Contains(filterMethod)
                            ).ToList();
            }
            else if (fromDate == null && toDate != null)
            {
                var dateTo = toDate.Value.AddDays(1);
                items =
                    _uow.Context.MessageLogs.Where(
                        x =>
                            x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) &&
                            x.Dir.Contains(filterDir) && x.Service.Contains(filterService) &&
                            x.ServiceMethod.Contains(filterMethod) && x.Timestamp <= dateTo).ToList();
            }
            else if (fromDate != null && toDate == null)
            {
                items =
                    _uow.Context.MessageLogs.Where(
                     x => x.Consumer.Contains(filterConsumer) &&
                            x.RoutingToken.Contains(filterProvider) && x.Dir.Contains(filterDir) &&
                            x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) &&
                            x.Timestamp >= fromDate).ToList();
            }
            else
            {
                var dateTo = toDate.Value.AddDays(1);
                items =
                      _uow.Context.MessageLogs.Where(
                          x => x.Consumer.Contains(filterConsumer) &&
                              x.RoutingToken.Contains(filterProvider) && x.Dir.Contains(filterDir) &&
                              x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) &&
                              x.Timestamp >= fromDate && x.Timestamp <= dateTo).ToList();
            }
            //else
            //{
            //    items = _uow.Context.MessageLogs.Where(x => x.Consumer.Contains(filterConsumer) && x.RoutingToken.Contains(filterProvider) && x.Dir.Contains(filterDir) && x.Service.Contains(filterService) && x.ServiceMethod.Contains(filterMethod) && x.CreateDate >= fromDate && x.CreateDate <= toDate).OrderByDescending(x => x.CreateDate).ToList();
            //}
            var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
            foreach (var messageLog in items)
            {
                itemsDto.Add(new MessageLogExcelDTO
                {
                    Consumer = messageLog.Consumer,
                    CreateDate = messageLog.CreateDate.ToString("dd.MM.yyyy hh:mm:ss", macCultureInfo),
                    Dir = messageLog.Dir,
                    Provider = messageLog.Provider,
                    RoutingToken = messageLog.RoutingToken,
                    Service = messageLog.Service,
                    ServiceMethod = messageLog.ServiceMethod,
                    Timestamp = messageLog.Timestamp.ToString("dd.MM.yyyy hh:mm:ss", macCultureInfo),
                    TransactionId = messageLog.TransactionId,
                    TokenTimestamp = messageLog.TokenTimestamp

                });
            }

            if (sortDir == "asc")
            {
                itemsDto = itemsDto.OrderBy(sortCol).ToList();
            }
            else if (sortDir == "desc")
            {
                itemsDto = itemsDto.OrderBy(sortCol + " descending").ToList();
            }
            else
            {
                itemsDto = itemsDto.OrderBy(x => x.Timestamp).ToList();
            }

            return itemsDto.ToList();
        }

        //Опис: Методот пребарува запис од базата за Логови според влезните параметри
        //Влезни параметри: Датум на креирање
        //Излезни параметри: Објект од класата MessageLog
        public IEnumerable<MessageLog> GetMessageLogsByDate(DateTime createdDate)
        {
            var allMessageLogs = _uow.Context.MessageLogs.Where(x => x.IsInteropTestCommunicationCall.Equals(false)).ToList();
            var messageLogsFromToday = allMessageLogs.Where(messageLog => messageLog.CreateDate.Date == createdDate.Date).ToList();
            return messageLogsFromToday;
        }

        //Опис: Методот ажурира запис во базата за Логови, притоа тој запис се пребарува според влезните параметри
        //Влезни параметри: Id за лог, и параметар кој служи да прикаже таков лог постои
        public void UpdateMessageLog(long messageLogId, bool isCorrect)
        {
            var messageLog = _uow.Context.MessageLogs.FirstOrDefault(ml => ml.Id == messageLogId);
            if (messageLog != null)
            {
                messageLog.IsCorrect = isCorrect;
                _uow.Context.SaveChanges();
            }
        }

        public List<MessageLogStatistic> GetAndCreateMissingMesageLogsInStatistic()
        {
            //var transactionIdsForCase = new List<string>();
            var mlCreateInStatistic = new List<MessageLogStatistic>();

            var mlFromCs = _uow.Context.MessageLogStatistic.Where(statistic => statistic.ParticipantCode == "CS");
            var statisticGroupedByTransaction = mlFromCs.GroupBy(log => log.TransactionId).ToList();
            var statisticNoPairs = statisticGroupedByTransaction.Where(dirsByTransaction => dirsByTransaction.Count() == 1);
            if (statisticNoPairs.Any())
            {
                foreach (var groupedStatistic in statisticNoPairs)
                {
                    foreach (var statistic in groupedStatistic)
                    {
                        IQueryable<MessageLog> existInMessageLog = _uow.Context.MessageLogs.Where(log => log.TransactionId == statistic.TransactionId && log.Dir != statistic.Dir);
                        if (existInMessageLog.Any())
                        {
                            mlCreateInStatistic.Add(new MessageLogStatistic
                            {
                                Consumer = existInMessageLog.FirstOrDefault().Consumer,
                                Provider = existInMessageLog.FirstOrDefault().Provider,
                                RoutingToken = existInMessageLog.FirstOrDefault().RoutingToken,
                                Service = existInMessageLog.FirstOrDefault().Service,
                                ServiceMethod = existInMessageLog.FirstOrDefault().ServiceMethod,
                                TransactionId = existInMessageLog.FirstOrDefault().TransactionId,
                                Dir = existInMessageLog.FirstOrDefault().Dir,
                                CallType = existInMessageLog.FirstOrDefault().CallType,
                                PublicKey = existInMessageLog.FirstOrDefault().PublicKey,
                                Status = existInMessageLog.FirstOrDefault().Status,
                                MimeType = existInMessageLog.FirstOrDefault().MimeType,
                                Timestamp = existInMessageLog.FirstOrDefault().Timestamp,
                                CreateDate = existInMessageLog.FirstOrDefault().CreateDate,
                                Signature = existInMessageLog.FirstOrDefault().Signature,
                                CorrelationId = existInMessageLog.FirstOrDefault().CorrelationId,
                                ParticipantUri = "CS",
                                TokenTimestamp = existInMessageLog.FirstOrDefault().TokenTimestamp,
                                IsCorrect = existInMessageLog.FirstOrDefault().IsCorrect,
                                ParticipantCode = "CS",
                                ConsumerName = statistic.ConsumerName,
                                RoutingTokenName = statistic.RoutingTokenName
                            });

                            //transactionIdsForCase.Add(existInMessageLog.FirstOrDefault().TransactionId.ToString());
                        }
                    }
                }
            }
            return mlCreateInStatistic;
        }

        public List<string> GetAndCreateMissingMesageLogs()
        {
            var transactionIdsForCase = new List<string>();
            var messageLogsGroupedByTransaction = _uow.Context.MessageLogs.GroupBy(log => log.TransactionId).ToList();
            var messageLogsNoPairs =
                messageLogsGroupedByTransaction.Where(dirsByTransaction => dirsByTransaction.Count() == 1);
            if (messageLogsNoPairs.Any())
            {
                foreach (var groupedMessageLog in messageLogsNoPairs)// grupirano po transakcija da nema par Request/Response, a sepak nema SoapFault, greskata na Kornel
                {
                    foreach (var messageLog in groupedMessageLog)
                    {
                        var existInSoapFault = _uow.Context.SoapFaults.Any(fault => fault.TransactionId == messageLog.TransactionId);

                        if (!existInSoapFault)
                        {
                            //var logTokenTimestamp = messageLog.TokenTimestamp.Remove(100, 15);
                            //logTokenTimestamp = logTokenTimestamp.Insert(101, "T/gBUGgMWct3Cik");
                            if (messageLog.Dir == "Request")
                            {
                                InsertMessageLog(new MessageLog
                                {
                                    Consumer = messageLog.Consumer,
                                    Provider = messageLog.RoutingToken,
                                    RoutingToken = messageLog.RoutingToken,
                                    Service = messageLog.Service,
                                    ServiceMethod = messageLog.ServiceMethod,
                                    TransactionId = messageLog.TransactionId,
                                    Dir = "Response",
                                    CallType = messageLog.CallType,
                                    PublicKey = messageLog.PublicKey,
                                    Status = "200",
                                    MimeType = "application/soap+xml; charset=utf-8",
                                    Timestamp = messageLog.Timestamp.AddSeconds(1),
                                    CreateDate = messageLog.CreateDate.AddSeconds(1),
                                    Signature = messageLog.Signature,
                                    CorrelationId = messageLog.CorrelationId,
                                    TokenTimestamp = messageLog.TokenTimestamp,
                                    IsCorrect = messageLog.IsCorrect,
                                    IsInteropTestCommunicationCall = messageLog.IsInteropTestCommunicationCall
                                });
                                transactionIdsForCase.Add(messageLog.TransactionId.ToString());
                            }
                            else if (messageLog.Dir == "Response")
                            {
                                InsertMessageLog(new MessageLog
                                {
                                    Consumer = messageLog.Consumer,
                                    Provider = "",
                                    RoutingToken = messageLog.RoutingToken,
                                    Service = messageLog.Service,
                                    ServiceMethod = messageLog.ServiceMethod,
                                    TransactionId = messageLog.TransactionId,
                                    Dir = "Request",
                                    CallType = messageLog.CallType,
                                    PublicKey = messageLog.PublicKey,
                                    Status = "",
                                    MimeType = "",
                                    Timestamp = messageLog.Timestamp.AddSeconds(1),
                                    CreateDate = messageLog.CreateDate.AddSeconds(1),
                                    Signature = messageLog.Signature,
                                    CorrelationId = messageLog.CorrelationId,
                                    TokenTimestamp = messageLog.TokenTimestamp,
                                    IsCorrect = messageLog.IsCorrect,
                                    IsInteropTestCommunicationCall = messageLog.IsInteropTestCommunicationCall
                                });
                                transactionIdsForCase.Add(messageLog.TransactionId.ToString());
                            }
                           
                        }
                    }

                }
            }

            return transactionIdsForCase;
        }


        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
