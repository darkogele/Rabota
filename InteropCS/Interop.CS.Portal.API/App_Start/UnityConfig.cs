using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.Repository.MIOARecords;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.RepositoryContracts.MIOARecords;
using Interop.CS.Models.UoW;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Interop.CS.Portal.API.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //var configurationFactory = new ConfigurationFactory();
            //container.RegisterType(typeof(IDataProvider), configurationFactory.GetDataProvider().GetType());

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IMessageLogRepository, MessageLogRepository>();
            container.RegisterType<IAccessMappingRepository, AccessMappingRepository>();
            container.RegisterType<IParticipantRepository, ParticipantRepository>();
            container.RegisterType<IBusesRepository, BusesRepository>();
            container.RegisterType<IServiceRepository, ServiceRepository>();
            container.RegisterType<ISoapFaultRepository, SoapFaultRepository>();
            container.RegisterType<IAuthRepository, AuthRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IInteropContext, InteropContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IMessageLogStatisticRepository, MessageLogStatisticRepository>();
            container.RegisterType<IAdministrativeRecordRepository, AdministrativeRecordRepository>();
            container.RegisterType<IAdministrativeServiceRepository, AdministrativeServiceRepository>();
            container.RegisterType<ILogger, NLogger>(new InjectionConstructor("logger"));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}