using System;
using System.Collections.Generic;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IServiceRepository : IDisposable
    {
        IEnumerable<CSService> GetServices();
        CSService GetService(string serviceCode, string serviceParticipantCode);
        void CreateService(CSService service);
        PagedCollection<ServiceDTO> GetServicesPaged(int pageIndex, int itemsPerPage, string providerCode,string pickProvider);
        string GetWSDL(string providerName, string serviceCode);
        string GetWSDLAccMap(string providerName, string serviceCode);
        string GetServiceName(string serviceCode);

    }
}
