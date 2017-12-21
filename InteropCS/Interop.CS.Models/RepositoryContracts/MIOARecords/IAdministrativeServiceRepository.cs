using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models.MIOARecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Models.RepositoryContracts.MIOARecords
{
    public interface IAdministrativeServiceRepository : IDisposable
    {
        IEnumerable<AdministrativeService> GetAdministrativeService();
        PagedCollection<AdministrativeService> GetAdministrativeServicePaged(int pageIndex, int itemsPerPage, string sortDir, string sortCol);
        PagedCollection<AdministrativeServiceLog> GetServiceDetailsPaged(int id, int pageIndex, int itemsPerPage, string sortDir, string sortCol);
        AdministrativeService GetService(int id);
        void EditService(AdministrativeService serviceNew, AdministrativeServiceLog log);
        void CreateService(AdministrativeService serviceNew);
    }
}
