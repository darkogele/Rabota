using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using LoadTestClient.AKN_ImotenListParcela;
using LoadTestClient.CRRM_TekovnaSostojba;
using LoadTestClient.FPIOM_PodatociPenzionerOsigurenik;
using LoadTestClient.FPIOM_GodiniRabotnoIskustvo;
using LoadTestClient.MON_StatusForRegularStudent;
using LoadTestClient.MzTV_OdobrenieGradbaDozovla;

namespace LoadTestClient
{
    class Program
    {
        private static int _requestsFinished;
        private static int _totaNumberOfRequests;
        private static int _firedRequests;
        private static Timer _timer;
        private static string _signedCertificate;
        private static List<string> _logItems;
        private static StreamWriter _stream;
        private static List<LogItemModel> _logItemModels; 
        static void Main(string[] args)
        {
            _logItemModels = new List<LogItemModel>();
            _logItems = new List<string>();
            Directory.CreateDirectory("Logs");
            var filePath = Path.Combine("Logs", DateTime.Now.ToString("dd-MM-yyyy") +".txt");
            _stream = new StreamWriter(filePath, true);
            _signedCertificate = CertificateHelper.GetSignedCertificate("6646123");
            _totaNumberOfRequests = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfRequests"]);
            var period =
                Convert.ToInt32(ConfigurationManager.AppSettings["CallingPeriodInMiliseconds"]);
            _logItems.Add("--- Testing parameters: " + "Call from: " + ConfigurationManager.AppSettings["Institution"] + " to: " + ConfigurationManager.AppSettings["CalledInstitution"] + ". Firing " + _totaNumberOfRequests + " requests, one every " + period + " ms. ---");
            _logItems.Add("");
            _logItems.Add("--- Start testing --- " + DateTime.Now);
            _logItems.Add("");
            Console.WriteLine("--- Testing parameters: Firing " + _totaNumberOfRequests + " requests, one every " + period + " ms. ---");
            Console.WriteLine();
            Console.WriteLine("--- Start testing --- " + DateTime.Now);
            Console.WriteLine();
            _timer = new Timer(FireRequests, null, 1000, period);
            Console.ReadLine();
        }


        private static void FireRequests(object filePath)
        {
            var whenFired = DateTime.Now;
            _firedRequests++;
            //_logItems.Add("Request  - ID: " + _firedRequests + " " + whenFired.ToString("hh:mm:ss.fff tt"));
            Console.WriteLine("Request  - ID: " + _firedRequests + " " + whenFired.ToString("hh:mm:ss.fff tt"));
            var responseId = _requestsFinished + 1;
            var request = new LogItemMessage
            {
                Id = _firedRequests,
                TimeStamp = whenFired
            };

            if (_firedRequests == _totaNumberOfRequests)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            var callIsSuccessful = false;
            try
            {
                CallService(ConfigurationManager.AppSettings["Institution"]);
                callIsSuccessful = true;
            }
            catch
            {
                callIsSuccessful = false;
            }
            finally
            {
                var whenFinished = DateTime.Now;
                _logItemModels.Add(new LogItemModel
                {
                    Request = request,
                    Response = new LogItemMessage
                    {
                        Id = responseId,
                        TimeStamp = whenFinished
                    },
                    WasCallSuccessful = callIsSuccessful
                });
                
                if (callIsSuccessful)
                {
                    Console.WriteLine("Response - ID: " + (_requestsFinished + 1) + " " +
                                      whenFinished.ToString("hh:mm:ss.fff tt") + " (" +
                                      Math.Round((whenFinished - whenFired).TotalMilliseconds) + " ms.) OK");
                }
                else
                {
                    Console.WriteLine("Response - ID: " + (_requestsFinished + 1) + " " +
                                  whenFinished.ToString("hh:mm:ss.fff tt") + " (" +
                                  Math.Round((whenFinished - whenFired).TotalMilliseconds) + " ms.) ERROR");
                }
            }

            _requestsFinished++;

            if (_requestsFinished == _totaNumberOfRequests)
            {
                foreach (var logItem in _logItems)
                {
                    _stream.WriteLine(logItem);
                }
                foreach (var logItemModel in _logItemModels)
                {
                    _stream.WriteLine(logItemModel.ToString());
                }
                _stream.WriteLine();
                _stream.WriteLine("--- End testing --- " + DateTime.Now);
                _stream.Close();
                Console.WriteLine();
                Console.WriteLine("--- End testing --- " + DateTime.Now);
            }
           
        }

        private static void CallService(string institutionName)
        {
            switch (institutionName)
            {
                case "MON":
                {
                    var client = new FPIOMServiceClient();
                    client.GetDataForRetired("0606949469013");
                    break;
                }
                case "MZTV":
                {
                    var client = new YearsOfWorkExperienceClient();
                    client.GetYWExpXML("1604991455147");
                    break;
                }
                case "FPIOM":
                {
                    var client = new SRegStudentClient();
                    client.GetStuS("1810997495034");
                    break;
                }
                case "CRRM":
                {
                    var client = new AKNServiceClient();
                    client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");
                    break;
                }
                case "CURM":
                {
                    var client = new AKNServiceClient();
                    client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");
                    break;
                }
                case "AVRM":
                {
                    var client = new YearsOfWorkExperienceClient();
                    client.GetYWExpXML("1604991455147");
                    break;
                }
                case "UJP":
                {
                    var client = new MzTVAdapterClient();
                    client.ConsPerm("УП 221/2014", "2", "108", null, null);
                    break;
                }
                case "AKN":
                {
                    var client = new CRMServiceClient();
                    client.GetTekovnaSostojba(_signedCertificate);
                    break;
                }
                case "MVR":
                {
                    var client = new YearsOfWorkExperienceClient();
                    client.GetYWExpXML("1604991455147");
                    break;
                }
                case "FZOM":
                {
                    var client = new CRMServiceClient();
                    client.GetTekovnaSostojba(_signedCertificate);
                    break;
                }
                case "UIPR":
                {
                    var client = new AKNServiceClient();
                    client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");
                    break;
                }
                case "GS":
                {
                    var client = new FPIOMServiceClient();
                    client.GetDataForRetired("0606949469013");
                    break;
                }
                case "MTSP":
                {
                    var client = new SRegStudentClient();
                    client.GetStuS("1810997495034");
                    break;
                }
                case "OSS2":
                {
                    var client = new FPIOMServiceClient();
                    client.GetDataForRetired("0606949469013");
                    break;
                }
                case "USS":
                {
                    var client = new SRegStudentClient();
                    client.GetStuS("1810997495034");
                    break;
                }
                default:
                {
                    Thread.Sleep(2000);
                    break;
                }
            }
        }
    }
}
