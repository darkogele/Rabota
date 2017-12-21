using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;
using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Interop.CC.Portal.API.MONBigDataService;
using Interop.CC.Portal.API.MONDataForRegularStudent;
using Interop.CC.Portal.API.MONStatusForRegularStudent;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Configuration;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    //[System.Web.Http.Authorize]
    public class MONController : ApiController
    {
        private const string InstitutionName = "Министерство за Образование и Наука";
        private ILogger _logger;

        // Опис: Конструктор на MONController модулот 
        // Влезни параметри: модел ILogger
        public MONController(ILogger logger)
        {
            _logger = logger;
        }
        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };

        // Опис: Методот го повикува DRegStudentClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: DataForRegularStudentDTO модел 
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        public DataForRegularStudentDTO GetDataForRegularStudent(string embg)
        {
            if (string.IsNullOrEmpty(embg))
            {
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);
            }
            if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ содржи карактери/симболи:", embg);
            }
            var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
            if (embgTemp.Length != 13)
                throw new ArgumentException("Невалиден ЕМБГ:", embg);
            _logger.Info("start GetDataForRegularStudent");
            //var monClient = new DRegStudentClient();

            var studentDto = new DataForRegularStudentDTO();
            var student = "";
            try
            {
                _logger.Info("MONClient");
                var production = ConfigurationManager.AppSettings["Enviroment"];
                if (production == "Production")
                {
                    //New corrected adapter
                    var monClient = new MONPotvrdaZaRedovenUcenik.DRegStudentClient();
                    using (OperationContextScope scope = new OperationContextScope(monClient.InnerChannel))
                    {
                        var httpRequestProperty = new HttpRequestMessageProperty();
                        httpRequestProperty.Headers[HttpRequestHeader.Authorization] = "Basic " +
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(monClient.ClientCredentials.UserName.UserName + ":" +
                        monClient.ClientCredentials.UserName.Password));
                        httpRequestProperty.Headers.Add("NekakovKod", "kodot");
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                        student = monClient.GetStuD(embg);
                        //dzgrObject = MONClient.GetPropertyList(username, password, municipality, cadastralMunicipality, noPropertyList);
                        //PaymentResponse = AKNClient.Payment(PaymentRequest);
                    }
                }
                else
                {
                    //New corrected adapter
                    var monClient = new MONPotvrdaZaRedovenUcenikTest.DRegStudentClient();
                    using (OperationContextScope scope = new OperationContextScope(monClient.InnerChannel))
                    {
                        var httpRequestProperty = new HttpRequestMessageProperty();
                        httpRequestProperty.Headers[HttpRequestHeader.Authorization] = "Basic " +
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(monClient.ClientCredentials.UserName.UserName + ":" +
                        monClient.ClientCredentials.UserName.Password));
                        httpRequestProperty.Headers.Add("NekakovKod", "kodot");
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                        student = monClient.GetStuD(embg);
                        //dzgrObject = MONClient.GetPropertyList(username, password, municipality, cadastralMunicipality, noPropertyList);
                        //PaymentResponse = AKNClient.Payment(PaymentRequest);
                    }
                }

                //var student = MONClient.GetStuD(embg);
                if (student != null)
                {
                    _logger.Info(student);
                    var stringReader = new StringReader(student);
                    var serializer = new XmlSerializer(typeof(DataForRegularStudentDTO));
                    try
                    {
                        studentDto = (DataForRegularStudentDTO)serializer.Deserialize(stringReader);

                        var context = HttpContext.Current;
                        var contextBase = new HttpContextWrapper(context);
                        var routeData = new RouteData();
                        routeData.Values.Add("controller", "Template");
                        var controllerContext = new ControllerContext(contextBase, routeData, new MONController.EmptyController());
                        studentDto.FillBasicPrintInfo("Податоци за редовен ученик", InstitutionName);
                        var r = new Rotativa.ViewAsPdf("PrintDataForRegularStudent", studentDto);
                        var binary = r.BuildPdf(controllerContext);
                        var date = DateTime.Now.Day;
                        var month = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var minutes = DateTime.Now.Minute;
                        var secods = DateTime.Now.Second;
                        var namepdf = "MON_GetDataForStudent_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                        var namexml = "MON_GetDataForStudent_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                        var path = WebConfigurationManager.AppSettings["PathToFile"];

                        studentDto.FilePdfName = namepdf;
                        studentDto.FileXMLName = namexml;

                        var pathpdf = path + namepdf;
                        File.WriteAllBytes(pathpdf, binary);
                        var pathxml = path + namexml;
                        File.WriteAllText(pathxml, student);
                    }
                    catch
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(student);
                        var mes = xmlDoc.GetElementsByTagName("message")[0].InnerText;
                        studentDto.Message = mes;
                    }
                }
            }
            //catch (FaultException<MONPotvrdaZaRedovenUcenikTest.InteropFault> ex)
            //{
            //    _logger.Error(ex.Message, "MON - GetDataForRegularStudent() - Custom fault error.");
            //    throw ex;
            //}
            catch (FaultException ex)
            {
                _logger.Error(ex.Message, "MON - GetDataForRegularStudent() - Fault exception error.");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, "MON - GetDataForRegularStudent() - Exception error.");
                throw ex;
            }
            return studentDto;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage BigData(string typeDoc, string responseSize)
        {
            //Vtora verzija, vrakjame byte[] fileBytes
            var client = new BigDataClient();
            try
            {
                var timeCameInApi = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt");
                var output = client.GetLargeDoc(typeDoc, responseSize);

                _logger.Info("Came on API in " + timeCameInApi + " , recesived response on API in " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "APIHaveResult");
                if (output.HasDocument)
                {
                    var context = HttpContext.Current;
                    var contextBase = new HttpContextWrapper(context);
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Template");

                    var result = new HttpResponseMessage(HttpStatusCode.OK);
                    var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                    result.Content = new ByteArrayContent(output.Document);
                   
                    if (typeDoc == "doc")
                    {
                        result.Content.Headers.Add("Content-Encoding", "UTF-8");
                        result.Content.Headers.Add("Charset", "UTF-8");
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "BigDocumentDoc" + date
                        };
                    }
                    else if (typeDoc == "pdf")
                    {
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "BigDocumentPdf" + date
                        };
                    }

                    _logger.Info("Rezultat se prakja na UI vo " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt") + " casot!", "ResultToUI");
                    return result;
                }
                throw new Exception(output.Message);
            }
            catch (FaultException ex)
            {
                _logger.Info("dosol vo fault exception.");
                _logger.Error("Error koj se vrakja od fault exception e: ", ex);
                throw;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataForRegularStudentPdf(string namepdf)
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

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataForRegularStudentXml(string namexml)
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

        // Опис: Методот го повикува SRegStudentClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: податочен тип string  
        [System.Web.Http.HttpPost]
        public string GetStatusForStudent(string embg)
        {
            if (string.IsNullOrEmpty(embg))
            {
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);
            }
            if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ содржи карактери/симболи:", embg);
            }
            var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
            if (embgTemp.Length != 13)
                throw new ArgumentException("Невалиден ЕМБГ:", embg);
            _logger.Info("start GetStatusForStudent");
            //var monClient = new SRegStudentClient();

            var status = "";
            try
            {
                var production = ConfigurationManager.AppSettings["Enviroment"];
                if (production == "Production")
                {
                    //New corrected adapter
                    var monClient = new MONStatusNaUcenik.SRegStudentClient();
                    _logger.Info("MONClient GetStatusForStudent");
                    status = monClient.GetStuS(embg);
                    _logger.Info(status);
                }
                else
                {
                    //New corrected adapter
                    var monClient = new MONStatusNaUcenikTest.SRegStudentClient();
                    _logger.Info("MONClient GetStatusForStudent");
                    status = monClient.GetStuS(embg);
                    _logger.Info(status);
                }
            }
            //catch (FaultException<MONStatusNaUcenikTest.InteropFault> ex)
            //{
            //    _logger.Error(ex.Message, "MON - GetStatusForStudent() - Custom fault error");
            //    throw ex;
            //}
            catch (FaultException ex)
            {
                _logger.Error(ex.Message, "MON - GetStatusForStudent() - Fault exception error");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, "MON - GetStatusForStudent() - Exception error");
                throw ex;
            }
            return status;
        }
    }
}