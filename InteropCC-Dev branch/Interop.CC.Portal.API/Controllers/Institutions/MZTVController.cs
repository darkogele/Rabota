using System.Web.Services.Protocols;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using Interop.CC.Portal.API.MzTVOdobrenieGradbaDozvolaBezAdapter;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    [System.Web.Http.Authorize]
    public class MZTVController : ApiController
    {
        private const string InstitutionName = "Министерство за Транспорт и Врски";
         private ILogger _logger;

         // Опис: Конструктор на MZTVController модулот 
         // Влезни параметри: модел ILogger
         public MZTVController(ILogger logger)
         {
            _logger = logger;
         }
        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };

        // Опис: Методот го повикува MzTVAdapterClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност archiveNumber, constructionTypeId, municipalityId, sendDate
        // Излезни параметри: DataForConstructionPermitDTO модел 
        [System.Web.Http.HttpPost]
         public DataForConstructionPermitDTO GetDataForConstructionPermit(string archiveNumber,  string constructionTypeId, string municipalityId,string sendDate = null, string getDocuments = null )
         {
             ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
             int k;
             if(!int.TryParse(constructionTypeId, out k))
                 throw new ArgumentException("Погрешен идентификационен број на тип на градба:", constructionTypeId);
             if (!int.TryParse(municipalityId, out k))
                 throw new ArgumentException("Погрешен идентификационен број на општина:", municipalityId);
             if ((!string.IsNullOrEmpty(getDocuments) && getDocuments != "y") && (!string.IsNullOrEmpty(getDocuments) && getDocuments != "n")) 
                 throw new ArgumentException("Погрешенo внесен параметар Преземи документ(y/n):", getDocuments);
             var output = new DataForConstructionPermitDTO();
             try
             {
                 var MZTVClient = new MZTVConstructionPermitAdapter.MzTVAdapterClient();
                 _logger.Info("GetConstructionPermitData" + archiveNumber + "/" + constructionTypeId + "/" + municipalityId + "/" + sendDate);
                 var outputMZTV = MZTVClient.ConsPerm(archiveNumber, constructionTypeId, municipalityId, sendDate, getDocuments);
                 _logger.Info("outputMZTV");
                 
                 List<MunicipalitiesViewModel> municipalities = new List<MunicipalitiesViewModel>();
                 List<string> investors = new List<string>();
                 List<DocumentsViewModel> documents = new List<DocumentsViewModel>();

                 if (outputMZTV != null)
                 {
                     if (outputMZTV.Investors.Count != 0)
                     {
                         foreach (string invest in outputMZTV.Investors)
                             investors.Add(invest);
                     }

                     if (outputMZTV.Municipalities.Length != 0)
                     {
                         for (int i = 0; i < outputMZTV.Municipalities.Length; i++)
                         {
                             if (outputMZTV.Municipalities[i].CadastreMunicipalities.Length != 0)
                             {
                                 List<CadastreMunicipalitiesViewModel> cadas = new List<CadastreMunicipalitiesViewModel>();
                                 for (int j = 0; j < outputMZTV.Municipalities[i].CadastreMunicipalities.Length; j++)
                                 {
                                     CadastreMunicipalitiesViewModel cadM = new CadastreMunicipalitiesViewModel
                                     {
                                         Ko = outputMZTV.Municipalities[i].CadastreMunicipalities[j].Ko,
                                         Kp = outputMZTV.Municipalities[i].CadastreMunicipalities[j].Kp
                                     };
                                     cadas.Add(cadM);
                                 }

                                 MunicipalitiesViewModel munM = new MunicipalitiesViewModel
                                 {
                                     CadastreMunicipalities = cadas,
                                     MunicipalityName = outputMZTV.Municipalities[i].MunicipalityName
                                 };
                                 municipalities.Add(munM);
                             }
                         }
                     }
                     if (outputMZTV.Documents.Length != 0)
                     {
                         for (int i = 0; i < outputMZTV.Documents.Length; i++)
                         {
                             DocumentsViewModel doc = new DocumentsViewModel
                             {
                                 ContentBytes = outputMZTV.Documents[i].ContentBytes,
                                 FileName = outputMZTV.Documents[i].FileName
                             };
                             documents.Add(doc);
                         }
                     }

                     output.Documents = documents;
                     output.Investors = investors;
                     output.Municipalities = municipalities;
                     output.ArchiveDate = outputMZTV.ArchiveDate.ToString();
                     output.ConstructionAddress = outputMZTV.ConstructionAddress;
                     output.ConstructionDescription = outputMZTV.ConstructionDescription;
                     output.ConstructionTypeName = outputMZTV.ConstructionTypeName;
                     output.EffectDate = outputMZTV.EffectDate.ToString();
                     output.SendDate = outputMZTV.SendDate.ToString();
                     output.Status = outputMZTV.Status;
                 }
                 _logger.Info("Povikot kon servisot e uspesen");

                 var context = HttpContext.Current;
                 var contextBase = new HttpContextWrapper(context);
                 var routeData = new RouteData();
                 routeData.Values.Add("controller", "Template");
                 var controllerContext = new ControllerContext(contextBase, routeData, new MZTVController.EmptyController());
                 var dataForConstructionPermit = output;
                 dataForConstructionPermit.FillBasicPrintInfo("Одобрение за градба", InstitutionName);
                 var r = new Rotativa.ViewAsPdf("PrintDataForConstructionPermit", dataForConstructionPermit);
                 var binary = r.BuildPdf(controllerContext);

                 var date = DateTime.Now.Day;
                 var month = DateTime.Now.Month;
                 var year = DateTime.Now.Year;
                 var hour = DateTime.Now.Hour;
                 var minutes = DateTime.Now.Minute;
                 var secods = DateTime.Now.Second;
                 var namepdf = "MZTV_GetDataForConstructionPermit_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                 var namexml = "MZTV_GetDataForConstructionPermit_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                 var path = WebConfigurationManager.AppSettings["PathToFile"];
                 output.ConstructionPermitPDF = namepdf;

                 var pathpdf = path + namepdf;
                 File.WriteAllBytes(pathpdf, binary);


                 output.ConstructionPermitXML = namexml;
                 XmlDocument myXml = new XmlDocument();
                 XPathNavigator xNav = myXml.CreateNavigator();

                 XmlSerializer x = new XmlSerializer(output.GetType());
                 using (var xs = xNav.AppendChild())
                 {
                     x.Serialize(xs, output);
                 }

                 var pathxml = path + namexml;
                 File.WriteAllText(pathxml, myXml.OuterXml);
             }
             catch (Exception ex)
             {
                 _logger.Error(ex.Message, "MzTV ERROR");
                 throw ex;
             }
             return output;
         }

        [System.Web.Http.HttpPost]
        public string GetDataForConstructionPermit2(string archiveNumber, string constructionTypeId,
            string municipalityId, string sendDate = null, string getDocuments = null)
        {
            try
            {
                var client = new InteropWebService();
                client.SoapVersion = SoapProtocolVersion.Soap12;

                string archNumber = archiveNumber;

                DateTime sDate = new DateTime();
                if (sendDate != null)
                    sDate = DateTime.Parse(sendDate);

                int consTypreId = Int32.Parse(constructionTypeId);

                int munId = Int32.Parse(municipalityId);

                bool getDoc = false;
                if (getDocuments != null && getDocuments == "y")
                    getDoc = true;

                var result = client.GetRequestDetails(new InteropInputViewModel
                {
                    ArchiveNumber = archNumber,
                    ConstructionTypeId = consTypreId,
                    MunicipalityId = munId,
                    SendDate = sDate,
                    GetDocuments = getDoc
                });
                _logger.Info("Result Status: ", result.Status);
                return result.Status;
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, "MzTV ERROR");
                return exception.Message;
            }
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdf
        // Излезни параметри: HttpResponseMessage модел 
         [System.Web.Http.HttpGet]
         public HttpResponseMessage DataForConstructionPermitPdf(string namepdf)
         {
             var path = WebConfigurationManager.AppSettings["PathToFile"];
             var content = File.ReadAllBytes(path + namepdf);
             HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
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
         public HttpResponseMessage DataForConstructionPermitXML(string namexml)
         {
             var path = WebConfigurationManager.AppSettings["PathToFile"];
             var content = File.ReadAllBytes(path + namexml);

             HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
             result.Content = new ByteArrayContent(content);
             result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

             result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
             {
                 FileName = namexml
             };

             return result;
         }
    }
}