using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using System.Linq.Expressions;

namespace Interop.CS.Models.Repository
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IUnitOfWork _uow;

        public ParticipantRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //Опис: Методот ги вчитува сите учесници од база
        //Излезни параметри: листа од сите учесници
        public IEnumerable<Participant> GetParticipants()
        {
            return _uow.Context.Participants;
        }
        //Опис: Методот ги вчитува сите учесници каде што полето IsActive од табелата Participants во база има вредност True
       
        public IEnumerable<Participant> GetInternalParticipants(bool? isActive)
        {
            var participants = _uow.Context.Participants.Where(p => p.Code.Contains("MIM1$$"));
            if (isActive.HasValue)
            {
                participants = participants.Where(p => p.IsActive == isActive.Value);
        }
            return participants;
        }

        public IEnumerable<Participant> GetExternalParticipants(bool? isActive)
        {
            var participants = _uow.Context.Participants.Where(p => !p.Code.Contains("MIM1$$"));
            if (isActive.HasValue)
            {
                participants = participants.Where(p => p.IsActive == isActive.Value);
            }
            return participants;
        }


        //Опис: Методот вчитува податоци од база за приказ на Учесници, притоа филтрира според влезните параметри
        //Влезни параметри: код на учесник
        //Излезни параметри: Учесник кој што има код како влезниот параметар
        public Participant GetParticipant(string participantCode)
        {
            var participant = _uow.Context.Participants.Find(participantCode);
            if (participant == null)
            {
                throw new NotFoundParticipantException(participantCode);
            }

            return participant;
        }

        public Participant GetParticipantByBus(string participantCode)
        {
            var participant = _uow.Context.Participants.FirstOrDefault(c => c.Code.Contains(participantCode));
            if (participant == null)
            {
                throw new NotFoundParticipantException(participantCode);
            }

            return participant;
        }


        //Опис: Методот креира нов учесник во табелата Учесници во база, и притоа проверува дали веќе постои таков учесник
        //Доколку постои, се појавува грешка дека не може да се креира учесник со тој код,бидејќи таков веќе постои
        //Влезни параметри: Објект од класата Учесник
        
        public void CreateParticipant(Participant participant)
        {
            var participantExist = _uow.Context.Participants.Find(participant.Code);
            if (participantExist != null)
            {
                throw new DuplicateParticipantException(participant);
            }

            if (!participant.Code.Contains("MIM1$$") && !participant.Code.Contains("MIM2$$"))
            {
                participant.Code = "MIM1$$" + participant.Code;
            }

            UrlAttribute.IsValid(participant.Uri);

            try
            {
                _uow.Context.Participants.Add(participant);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException s = ex.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    throw new DuplicateParticipantException(participant);
                }
            }
        }


        //Опис: Методот вчитува јавен клуч од база за приказ на Учесници, според влезниот параметар
        //Влезни параметри: Код на учесник
        //Излезни параметри: Јавен клуч за код на учесник како влезниот параметар
        public string GetPublicKey(string participantCode)
        {
            var participant = _uow.Context.Participants.Find(participantCode);
            if (participant == null)
            {
                throw new NotFoundPublicKeyException(participantCode);
            }
            return participant.PublicKey;
        }

        //Опис: Методот врши ажурирање на даден учесник
        //Влезни параметри: Објект од класата Учесник
        //Дополнително се проверува дали постои таков учесник, и доколку не постои, се јавува грешка дека не може истиот да се ажурира
        
        public void UpdateParticipant(Participant participant)
        {
            var participantExist = _uow.Context.Participants.Find(participant.Code);

            if (participantExist == null)
            {
                throw new NotFoundParticipantException(participant.Code);
            }

            UrlAttribute.IsValid(participant.Uri);

            try
            {
                _uow.Context.Entry(participantExist).State = EntityState.Detached;
                _uow.Context.Participants.Attach(participant);
                _uow.Context.Entry(participant).State = EntityState.Modified;
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo
            }

        }

        //Методот брише учесник, и притоа прво се врши проверка дали тој учесник постои, ако не постои, се јавува грешка
        //Потоа се бараат сите записи од пристапната листа кои за код за корисник го имаат истиот код како влезниот параметар
        //Се бараат сите сервиси кои за код за учесник го имаат истиот код како влезниот параметар
        //Се бришат сите записи од пристапната листа и записите од сервиси
        //На крај се брише учесникот
        //Влезни параметри: код за учесник
        public void DeleteParticipant(string participantCode)
        {
            var participantExist = _uow.Context.Participants.Find(participantCode);

            if (participantExist == null)
            {
                throw new NotFoundParticipantException(participantCode);
            }

            var participantHasAccessMapping =
                _uow.Context.AccessMappings.Where(
                    x => x.ProviderCode == participantCode || x.ConsumerCode == participantCode).ToList();

            var participantRegisteredServices =
                _uow.Context.Services.Where(x => x.ParticipantCode == participantCode).ToList();

            if (participantHasAccessMapping.Count > 0)
            {
                foreach (var accessMapping in participantHasAccessMapping)
                {
                    try
                    {
                        _uow.Context.AccessMappings.Remove(accessMapping);
                        _uow.SaveChanges();
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
            }

            if (participantRegisteredServices.Count > 0)
            {
                foreach (var participantRegisteredService in participantRegisteredServices)
                {
                    try
                    {
                        _uow.Context.Services.Remove(participantRegisteredService);
                        _uow.SaveChanges();
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
            }

            try
            {
                Participant participant = _uow.Context.Participants.Find(participantCode);
                _uow.Context.Participants.Remove(participant);
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Опис: Методот вчитува податоци од база за приказ на Учесници, притоа филтрира и сортира според влезните параметри
        //Влезни параметри: индекс за страна, број на записи по страна,код за учесник
        //Излезни параметри: излезна листа од Учесници(индекс за страна, број на записи по страна,вкупен број на записи,записи)
        public PagedCollection<Participant> GetParticipantsPaged(int pageIndex, int itemPerPage,string sortDir,string sortCol, string searchParticipantCode)
        {
            if (String.IsNullOrEmpty(searchParticipantCode))
            {
                searchParticipantCode = "";
            }
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "";
            }

            var param = Expression.Parameter(typeof(Participant), "x");
            var mySortExpression = Expression.Lambda<Func<Participant, object>>(Expression.Property(param, sortCol), param);

            IQueryable<Participant> items = _uow.Context.Participants.Where(x => x.Code.ToLower().Contains(searchParticipantCode) || x.Name.ToLower().Contains(searchParticipantCode));

            var totalSize = items.Count();
            if (sortDir == "asc")
            {
                items = items.OrderBy(mySortExpression);
            }
            else if (sortDir == "desc")
            {
                items = items.OrderByDescending(mySortExpression);
            }

            IReadOnlyCollection<Participant> pagedItems = items.Skip((pageIndex - 1) * itemPerPage).Take(itemPerPage).ToList();

            return new PagedCollection<Participant>(pageIndex, itemPerPage, totalSize, pagedItems);
        }

        public string GetParticipantNameByCode(string participantCode)
        {
            var participant = _uow.Context.Participants.FirstOrDefault(x => x.Code == participantCode);
            if (participant != null)
            {
                return participant.Name;
            }
            return null;
        }

        public string GetParticipantCodeByName(string participantName)
        {
            var participant = _uow.Context.Participants.Where(x => x.Name == participantName);
            if (participant != null)
            {
                return participant.FirstOrDefault().Code;
            }
            return string.Empty;
        }
        public string GetParticipantCode(string participanCode)
        {
            var participant = _uow.Context.Participants.Where(x => x.Code == participanCode);
            if (participant != null)
            {
                return participant.FirstOrDefault().Code;
            }
            return string.Empty;
        }

        public string Sync()
        {
            throw  new AccessViolationException();
        }

        public string GetParticipantName(string participantCode)
        {
            var participant = _uow.Context.Participants.FirstOrDefault(particant => particant.Code.Contains(participantCode));
            if (participant != null)
            {
                return participant.Name;
            }
            return string.Empty;
        }

        public void Dispose()
        {
            _uow.Context.Dispose();
        }
    }
}
