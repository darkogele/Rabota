using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using System.Web.Mvc;
using OfficeOpenXml;
using Rotativa.Options;
using Interop.CS.Models;

namespace Interop.CS.Portal.API.Controllers
{
    public class MessageLogController : ApiController
    {
        private readonly IMessageLogRepository _messageLogsRepository;

        public MessageLogController()
        {

        }

        public MessageLogController(IMessageLogRepository messageLogsRepository)
        {
            _messageLogsRepository = messageLogsRepository;
        }

        //Опис: Методот прави повик до GetMessageLogByTransactionId од MessageLogRepository
        //Влезни параметри: Id на трансакција, насока
        [System.Web.Http.HttpGet]
        public MessageLog GetMessageLogByTid(Guid transactionId, string dir)
        {
            try
            {
                return _messageLogsRepository.GetMessageLogByTransactionId(transactionId, dir);
            }
            catch (NotFoundMessageLogTransactionIdException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот прави повик до GetMessageLogs од MessageLogRepository
        [System.Web.Http.HttpGet]
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

        //Опис: Методот прави повик до GetMessageLogsPaged од MessageLogRepository
        //Влезни параметри: индекс за страна, број на записи по страна, консумер, провајдер, насока, сервис, метод, дата од, дата до, насока за сортирање, колона за сортирање
        [System.Web.Http.HttpGet]
        public PagedCollection<MessageLogDTO> GetMessageLogListPaged(int pageIndex, int itemsPerPage, string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            var messageLogs = _messageLogsRepository.GetMessageLogsPaged(pageIndex, itemsPerPage, filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol);
            return messageLogs;
        }

        //Опис: Методот овозможува креирање на Excel документ за записите од базата за Логови
        //Влезни параметри: корисник, провајдер, насока, сервис, метод, датум од (за временски печат),датум до (за временски печат)
        public HttpResponseMessage CreateExcel(string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {

            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK);
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(GetExcelSheet(filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "MessageLogs.xlsx";
            return response;
        }

        //Опис: Методот овозможува креирање на Excel документ за записите од базата за Логови
        //Се прави повик до GetFilteredMessageLogs од MessageLogRepository каде се филтрираат записите според влезните параметри
        //Влезни параметри: корисник, провајдер, насока, сервис, метод, датум од (за временски печат),датум до (за временски печат)
        public MemoryStream GetExcelSheet(string filterConsumer, string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {
            using (var package = new ExcelPackage())
            {
                var resultData = _messageLogsRepository.GetFilteredMessageLogs(filterConsumer, filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol);
                var worksheet = package.Workbook.Worksheets.Add("MessageLogsExcel");
                //if (resultData != null) 
                worksheet.Cells["A1"].LoadFromCollection(resultData, true);
                // package.Save();
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream;
            }
        }
        //Опис: Методот овозможува креирање на PDF документ за записите од базата за Логови
        //Се прави повик до GetFilteredMessageLogs од MessageLogRepository каде се филтрираат записите според влезните параметри
        //Влезни параметри: корисник, провајдер, насока, сервис, метод, датум од (за временски печат),датум до (за временски печат)
        [System.Web.Http.HttpGet]
       
        public HttpResponseMessage CreatePdf(string filterConsumer,
            string filterProvider, string filterDir, string filterService, string filterMethod, DateTime? fromDate, DateTime? toDate, string sortDir, string sortCol)
        {

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase,
                routeData,
                new EmptyController());
            var messagelogs = _messageLogsRepository.GetFilteredMessageLogs( filterConsumer,
                filterProvider, filterDir, filterService, filterMethod, fromDate, toDate, sortDir, sortCol);

            var r = new Rotativa.ViewAsPdf("PrintMessageLogs", messagelogs)
            {
                //PageSize = Size.A4,

                //PageMargins = {Left = 0, Right = 0, Bottom = 30, Top = 20},
                //FileName = "testpdf",
            };
            var binary = r.BuildPdf(controllerContext);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new ByteArrayContent(binary);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "testpdf"
            };
            return result;

        }

        //public static string RenderViewToString(string controllerName, string viewName, object viewData)
        //{
        //    var context = HttpContext.Current;
        //    var contextBase = new HttpContextWrapper(context);
        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", controllerName);

        //    var controllerContext = new ControllerContext(contextBase,
        //                                                  routeData,
        //                                                  new EmptyController());

        //    var razorViewEngine = new RazorViewEngine();
        //    var razorViewResult = razorViewEngine.FindView(controllerContext,
        //                                                   viewName,
        //                                                   "",
        //                                                   false);

        //    var writer = new StringWriter();
        //    var viewContext = new ViewContext(controllerContext,
        //                                      razorViewResult.View,
        //                                      new ViewDataDictionary(viewData),
        //                                      new TempDataDictionary(),
        //                                      writer);
        //    razorViewResult.View.Render(viewContext, writer);

        //    return writer.ToString();
        //}

         }

    public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };
    }
