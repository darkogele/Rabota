using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;

namespace Interop.CC.Models.Repository
{
    public class ProvidersRepository : IProvidersRepository
    {
        private readonly IUnitOfWork _uow;

        // Опис: Конструктор на ProvidersRepository модулот 
        // Влезни параметри: модел IUnitOfWork
        public ProvidersRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Опис: Методот врши внесување на провајдер во база
        // Влезни параметри: Provider provider
        // Излезни параметри: / 
        public void AddProvider(Provider provider)
        {
            //_uow.Context.Providers.Add(provider);
            _uow.Context.Providers.AddOrUpdate(x=>x.RoutingToken,provider);
            _uow.SaveChanges();
        }

        // Опис: Методот врши вчитување на сите провајдери од база
        // Влезни параметри: /
        // Излезни параметри: IEnumerable<Provider> 
        public IEnumerable<Provider> GetProviders()
       { 
            return _uow.Context.Providers.ToList();
        }

        // Опис: Методот врши вчитување на Јавен клуч од база
        // Влезни параметри: податочна вредност routingToken
        // Излезни параметри: податочен тип string
        public string GetPublicKey(string routingToken)
        {
            var provider = _uow.Context.Providers.Find(routingToken);
            if (provider == null)
            {
                throw new NotFoundPublicKeyException(routingToken);
            }
            return provider.PublicKey;
        }
    }
}
