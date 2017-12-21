using System.Collections.Generic;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;

namespace Interop.CC.MetaService.Helpers
{
    public interface ICCMetaServiceHelper
    {
        void InsertService(IServiceRepository serviceRepository, Service service, string ednPoint, ILogger logger);
        void DeleteService(IServiceRepository serviceRepository, Service service, ILogger logger);
        string GetEndpoint(string wsdl, ILogger logger);
        List<string> RebindServiceRoles(IAuthRepository authRepository, List<string> serviceRolesFromGetProviders, string loggedUser, ILogger logger);
    }
}
