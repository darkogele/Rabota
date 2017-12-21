using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    [System.Web.Http.Authorize]
    public class CURMController : ApiController
    {
        private const string InstitutionName = "Царинска Управа на Република Македонија";
        private ILogger _logger;

        // Опис: Конструктор на CURMController модулот 
        // Влезни параметри: модел ILogger
        public CURMController(ILogger logger)
         {
            _logger = logger;
         }

        // Опис: Методот го повикува DataExeExpClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb, amountOfExportFrom, amountOfExportTo, monthOfExportFrom, monthOfExportTo, yearOfExportFrom, yearOfExportTo
        // Излезни параметри: DataForExecutedExportDTO модел 
        [System.Web.Http.HttpPost]
        public DataForExecutedExportDTO GetDataForExecutedExport(long edb, string amountOfExportFrom = null, string amountOfExportTo = null, string monthOfExportFrom = null, string monthOfExportTo = null, string yearOfExportFrom = null, string yearOfExportTo = null)
        {
            if (edb < 1000000 || edb > 10000000000000)
            {
                throw new ArgumentException("Вредноста на параметарот 'едб' не е во границите на дозволени вредности.", edb.ToString());
            }
            if (monthOfExportFrom != null)
            {
                if (monthOfExportFrom.Any(m => (char.IsLetter(m) || char.IsSeparator(m) || char.IsPunctuation(m) || char.IsSymbol(m))))
                {
                    throw new ArgumentException("Параметарот 'Месец на извоз од' е невалиден. Внесената вредност содржи карактери/симболи:", monthOfExportFrom);
                }
                var tempMonthFrom = Convert.ToInt32(monthOfExportFrom);
                if (tempMonthFrom < 1 || tempMonthFrom > 12)
                {
                    throw new ArgumentException("Параметарот 'Месец на извоз од' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12.", monthOfExportFrom);
                }
            }
            if (monthOfExportTo != null)
            {
                if (monthOfExportTo.Any(m => (char.IsLetter(m) || char.IsSeparator(m) || char.IsPunctuation(m) || char.IsSymbol(m))))
                {
                    throw new ArgumentException("Параметарот 'Месец на извоз до' е невалиден. Внесената вредност содржи карактери/симболи:", monthOfExportTo);
                }
                var tempMonthTo = Convert.ToInt32(monthOfExportTo);
                if (tempMonthTo < 1 || tempMonthTo > 12)
                {
                    throw new ArgumentException("Параметарот 'Месец на извоз до' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12.", monthOfExportTo);
                }
            }
            if (yearOfExportFrom != null)
            {
                if (yearOfExportFrom.Any(y => (char.IsLetter(y) || char.IsSeparator(y) || char.IsPunctuation(y) || char.IsSymbol(y))))
                {
                    throw new ArgumentException("Параметарот 'Година на извоз од' е невалиден. Внесената вредност содржи карактери/симболи:", yearOfExportFrom);
                }
                var tempYearFrom = Convert.ToInt32(yearOfExportFrom);
                if (tempYearFrom < 1800 || tempYearFrom > 5000)
                {
                    throw new ArgumentException("Параметарот 'Година на извоз од' е невалиден. Внесената вредност е надвор од дозволениот опсег.", yearOfExportFrom);
                }
            }
            if (yearOfExportTo != null)
            {
                if (yearOfExportTo.Any(y => (char.IsLetter(y) || char.IsSeparator(y) || char.IsPunctuation(y) || char.IsSymbol(y))))
                {
                    throw new ArgumentException("Параметарот 'Година на извоз до' е невалиден. Внесената вредност содржи карактери/симболи:", yearOfExportTo);
                }
                var tempYearTo = Convert.ToInt32(yearOfExportTo);
                if (tempYearTo < 1800 || tempYearTo > 5000)
                {
                    throw new ArgumentException("Параметарот 'Година на извоз до' е невалиден. Внесената вредност е надвор од дозволениот опсег.", yearOfExportTo);
                }
            }
            
            var output = new DataForExecutedExportDTO();

            double amountOfExportFromLocal = 1;
            double amountOfExportToLocal = 1;
            int monthOfExportFromLocal = 1;
            int monthOfExportToLocal = 1;
            int yearOfExportFromLocal = 1;
            int yearOfExportToLocal = 1;
            if (amountOfExportFrom != null)
                amountOfExportFromLocal = Double.Parse(amountOfExportFrom);
            if (amountOfExportTo != null)
                amountOfExportToLocal = Double.Parse(amountOfExportTo);
            if (monthOfExportFrom != null)
                monthOfExportFromLocal = Int32.Parse(monthOfExportFrom);
            if (monthOfExportTo != null)
                monthOfExportToLocal = Int32.Parse(monthOfExportTo);
            if (yearOfExportFrom != null)
                yearOfExportFromLocal = Int32.Parse(yearOfExportFrom);
            if (yearOfExportTo != null)
                yearOfExportToLocal = Int32.Parse(yearOfExportTo);

            var enviroment = ConfigurationManager.AppSettings["Enviroment"];

            if (enviroment == "Test")
            {
                //var input = new CURMDataForExecutedExport.ExecutedExportInput();
                //New corrected adapter
                var input = new CURMPodatociZaIzvozTest.ExecutedExportInput();
                input.EDB = edb;
                if (amountOfExportFrom != null)
                    input.AmountOfExportFrom = amountOfExportFromLocal;
                if (amountOfExportTo != null)
                    input.AmountOfExportTo = amountOfExportToLocal;
                if (monthOfExportFrom != null)
                    input.MonthOfExportFrom = monthOfExportFromLocal;
                if (monthOfExportTo != null)
                    input.MonthOfExportTo = monthOfExportToLocal;
                if (yearOfExportFrom != null)
                    input.YearOfExportFrom = yearOfExportFromLocal;
                if (yearOfExportTo != null)
                    input.YearOfExportTo = yearOfExportToLocal;

                var list = new List<ExecutedExportDTO>();
                try
                {
                    //var curmClient = new CURMDataForExecutedExport.DataExeExpClient();
                    //New corrected adapter
                    var curmClient = new CURMPodatociZaIzvozTest.DataExeExpClient();
                    var outputCURM = curmClient.GetDataExeExp(input);
                    for (int i = 0; i < outputCURM.Length; i++)
                    {
                        var temp = new ExecutedExportDTO()
                        {
                            EDB = outputCURM[i].EDB,
                            ExportAmount = outputCURM[i].ExportAmount,
                            ExportMonth = outputCURM[i].ExportMonth,
                            ExportYear = outputCURM[i].ExportYear
                        };
                        list.Add(temp);
                    }
                    output.ExecutedExportList = list;

                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "ERROR!!!";
                    _logger.Error(ex.Message, "CURM - GetDataForExecutedExport() - ERROR");
                    throw ex;
                }
            }
            else
            {
                //var input = new CURMDataForExecutedExport.ExecutedExportInput();
                //New corrected adapter
                var input = new CURMPodatociZaIzvoz.ExecutedExportInput();
                input.EDB = edb;
                if (amountOfExportFrom != null)
                    input.AmountOfExportFrom = amountOfExportFromLocal;
                if (amountOfExportTo != null)
                    input.AmountOfExportTo = amountOfExportToLocal;
                if (monthOfExportFrom != null)
                    input.MonthOfExportFrom = monthOfExportFromLocal;
                if (monthOfExportTo != null)
                    input.MonthOfExportTo = monthOfExportToLocal;
                if (yearOfExportFrom != null)
                    input.YearOfExportFrom = yearOfExportFromLocal;
                if (yearOfExportTo != null)
                    input.YearOfExportTo = yearOfExportToLocal;

                var list = new List<ExecutedExportDTO>();
                try
                {
                    //var curmClient = new CURMDataForExecutedExport.DataExeExpClient();
                    //New corrected adapter
                    var curmClient = new CURMPodatociZaIzvoz.DataExeExpClient();
                    var outputCURM = curmClient.GetDataExeExp(input);
                    for (int i = 0; i < outputCURM.Length; i++)
                    {
                        var temp = new ExecutedExportDTO()
                        {
                            EDB = outputCURM[i].EDB,
                            ExportAmount = outputCURM[i].ExportAmount,
                            ExportMonth = outputCURM[i].ExportMonth,
                            ExportYear = outputCURM[i].ExportYear
                        };
                        list.Add(temp);
                    }
                    output.ExecutedExportList = list;

                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "ERROR!!!";
                    _logger.Error(ex.Message, "CURM - GetDataForExecutedExport() - ERROR");
                    throw ex;
                }
                //var temp1 = new ExecutedExportDTO()
                //{
                //    EDB = "111111",
                //    ExportAmount = 9837428974,
                //    ExportMonth = 2,
                //    ExportYear = 2011
                //};
                //var temp2 = new ExecutedExportDTO()
                //{
                //    EDB = "222222",
                //    ExportAmount = 2358,
                //    ExportMonth = 3,
                //    ExportYear = 2012
                //};
                //var list1 = new List<ExecutedExportDTO>();
                //list1.Add(temp1);
                //list1.Add(temp2);
                //output.ExecutedExportList = list1;
            }
           
            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforexport = output;
            dataforexport.FillBasicPrintInfo("Податоци за извршен извоз", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintDataExport", dataforexport);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfexport = "CURM_GetDataForExecutedExport_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlexport = "CURM_GetDataForExecutedExport_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            output.FilePDFName = namepdfexport;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfexport;
            File.WriteAllBytes(pathpdf, binary);

            output.FileXMLName = namexmlexport;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforexport.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforexport);
            }
            var pathxml = path + namexmlexport;
            File.WriteAllText(pathxml, myXml.OuterXml);
            
            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност yearsexperiencexml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataForExportXML(string namexml)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexml);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexml
            };
            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност yearsexperiencepdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataForExportPDF(string namepdf)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdf);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdf
            };
            return result;
        }

        // Опис: Методот го повикува DataExeImpClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb, amountOfExportFrom, amountOfExportTo, amountOfImportTaxFrom, amountOfImportTaxTo, monthOfImportFrom, monthOfImportTo, yearOfImportFrom, yearOfImportTo
        // Излезни параметри: DataForExecutedImportDTO модел 
        [System.Web.Http.HttpPost]
        public DataForExecutedImportDTO GetDataForExecutedImport(long edb, string amountOfImportFrom = null, string amountOfImportTo = null, string amountOfImportTaxFrom = null, string amountOfImportTaxTo = null, string monthOfImportFrom = null, string monthOfImportTo = null, string yearOfImportFrom = null, string yearOfImportTo = null)
        {
            if (edb < 1000000 || edb > 10000000000000)
            {
                throw new ArgumentException("Вредноста на параметарот 'едб' не е во границите на дозволени вредности.", edb.ToString());
            }
            if (monthOfImportFrom != null)
            {
                if (monthOfImportFrom.Any(m => (char.IsLetter(m) || char.IsSeparator(m) || char.IsPunctuation(m) || char.IsSymbol(m))))
                {
                    throw new ArgumentException("Параметарот 'Месец на увоз од' е невалиден. Внесената вредност содржи карактери/симболи:", monthOfImportFrom);
                }
                var tempMonthFrom = Convert.ToInt32(monthOfImportFrom);
                if (tempMonthFrom < 1 || tempMonthFrom > 12)
                {
                    throw new ArgumentException("Параметарот 'Месец на увоз од' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12.", monthOfImportFrom);
                }
            }
            if (monthOfImportTo != null)
            {
                if (monthOfImportTo.Any(m => (char.IsLetter(m) || char.IsSeparator(m) || char.IsPunctuation(m) || char.IsSymbol(m))))
                {
                    throw new ArgumentException("Параметарот 'Месец на увоз до' е невалиден. Внесената вредност содржи карактери/симболи:", monthOfImportTo);
                }
                var tempMonthTo = Convert.ToInt32(monthOfImportTo);
                if (tempMonthTo < 1 || tempMonthTo > 12)
                {
                    throw new ArgumentException("Параметарот 'Месец на увоз до' е невалиден. Дозволени вредности за внесување се помеѓу 1 и 12.", monthOfImportTo);
                }
            }
            if (yearOfImportFrom != null)
            {
                if (yearOfImportFrom.Any(y => (char.IsLetter(y) || char.IsSeparator(y) || char.IsPunctuation(y) || char.IsSymbol(y))))
                {
                    throw new ArgumentException("Параметарот 'Година на увоз од' е невалиден. Внесената вредност содржи карактери/симболи:", yearOfImportFrom);
                }
                var tempYearFrom = Convert.ToInt32(yearOfImportFrom);
                if (tempYearFrom < 1800 || tempYearFrom > 5000)
                {
                    throw new ArgumentException("Параметарот 'Година на увоз од' е невалиден. Внесената вредност е надвор од дозволениот опсег.", yearOfImportFrom);
                }
            }
            if (yearOfImportTo != null)
            {
                if (yearOfImportTo.Any(y => (char.IsLetter(y) || char.IsSeparator(y) || char.IsPunctuation(y) || char.IsSymbol(y))))
                {
                    throw new ArgumentException("Параметарот 'Година на увоз до' е невалиден. Внесената вредност содржи карактери/симболи:", yearOfImportTo);
                }
                var tempYearTo = Convert.ToInt32(yearOfImportTo);
                if (tempYearTo < 1800 || tempYearTo > 5000)
                {
                    throw new ArgumentException("Параметарот 'Година на увоз до' е невалиден. Внесената вредност е надвор од дозволениот опсег.", yearOfImportTo);
                }
            }
            var output = new DataForExecutedImportDTO();

            double amountOfImportFromLocal = 1;
            double amountOfImportToLocal = 1;
            double amountOfImportTaxFromLocal = 1;
            double amountOfImportTaxToLocal = 1;
            int monthOfImportFromLocal = 1;
            int monthOfImportToLocal = 1;
            int yearOfImportFromLocal = 1;
            int yearOfImportToLocal = 1;
            if (amountOfImportFrom != null)
                amountOfImportFromLocal = Double.Parse(amountOfImportFrom);
            if (amountOfImportTo != null)
                amountOfImportToLocal = Double.Parse(amountOfImportTo);
            if (amountOfImportTaxFrom != null)
                amountOfImportTaxFromLocal = Double.Parse(amountOfImportTaxFrom);
            if (amountOfImportTaxTo != null)
                amountOfImportTaxToLocal = Double.Parse(amountOfImportTaxTo);
            if (monthOfImportFrom != null)
                monthOfImportFromLocal = Int32.Parse(monthOfImportFrom);
            if (monthOfImportTo != null)
                monthOfImportToLocal = Int32.Parse(monthOfImportTo);
            if (yearOfImportFrom != null)
                yearOfImportFromLocal = Int32.Parse(yearOfImportFrom);
            if (yearOfImportTo != null)
                yearOfImportToLocal = Int32.Parse(yearOfImportTo);

            var enviroment = ConfigurationManager.AppSettings["Enviroment"];

            if (enviroment == "Test")
            {
                //var input = new CURMDataForExecutedImport.ExecutedImportInput();
                //New corrected adapter
                var input = new CURMPodatociZaUvozTest.ExecutedImportInput();
                input.EDB = edb;
                if (amountOfImportFrom != null)
                    input.AmountOfImportFrom = amountOfImportFromLocal;
                if (amountOfImportTo != null)
                    input.AmountOfImportTo = amountOfImportToLocal;
                if (amountOfImportTaxFrom != null)
                    input.AmountOfImportTaxFrom = amountOfImportTaxFromLocal;
                if (amountOfImportTaxTo != null)
                    input.AmountOfImportTaxTo = amountOfImportTaxToLocal;
                if (monthOfImportFrom != null)
                    input.MonthOfImportFrom = monthOfImportFromLocal;
                if (monthOfImportTo != null)
                    input.MonthOfImportTo = monthOfImportToLocal;
                if (yearOfImportFrom != null)
                    input.YearOfImportFrom = yearOfImportFromLocal;
                if (yearOfImportTo != null)
                    input.YearOfImportTo = yearOfImportToLocal;

                var list = new List<ExecutedImportDTO>();
                try
                {
                    //var curmClient = new CURMDataForExecutedImport.DataExeImpClient();
                    //New corrected adapter
                    var curmClient = new CURMPodatociZaUvozTest.DataExeImpClient();

                    var outputCURM = curmClient.GetDataExeImp(input);
                    for (int i = 0; i < outputCURM.Length; i++)
                    {
                        var temp = new ExecutedImportDTO()
                        {
                            EDB = outputCURM[i].EDB,
                            ImportAmount = outputCURM[i].ImportAmount,
                            ImportTaxAmount = outputCURM[i].ImportTaxAmount,
                            ImportMonth = outputCURM[i].ImportMonth,
                            ImportYear = outputCURM[i].ImportYear
                        };
                        list.Add(temp);
                    }
                    output.ExecutedImportList = list;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, "CURM - GetDataForExecutedImport() - ERROR");
                    output.Message = "ERROR!!!";
                    throw ex;
                }
            }
            else
            {
                //var input = new CURMDataForExecutedImport.ExecutedImportInput();
                //New corrected adapter
                var input = new CURMPodatociZaUvoz.ExecutedImportInput();
                input.EDB = edb;
                if (amountOfImportFrom != null)
                    input.AmountOfImportFrom = amountOfImportFromLocal;
                if (amountOfImportTo != null)
                    input.AmountOfImportTo = amountOfImportToLocal;
                if (amountOfImportTaxFrom != null)
                    input.AmountOfImportTaxFrom = amountOfImportTaxFromLocal;
                if (amountOfImportTaxTo != null)
                    input.AmountOfImportTaxTo = amountOfImportTaxToLocal;
                if (monthOfImportFrom != null)
                    input.MonthOfImportFrom = monthOfImportFromLocal;
                if (monthOfImportTo != null)
                    input.MonthOfImportTo = monthOfImportToLocal;
                if (yearOfImportFrom != null)
                    input.YearOfImportFrom = yearOfImportFromLocal;
                if (yearOfImportTo != null)
                    input.YearOfImportTo = yearOfImportToLocal;

                var list = new List<ExecutedImportDTO>();
                try
                {
                    //var curmClient = new CURMDataForExecutedImport.DataExeImpClient();
                    //New corrected adapter
                    var curmClient = new CURMPodatociZaUvoz.DataExeImpClient();

                    var outputCURM = curmClient.GetDataExeImp(input);
                    for (int i = 0; i < outputCURM.Length; i++)
                    {
                        var temp = new ExecutedImportDTO()
                        {
                            EDB = outputCURM[i].EDB,
                            ImportAmount = outputCURM[i].ImportAmount,
                            ImportTaxAmount = outputCURM[i].ImportTaxAmount,
                            ImportMonth = outputCURM[i].ImportMonth,
                            ImportYear = outputCURM[i].ImportYear
                        };
                        list.Add(temp);
                    }
                    output.ExecutedImportList = list;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, "CURM - GetDataForExecutedImport() - ERROR");
                    output.Message = "ERROR!!!";
                    throw ex;
                }
                //var temp1 = new ExecutedImportDTO()
                //{
                //    EDB = "111111",
                //    ImportAmount = 9837428974,
                //    ImportTaxAmount = 123,
                //    ImportMonth = 2,
                //    ImportYear = 2011
                //};
                //var temp2 = new ExecutedImportDTO()
                //{
                //    EDB = "222222",
                //    ImportAmount = 2358,
                //    ImportTaxAmount = 321,
                //    ImportMonth = 3,
                //    ImportYear = 2012
                //};
                //var list1 = new List<ExecutedImportDTO>();
                //list1.Add(temp1);
                //list1.Add(temp2);
                //output.ExecutedImportList = list1;
            }
            
            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforeimport = output;
            dataforeimport.FillBasicPrintInfo("Податоци за извршен увоз", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintDataImport", dataforeimport);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfimport = "CURM_GetDataForExecutedImport_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlimport = "CURM_GetDataForExecutedImport_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            output.FilePDFNameImport = namepdfimport;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfimport;
            File.WriteAllBytes(pathpdf, binary);

            output.FileXMLNameImport = namexmlimport;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforeimport.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforeimport);
            }
            var pathxml = path + namexmlimport;
            File.WriteAllText(pathxml, myXml.OuterXml);
            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност yearsexperiencexml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataForImportXML(string namexmlimport)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmlimport);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmlimport
            };
            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност yearsexperiencepdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataForImportPDF(string namepdfimport)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfimport);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfimport
            };
            return result;
        }

        // Опис: Методот го повикува SingleCustDocClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност year, edbOfShippingCompany, numberOfCustomsOffice, regNumber
        // Излезни параметри: SingleCustomsDocumentDTO модел 
        [System.Web.Http.HttpPost]
        public SingleCustomsDocumentDTO GetSingleCustomsDocument(int year, long edbOfShippingCompany, int numberOfCustomsOffice, int regNumber)
        {
            if (year < 1800 || year > 5000)
            {
                throw new ArgumentException("Параметарот 'Година' е невалиден. Вредноста на параметарот 'година' е надвор од дозволениот опсег.", year.ToString());
            }
            if (edbOfShippingCompany < 1000000 || edbOfShippingCompany > 10000000000000)
            {
                throw new ArgumentException("Вредноста на параметарот 'ЕДБ на фирмата испраќач' не е во границите на дозволени вредности.", edbOfShippingCompany.ToString());
            }
            var output = new SingleCustomsDocumentDTO();
            var enviroment = ConfigurationManager.AppSettings["Enviroment"];

            if (enviroment == "Test")
            {
                try
                {
                    //var curmClient = new CURMSingleCustomsDocument.SingleCustDocClient();
                    //New corrected adapter
                    var curmClient = new CURMEdinstvenCarinskiDokumentTest.SingleCustDocClient();
                    var outputCURM = curmClient.GetSingleCustDoc(year, edbOfShippingCompany, numberOfCustomsOffice, regNumber);
                    var generalData = new SCDGeneralDataDTO()
                    {
                        ExciseStoreCode = outputCURM.GeneralData.ExciseStoreCode,
                        DeclarantCode = outputCURM.GeneralData.DeclarantCode,
                        ReferentNumber = outputCURM.GeneralData.ReferentNumber,
                        ImportExport = outputCURM.GeneralData.ImportExport,
                        DeclatarionType = outputCURM.GeneralData.DeclatarionType,
                        ProcedureType = outputCURM.GeneralData.ProcedureType,
                        SenderEDB = outputCURM.GeneralData.SenderEDB,
                        RegistrationSeries = outputCURM.GeneralData.RegistrationSeries,
                        RegistrationNumber = outputCURM.GeneralData.RegistrationNumber,
                        RegistrationDate = outputCURM.GeneralData.RegistrationDate,
                        ItemNumber = outputCURM.GeneralData.ItemNumber,
                        ImporterEDB = outputCURM.GeneralData.ImporterEDB,
                        ValueData = outputCURM.GeneralData.ValueData,
                        ExportingCountry = outputCURM.GeneralData.ExportingCountry,
                        OriginCountry = outputCURM.GeneralData.OriginCountry,
                        DestinationCountry = outputCURM.GeneralData.DestinationCountry,
                        ExportingConditionCode = outputCURM.GeneralData.ExportingConditionCode,
                        ExportingConditionPlace = outputCURM.GeneralData.ExportingConditionPlace,
                        DeliveryTermsSituationCode = outputCURM.GeneralData.DeliveryTermsSituationCode,
                        RegistrationOfVehicle = outputCURM.GeneralData.RegistrationOfVehicle,
                        NationalityOfVehicle = outputCURM.GeneralData.NationalityOfVehicle,
                        CurrencyCode = outputCURM.GeneralData.CurrencyCode,
                        TotalInvoiceAmount = outputCURM.GeneralData.TotalInvoiceAmount

                    };
                    var itemDataList = new List<SCDItemDataDTO>();
                    for (int i = 0; i < outputCURM.ItemData.Length; i++)
                    {
                        var temp = new SCDItemDataDTO()
                        {
                            TariffTagPart1 = outputCURM.ItemData[i].TariffTagPart1,
                            TariffTagPart2 = outputCURM.ItemData[i].TariffTagPart2,
                            DescriptionOfGoodsPart1 = outputCURM.ItemData[i].DescriptionOfGoodsPart1,
                            DescriptionOfGoodsPart2 = outputCURM.ItemData[i].DescriptionOfGoodsPart2,
                            DescriptionOfGoodsPart3 = outputCURM.ItemData[i].DescriptionOfGoodsPart3,
                            CountryOfOrigin = outputCURM.ItemData[i].CountryOfOrigin,
                            GrossMass = outputCURM.ItemData[i].GrossMass,
                            Preference = outputCURM.ItemData[i].Preference,
                            StatisticalValue = outputCURM.ItemData[i].StatisticalValue
                        };
                        itemDataList.Add(temp);
                    }
                    var exporterData = new SCDExporterDataDTO()
                    {
                        SenderName = outputCURM.ExporterData.SenderName,
                        SenderAddressPart1 = outputCURM.ExporterData.SenderAddressPart1,
                        SenderAddressPart2 = outputCURM.ExporterData.SenderAddressPart2,
                        SenderPlace = outputCURM.ExporterData.SenderPlace,
                        SenderPostalCode = outputCURM.ExporterData.SenderPostalCode
                    };
                    var importerData = new SCDImporterDataDTO()
                    {
                        ImporterName = outputCURM.ImporterData.ImporterName,
                        ImporterAddressPart1 = outputCURM.ImporterData.ImporterAddressPart1,
                        ImporterAddressPart2 = outputCURM.ImporterData.ImporterAddressPart2,
                        ImporterPlace = outputCURM.ImporterData.ImporterPlace,
                        ImporterPostalCode = outputCURM.ImporterData.ImporterPostalCode
                    };
                    output.GeneralData = generalData;
                    output.ItemData = itemDataList;
                    output.ExporterData = exporterData;
                    output.ImporterData = importerData;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "ERROR";
                    _logger.Error(ex.Message, "CURM ERROR");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    //var curmClient = new CURMSingleCustomsDocument.SingleCustDocClient();
                    //New corrected adapter
                    var curmClient = new CURMEdinstvenCarinskiDokument.SingleCustDocClient();
                    var outputCURM = curmClient.GetSingleCustDoc(year, edbOfShippingCompany, numberOfCustomsOffice, regNumber);
                    var generalData = new SCDGeneralDataDTO()
                    {
                        ExciseStoreCode = outputCURM.GeneralData.ExciseStoreCode,
                        DeclarantCode = outputCURM.GeneralData.DeclarantCode,
                        ReferentNumber = outputCURM.GeneralData.ReferentNumber,
                        ImportExport = outputCURM.GeneralData.ImportExport,
                        DeclatarionType = outputCURM.GeneralData.DeclatarionType,
                        ProcedureType = outputCURM.GeneralData.ProcedureType,
                        SenderEDB = outputCURM.GeneralData.SenderEDB,
                        RegistrationSeries = outputCURM.GeneralData.RegistrationSeries,
                        RegistrationNumber = outputCURM.GeneralData.RegistrationNumber,
                        RegistrationDate = outputCURM.GeneralData.RegistrationDate,
                        ItemNumber = outputCURM.GeneralData.ItemNumber,
                        ImporterEDB = outputCURM.GeneralData.ImporterEDB,
                        ValueData = outputCURM.GeneralData.ValueData,
                        ExportingCountry = outputCURM.GeneralData.ExportingCountry,
                        OriginCountry = outputCURM.GeneralData.OriginCountry,
                        DestinationCountry = outputCURM.GeneralData.DestinationCountry,
                        ExportingConditionCode = outputCURM.GeneralData.ExportingConditionCode,
                        ExportingConditionPlace = outputCURM.GeneralData.ExportingConditionPlace,
                        DeliveryTermsSituationCode = outputCURM.GeneralData.DeliveryTermsSituationCode,
                        RegistrationOfVehicle = outputCURM.GeneralData.RegistrationOfVehicle,
                        NationalityOfVehicle = outputCURM.GeneralData.NationalityOfVehicle,
                        CurrencyCode = outputCURM.GeneralData.CurrencyCode,
                        TotalInvoiceAmount = outputCURM.GeneralData.TotalInvoiceAmount

                    };
                    var itemDataList = new List<SCDItemDataDTO>();
                    for (int i = 0; i < outputCURM.ItemData.Length; i++)
                    {
                        var temp = new SCDItemDataDTO()
                        {
                            TariffTagPart1 = outputCURM.ItemData[i].TariffTagPart1,
                            TariffTagPart2 = outputCURM.ItemData[i].TariffTagPart2,
                            DescriptionOfGoodsPart1 = outputCURM.ItemData[i].DescriptionOfGoodsPart1,
                            DescriptionOfGoodsPart2 = outputCURM.ItemData[i].DescriptionOfGoodsPart2,
                            DescriptionOfGoodsPart3 = outputCURM.ItemData[i].DescriptionOfGoodsPart3,
                            CountryOfOrigin = outputCURM.ItemData[i].CountryOfOrigin,
                            GrossMass = outputCURM.ItemData[i].GrossMass,
                            Preference = outputCURM.ItemData[i].Preference,
                            StatisticalValue = outputCURM.ItemData[i].StatisticalValue
                        };
                        itemDataList.Add(temp);
                    }
                    var exporterData = new SCDExporterDataDTO()
                    {
                        SenderName = outputCURM.ExporterData.SenderName,
                        SenderAddressPart1 = outputCURM.ExporterData.SenderAddressPart1,
                        SenderAddressPart2 = outputCURM.ExporterData.SenderAddressPart2,
                        SenderPlace = outputCURM.ExporterData.SenderPlace,
                        SenderPostalCode = outputCURM.ExporterData.SenderPostalCode
                    };
                    var importerData = new SCDImporterDataDTO()
                    {
                        ImporterName = outputCURM.ImporterData.ImporterName,
                        ImporterAddressPart1 = outputCURM.ImporterData.ImporterAddressPart1,
                        ImporterAddressPart2 = outputCURM.ImporterData.ImporterAddressPart2,
                        ImporterPlace = outputCURM.ImporterData.ImporterPlace,
                        ImporterPostalCode = outputCURM.ImporterData.ImporterPostalCode
                    };
                    output.GeneralData = generalData;
                    output.ItemData = itemDataList;
                    output.ExporterData = exporterData;
                    output.ImporterData = importerData;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "ERROR";
                    _logger.Error(ex.Message, "CURM ERROR");
                    throw ex;
                }
                //var temp = new SCDItemDataDTO()
                //{
                //    TariffTagPart1 = "1",
                //    TariffTagPart2 = "2",
                //    DescriptionOfGoodsPart1 = "3",
                //    DescriptionOfGoodsPart2 = "4",
                //    DescriptionOfGoodsPart3 = "5",
                //    CountryOfOrigin = "6",
                //    GrossMass = "7",
                //    Preference = "8",
                //    StatisticalValue = "9"
                //};
                //var temp1 = new SCDItemDataDTO()
                //{
                //    TariffTagPart1 = "11",
                //    TariffTagPart2 = "22",
                //    DescriptionOfGoodsPart1 = "33",
                //    DescriptionOfGoodsPart2 = "44",
                //    DescriptionOfGoodsPart3 = "55",
                //    CountryOfOrigin = "66",
                //    GrossMass = "77",
                //    Preference = "88",
                //    StatisticalValue = "99"
                //};
                //var temp2 = new SCDItemDataDTO()
                //{
                //    TariffTagPart1 = "111",
                //    TariffTagPart2 = "222",
                //    DescriptionOfGoodsPart1 = "333",
                //    DescriptionOfGoodsPart2 = "444",
                //    DescriptionOfGoodsPart3 = "555",
                //    CountryOfOrigin = "666",
                //    GrossMass = "777",
                //    Preference = "888",
                //    StatisticalValue = "999"
                //};
                //var itemData = new List<SCDItemDataDTO>();
                //itemData.Add(temp);
                //itemData.Add(temp1);
                //itemData.Add(temp2);
                //output = new SingleCustomsDocumentDTO()
                //{
                //    GeneralData = new SCDGeneralDataDTO()
                //    {
                //        ExciseStoreCode = "a",
                //        DeclarantCode = "b",
                //        ReferentNumber = "c",
                //        ImportExport = "d",
                //        DeclatarionType = "e",
                //        SenderEDB = "f",
                //        RegistrationDate = "g",
                //        ImporterEDB = "123456",
                //        OriginCountry = "h",
                //        ExportingConditionCode = "g",
                //        NationalityOfVehicle = "h",
                //        TotalInvoiceAmount = "i"

                //    },
                //    ExporterData = new SCDExporterDataDTO()
                //    {
                //        SenderName = "aa",
                //        SenderAddressPart1 = "bb",
                //        SenderAddressPart2 = "cc",
                //        SenderPlace = "dd",
                //        SenderPostalCode = "ee"
                //    },
                //    ImporterData = new SCDImporterDataDTO()
                //    {
                //        ImporterName = "aaа",
                //        ImporterAddressPart1 = "bbb",
                //        ImporterAddressPart2 = "ccc",
                //        ImporterPlace = "ddd",
                //        ImporterPostalCode = "eee"
                //    },
                //    ItemData = itemData
                //};
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforsingledocument = output;
            dataforsingledocument.FillBasicPrintInfo("Единствен царински документ", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintSingleDocument", dataforsingledocument);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year1 = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfdocument = "CURM_GetSingleCustomsDocument_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".pdf";
            var namexmldocument = "CURM_GetSingleCustomsDocument_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".xml";
            output.FilePDFNameDocument = namepdfdocument;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfdocument;
            File.WriteAllBytes(pathpdf, binary);

            output.FileXMLNameDocument = namexmldocument;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforsingledocument.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforsingledocument);
            }
            var pathxml = path + namexmldocument;
            File.WriteAllText(pathxml, myXml.OuterXml);

            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност yearsexperiencexml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SingleDocumentXML(string namexmldocument)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmldocument);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmldocument
            };
            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност yearsexperiencepdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SingleDocumentPDF(string namepdfdocument)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfdocument);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfdocument
            };
            return result;
        }

        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };
    }
}