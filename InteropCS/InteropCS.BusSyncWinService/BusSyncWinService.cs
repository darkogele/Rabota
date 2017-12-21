using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Interop.CS.Models.RepositoryContracts;
using InteropCS.BusSyncWinService.BusSyncServiceReference;

namespace InteropCS.BusSyncWinService
{
    public partial class BusSyncWinService : ServiceBase
    {
        private Timer _scheduler;
        private readonly IParticipantRepository _participantRepository;
        public BusSyncWinService(IParticipantRepository participantRepository)
        {
            InitializeComponent();
            _participantRepository = participantRepository;
        }

        protected override void OnStart(string[] args)
        {
            Log("Bus Sync Win Service started ...");
            ScheduleService();
        }

        protected override void OnStop()
        {
            Log("Bus Sync Win Service stopped ...");
            _scheduler.Dispose();
        }

        public void ScheduleService()
        {
            try
            {
                _scheduler = new Timer(SchedularCallback);
                var mode = ConfigurationManager.AppSettings["Mode"].ToUpper();

                var scheduledTime = DateTime.MinValue;
                if (mode == "DAILY")
                {
                    scheduledTime = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledTime"]);
                    if (DateTime.Now > scheduledTime)
                    {
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode == "INTERVAL")
                {
                    var intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }

                var timeSpan = scheduledTime.Subtract(DateTime.Now);

                var dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

                _scheduler.Change(dueTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Log("Error occured: " + ex.Message);
            }
        }

        private void Log(string message)
        {
            string path = ConfigurationManager.AppSettings["LogFilePath"];
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message, DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt"));
                writer.Close();
            }
        }

        private void SchedularCallback(object e)
        {
            try
            {
                var dbParticipants = _participantRepository.GetInternalParticipants(null);
                BusSyncMockServiceClient client = new BusSyncMockServiceClient();
                var participants = client.GetParticipants().ToList();
                foreach (var participant in participants)
                {
                    Log("Syncing: " + participant.Code);
                    //Log("Syncing: " + _participantRepository.Sync());
                }
                
            }
            catch (Exception ex)
            {
                Log("Exception: " + ex.Message);
            }
            
            ScheduleService();
        }
    }
}
