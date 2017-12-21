using System.ServiceProcess;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using Microsoft.Practices.Unity;

namespace InteropCS.MessageLogStatisticService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
        //    ServiceBase[] ServicesToRun;

            var unityContainer = BuildUnityContainer();

            var participantRepository = unityContainer.Resolve<IParticipantRepository>();
            var messageLogStatisticRepository = unityContainer.Resolve<IMessageLogStatisticRepository>();
            var messageLogRepository = unityContainer.Resolve<IMessageLogRepository>();
            var soapFaultRepository = unityContainer.Resolve<ISoapFaultRepository>();
            var servicesRepository = unityContainer.Resolve<IServiceRepository>();

        //    ServicesToRun = new ServiceBase[]
        //    {
        //        new MessageLogStatisticService(participantRepository),
        //    };

        //    ServiceBase.Run(ServicesToRun);


        //    MessageLogStatisticService messageLogStatisticService = new MessageLogStatisticService(participantRepository);
        //    messageLogStatisticService.StartService();
#if(!DEBUG)
           ServiceBase[] ServicesToRun;
           ServicesToRun = new ServiceBase[] 
	   { 
	        new MessageLogStatisticService(participantRepository, messageLogStatisticRepository, messageLogRepository, soapFaultRepository, servicesRepository) 
	   };
           ServiceBase.Run(ServicesToRun);
#else
            var messageLogStatisticService = new MessageLogStatisticService(participantRepository, messageLogStatisticRepository, messageLogRepository, soapFaultRepository, servicesRepository);
            messageLogStatisticService.StartService();
            // here Process is my Service function
            // that will run when my service onstart is call
            // you need to call your own method or function name here instead of Process();
#endif


        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IParticipantRepository, ParticipantRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IInteropContext, InteropContext>();
            container.RegisterType<IMessageLogStatisticRepository, MessageLogStatisticRepository>();
            container.RegisterType<IMessageLogRepository, MessageLogRepository>();
            container.RegisterType<ISoapFaultRepository, SoapFaultRepository>();
            container.RegisterType<IServiceRepository, ServiceRepository>();
            return container;
        }

    }
}
