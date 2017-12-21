using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using InteropCS.MessageLogStatisticService.Kibs;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace InteropCS.MessageLogStatisticService
{
    public partial class MessageLogStatisticService : ServiceBase
    {
        #region Properties
        private Timer timer;
        public int? TimerInterval
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]); }
        }
        public TimeSpan? TimeForFirstStartService
        {
            get { return TimeSpan.Parse(ConfigurationManager.AppSettings["TimeForFirstStartService"]); }
        }

        public TimeSpan? TestClock
        {
            get { return TimeSpan.Parse(ConfigurationManager.AppSettings["TestClock"]); }
        }
        #endregion

        private readonly IParticipantRepository _participantRepository;
        private readonly IMessageLogStatisticRepository _messageLogStatisticRepository;
        private readonly IMessageLogRepository _messageLogRepository;
        private readonly ISoapFaultRepository _soapFaultRepository;
        private readonly IServiceRepository _serviceRepository;
        public MessageLogStatisticService(IParticipantRepository participantRepository, IMessageLogStatisticRepository messageLogStatisticRepository, IMessageLogRepository messageLogRepository,
            ISoapFaultRepository soapFaultRepository, IServiceRepository serviceRepository)
        {
            _participantRepository = participantRepository;
            _messageLogStatisticRepository = messageLogStatisticRepository;
            _messageLogRepository = messageLogRepository;
            _soapFaultRepository = soapFaultRepository;
            _serviceRepository = serviceRepository;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogToLocalFile(DateTime.Now + " --**--Vo OnStart metodot e");
            timer = new Timer { Interval = TimerInterval.HasValue ? TimerInterval.Value : 60 * 60 };
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
            LogToLocalFile(DateTime.Now + " --**--Zavrsil elapsed i zavrsuva OnStart");
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                var firstStart = TimeForFirstStartService.HasValue ? TimeForFirstStartService.Value : TimeSpan.Parse("14:40");

                var currentTime = DateTime.Now.TimeOfDay;/*DateTime.Now.TimeOfDay;*/
                //FirstTime start Windows Service
                LogToLocalFile(DateTime.Now + " --**-- Proveruva uslov vo timer elapsed");
                if (currentTime >= firstStart && currentTime <= firstStart.Add(new TimeSpan(0, 1, 0)))
                {
                    LogToLocalFile(DateTime.Now + " --**-- WindowsService FirstTime start.");
                    StartService();
                }
                timer.Start();
            }
            catch (Exception ex)
            {
                LogToLocalFile(DateTime.Now + " --**-- WindowsService-Timer_elapsed exception: + " + ex +
                               " --**-- ex.Message: " + ex.Message + " --**-- ex.Inner: " + ex.InnerException);
            }
            LogToLocalFile(DateTime.Now + " --**-- Zavrsil timer_elapsed.");
        }

        public void StartService()
        {
            try
            {
                var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
                LogToLocalFile(DateTime.Now + " --**-- Dosol vo start service.");
                ClearContentsOfCreatingStatisticsFile();
                var participants = _participantRepository.GetParticipants().ToList();
                LogToLocalFile(DateTime.Now + " --**-- Gi zemal site participanti.");
                if (participants.Any())
                {
                    LogToLocalFile(DateTime.Now + " --**-- participants.Any()>0");
                    var allServices = _serviceRepository.GetServices().ToDictionary(s => s.Code, s => s.Name);
                    foreach (var participant in participants)
                    {
                        //IMPORTANT for testing, remove it!!!!!
                        //LogsInCreatingStatistics(DateTime.Now + " --**-- Pred CREATE!!!!!Uri-to za participantot e:" + participant.Uri + ", i kodot na participantot e: " + participant.Code);
                        try
                        {
                            LogToLocalFile(DateTime.Now + " --**-- Uri-to za participantot e:" + participant.Uri);

                            var participantMessageLogsFromCc = GetParticipantMessageLogs(participant.Uri);
                            LogToLocalFile(DateTime.Now + " --**-- participantMessageLogsFromCc count e: " + participantMessageLogsFromCc.Count);
                            if (participantMessageLogsFromCc != null)
                            {
                                LogToLocalFile(DateTime.Now + " --**-- Gi zemal site message logs za participantot " + participant.Name);

                                if (participantMessageLogsFromCc.Count() > 0)
                                {
                                    CreateMessageLogStatistics(participantMessageLogsFromCc, participant.Uri, participant.Code, allServices);
                                }
                            }

                        }
                        catch (Exception exception)
                        {
                            //LogsInCreatingStatistics(DateTime.Now + " --**-- Error na CREATE!!!!!Uri-to za participantot e:" + participant.Uri + ", i kodot na participantot e: " + participant.Code);
                            LogsInCreatingStatistics(DateTime.Now + "--**-- Nastanata e greshka kaj participant " + participant.Name + ". Treba da se iskreiraat logovi vo statistika od UI za datum " + DateTime.Today.ToString("dd.MM.yyyy", macCultureInfo));
                            LogToLocalFile(DateTime.Now + " --**-- Nastanata e greska vo procesot na kreiranje vo statistika. Greskata e: " + exception);
                        }

                    }


                    LogToLocalFile(DateTime.Now + " --**-- zavrsil so zemanje na message logs od CC, trgnuva od CS.");
                    //todo:Tuka smeni go na datetime.today
                    var today = DateTime.Today.Date;

                    try
                    {
                        var messageLogsAndFaultsFromCs = GetMessageLogsAndFaultsFromCs(DateTime.Today).ToList();/*DateTime.Today*/;//new DateTime(2015, 10, 16)
                        LogToLocalFile(DateTime.Now + " --**-- Gi zemal site message logovi od CS shto se na denesen datum");
                        LogToLocalFile(DateTime.Now + " --**-- messageLogsAndFaultsFromCs count e: " + messageLogsAndFaultsFromCs.Count);
                        if (messageLogsAndFaultsFromCs != null)
                        {
                            if (messageLogsAndFaultsFromCs.Count() > 0)
                            {
                                //TODO:Ova odkomentiraj go
                                //foreach (var logAndFault in messageLogsAndFaultsFromCs)
                                //{
                                //    _messageLogRepository.UpdateMessageLog(logAndFault.MessageLog.Id, GetMessageLogCheckTimeStamp(logAndFault.MessageLog.TokenTimestamp));
                                //}

                                CreateMessageLogStatistics(messageLogsAndFaultsFromCs, ConfigurationManager.AppSettings["CSAddress"], ConfigurationManager.AppSettings["CSAddress"], allServices);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        LogsInCreatingStatistics(DateTime.Now + "--**-- Nastanata e greshka pri zemanje na logovi od CS. Treba da se iskreiraat logovi vo statistika od UI za datum " + DateTime.Today.ToString("dd.MM.yyyy", macCultureInfo));
                        LogToLocalFile(DateTime.Now + " --**-- Nastanata e greska vo procesot na kreiranje na logovi zemeni od CS vo statistika . Greskata e: " + exception);
                    }

                    SendingEmail();
                }

            }
            catch (Exception ex)
            {
                LogToLocalFile(DateTime.Now + " --**-- WindowsService - StartService: " + Environment.NewLine + "Exception: " + Environment.NewLine + ex + Environment.NewLine + "Exception.Message: " + Environment.NewLine + ex.Message);
            }
        }

        private IEnumerable<StatisticLogsFaults> GetMessageLogsAndFaultsFromCs(DateTime createdDate)
        {
            var statiticLogsFaultsList = new List<StatisticLogsFaults>();
            IEnumerable<MessageLog> messageLogs = _messageLogRepository.GetMessageLogsByDate(createdDate);

            //if (messageLogs.Any())
            //{

            IEnumerable<SoapFault> soapFaults = _soapFaultRepository.GetSoapFaultsByDate(createdDate);

            //var joinedLogsAndFaults = messageLogs.FullOuterJoin(soapFaults, messageLog => messageLog.TransactionId, soapFault => soapFault.TransactionId,
            //(messageLog, soapFault, transactionId) => new { messageLog, soapFault, transactionId }).ToList();

            var joinedLogsAndFaults = messageLogs.GroupJoin(soapFaults, messageLog => messageLog.TransactionId,
            soapFault => soapFault.TransactionId, (ml, sf) => new StatisticLogsFaults { MessageLog = ml, SoapFault = sf.FirstOrDefault(), TransactionId = ml.TransactionId }).ToList();

            if (joinedLogsAndFaults.Any())
            {
                foreach (var joinedLogsAndFault in joinedLogsAndFaults)
                {
                    statiticLogsFaultsList.Add(new StatisticLogsFaults
                    {
                        MessageLog = joinedLogsAndFault.MessageLog ?? new MessageLog(),
                        SoapFault = joinedLogsAndFault.SoapFault ?? new SoapFault(),
                        TransactionId = joinedLogsAndFault.TransactionId
                    });
                }
            }
            //}
            return statiticLogsFaultsList;
        }

        private List<StatisticLogsFaults> GetParticipantMessageLogs(string uri)
        {
            var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
            var date = DateTime.Today;
            return GetMessageLogsFromResponse(uri + "/logs/" + date.ToString("dd.MM.yyyy", macCultureInfo));
        }


        private void CreateMessageLogStatistics(IEnumerable<StatisticLogsFaults> participantMessageLogs, string participantUri, string participantCode, Dictionary<string, string> allServices)
        {
            LogToLocalFile(DateTime.Now + " --**--Pocnuva da zapisuva vo message log statistics tabelata");
            bool loggedP = false;
            bool loggedCs = false;
            foreach (var logsAndFaults in participantMessageLogs)
            {
                var messageLogStatisticExist =
                    _messageLogStatisticRepository.MessageLogStatisticExist(logsAndFaults.TransactionId, logsAndFaults.MessageLog.Dir,
                        participantCode);
                var makeConsumerWithMim12 = logsAndFaults.MessageLog.Consumer;
                LogToLocalFile(DateTime.Now + " --**--transakcija,dir,participant code: " + logsAndFaults.TransactionId + "," + logsAndFaults.MessageLog.Dir + "," + participantCode);
                //LogToLocalFile(DateTime.Now + " --**--consumer,provider,routingtoken,service,servicemethod,transactionid,dir,calltype,publickey,status,mimetype,timestamp,createdate,signature,correlationid,participanturi,timestamp, iscorrect," +
                //               "participantcode,consumername,routingtokenname,faultcode,faultsubcode,faulktreason,faultdetails,faultdatecreated: " + logsAndFaults.MessageLog.Consumer + "/" +
                //               logsAndFaults.MessageLog.Provider + "/" + logsAndFaults.MessageLog.RoutingToken + "/" + logsAndFaults.MessageLog.Service + "/" + logsAndFaults.MessageLog.ServiceMethod
                //               + "/" + logsAndFaults.MessageLog.TransactionId + "/" + logsAndFaults.MessageLog.Dir + "/" + logsAndFaults.MessageLog.CallType + "/" + logsAndFaults.MessageLog.PublicKey
                //               + "/" + logsAndFaults.MessageLog.MimeType
                //               + "/" + logsAndFaults.MessageLog.Timestamp
                //               + "/" + logsAndFaults.MessageLog.CreateDate
                //               + "/" + logsAndFaults.MessageLog.Signature + "/" + logsAndFaults.MessageLog.CorrelationId
                //               + "/" + participantUri
                //               + "/" + logsAndFaults.MessageLog.TokenTimestamp
                //               + "/" + logsAndFaults.MessageLog.IsCorrect
                //               + "/" + participantCode
                //               + "/" + _participantRepository.GetParticipantByBus(makeConsumerWithMim12).Name
                //                + "/" + _participantRepository.GetParticipant(logsAndFaults.MessageLog.RoutingToken).Name
                //                + "/" + logsAndFaults.SoapFault.Code
                //                + "/" + logsAndFaults.SoapFault.SubCode
                //                + "/" + logsAndFaults.SoapFault.Reason
                //                + "/" + logsAndFaults.SoapFault.Details
                //                + "/" + logsAndFaults.SoapFault.DateCreated);

                LogToLocalFile(DateTime.Now + " --**--messageLogStatisticExist: " + messageLogStatisticExist);

                if (!messageLogStatisticExist)
                {
                    //try
                    //{

                    //Ova e samo privremeno se duri ne se postavat Participant-tite od dva razlicni Bus-a da se so razlicen ParticipantCode
                    string makeConsumerWithMim1;
                    if (logsAndFaults.MessageLog.Consumer == "AVRM")
                    {
                        makeConsumerWithMim1 = "MIM2$$" + logsAndFaults.MessageLog.Consumer;
                    }
                    else
                    {
                        makeConsumerWithMim1 = logsAndFaults.MessageLog.Consumer;
                    }

                    var serviceCodeFromMessageLog = logsAndFaults.MessageLog.Service;
                    var serviceNameFromCode = allServices[serviceCodeFromMessageLog];
                    _messageLogStatisticRepository.InsertMessageLogStatistic(new MessageLogStatistic
                    {
                        Consumer = logsAndFaults.MessageLog.Consumer,
                        Provider = logsAndFaults.MessageLog.Provider,
                        RoutingToken = logsAndFaults.MessageLog.RoutingToken,
                        //Service = logsAndFaults.MessageLog.Service,
                        Service = serviceNameFromCode,
                        ServiceMethod = logsAndFaults.MessageLog.ServiceMethod,
                        TransactionId = logsAndFaults.MessageLog.TransactionId,
                        Dir = logsAndFaults.MessageLog.Dir,
                        CallType = logsAndFaults.MessageLog.CallType,
                        PublicKey = logsAndFaults.MessageLog.PublicKey,
                        Status = logsAndFaults.MessageLog.Status,
                        MimeType = logsAndFaults.MessageLog.MimeType,
                        Timestamp = logsAndFaults.MessageLog.Timestamp,
                        CreateDate = logsAndFaults.MessageLog.CreateDate,
                        Signature = logsAndFaults.MessageLog.Signature,
                        CorrelationId = logsAndFaults.MessageLog.CorrelationId,
                        ParticipantUri = participantUri,
                        TokenTimestamp = logsAndFaults.MessageLog.TokenTimestamp,
                        IsCorrect = logsAndFaults.MessageLog.IsCorrect,
                        /*Za testiranje ako neshto ne e vo red so Create vo statistika dali kje prodolzi so sleden participant, kje prati mail, kje zapise vo .txt*/
                        ParticipantCode = participantCode,
                        ConsumerName = _participantRepository.GetParticipantByBus(makeConsumerWithMim1).Name,
                        RoutingTokenName = _participantRepository.GetParticipant(logsAndFaults.MessageLog.RoutingToken).Name,
                        FaultCode = logsAndFaults.SoapFault.Code,
                        FaultSubCode = logsAndFaults.SoapFault.SubCode,
                        FaultReason = logsAndFaults.SoapFault.Reason,
                        FaultDetails = logsAndFaults.SoapFault.Details,
                        FaultDateCreated = logsAndFaults.SoapFault.DateCreated
                    });

                    if (participantCode == "CS" && !loggedCs)
                    {
                        loggedCs = true;
                        LogsInCreatingStatistics(DateTime.Now + "--**-- Uspeshno kreirani logovi od CS vo statistics tabelata");
                    }
                    else if (participantCode != "CS" && !loggedP)
                    {
                        loggedP = true;
                        LogsInCreatingStatistics(DateTime.Now + "--**-- Uspeshno kreirani logovi za participant " + participantCode);
                    }

                    //LogToLocalFile(DateTime.Now + " --**--Zapisal eden podatok");
                    //}
                    //catch (Exception exception)
                    //{
                    //    LogToLocalFile(DateTime.Now + "Se slucila greska pri zapisuvanje vo MessageLogStatistic" + exception);

                    //    //TODO:Diskutabilno
                    //    /*throw exception;*/
                    //}
                }
            }
            if (participantCode == "CS")
            {
                LogToLocalFile(DateTime.Now + " --**-- Napravil insert vo tabela MessageLogsStatistic");
            }
            LogToLocalFile(DateTime.Now + " --**-- napravil insert na message logs vo message logs statistic za participantot " + participantCode);
        }

        public List<StatisticLogsFaults> GetMessageLogsFromResponse(string url)
        {
            LogToLocalFile(DateTime.Now + " --**-- Dosol do momentot koa kje treba da gagja na CC handler internal api i url-to mu e:" + url);
            string responseFromServer = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            LogToLocalFile(DateTime.Now + " --**-- requestot mu e:" + url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse response = request.GetResponse();
            LogToLocalFile(DateTime.Now + " --**-- responsot mu e:" + url);
            Stream dataStream = response.GetResponseStream();
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }

            if (!string.IsNullOrEmpty(responseFromServer))
            {
                LogToLocalFile(DateTime.Now + " --**-- response from CC:" + responseFromServer);
                return JsonConvert.DeserializeObject<List<StatisticLogsFaults>>(responseFromServer);
            }
            LogToLocalFile(DateTime.Now + " --**-- response from CC e prazno. Nema logovi kaj ova CC za ovoj den.");
            return new List<StatisticLogsFaults>();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        public bool GetMessageLogCheckTimeStamp(string tokenTimestamp)
        {
            var oWS = new wsTSATest();
            oWS.Url = "https://wstsatest.kibs.mk/wsTSATest.asmx";
            var kibsCertificationPath = ConfigurationManager.AppSettings["KIBSCertificationPath"];
            var kibsCertificationPassword = ConfigurationManager.AppSettings["KIBSCertificationPassword"];
            var cer = new X509Certificate2(kibsCertificationPath, kibsCertificationPassword);
            oWS.ClientCertificates.Add(cer);
            byte[] temp_backToBytes = Convert.FromBase64String(tokenTimestamp);
            var response = oWS.funCheckTS_Bytes(temp_backToBytes);
            oWS.Dispose();
            if (response.strFailureInfo == "")
                return true;
            return false;
        }

        public static void LogToLocalFile(string message)
        {
            //var str = new FileStream(@"C:\Interop_WindowsService_Release\WindowsServiceLog.txt", FileMode.Append, FileAccess.Write);
            if (File.Exists(ConfigurationManager.AppSettings["LogsWindowsServicePath"]))
            {
                var str = new FileStream(ConfigurationManager.AppSettings["LogsWindowsServicePath"], FileMode.Append, FileAccess.Write);

                var streamWriter = new StreamWriter(str);
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }

        }

        public static void ClearContentsOfCreatingStatisticsFile()
        {
            if (File.Exists(ConfigurationManager.AppSettings["LogsCreatingStatisticsPath"]))
            {
                File.WriteAllText(ConfigurationManager.AppSettings["LogsCreatingStatisticsPath"], string.Empty);
            }
        }

        public static void LogsInCreatingStatistics(string message)
        {
            if (File.Exists(ConfigurationManager.AppSettings["LogsCreatingStatisticsPath"]))
            {
                var str = File.Open(ConfigurationManager.AppSettings["LogsCreatingStatisticsPath"], FileMode.Append);

                using (var streamWriter = new StreamWriter(str))
                {
                    streamWriter.WriteLine(message);
                }
                str.Close();
            }
        }

        public void SendingEmail()
        {
            if (bool.Parse(ConfigurationManager.AppSettings["SendingMail"]))
            {
                var client = new SmtpClient(ConfigurationManager.AppSettings["MailHost"], Convert.ToInt32(ConfigurationManager.AppSettings["MailHostPort"]));
                var msg = new MailMessage();

                var credentials = new NetworkCredential();
                client.UseDefaultCredentials = false;
                credentials.UserName = ConfigurationManager.AppSettings["MailServiceUserName"];
                credentials.Password = ConfigurationManager.AppSettings["MailServicePassword"];

                msg.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "Интероперабилност грешки");
                msg.To.Add(ConfigurationManager.AppSettings["MailToAddress"]);
                msg.Subject = "Windows service - Грешки при креирање на логови во статистика за датум " + DateTime.Today.Date.ToShortDateString();
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Headers.Add("Content-Type", "multipart/mixed");
                var attachmentFilename = ConfigurationManager.AppSettings["LogsCreatingStatisticsPath"];
                if (attachmentFilename != null)
                {
                    var attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = DateTime.Today;
                    disposition.FileName = Path.GetFileName(attachmentFilename);
                    disposition.Size = new FileInfo(attachmentFilename).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    msg.Attachments.Add(attachment);
                }

                msg.Body = "Потребно е да се искреираат логови за датум " + DateTime.Today.Date.ToShortDateString();
                client.Credentials = credentials;
                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    LogToLocalFile("Exception when send mail to employee. Ex.message: " + ex.Message + "-- Ex.InnerException: " + ex.InnerException);
                }
            }
        }
    }


}
