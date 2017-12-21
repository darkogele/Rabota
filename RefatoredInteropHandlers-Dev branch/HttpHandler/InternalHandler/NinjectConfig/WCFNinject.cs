using CrossCutting.Logging;
using Helpers.Contracts;
using Helpers.Implementations;
using Ninject.Modules;

namespace InternalHandler.NinjectConfig
{
    public class WCFNinject : NinjectModule
    {
        public override void Load()
        {
            Bind<IRequestTestHelper>().To<RequestTestHelper>();
            Bind<IMimMsgHelper>().To<MimMsgHelper>();
            Bind<ILogger>().To<NLogger>();
            Bind<ISoapRequestHelper>().To<SoapRequestHelper>();
            Bind<IValidXmlMsgHelper>().To<ValidXmlMsgHelper>();
        }
    }
}