using Interop.CS.CrossCutting.Logging;
using Interop.CS.MetaService.Helpers;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using Ninject.Modules;
using ILogger = Interop.CS.CrossCutting.Logging.ILogger;

namespace Interop.CS.MetaServiceSite.Ninject
{
    public class WCFNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //Injects the constructors of all DI-ed objects 
            //with a LinqToSQL implementation of IRepository
            Bind<IUnitOfWork>().To<UnitOfWork>();// _uow;
            //Bind<IMetaServiceHelper>().To<MetaServiceHelper>();
            Bind<IServiceRepository>().To<ServiceRepository>();
            Bind<IParticipantRepository>().To<ParticipantRepository>();
            Bind<IAccessMappingRepository>().To<AccessMappingRepository>();
            Bind<IBusesRepository>().To<BusesRepository>();
            Bind<IInteropContext>().To<InteropContext>();
            Bind<ICSRepoFactory>().To<CSRepoFactory>();
            Bind<ILogger>().To<NLogger>();
        }
    }
}