using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models.MIOARecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.RepositoryContracts.MIOARecords
{
    public interface IAdministrativeRecordRepository:IDisposable
    {
        IEnumerable<AdministrativeRecord> GetAdministrativeRecord();
        PagedCollection<AdministrativeRecord> GetAdministrativeRecordPaged(int pageIndex, int itemsPerPage, string sortDir, string sortCol);
        PagedCollection<AdministrativeRecordLog> GetRecordDetailsPaged(int id, int pageIndex, int itemsPerPage, string sortDir, string sortCol);
        AdministrativeRecord GetRecord(int id);
        void EditRecord(AdministrativeRecord recordNew, AdministrativeRecordLog log);
        void CreateRecord(AdministrativeRecord recordNew);
    }
}
