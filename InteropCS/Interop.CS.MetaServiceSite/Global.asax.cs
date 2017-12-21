using Interop.CS.MetaServiceSite.Ninject;
using Ninject;
using Ninject.Web.Common;

namespace Interop.CS.MetaServiceSite
{
    public class Global : NinjectHttpApplication
    {
        protected override global::Ninject.IKernel CreateKernel()
        {
            return new StandardKernel(new WCFNinjectModule());
        }
    }
}