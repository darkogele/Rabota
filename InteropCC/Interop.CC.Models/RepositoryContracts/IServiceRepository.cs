using System.Collections.Generic;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.RepositoryContracts
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetServices();
        Service GetServiceByCode(string code);
        void InsertService(Service service);
        void DeleteService(Service service);
        PagedCollection<ServiceDTO> GetServicesPaged(int pageIndex, int itemsPerPage, string filterCode, string filterName, string sortDir, string sortCol);
        string GetWSDL(string serviceCode);
    }
}
