using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;
using System.Xml;
using System.Xml.Serialization;
using Interop.CC.Portal.API.FPIOMRabotnoIskustvo;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    [System.Web.Http.Authorize]
    public class FPIOMController : ApiController
    {
        private const string InstitutionName = "Фонд за пензиско и инвалидско осигурување";
        private List<string> _listServices;
        private ILogger _logger;

        // Опис: Конструктор на TESTMIOAController модулот 
        // Влезни параметри: модел ILogger
        public FPIOMController(ILogger logger)
        {
            _logger = logger;
        }

        // Опис: Методот го повикува FPIOMServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: DataForRetiredDTO модел 
        [System.Web.Http.HttpPost]
        public DataForRetiredDTO GetDataForRetired(string embg)
        {
            var retiredDto = new DataForRetiredDTO();
            try
            {
                if (string.IsNullOrEmpty(embg))
                {
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);
                }
                if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
                {
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ содржи карактери/симболи. ", embg);
                }
                var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
                if (embgTemp.Length != 13)
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);

                //Service with unseparated methods
                //var fpiomClient = new FPIOMInsuredRetiredAdapter.FPIOMServiceClient();
                //var retired = fpiomClient.GetDataForRetired(embg);

                //Service with separated methods and separated services depend on enviroment
                var enviroment = AppSettings.Get<string>("Enviroment");
                string result;

                if (enviroment == "Production")
                {
                    var fpiomClient = new FPIOMPodatociPenzioner.DataForRetiredClient();
                    result = fpiomClient.GetDataForRetired(embg);
                }
                else
                {
                    var fpiomClient = new FPIOMPodatociPenzionerTest.DataForRetiredClient();
                    result = fpiomClient.GetDataForRetired(embg);
                }


                var stringReader = new StringReader(result);
                var serializer = new XmlSerializer(typeof(DataForRetiredDTO));
                retiredDto = (DataForRetiredDTO)serializer.Deserialize(stringReader);
                _logger.Info("Result from adapter: " + result);

                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData,
                    new FPIOMController.EmptyController());
                var dataforRetired = retiredDto;
                dataforRetired.FillBasicPrintInfo("Податоци за пензионер", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintDataForRetired", dataforRetired);
                var binary = r.BuildPdf(controllerContext);

                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdfretired = "PIOM_GetDataForRetired_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" +
                                     month + "_" + year + ".pdf";
                var namexmlretired = "PIOM_GetDataForRetired_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" +
                                     month + "_" + year + ".xml";

                var path = WebConfigurationManager.AppSettings["PathToFile"];

                retiredDto.FilePdfNameRetired = namepdfretired;
                retiredDto.FileXMLNameRetired = namexmlretired;

                var pathpdf = path + namepdfretired;
                File.WriteAllBytes(pathpdf, binary);
                var pathxml = path + namexmlretired;
                File.WriteAllText(pathxml, result);
            }
            catch (FaultException e)
            {
                _logger.Error(e.Message, "PIOM - GetDataForRetired() - Custom Interop fault exception");
                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "PIOM - GetDataForRetired() - Exception Error");
                throw e;
            }
            return retiredDto;
        }

        // Опис: Методот го повикува FPIOMServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: DataForEmployeeDTO модел 
        [System.Web.Http.HttpPost]
        public DataForEmployeeDTO GetDataForEmployee(string embg)
        {
            var employeeDto = new DataForEmployeeDTO();
            try
            {
                if (string.IsNullOrEmpty(embg))
                {
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);
                }
                if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
                {
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ содржи карактери/симболи. ", embg);
                }
                var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
                if (embgTemp.Length != 13)
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);

                //Service with unseparated methods
                //var fpiomClient = new FPIOMInsuredRetiredAdapter.FPIOMServiceClient();
                //var ensuree = fpiomClient.GetDataForEnsurees(embg);

                //Service with separated methods and separated services depend on enviroment
                var enviroment = AppSettings.Get<string>("Enviroment");
                string result;

                if (enviroment == "Production")
                {
                    var fpiomClient = new FPIOMPodatociOsigurenik.DataForEnsurersClient();
                    result = fpiomClient.GetDataForEnsurees(embg);
                }
                else
                {
                    var fpiomClient = new FPIOMPodatociOsigurenikTest.DataForEnsurersClient();
                    result = fpiomClient.GetDataForEnsurees(embg);
                }


                _logger.Info("Result from adapter: " + result);
                var stringReader = new StringReader(result);
                var serializer = new XmlSerializer(typeof(DataForEmployeeDTO));
                employeeDto = (DataForEmployeeDTO)serializer.Deserialize(stringReader);
                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData,
                    new FPIOMController.EmptyController());
                var dataforEnsuree = employeeDto;
                dataforEnsuree.FillBasicPrintInfo("Податоци за осигуреник", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintDataForEnsuree", dataforEnsuree);
                var binary = r.BuildPdf(controllerContext);
                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdf = "PIOM_GetDataForEnsure_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month +
                              "_" + year + ".pdf";
                var namexml = "PIOM_GetDataForEnsure_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month +
                              "_" + year + ".xml";
                var path = WebConfigurationManager.AppSettings["PathToFile"];

                employeeDto.FilePdfName = namepdf;
                employeeDto.FileXMLName = namexml;

                var pathpdf = path + namepdf;
                File.WriteAllBytes(pathpdf, binary);
                var pathxml = path + namexml;
                File.WriteAllText(pathxml, result);

            }
            catch (FaultException e)
            {
                _logger.Error(e.Message, "PIOM - GetDataForEmployee() - Custom Interop fault exception");
                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "PIOM - GetDataForEmployee() - Exception Error");
                throw e;
            }
            return employeeDto;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataEnsureePdf(string namepdf)
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
        public HttpResponseMessage DataEnsureeXML(string namexml)
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
        // Влезни параметри: податочна вредност namepdfretired
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataRetiredPdf(string namepdfretired)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfretired);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfretired
            };
            return result;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexmlretired
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DataRetiredXML(string namexmlretired)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmlretired);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmlretired
            };
            return result;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност yearsexperiencexml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage YearsOfExperienceXML(string yearsexperiencexml)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + yearsexperiencexml);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = yearsexperiencexml
            };
            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност yearsexperiencepdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage YearsOfExperiencePDF(string yearsexperiencepdf)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + yearsexperiencepdf);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = yearsexperiencepdf
            };
            return result;
        }

        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };

        // Опис: Методот го повикува InsuredStatusClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: податочен тип стринг 
        [System.Web.Http.HttpPost]
        public string GetStatusForInsured(string embg)
        {
            //string status;
            string result;
            try
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
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри.:", embg);

                //var fpiomClient = new FPIOMInsuredStatus.InsuredStatusClient();
                //status = fpiomClient.GetInsuredStatus(embg);


                //Service with separated methods and separated services depend on enviroment
                var enviroment = AppSettings.Get<string>("Enviroment");

                if (enviroment == "Production")
                {
                    var fpiomClient = new FPIOMStatusOsigurenik.InsuredStatusClient();
                    result = fpiomClient.GetInsuredStatus(embg);
                }
                else
                {
                    var fpiomClient = new FPIOMStatusOsigurenikTest.InsuredStatusClient();
                    result = fpiomClient.GetInsuredStatus(embg);
                }

                _logger.Info("Result from adapter: " + result);
            }
            catch (FaultException e)
            {
                _logger.Error(e.Message, "PIOM - GetStatusForInsured() - Custom Interop fault exception");
                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "PIOM - GetStatusForInsured() - Exception Error");
                throw e;
            }
            return result;
        }

        // Опис: Методот го повикува RetiredStatusClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: податочен тип стринг 
        [System.Web.Http.HttpPost]
        public string GetStatusForRetired(string embg)
        {
            //var status = "";
            string result;
            try
            {
                if (string.IsNullOrEmpty(embg))
                {
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);
                }
                if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
                {
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ содржи карактери/симболи. ", embg);
                }
                var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
                if (embgTemp.Length != 13)
                    throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);

                //var fpiomClient = new FPIOMRetiredStatus.RetiredStatusClient();
                //status = fpiomClient.GetRetiredStatus(embg);

                //Service with separated methods and separated services depend on enviroment
                var enviroment = AppSettings.Get<string>("Enviroment");

                if (enviroment == "Production")
                {
                    var fpiomClient = new FPIOMStatusPenzioner.RetiredStatusClient();
                    result = fpiomClient.GetRetiredStatus(embg);
                }
                else
                {
                    var fpiomClient = new FPIOMStatusPenzionerTest.RetiredStatusClient();
                    result = fpiomClient.GetRetiredStatus(embg);
                }

                _logger.Info("Result from adapter: " + result);
            }
            catch (FaultException e)
            {
                _logger.Error(e.Message, "PIOM - GetStatusForRetired() - Custom Interop fault exception");
                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "PIOM - GetStatusForRetired() - Exception Error");
                throw e;
            }
            return result;
        }

        // Опис: Методот го повикува YearsOfWorkExperienceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност embg
        // Излезни параметри: YearsOfWorkExperienceDTO модел 
        [System.Web.Http.HttpPost]
        public YearsOfWorkExperienceDTO GetYearsOfWorkExperience(string embg)
        {
            if (string.IsNullOrEmpty(embg))
            {
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);
            }
            if (embg.Any(y => (char.IsLetter(y) || char.IsSeparator(y) || char.IsPunctuation(y) || char.IsSymbol(y))))
            {
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ содржи карактери/симболи. ", embg);
            }
            var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
            if (embgTemp.Length != 13)
                throw new ArgumentException("Невалиден ЕМБГ, внесениот ЕМБГ треба да содржи 13 цифри. ", embg);

            var enviroment = AppSettings.Get<string>("Enviroment");
            try
            {
                //Service with separated methods and separated services depend on enviroment
                YearsOfWorkExperienceDTO yearsOfWorkExperience = null;
                string resultDataXml;
                var sww = new StringWriter();
                if (enviroment == "Production")
                {
                    //star adapter
                    //var fpiomClient = new YearsOfWorkExperienceClient();

                    var fpiomClient = new FPIOMRabotnoIskustvo.YearsOfWorkExperienceClient();
                    YearsOfWorkExperienceOutput resultProduction = fpiomClient.GetYWExpXML(embg);
                    _logger.Info("Result from production adapter: " + resultProduction);

                    var serializer = new XmlSerializer(typeof(YearsOfWorkExperienceOutput));
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        serializer.Serialize(writer, resultProduction);
                        resultDataXml = sww.ToString();
                    }
                    // _logger.Info("resultDataXml from production adapter: " + resultDataXml);
                }
                else
                {
                    var fpiomClient = new FPIOMRabotnoIskustvoTest.YearsOfWorkExperienceClient();
                    FPIOMRabotnoIskustvoTest.YearsOfWorkExperienceOutput resultTest = fpiomClient.GetYWExpXML(embg);
                    _logger.Info("Result from test adapter: " + resultTest);

                    var serializer = new XmlSerializer(typeof(FPIOMRabotnoIskustvoTest.YearsOfWorkExperienceOutput));
                    
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        serializer.Serialize(writer, resultTest);
                        resultDataXml = sww.ToString();
                    }
                    // _logger.Info("resultDataXml from test adapter: " + resultDataXml);
                }

                
                var serializerToDto = new XmlSerializer(typeof(YearsOfWorkExperienceDTO));
                yearsOfWorkExperience = (YearsOfWorkExperienceDTO)serializerToDto.Deserialize(new StringReader(resultDataXml));
                yearsOfWorkExperience.Embg = embg;


                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
                var dataforyearsofexperience = yearsOfWorkExperience;
                dataforyearsofexperience.FillBasicPrintInfo("Години работен стаж", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintYearsOfExperience", dataforyearsofexperience);
                var binary = r.BuildPdf(controllerContext);

                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdf = "PIOM_GetYearsOfWorkExperience_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                var namexml = "PIOM_GetYearsOfWorkExperience_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                var path = WebConfigurationManager.AppSettings["PathToFile"];
                yearsOfWorkExperience.YearsOfWorkPDF = namepdf;

                var pathpdf = path + namepdf;
                File.WriteAllBytes(pathpdf, binary);

                yearsOfWorkExperience.YearsOfWorkXML = namexml;
                var myXml = new XmlDocument();
                var xNav = myXml.CreateNavigator();

                var x = new XmlSerializer(yearsOfWorkExperience.GetType());
                using (var xs = xNav.AppendChild())
                {
                    x.Serialize(xs, yearsOfWorkExperience);
                }

                var pathxml = path + namexml;
                File.WriteAllText(pathxml, myXml.OuterXml);

                return yearsOfWorkExperience;
            }
            catch (FaultException e)
            {
                _logger.Error(e.Message, "PIOM - YearsOfWorkExperience - Custom Interop fault exception");
                throw;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, "PIOM - YearsOfWorkExperience - Exception Error");
                throw e;
            }

        }
    }
}