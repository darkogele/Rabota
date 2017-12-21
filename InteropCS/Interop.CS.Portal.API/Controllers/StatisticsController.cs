using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ikvm.extensions;
using Interop.CS.CrossCutting;
using Interop.CS.CrossCutting.Logging;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Microsoft.Office.Interop.Word;
using OfficeOpenXml;
using Interop.CS.Portal.API.Models;
using Interop.CS.Portal.API.KIBSref;
using System.Security.Cryptography.X509Certificates;
using System.Web.Configuration;
using System.Text;
using OfficeOpenXml.Style;
using Color = System.Drawing.Color;
using Table = Microsoft.Office.Interop.Word.Table;

namespace Interop.CS.Portal.API.Controllers
{
    //[System.Web.Http.Authorize]
    public class StatisticsController : ApiController
    {
        private readonly IParticipantRepository _participantsRepository;
        private readonly IAccessMappingRepository _accessMappingRepository;
        private readonly IMessageLogStatisticRepository _messageLogStatisticRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger _logger;
        public StatisticsController()
        {

        }
        public StatisticsController(IParticipantRepository participantsRepository, IAccessMappingRepository accessMappingRepository, IMessageLogStatisticRepository messageLogStatisticRepository,
            IServiceRepository serviceRepository, ILogger logger)
        {
            _participantsRepository = participantsRepository;
            _accessMappingRepository = accessMappingRepository;
            _messageLogStatisticRepository = messageLogStatisticRepository;
            _serviceRepository = serviceRepository;
            _logger = logger;
        }

        private List<string> GetServicesByName(IEnumerable<AccessMapping> accessMappings)
        {
            string serviceName;
            var services = new List<string>();
            foreach (var accessMapping in accessMappings.ToList())
            {
                serviceName = _serviceRepository.GetServiceName(accessMapping.ServiceCode);
                if (!services.Contains(serviceName))
                {
                    services.Add(serviceName);
                }
            }
            return services;
        }

        [System.Web.Http.HttpGet]
        public List<string> FillServices(string provider = "", string consumer = "")
        {
            string serviceCodeToBeIgnored = ConfigurationManager.AppSettings["MunicipalitiesKey"] ?? "Municipalities";

            bool consumerContainsMim = false;
            bool providerContainsMim = false;
            var mims = new[] { "MIM1$$", "MIM2$$" };
            string serviceName;
            var services = new List<string>();
            if (!string.IsNullOrEmpty(consumer))
            {
                consumerContainsMim = mims.Any(m => consumer.Contains(m));
            }
            if (!string.IsNullOrEmpty(provider))
            {
                providerContainsMim = mims.Any(m => provider.Contains(m));
            }

            if (consumerContainsMim)
            {
                consumer = consumer.Remove(0, 6);
            }
            if (providerContainsMim)
            {
                provider = provider.Remove(0, 6);
            }
            if (!string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
            {
                IQueryable<AccessMapping> accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer && x.ProviderCode == provider && x.ServiceCode != serviceCodeToBeIgnored).AsQueryable();
                if (accessMappings.Any())
                {
                    services = GetServicesByName(accessMappings);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(consumer) && !string.IsNullOrEmpty(provider))
                {
                    IQueryable<AccessMapping> accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ProviderCode == provider && x.ServiceCode != serviceCodeToBeIgnored).AsQueryable();
                    if (accessMappings.Any())
                    {
                        services = GetServicesByName(accessMappings);
                    }
                }
                if (string.IsNullOrEmpty(provider) && !string.IsNullOrEmpty(consumer))
                {
                    IQueryable<AccessMapping> accessMappings = _accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer && x.ServiceCode != serviceCodeToBeIgnored).AsQueryable();
                    if (accessMappings.Any())
                    {
                        services = GetServicesByName(accessMappings);
                    }
                }
                if (string.IsNullOrEmpty(provider) && string.IsNullOrEmpty(consumer))
                {
                    var allServices = _serviceRepository.GetServices().Where(x => x.Code != serviceCodeToBeIgnored);
                    if (allServices.Any())
                    {
                        foreach (var service in allServices)
                        {
                            if (!services.Contains(service.Code))
                            {
                                serviceName = _serviceRepository.GetServiceName(service.Code);
                                services.Add(serviceName);
                            }

                        }
                    }
                }
            }
            return services;
        }
            
        public List<MessageLogStatisticToDisplayInDocs> PrepareDataForReports(bool successfully, bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string consumer = "", string provider = "", string service = "")
        {
            var messageLogsStatisticDto = _messageLogStatisticRepository.GetMessageLogStatistics(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service, string.Empty, string.Empty);

            var messageLogsGrouped =
                 from allMessageLogs in messageLogsStatisticDto
                 group allMessageLogs by allMessageLogs.Service into logsGroupedByService
                 from logsGroupedByConsumer in
                     (from logGroupedByService in logsGroupedByService
                      group logGroupedByService by logGroupedByService.Consumer)
                 group logsGroupedByConsumer by logsGroupedByService.Key;

            var messageLogStatisticList = new List<MessageLogStatisticToDisplayInDocs>();
            foreach (var mlByService in messageLogsGrouped)
            {
                int serviceTotalCalled = 0;

                var listConsumers = new List<Consumer>();
                var messageLogStatisticToDisplayInDocs = new MessageLogStatisticToDisplayInDocs();
                foreach (var mlConsumerInGroupedByService in mlByService)
                {
                    _logger.Info("PrepareDataForReports mlConsumerInGroupedByService: " + mlConsumerInGroupedByService.Key);
                    var listProviders = new List<Provider>();
                    string pointOfError = string.Empty;

                    var groupedByRoutingToken = mlConsumerInGroupedByService.GroupBy(x => x.RoutingToken); //Tuka pravese problem, davase dupli redovi oti starata statistika vo Routing token kolonata nemase MIM1$$
                    foreach (var routingToken in groupedByRoutingToken)
                    {
                        _logger.Info("PrepareDataForReports routingToken: " + routingToken.Key);
                        var listTransactionsInListServices = new List<Transaction>();
                        var groupedByTransaction = routingToken.GroupBy(x => x.TransactionId);
                        var prov = new Provider();
                        foreach (var transaction in groupedByTransaction)
                        {
                            _logger.Info("PrepareDataForReports transaction: " + transaction.Key);
                            var tran = new Transaction
                            {
                                TransactionId = transaction.Key
                            };

                            if (transaction.Count() == 6)
                            {
                                tran.isSuccessfull = true;
                                tran.NumberOfSuccesfull++;
                            }
                            else
                            {
                                tran.isSuccessfull = false;
                                tran.NumberOfUnSuccesfull++;

                                var transactionGroupedByParticipantCode = transaction.GroupBy(x => x.ParticipantCode);
                                foreach (var participantCode in transactionGroupedByParticipantCode)
                                {
                                    if (transactionGroupedByParticipantCode.Count() != 2)
                                    {
                                        tran.ErrorInstitution += participantCode.Key + "; ";
                                        pointOfError = tran.ErrorInstitution + "; ";
                                    }
                                }
                            }

                            //Filtriranje uspeshno/neuspesno
                            if (!successfully && !unsuccessfully)
                            {
                                listTransactionsInListServices.Add(tran);
                                prov.SuccesfullCalls += tran.NumberOfSuccesfull;
                                prov.UnSuccesfullCalls += tran.NumberOfUnSuccesfull;
                                serviceTotalCalled++;
                            }
                            else
                            {
                                if (successfully && tran.isSuccessfull)
                                {
                                    listTransactionsInListServices.Add(tran);
                                    prov.SuccesfullCalls += tran.NumberOfSuccesfull;
                                    prov.Count += prov.SuccesfullCalls;
                                    serviceTotalCalled++;
                                }
                                else if (unsuccessfully && !tran.isSuccessfull)
                                {
                                    listTransactionsInListServices.Add(tran);
                                    prov.UnSuccesfullCalls += tran.NumberOfUnSuccesfull;
                                    prov.Count += prov.UnSuccesfullCalls;
                                    serviceTotalCalled++;
                                }
                            }

                        }

                        listProviders.Add(new Provider
                        {
                            ProviderName = routingToken.Key,
                            Count = prov.SuccesfullCalls + prov.UnSuccesfullCalls/*groupedByTransaction.Count()*/,
                            ListTransactions = listTransactionsInListServices,
                            SuccesfullCalls = prov.SuccesfullCalls,
                            UnSuccesfullCalls = prov.UnSuccesfullCalls,
                            PointOfError = pointOfError
                        });
                    }
                    listConsumers.Add(new Consumer
                    {
                        ConsumerName = mlConsumerInGroupedByService.FirstOrDefault().ConsumerName/*mlConsumerInGroupedByService.Key*/,
                        ListProviders = listProviders
                    });

                    messageLogStatisticToDisplayInDocs.ListServices = new List<Service>
                    {
                        new Service
                        {
                            ServiceName = mlByService.Key,
                            ListConsumers = listConsumers,
                            TotalCalledTimes = serviceTotalCalled
                        }
                    };
                }
                messageLogStatisticList.Add(messageLogStatisticToDisplayInDocs);
            }
            return messageLogStatisticList;
        }

        //Опис: 
        //Влезни параметри: корисник, провајдер, успешно, неуспешно, дата од (за Логови), дата до (за Логови)
        //Излезни параметри: Листа од MessageLogStatisticDTO записи
        [System.Web.Http.HttpGet]
        public PagedCollection<MessageLogStatisticDTO> GetMessageLogs(string consumer, string provider, bool successfully,
            bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string service, int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            var messageLogsDto =
               _messageLogStatisticRepository.GetMessageLogStatistics(consumer, provider, successfully, unsuccessfully, fromDate, toDate, service, sortDir, sortCol);

            var statisticByTransaction = from ml in messageLogsDto
                group ml by ml.TransactionId
                into mlByTransaction
                select new MessageLogStatisticDTO
                  {
                    Id = mlByTransaction.FirstOrDefault().Id,
                    TransactionId = mlByTransaction.Key,
                    Count = mlByTransaction.Count(),
                    Consumer = mlByTransaction.FirstOrDefault().Consumer,
                    RoutingToken = mlByTransaction.FirstOrDefault().RoutingToken,
                    ParticipantUri = mlByTransaction.FirstOrDefault().ParticipantUri,
                    CreateDate = mlByTransaction.FirstOrDefault().CreateDate,
                    ConsumerName = mlByTransaction.FirstOrDefault().ConsumerName,
                    RoutingTokenName = mlByTransaction.FirstOrDefault().RoutingTokenName,
                    Service = mlByTransaction.FirstOrDefault().Service
                };

            if (successfully)
            {
                statisticByTransaction = statisticByTransaction.Where(x => x.Count == 6);
            }
            else if (unsuccessfully)
            {
                statisticByTransaction = statisticByTransaction.Where(x => x.Count < 6);
            }

            var totalSize = statisticByTransaction.Count();

            var statisticItems =
                statisticByTransaction.OrderByDescending(x => x.Id)
                    .Skip((pageIndex - 1) * itemsPerPage)
                    .Take(itemsPerPage);

            return new PagedCollection<MessageLogStatisticDTO>(pageIndex, itemsPerPage, totalSize, statisticItems.ToList());
        }

        //Опис: Методот прави повик до GetMessageLogStatisticByTransactionId од MessageLogStatisticRepository
        //Влезни параметри: Id на трансакција
        [System.Web.Http.HttpGet]
        public MessageLogStatisticDetails GetMessageLogStatisticsByTransactionId(Guid transactionId, string consumer, string routingToken)
        {
            try
            {
                return _messageLogStatisticRepository.GetMessageLogStatisticByTransactionId(transactionId, consumer, routingToken);
            }
            catch (NotFoundMessageLogTransactionIdException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот овозможува креирање на PDF документ за записите од базата за MessageLogStatistic
        //Се прави повик до PrepareDataForReports  каде се филтрираат записите според влезните параметри
        //Влезни параметри: успешно, неуспешно, дата од (за Логови), дата до (за Логови),корисник, провајдер
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreatePdf(bool successfully, bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string consumer = "", string provider = "")
        {
            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase,
                routeData,
                new EmptyController());

            //var statistics = GetMessageLogs(successfully, unsuccessfully, fromDate, toDate, consumer, provider).ToList();
            var resultData = PrepareDataForReports(successfully, unsuccessfully, fromDate, toDate, consumer,
                   provider);

            var r = new Rotativa.ViewAsPdf("PrintStatistics", resultData);
            var binary = r.BuildPdf(controllerContext);
            var result = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(binary) };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "testpdf"
            };
            return result;
        }

        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };

        //Опис: Методот овозможува креирање на Excel документ за записите од базата за MessageLogStatisticsRepository
        //Влезни параметри: успешно, неуспешно, дата од (за Логови), дата до (за Логови),корисник, провајдер
        public HttpResponseMessage CreateExcel(bool successfully, bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string consumer = "", string provider = "", string service = "")
        {
            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK);
            var mediaType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            response.Content = new StreamContent(GetExcelSheet(successfully, unsuccessfully, fromDate, toDate, consumer, provider, service));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "MessageLogsStatistics.xls";
            return response;
        }

        //Опис: Методот овозможува креирање на Word документ за записите од базата за MessageLogStatisticsRepository
        //Влезни параметри: успешно, неуспешно, дата од (за Логови), дата до (за Логови),корисник, провајдер, сервис 
        public HttpResponseMessage CreateWord(bool successfully, bool unsuccessfully, DateTime? fromDate,
            DateTime? toDate, string consumer = "", string provider = "", string service = "")
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(GetWordStreamData(successfully, unsuccessfully, fromDate, toDate, consumer, provider, service));
            result.Content.Headers.Add("Content-Disposition", "attchment; filename=" + HttpUtility.UrlEncode("Статистика", Encoding.UTF8));
            result.Content.Headers.Add("Content-Encoding", "UTF-8");
            result.Content.Headers.Add("Charset", "UTF-8");
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "Statistics.doc";

            _logger.Info("CreateWord result e: " + result);

            return result;
        }

        private float InchesToPoints(float fInches)
        {
            return fInches * 72.0f;
        }

        //Опис: Методот овозможува прибирање на сите податоци според избраните филтри од UI, задавање на изглед на самиот документ
        //Влезни параметри: успешно, неуспешно, дата од (за Логови), дата до (за Логови),корисник, провајдер, сервис 
        public MemoryStream CreatingWordDocument(bool successfully, bool unsuccessfully, DateTime? fromDate,
            DateTime? toDate, string consumer = "", string provider = "", string service = "")
        {
            var messageLogs = PrepareDataForReports(successfully, unsuccessfully, fromDate, toDate, consumer, provider, service);

            _logger.Info("CreatingWordDocument get message logs from PrepareDataForReports, count: " + messageLogs.Count);

            try
            {
                var winword = new Microsoft.Office.Interop.Word.Application();

                object missing = System.Reflection.Missing.Value;
                Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                document.PageSetup.TopMargin = InchesToPoints(0.9f);
                document.PageSetup.BottomMargin = InchesToPoints(0.9f);
                document.PageSetup.LeftMargin = InchesToPoints(0.9f);
                document.PageSetup.RightMargin = InchesToPoints(0.9f);

                var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
                foreach (Section section in document.Sections)
                {
                    //Get the header range and add the header details.
                    Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    headerRange.Font.Name = "Calibri";
                    headerRange.Font.ColorIndex = WdColorIndex.wdBlue;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "Статистика за период " + fromDate.Value.ToString("dd.MM.yyyy", macCultureInfo) +
                                       " - " + toDate.Value.ToString("dd.MM.yyyy", macCultureInfo);
                }
                foreach (Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.
                    Range footerRange = wordSection.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 10;
                    footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "Детали за повикување на сервиси";
                }

                #region All services

                bool allServices = false;
                if (string.IsNullOrEmpty(service))
                {
                    allServices = true;
                    Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    para1.Range.Text = "Сервиси и вкупен број на повикувања:";
                    para1.Range.InsertParagraphAfter();
                }
                if (allServices)
                {
                    foreach (var messageLog in messageLogs)
                    {
                        foreach (var calledService in messageLog.ListServices)
                        {
                            Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                            para1.Range.Text = "Сервис: " + calledService.ServiceName + ": " + calledService.TotalCalledTimes;
                            para1.Range.InsertParagraphAfter();
                        }
                    }
                }

                #endregion

                foreach (var messageLog in messageLogs)
                {
                    foreach (var services in messageLog.ListServices)
                    {
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        object styleHeading1 = "Heading 1";
                        para1.Range.set_Style(ref styleHeading1);
                        para1.Range.Text = "Сервис: " + services.ServiceName;
                        // para1.SpaceBefore = InchesToPoints(0.6f);
                        para1.Range.InsertParagraphAfter();

                        //Add paragraph with Heading 2 style
                        Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                        //object styleHeading2 = "Heading 3";
                        para2.Range.set_Style(WdBuiltinStyle.wdStyleIntenseReference);
                        para2.Range.Text = "Вкупен број на повикувања:" + services.TotalCalledTimes;
                        para2.Range.InsertParagraphAfter();

                        Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
                        para3.Range.Text = Environment.NewLine + "Детали за повикување на сервисот од институции";
                        para3.Range.InsertParagraphAfter();

                        //Na baranje na MIOA, dokolku od UI se filtrira po uspeshni ili neuspeshni, vo dokument fajlot treba da gi dava samo tie koloni
                        int count = 0;
                        if (successfully || unsuccessfully) count = 2;
                        if (!successfully && !unsuccessfully) count = 4;

                        Table consumersTable = document.Tables.Add(para3.Range, services.ListConsumers.Count + 1, count, ref missing, ref missing);
                        consumersTable.Borders.Enable = 1;
                        consumersTable.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow);

                        //Adding header row
                        for (int i = 1; i <= count; i++)
                        {
                            consumersTable.Cell(1, i).Range.Font.Bold = 1;
                            consumersTable.Cell(1, i).Range.Font.Name = "Calibri";
                            consumersTable.Cell(1, i).Range.Font.Size = 10;
                            consumersTable.Cell(1, i).Range.Font.Color = WdColor.wdColorGray75;
                            consumersTable.Cell(1, i).Shading.BackgroundPatternColor = WdColor.wdColorGray20;
                            consumersTable.Cell(1, i).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            consumersTable.Cell(1, i).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            if (i == 1)
                            {
                                consumersTable.Cell(1, i).Range.Paragraphs.SpaceBefore = 7;
                                consumersTable.Cell(1, i).Range.Text = "Институција";
                            }
                            if (successfully)
                            {
                                if (i == 2)
                                {
                                    consumersTable.Cell(1, i).Range.Paragraphs.SpaceBefore = 7;
                                    consumersTable.Cell(1, i).Range.Text = "Успешни трансакции";
                                }
                            }
                            else if (unsuccessfully)
                            {
                                //ako nema uspeshni, togash kolonata za neuspeshni ima edna pozicija minus, sega e na pozicija 2
                                if (i == 2)
                                {
                                    consumersTable.Cell(1, i).Range.Paragraphs.SpaceBefore = 7;
                                    consumersTable.Cell(1, i).Range.Text = "Неуспешни трансакции";
                                }
                            }
                            else
                            {
                                if (i == 2)
                                {
                                    consumersTable.Cell(1, i).Range.Paragraphs.SpaceBefore = 7;
                                    consumersTable.Cell(1, i).Range.Text = "Успешни трансакции";
                                }
                                if (i == 3)
                                {
                                    consumersTable.Cell(1, i).Range.Paragraphs.SpaceBefore = 7;
                                    consumersTable.Cell(1, i).Range.Text = "Неуспешни трансакции";
                                }
                                if (i == 4)
                                {
                                    consumersTable.Cell(1, i).Range.Paragraphs.SpaceBefore = 7;
                                    consumersTable.Cell(1, i).Range.Text = "Вкупно трансакции";
                                }
                            }
                        }

                        //Adding the rest of the rows
                        for (int j = 0; j < services.ListConsumers.Count; j++)
                        {
                            consumersTable.Cell(j + 2, 1).Range.Text = services.ListConsumers[j].ConsumerName;

                            for (int k = 0; k < services.ListConsumers[j].ListProviders.Count; k++)
                            {
                                if (successfully)
                                {
                                    consumersTable.Cell(j + 2, 2).Range.Text = services.ListConsumers[j].ListProviders[k].SuccesfullCalls.toString();
                                }
                                else if (unsuccessfully)
                                {
                                    //ako nema uspeshni, togash kolonata neuspeshni ima edna pozicija minus, t.e sega e na pozicija 2
                                    consumersTable.Cell(j + 2, 2).Range.Text = services.ListConsumers[j].ListProviders[k].UnSuccesfullCalls.toString();
                                }
                                else
                                {
                                    consumersTable.Cell(j + 2, 2).Range.Text = services.ListConsumers[j].ListProviders[k].SuccesfullCalls.toString();
                                    consumersTable.Cell(j + 2, 3).Range.Text = services.ListConsumers[j].ListProviders[k].UnSuccesfullCalls.toString();
                                    consumersTable.Cell(j + 2, 4).Range.Text = services.ListConsumers[j].ListProviders[k].Count.toString();
                                }
                                
                            }
                        }
                        Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                        para4.SpaceAfter = 7;
                        para4.Range.InsertParagraphAfter();
                    }
                }

                object filename = AppSettings.Get<string>("StatisticWordDocumentPath");
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                winword.Quit(ref missing, ref missing, ref missing);
                var getDocument = File.ReadAllBytes(filename.toString());
                var stream = new MemoryStream(getDocument);
                return stream;
            }
            catch (Exception ex)
            {
                _logger.Error("Se sluci greska pri kreiranje na word dokumentot: ", ex);
                throw ex;
            }

        }

        //Опис: Методот претставува подготовка за креирање на документ
        //Се повикува метода CreatingWordDocument со соодветни влезни параметри кои корисникот ги избира од UI
        //Влезни параметри: успешно, неуспешно, дата од (за Логови), дата до (за Логови),корисник, провајдер, сервис
        public MemoryStream GetWordStreamData(bool successfully, bool unsuccessfully, DateTime? fromDate,
            DateTime? toDate, string consumer = "", string provider = "", string service = "")
        {
            var stream = CreatingWordDocument(successfully, unsuccessfully, fromDate, toDate, consumer, provider,
                service);
            _logger.Info("GetWordStreamData stream Length e: " + stream.Length);
            return stream;
        }

        private void SetBordersAndHeaderBckColor(ExcelRange cell, bool setBckColor)
        {
            if (setBckColor)
            {
                Color headersBckColor = Color.FromArgb(141, 180, 226);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(headersBckColor);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, headersBckColor);
            }
            Color bordersColor = Color.FromArgb(0, 0, 0);
           
            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, bordersColor);
        }

        //Опис: Методот овозможува креирање на Excel документ за записите од базата за MessageLogStatisticsRepository
        //Се прави повик до PrepareDataForReports каде се филтрираат записите според влезните параметри
        //Влезни параметри: успешно, неуспешно, дата од (за Логови), дата до (за Логови),корисник, провајдер
        public MemoryStream GetExcelSheet(bool successfully, bool unsuccessfully, DateTime? fromDate, DateTime? toDate, string consumer = "", string provider = "", string service = "")
        {
            using (var package = new ExcelPackage())
            {
                var resultData = PrepareDataForReports(successfully, unsuccessfully, fromDate, toDate, consumer, provider, service);
                var worksheet = package.Workbook.Worksheets.Add("MessageLogsExcel");
                Color fontColorO = Color.FromArgb(22, 54, 92);
                Color fontColorP = Color.FromArgb(192, 0, 52);
                Color fontColorH = Color.FromArgb(173, 128, 255);

                int rowCounter = 1;

                var macCultureInfo = CultureInfo.CreateSpecificCulture("mk-MK");
                worksheet.Cells["A1:D2"].Merge = true;
                worksheet.Cells["A" + rowCounter].Value = "Статистика за период " + fromDate.Value.ToString("dd.MM.yyyy", macCultureInfo) +
                                       " - " + toDate.Value.ToString("dd.MM.yyyy", macCultureInfo);
                worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorH);
                worksheet.Cells["A" + rowCounter].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells["A" + rowCounter].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                rowCounter = rowCounter + 2;

                #region All services

                bool allServices = false;
                if (string.IsNullOrEmpty(service))
                {
                    allServices = true;
                    worksheet.Cells["A" + rowCounter].Value = "Сервиси и вкупен број повикувања:";
                    worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorP);
                    rowCounter++;
                }
                if (allServices)
                {
                    foreach (var itemInDoc in resultData)
                    {
                        foreach (var calledService in itemInDoc.ListServices)
                        {
                            worksheet.Cells["A" + rowCounter].Value = calledService.ServiceName + ": " + calledService.TotalCalledTimes;
                            worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            worksheet.Cells["A" + rowCounter].Style.Font.Bold = true;
                            rowCounter++;
                        }
                    }
                    rowCounter++;
                }
                

                #endregion

                foreach (var item in resultData)
                {
                    foreach (var ser in item.ListServices)
                    {
                        worksheet.Cells["A" + rowCounter].Value = "Сервис: " + ser.ServiceName;
                        worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                        worksheet.Cells["A" + rowCounter].Style.Font.Size = InchesToPoints(0.19f);
                        worksheet.Cells["A" + rowCounter].Style.Font.Bold = true;
                        rowCounter++;
                        worksheet.Cells["A" + rowCounter].Value = "Вкупен број на повикувања: " + ser.TotalCalledTimes;
                        worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorP);
                        worksheet.Cells["A" + rowCounter].Style.Font.Bold = true;
                        rowCounter = rowCounter + 2;
                        worksheet.Cells["A" + rowCounter].Value = "Детали за повикување на сервисот од институции";
                        worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                        rowCounter = rowCounter + 1;

                        //Setting table header
                        worksheet.Cells["A" + rowCounter].Value = "Институција";
                        worksheet.Cells["A" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                        worksheet.Cells["A" + rowCounter].Style.Font.Bold = true;
                        SetBordersAndHeaderBckColor(worksheet.Cells["A" + rowCounter], true);

                        if (successfully)
                        {
                            //Uspeshni
                            worksheet.Cells["B" + rowCounter].Value = "Успешни трансакции";
                            worksheet.Cells["B" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            worksheet.Cells["B" + rowCounter].Style.Font.Bold = true;
                            SetBordersAndHeaderBckColor(worksheet.Cells["B" + rowCounter], true);

                            //Nema neuspeshni, kolonata vkupno se pomestuva edna pozicija minus, na pozicija C
                            //worksheet.Cells["C" + rowCounter].Value = "Вкупно трансакции";
                            //worksheet.Cells["C" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            //worksheet.Cells["C" + rowCounter].Style.Font.Bold = true;
                            //SetBordersAndHeaderBckColor(worksheet.Cells["C" + rowCounter], true);
                        }
                        else if (unsuccessfully)
                        {
                            //Nema uspeshni, kolonata neuspeshni se pomestuva edna pozicija minus, na pozicija B
                            worksheet.Cells["B" + rowCounter].Value = "Неуспешни трансакции";
                            worksheet.Cells["B" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            worksheet.Cells["B" + rowCounter].Style.Font.Bold = true;
                            SetBordersAndHeaderBckColor(worksheet.Cells["B" + rowCounter], true);

                            //Nema uspeshni, kolonata vkupno se pomestuva edna pozicija minus, na pozicija C
                            //worksheet.Cells["C" + rowCounter].Value = "Вкупно трансакции";
                            //worksheet.Cells["C" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            //worksheet.Cells["C" + rowCounter].Style.Font.Bold = true;
                            //SetBordersAndHeaderBckColor(worksheet.Cells["C" + rowCounter], true);
                        }
                        else
                        {
                            worksheet.Cells["B" + rowCounter].Value = "Успешни трансакции";
                            worksheet.Cells["B" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            worksheet.Cells["B" + rowCounter].Style.Font.Bold = true;
                            SetBordersAndHeaderBckColor(worksheet.Cells["B" + rowCounter], true);

                            worksheet.Cells["C" + rowCounter].Value = "Неуспешни трансакции";
                            worksheet.Cells["C" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            worksheet.Cells["C" + rowCounter].Style.Font.Bold = true;
                            SetBordersAndHeaderBckColor(worksheet.Cells["C" + rowCounter], true);

                            worksheet.Cells["D" + rowCounter].Value = "Вкупно трансакции";
                            worksheet.Cells["D" + rowCounter].Style.Font.Color.SetColor(fontColorO);
                            worksheet.Cells["D" + rowCounter].Style.Font.Bold = true;
                            SetBordersAndHeaderBckColor(worksheet.Cells["D" + rowCounter], true);
                        }

                        rowCounter++;

                        for (int j = 0; j < ser.ListConsumers.Count; j++)
                        {
                            for (int k = 0; k < ser.ListConsumers[j].ListProviders.Count; k++)
                            {
                                worksheet.Cells["A" + rowCounter].Value = ser.ListConsumers[j].ConsumerName;
                                SetBordersAndHeaderBckColor(worksheet.Cells["A" + rowCounter], false);
                                
                                if (successfully)
                                {
                                    //Uspeshni
                                    worksheet.Cells["B" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].SuccesfullCalls;
                                    SetBordersAndHeaderBckColor(worksheet.Cells["B" + rowCounter], false);

                                    //Vklupno e na edna pozicija minus, odnosno na pozicija C
                                    //worksheet.Cells["C" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].Count;
                                    //SetBordersAndHeaderBckColor(worksheet.Cells["C" + rowCounter], false);
                                }
                                else if (unsuccessfully)
                                {
                                    //Nema uspeshni, neuspesni se pomestuva na edna pozicija nazad, odnosno pozicija B
                                    worksheet.Cells["B" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].UnSuccesfullCalls;
                                    SetBordersAndHeaderBckColor(worksheet.Cells["B" + rowCounter], false);

                                    //Vklupno e na edna pozicija minus, odnosno na pozicija C
                                    //worksheet.Cells["C" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].Count;
                                    //SetBordersAndHeaderBckColor(worksheet.Cells["C" + rowCounter], false);
                                }
                                else
                                {
                                    //Uspeshni
                                    worksheet.Cells["B" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].SuccesfullCalls;
                                    SetBordersAndHeaderBckColor(worksheet.Cells["B" + rowCounter], false);

                                    //Neuspeshni
                                    worksheet.Cells["C" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].UnSuccesfullCalls;
                                    SetBordersAndHeaderBckColor(worksheet.Cells["C" + rowCounter], false);

                                    //Vkupno si e na prava pozicija ako gi ima i uspeshni i neuspeshni
                                    worksheet.Cells["D" + rowCounter].Value = ser.ListConsumers[j].ListProviders[k].Count;
                                    SetBordersAndHeaderBckColor(worksheet.Cells["D" + rowCounter], false);
                                }

                                rowCounter = rowCounter + 1;
                            }
                        }
                        rowCounter = rowCounter + 2;
                    }
                }

                worksheet.Cells["A:D"].AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                return stream;
            }
        }

        [System.Web.Http.HttpPost]
        public string GetMessageLogCheckTimeStamp(TokenTimestampModel tokenTimestamp)
        {
            var oWS = new wsTSATest();
            oWS.Url = "https://wstsatest.kibs.mk/wsTSATest.asmx";
            var kibsCertificationPath = WebConfigurationManager.AppSettings["KIBSCertificationPath"];
            var kibsCertificationPassword = WebConfigurationManager.AppSettings["KIBSCertificationPassword"];
            var cer = new X509Certificate2(kibsCertificationPath, kibsCertificationPassword);
            oWS.ClientCertificates.Add(cer);
            byte[] temp_backToBytes = Convert.FromBase64String(tokenTimestamp.TokenTimestamp);
            var response = oWS.funCheckTS_Bytes(temp_backToBytes);
            oWS.Dispose();
            if (response.strFailureInfo == "")
                return "Проверката е успешна.";
            else
                return response.strFailureInfo;
        }

        //Опис: Методот прави повик до методот GetMessageLogsStatisticPaged од MessageLogStatisticRepository
        //Влезни параметри: индекс на страна, број на записи по страна
        //[System.Web.Http.HttpGet]
        //public PagedCollection<MessageLogStatisticDTO> GetMessageLogsStatisticPaged(int pageIndex, int itemsPerPage)
        //{
        //    var msgLogsStatisticsPaged = _messageLogStatisticRepository.GetMessageLogsStatisticPaged(pageIndex, itemsPerPage);
        //    return msgLogsStatisticsPaged;
        //}

    }
}
