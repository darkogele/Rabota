using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using System.Linq.Dynamic;
using System.Web.Configuration;

namespace Interop.CS.Models.Repository
{
    public class MessageLogStatisticRepository : IMessageLogStatisticRepository
    {
        private readonly IUnitOfWork _uow;

        public MessageLogStatisticRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void Dispose()
        {
            _uow.Context.Dispose();
        }

        //Опис: Методот додава нов запис во базата за Статистика на Логови спред влезниот параметар
        //Се врши проверка дали таков запис веќе постои во база, и ако постои, се јавува грешка
        //Влезни параметри: Објект од класата MessageLogStatistic

        public void InsertMessageLogStatistic(MessageLogStatistic messageLogStatistic)
        {
            try
            {
                _uow.Context.MessageLogStatistic.Add(messageLogStatistic);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                var sqlException = exception.InnerException.InnerException as SqlException;
                if (sqlException != null)
                {
                    throw new DbUpdateException(sqlException.Message);
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

        //Опис: Методот ги вчитува сите записи од базата за Статистика на Логови
        //Излезни параметри: Листа од сите записи за Статистика на Логови
        public IEnumerable<MessageLogStatistic> GetMessageLogStatistics()
        {
            var a = _uow.Context.MessageLogStatistic.ToList();
            return a;
        }

        //Опис: Методот вчитува записи од базата за Статистика на Логови, притоа филтрира според влезните параметри
        //Влезни параметри: корисник, провајдер, успешно, неуспешно, дата од (за Логови), дата до (за Логови)
        //Излезни параметри: Листа од исфилтрирани записи за Статистика на Логови
        public IQueryable<MessageLogStatistic> GetMessageLogStatistics(string consumer, string provider, bool successfully, bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string service, string sortDir, string sortCol)
        {
            string serviceCodeToBeIgnored = ConfigurationManager.AppSettings["MunicipalitiesKey"] ?? "Municipalities";

            IQueryable<MessageLogStatistic> mlStatistics = _uow.Context.MessageLogStatistic.Where(messageLog => messageLog.Service != serviceCodeToBeIgnored);

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "CreateDate";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "desc";
            }

            if (!string.IsNullOrEmpty(service))
            {
                mlStatistics = mlStatistics.Where(x => x.Service == service);
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                mlStatistics = mlStatistics.Where(messageLog => DbFunctions.TruncateTime(messageLog.CreateDate) >= fromDate.Value && DbFunctions.TruncateTime(messageLog.CreateDate) <= toDate.Value);
            }
            else
            {
                if (fromDate.HasValue)
                {
                    mlStatistics = mlStatistics.Where(messageLog => DbFunctions.TruncateTime(messageLog.CreateDate) >= fromDate.Value);
                }
                if (toDate.HasValue)
                {
                    mlStatistics = mlStatistics.Where(messageLog => DbFunctions.TruncateTime(messageLog.CreateDate) <= toDate.Value);
                }
            }



            if (!string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
            {
                //Sega vaka e postaveno poradi toa shto imame MIM1$$ vo vrednosta na RoutingToken vo tabelata Statistics
                //mlStatistics = mlStatistics.Where(messageLog => messageLog.RoutingToken == provider && messageLog.Consumer == consumer);
                mlStatistics = mlStatistics.Where(messageLog => messageLog.RoutingToken.Contains(provider) && messageLog.Consumer.Contains(consumer));
            }
            else
            {
                if (!string.IsNullOrEmpty(provider))
                {
                    //Sega vaka e postaveno poradi toa shto imame MIM1$$ vo vrednosta na RoutingToken vo tabelata Statistics
                    //mlStatistics = mlStatistics.Where(messageLog => messageLog.RoutingToken == provider);
                    mlStatistics = mlStatistics.Where(messageLog => messageLog.RoutingToken.Contains(provider));
                }
                if (!string.IsNullOrEmpty(consumer))
                {
                    //Sega vaka e postaveno poradi toa shto imame MIM1$$ vo vrednosta na RoutingToken vo tabelata Statistics
                    //mlStatistics = mlStatistics.Where(messageLog => messageLog.Consumer == consumer);
                    mlStatistics = mlStatistics.Where(messageLog => messageLog.Consumer.Contains(consumer));
                }
            }

            if (sortDir == "desc")
            {
                mlStatistics = mlStatistics.OrderBy(sortCol + " descending");
            }
            else if (sortDir == "asc")
            {
                mlStatistics = mlStatistics.OrderBy(sortCol + " ascending");
            }
            else
            {
                mlStatistics = mlStatistics.OrderBy(x => x.CreateDate);
            }

            return mlStatistics;
        }



        //Опис: Методот пребарува запис од Статистика за Логови спред влезниот параметар
        //Влезни параметри: Id на трансакција
        //Излезни параметри: Објект од класата MessageLogStatisticDetails

        public bool ParticipantContainsMim(string consumer)
        {
            var mims = new[] { "MIM1$$", "MIM2$$" };
            bool consumerContainsMim = mims.Any(m => consumer.Contains(m));
            return consumerContainsMim;
        }

        public string GetParticipantNameWithoutMim(string consumer)
        {
            if (ParticipantContainsMim(consumer))
            {
                consumer = consumer.Remove(0, 6);
            }
            return consumer;
        }
        public MessageLogStatisticDetails GetMessageLogStatisticByTransactionId(Guid transactionId, string consumer, string routingToken)
        {
            var messageLogsStatisticByTransaction = _uow.Context.MessageLogStatistic.Where(x => x.TransactionId == transactionId);

            var messageLogsStatisticByParticipantCode =
                messageLogsStatisticByTransaction.GroupBy(messageLog => messageLog.ParticipantCode).ToList();


            var messageLogStatisticDetails = new MessageLogStatisticDetails();

            foreach (var messageLogByUri in messageLogsStatisticByParticipantCode)
            {
                if (messageLogByUri.Key == "CS")
                {
                    messageLogStatisticDetails.LogCS = messageLogByUri.ToList();
                    messageLogStatisticDetails.DirCS = messageLogByUri.Count();
                }
                else
                {
                    //if (messageLogStatisticDetails.LogParticipantConsumer == null)
                    //{
                    //    messageLogStatisticDetails.LogParticipantConsumer = messageLogByUri.ToList();
                    //    messageLogStatisticDetails.DirParticipantX = messageLogByUri.Count();
                    //}
                    //else
                    //{
                    //    messageLogStatisticDetails.LogParticipantProvider = messageLogByUri.ToList();
                    //    messageLogStatisticDetails.DirParticipantY = messageLogByUri.Count();
                    //}


                    if (GetParticipantNameWithoutMim(messageLogByUri.Key) == consumer)
                    //dodadena logika za vadenje na delot MIMx$$ od samiot consumer nezavisno za koj Bus stanuva zbor
                    //if (messageLogByUri.Key == consumer)
                    {
                        //tuka polni LogParticipantConsumer
                        messageLogStatisticDetails.LogParticipantConsumer = messageLogByUri.ToList();
                        messageLogStatisticDetails.DirParticipantConsumer = messageLogByUri.Count();

                    }
                    if (GetParticipantNameWithoutMim(messageLogByUri.Key) == GetParticipantNameWithoutMim(routingToken))
                    {
                        //tuka polni LogParticipantProvider
                        messageLogStatisticDetails.LogParticipantProvider = messageLogByUri.ToList();
                        messageLogStatisticDetails.DirParticipantProvider = messageLogByUri.Count();
                    }
                }
            }

            return messageLogStatisticDetails;
        }

        public bool MessageLogStatisticExist(Guid transactionId, string dir, string participantCode)
        {
            var entryExist =
                _uow.Context.MessageLogStatistic.Any(
                    x => x.TransactionId == transactionId && x.Dir == dir && x.ParticipantCode == participantCode);
            if (entryExist) return true;
            return false;
        }

        public PagedCollection<MessageLogStatisticDTO> GetMessageLogsStatisticPaged(int pageIndex, int itemsPerPage)
        {
            var totalSize = _uow.Context.MessageLogStatistic.Count();

            var items = _uow.Context.MessageLogStatistic.Select(x => new MessageLogStatisticDTO
            {
                TransactionId = x.TransactionId,
                Consumer = x.Consumer,
                ConsumerName = x.ConsumerName,
                Provider = x.Provider,
                CreateDate = x.CreateDate,
                Dir = x.Dir,
                RoutingToken = x.RoutingToken,
                RoutingTokenName = x.RoutingTokenName,
                Service = x.Service,
                ServiceMethod = x.ServiceMethod,
                Timestamp = x.Timestamp,
                ParticipantUri = x.ParticipantUri
            }).OrderBy(x => x.TransactionId).Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            return new PagedCollection<MessageLogStatisticDTO>(pageIndex, itemsPerPage, totalSize, items);
        }
    }
}
