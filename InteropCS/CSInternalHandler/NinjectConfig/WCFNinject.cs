using CSHandlerHelper.Contracts;
using CSHandlerHelper.Methods;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSInternalHandler.NinjectConfig
{
    public class WCFNinject : NinjectModule
    {
        public override void Load()
        {
            //Injects the constructors of all DI-ed objects 
            //with a LinqToSQL implementation of IRepository
            Bind<IUnitOfWork>().To<UnitOfWork>();// _uow;
            Bind<IInteropContext>().To<InteropContext>();
            Bind<ISoapFaultRepository>().To<SoapFaultRepository>();
            Bind<ILogger>().To<NLogger>();
            Bind<IMimMsgHelper>().To<MimMsgHelper>();
            Bind<IRequestHelper>().To<RequestHelper>();
            Bind<ISoapRequestHelper>().To<SoapRequestHelper>();
            Bind<IValidXmlMsgHelper>().To<ValidXmlMsgHelper>();
        }
    }
}