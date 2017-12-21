using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Methods;
using Interop.CC.MetaService.Helpers;
using Interop.CC.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;
using Ninject.Modules;
using Interop.CC.Models.Repository;

namespace Interop.CC.Handler.External.NinjectConfig
{
    // Регистрирање на модули на зависности
    public class WCFNinject : NinjectModule
    {
        public override void Load()
        {
            //Injects the constructors of all DI-ed objects 
            //with a LinqToSQL implementation of IRepository
            Bind<IUnitOfWork>().To<UnitOfWork>();// _uow;
            Bind<IServiceRepository>().To<ServiceRepository>();
            Bind<IProvidersRepository>().To<ProvidersRepository>();
            Bind<IInteropContext>().To<InteropContext>();
            Bind<ICCMetaServiceHelper>().To<CCMetaServiceHelper>();
            Bind<ISoapFaultRepository>().To<SoapFaultRepository>();
            Bind<ILogger>().To<NLogger>();
            Bind<IMimMsgHelper>().To<MimMsgHelper>();
            Bind<IRequestHelper>().To<RequestHelper>();
            Bind<ISoapRequestHelper>().To<SoapRequestHelper>();
            Bind<IValidXmlMsgHelper>().To<ValidXmlMsgHelper>();
            Bind<IMessageLogsRepository>().To<MessageLogsRepository>();
            Bind<IAuthRepository>().To<AuthRepository>();
        }
    }
}