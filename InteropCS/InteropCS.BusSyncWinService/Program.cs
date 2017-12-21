using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Interop.CS.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using Microsoft.Practices.Unity;

namespace InteropCS.BusSyncWinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var unityContainer = BuildUnityContainer();
            var participantRepository = unityContainer.Resolve<IParticipantRepository>();

            var servicesToRun = new ServiceBase[]
            {
                new BusSyncWinService(participantRepository)
            };
            ServiceBase.Run(servicesToRun);

        }


        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IParticipantRepository, ParticipantRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IInteropContext, InteropContext>();
            //container.RegisterType<IMessageLogStatisticRepository, MessageLogStatisticRepository>();
            //container.RegisterType<IMessageLogRepository, MessageLogRepository>();
            //container.RegisterType<ISoapFaultRepository, SoapFaultRepository>();
            return container;
        }
    }
}
