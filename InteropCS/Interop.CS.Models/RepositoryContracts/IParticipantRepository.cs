using System;
using System.Collections.Generic;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IParticipantRepository : IDisposable
    {
        IEnumerable<Participant> GetParticipants();
        IEnumerable<Participant> GetInternalParticipants(bool? isActive);
        IEnumerable<Participant> GetExternalParticipants(bool? isActive);
        Participant GetParticipant(string participantCode);
        Participant GetParticipantByBus(string participantCode);
        void CreateParticipant(Participant participant);
        string GetPublicKey(string participantCode);
        void UpdateParticipant(Participant participant);
        void DeleteParticipant(string participantCode);
        PagedCollection<Participant> GetParticipantsPaged(int pageIndex, int itemsPerPage,string sortDir,string sortCol, string searchParticipantCode);
        string GetParticipantNameByCode(string participantCode);
        string GetParticipantCodeByName(string participantName);
        string GetParticipantCode(string participanCode);
        string Sync();
        string GetParticipantName(string participantCode);
    }
}
