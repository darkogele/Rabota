
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Interop.CC.Models;
using Interop.CC.Models.Repository;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;
using Interop.CC.Portal.API.Controllers;
using Interop.CC.Portal.API.Controllers.Institutions;
using Microsoft.Practices.Unity;

using Unity.WebApi;
using Interop.CC.CrossCutting.Logging;

namespace Interop.CC.Portal.API.App_Start
{
    public static class UnityConfig
    {

        // Регистрирање на модули на зависности
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IMessageLogsRepository, MessageLogsRepository>();
            container.RegisterType<IServiceRepository, ServiceRepository>();
            container.RegisterType<ISoapFaultRepository, SoapFaultRepository>();
            container.RegisterType<IAuthRepository, AuthRepository>();
            container.RegisterType<IProvidersRepository, ProvidersRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IInteropContext, InteropContext>();
            container.RegisterType<ILogger, NLogger>(new InjectionConstructor("logger"));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}