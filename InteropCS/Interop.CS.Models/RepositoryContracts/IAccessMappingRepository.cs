using System;
using System.Collections.Generic;
using System.Linq;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IAccessMappingRepository:IDisposable
    {
        IEnumerable<AccessMapping> GetAccessMappings();
        AccessMapping GetAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethod);
        CreateAccessMappingResult CreateAccessMapping(AccessMapping accessMapping);
        void ActivateAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethod, string accessMappingProviderBusCode, string accessMappingConsumerBusCode, bool accessMappingIsActive);
        CreateAccessMappingResult ChangeActivityAccessMapping(string accessMappingProviderCode, string accessMappingConsumerCode, string accessMappingServiceCode, string accessMappingMethod, string accessMappingProviderBusCode, string accessMappingConsumerBusCode, bool accessMappingIsActive);
        PagedCollection<AccessMapping> GetAccessMappingsPaged(int pageIndex, int itemsPerPage, string searchProviderCode, string searchConsumerCode, string searchServiceCode, bool? isActive);
        Dictionary<string, string> GetProviders();
        Dictionary<string, string> GetConsumers();
        IQueryable<AccessMappingCCDTO> GetOwnAccessMappings(string providerCode);
    }
}
