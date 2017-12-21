using System.Collections.Generic;
using Interop.CC.Models.Models;

namespace Interop.CC.Models.RepositoryContracts
{
    public interface IProvidersRepository
    {
        void AddProvider(Provider provider);

        IEnumerable<Provider> GetProviders();


        string GetPublicKey(string routingToken);
    }
}
