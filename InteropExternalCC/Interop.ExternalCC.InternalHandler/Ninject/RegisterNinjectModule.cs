using Interop.ExternalCC.HandlersHelper.Contracts;
using Interop.ExternalCC.HandlersHelper.HelperMethods;
using Ninject.Modules;

namespace Interop.ExternalCC.InternalHandler.Ninject
{
    public class RegisterNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //Injects the constructors of all DI-ed objects 
            //with a LinqToSQL implementation of IRepository
            Bind<IExternalCCRequestHelper>().To<ExternalCCRequestHelper>();
            Bind<ICacheHelper>().To<CacheHelper>();
            Bind<IRequestExtensionMethods>().To<RequestExtensionMethods>();
        }
    }
}