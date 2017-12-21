using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Interop.CC.CrossCutting;
using Interop.CC.Portal.API.Helpers;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.RepositoryContracts;

namespace Interop.CC.Portal.API.Controllers
{
    [System.Web.Http.Authorize]
    public class MessageLogController : ApiController
    {
        private readonly IMessageLogsRepository _messageLogsRepository;
        

        // Опис: Конструктор на MessageLogController модулот 
        // Влезни параметри: модел IMessageLogsRepository
        public MessageLogController(IMessageLogsRepository messageLogsRepository)
        {
            _messageLogsRepository = messageLogsRepository;
        }

        // Опис: Методот го повикува GetMessageLogByTransactionId методот на MessageLogRepository модулот 
        // Влезни параметри: трансакциски идентификациски број
        // Излезни параметри: MessageLogPairsViewModel модел 
        [System.Web.Http.HttpGet]
        public MessageLogPairsViewModelDetails GetMessageLogByTid(Guid transactionId)
        {
            try
            {
                var messageLog = _messageLogsRepository.GetMessageLogByTransactionId(transactionId);
                var allParticipants = GetParticipants();
                if (allParticipants.Count > 0)
                {
                    var splitter = new string[] { "$$" };
                    //var partsWoMim = allParticipants.Select(x=>x.Key.Split(splitter,StringSplitOptions.None).Last().ToLower());
                    //var partsMim = allParticipants.Select(x => x.Key.Split(splitter, StringSplitOptions.None).Last().ToLower());
                    //messageLog.Request.ConsumerName = partsMim.Select(x => (x.ToLower() == messageLog.Request.Consumer.ToLower()));
                    //var partsWoMim = 
                    //messageLog.Request.ConsumerName = allParticipants.Where(x => x.Key.Split(splitter, StringSplitOptions.None).Last().ToLower() == messageLog.Request.Consumer.ToLower()).Select(x=>x.Value).ToString();
                    if (messageLog.Request.RoutingToken.Contains("$$"))
                    {
                        messageLog.Request.RoutingToken = messageLog.Request.RoutingToken.Split(splitter, StringSplitOptions.None).Last().ToLower();
                    }
                    else
                    {
                        messageLog.Request.RoutingToken = messageLog.Request.RoutingToken.ToLower();
                    }

                    if (messageLog.Response != null)
                    {
                        if (messageLog.Response.RoutingToken.Contains("$$"))
                        {
                            messageLog.Response.RoutingToken = messageLog.Response.RoutingToken.Split(splitter, StringSplitOptions.None).Last().ToLower();
                        }
                        else
                        {
                            messageLog.Response.RoutingToken = messageLog.Response.RoutingToken.ToLower();
                        }
                    }
                    
                    foreach (var participant in allParticipants)
                    {
                        var partKeyMim = participant.Key.ToLower();
                        var partKey = participant.Key.Split(splitter, StringSplitOptions.None).Last().ToLower();
                        if (partKey == messageLog.Request.Consumer.ToLower())
                        {
                            messageLog.Request.ConsumerName = participant.Value;
                        }
                        if (partKey == messageLog.Request.RoutingToken)
                        {
                            messageLog.Request.RoutingTokenName = participant.Value;
                        }
                        if (partKey == messageLog.Request.RoutingToken)
                        {
                            messageLog.Request.ProviderName = participant.Value;
                        }
                        if (messageLog.Response != null)
                        {
                            if (partKey == messageLog.Response.Consumer.ToLower())
                            {
                                messageLog.Response.ConsumerName = participant.Value;
                            }
                            if (partKey == messageLog.Response.RoutingToken)
                            {
                                messageLog.Response.RoutingTokenName = participant.Value;
                            }
                            if (partKey == messageLog.Response.Provider.ToLower())
                            {
                                messageLog.Response.ProviderName = participant.Value;
                            }
                        }  
                    }
                }

                return messageLog;
            }
            catch (NotFoundMessageLogTransactionIdException ex)
            {
                throw new HttpException(ex.Message);
            }

        }

        // Опис: Методот го повикува GetExcelSheet методот
        // Влезни параметри: параметри за филтрирање - консумер, провајдер, сервис, метод за сервис, дата од, дата до, успешност на трансакција
        // Излезни параметри: HttpResponseMessage модел 
        public HttpResponseMessage CreateExcel(string filterConsumer, string filterProvider, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol, bool? filterTransactionSuccess = null)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            var mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(GetExcelSheet(filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "MessageLogs.xlsx"
            };
            return response;
        }

        // Опис: Методот го повикува GetFilteredMessageLogs методот на MessageLogRepository модулот
        // Влезни параметри: параметри за филтрирање - консумер, провајдер, сервис, метод за сервис, успешност на трансакција, дата од, дата до
        // Излезни параметри: MemoryStream модел 
        public MemoryStream GetExcelSheet(string filterConsumer, string filterProvider, string filterService, string filterMethod, bool? filterTransactionSuccess, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            using (var package = new ExcelPackage())
            {
                var resultData = _messageLogsRepository.GetFilteredMessageLogs(filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol);
                var worksheet = package.Workbook.Worksheets.Add("MessageLogsExcel");

                int rowCounter = 1;

                ExcelHelper.SetExcelDescription(worksheet, fromDate, toDate);
                rowCounter = rowCounter + 2;
                ExcelHelper.SetExcelHeader(worksheet, rowCounter);
                rowCounter++;

                var allParticipants = GetParticipants();

                var allServices = GetServices();

                foreach (var log in resultData)
                {
                    worksheet.Cells["A" + rowCounter].Value = log.TransactionId;
                    worksheet.Cells["B" + rowCounter].Value = !log.Consumer.Contains("MIM") ? allParticipants.FirstOrDefault(x => x.Key.ToUpper().Contains(log.Consumer)).Value : allParticipants.FirstOrDefault(x => x.Key.ToUpper() == log.Consumer).Value;
                    worksheet.Cells["C" + rowCounter].Value = !log.RoutingToken.Contains("MIM") ? allParticipants.FirstOrDefault(x => x.Key.ToUpper().Contains(log.RoutingToken)).Value : allParticipants.FirstOrDefault(x => x.Key.ToUpper() == log.RoutingToken).Value;
                    worksheet.Cells["D" + rowCounter].Value = allServices.FirstOrDefault(x=>x.Key.ToLower() == log.Service.ToLower()).Value;
                    worksheet.Cells["E" + rowCounter].Value = log.HasResponse;
                    worksheet.Cells["F" + rowCounter].Value = log.RequestTimestamp;
                    worksheet.Cells["G" + rowCounter].Value = log.ResponseTimestamp;
                    rowCounter++;
                }

                worksheet.Cells["A:G"].AutoFitColumns();
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream;
            }
        }

        // Опис: Методот го повикува GetFilteredMessageLogs методот на MessageLogRepository модулот
        // Влезни параметри: параметри за филтрирање - консумер, провајдер, сервис, метод за сервис, дата од, дата до, успешност на трансакција
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreatePdf(string filterConsumer, string filterProvider, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol, bool? filterTransactionSuccess = null)
        {
            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase,routeData,new EmptyController());
            var messagelogs = _messageLogsRepository.GetFilteredMessageLogs(filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol).ToList();
            var splitter = new[] { "$$" };
            var allParticipants = GetParticipants();
            var allServices = GetServices();
            foreach (var messageLogExcelDto in messagelogs)
            {
                string msgLogRoutingToken = messageLogExcelDto.RoutingToken.Contains("$$") ? messageLogExcelDto.RoutingToken.Split(splitter, StringSplitOptions.None).Last().ToLower() : messageLogExcelDto.RoutingToken.ToLower();

                string msgLogConsumer = messageLogExcelDto.Consumer.Contains("$$") ? messageLogExcelDto.Consumer.Split(splitter, StringSplitOptions.None).Last().ToLower() : messageLogExcelDto.Consumer.ToLower();

                if (allParticipants.Count > 0)
                {
                    foreach (var participant in allParticipants)
                    {
                        string partKey = participant.Key.Contains("$$") ? participant.Key.Split(splitter, StringSplitOptions.None).Last().ToLower() : participant.Key.ToLower();
                         
                        if (partKey == msgLogRoutingToken)
                        {
                            messageLogExcelDto.RoutingToken = participant.Value;
                        }
                        if (partKey == msgLogConsumer)
                        {
                            messageLogExcelDto.Consumer = participant.Value;
                        }
                    }
                }

                messageLogExcelDto.Service = allServices.FirstOrDefault(x => x.Key.ToLower() == messageLogExcelDto.Service.ToLower()).Value;
            }

            var r = new Rotativa.ViewAsPdf("PrintMessageLogs", messagelogs);
            var binary = r.BuildPdf(controllerContext);
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new ByteArrayContent(binary);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            return result;
        }

        public class EmptyController : ControllerBase
        {
            // За експортирање на податоци
            protected override void ExecuteCore()
            {
            }
        };

        // Опис: Методот го повикува GetMessageLogs методот на MessageLogRepository модулот
        // Влезни параметри: /
        // Излезни параметри: листа со податоци од MessageLogDTO моделот
        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        //[System.Web.Http.Authorize(Roles = "SuperAdmin")]
        public IEnumerable<MessageLogDTO> GetMessageLogList()
        {
            var messageLogs = _messageLogsRepository.GetMessageLogs();

            var messageLogsDto = messageLogs.Select(x => new MessageLogDTO
            {
                TransactionId = x.TransactionId,
                Consumer = x.Consumer,
                Provider = x.Provider,
                RoutingToken = x.RoutingToken,
                CreateDate = x.CreateDate,
                Dir = x.Dir,
                Service = x.Service,
                ServiceMethod = x.ServiceMethod,
                Timestamp = x.Timestamp
            }).ToList();

            return messageLogsDto;
        }

        public Dictionary<string, string> RequestToCs(string url)
        {
            string responseFromServer = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
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
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseFromServer);
            }
            return new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetParticipants()
        {
            var participantsFromCs = RequestToCs(AppSettings.Get<string>("ApiCSUrl") + "Participant/GetParticipants");
            return participantsFromCs;
        }

        public Dictionary<string, string> GetServices()
        {
            var participantsFromCs = RequestToCs(AppSettings.Get<string>("ApiCSUrl") + "Service/GetServices");
            return participantsFromCs;
        }

            // Опис: Методот го повикува GetMessageLogs методот на MessageLogRepository модулот
        // Влезни параметри: параметри за филтрирање - консумер, провајдер, насока на комуникација, сервис, метод за сервис, дата од, дата до, насока на сортирање, колона за сортирање
        // Излезни параметри: нумерирана листа со податоци од MessageLogDTO моделот
        [System.Web.Http.Authorize]
        public PagedCollection<MessageLogPairsViewModel> GetMessageLogListPaged(int pageIndex, int itemsPerPage, string filterConsumer, string filterProvider, string filterService, string filterMethod, bool? filterTransactionSuccess, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            var messageLogs = _messageLogsRepository.GetMessageLogsPaged(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterService, filterMethod, filterTransactionSuccess, fromDate, toDate, sortDir, sortCol);
            var allParticipants = GetParticipants();
            if (allParticipants.Count > 0)
            {
                foreach (var msgLog in messageLogs.Items)
                {
                    msgLog.ConsumerName = allParticipants.Where(participant => participant.Key.ToLower().Contains(msgLog.Consumer.ToLower())).Select(participant => participant.Value).FirstOrDefault();
                    msgLog.ProviderName = allParticipants.Where(participant => participant.Key.ToLower().Contains(msgLog.RoutingToken.ToLower())).Select(participant => participant.Value).FirstOrDefault();
                }
            }
            return messageLogs;
        }

    }

}
