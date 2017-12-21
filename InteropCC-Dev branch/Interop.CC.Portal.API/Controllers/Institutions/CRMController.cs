using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Interop.CC.CrossCutting;
using System.Security.Cryptography;
using Interop.CC.Models.Exceptions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    [System.Web.Http.Authorize]
    public class CRMController : ApiController
    {
        private const string InstitutionName = "Централен Регистар на Република Македонија";
        private ILogger _logger;

        // Опис: Конструктор на CRMController модулот 
        // Влезни параметри: модел ILogger
        public CRMController(ILogger logger)
        {
            _logger = logger;
        }

        // Опис: Методот се користи за вчитување на патеката до соодветниот сертификат
        // Влезни параметри: /
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> GetCertPath()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new PhotoMultipartFormDataStreamProvider(AppSettings.Get<string>("UploadCertPath"));
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            //var cert = new X509Certificate2();
            try
            {
                //cert = new X509Certificate2(uploadedFileInfo.FullName);
             
                //var publicKey = cert.GetPublicKey();
               
                return Request.CreateResponse(HttpStatusCode.OK, uploadedFileInfo.FullName);
            }
            catch (CryptographicException ex)
            {

                throw new InvalidCertificate(ex.Message);
            }

        }

       
        // Опис: Методот се користи за конвертирање на XML документот во string 
        // Влезни параметри: XmlDocument xmlDoc, X509Certificate2 cert
        // Излезни параметри: податочен тип string
        public string SignXml(XmlDocument xmlDoc, X509Certificate2 cert)
        {
            var rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (rsaKey == null)
                throw new ArgumentException("Key");

            var signedXml = new SignedXml(xmlDoc);
            var xmlSignature = signedXml.Signature;
            signedXml.SigningKey = rsaKey;
            var reference = new Reference();
            reference.Uri = "";
            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            xmlSignature.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            return xmlDoc.InnerXml;
        }

        // Опис: Методот го повикува CRMServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: signed XML vo vid na string
        // Излезни параметри: TekovnaSostojbaDTO модел
        [System.Web.Http.HttpPost]
        public TekovnaSostojbaDTO GetTekovnaSostojba(string edb)
        {
            if (string.IsNullOrEmpty(edb))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесете вредност за параметарот ЕДБ. ", edb);
            }
            if (edb.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесениот ЕДБ содржи карактери/симболи:", edb);
            }
            if (edb.Length < 20)
            {
                long tempEdb = Convert.ToInt64(edb);
                if (tempEdb < 1000000 || tempEdb > 10000000000000)
                {
                    throw new ArgumentException("Вредноста на параметарот 'едб' не е во границите на дозволени вредности.", edb);
                }
            }
            else
            {
                throw new ArgumentException("Должината на параметарот 'едб' не е соодветна.", edb);
            }

            var outputDTO = new TekovnaSostojbaDTO();
            string output = "";
            
            var info = new List<CVLEInfo>();
            var units = new List<CVUnits>();
            var actors = new List<CVActors>();
            var owners = new List<CVOwners>();
            var activities = new List<CVActivities>();
            var membership = new List<CVMembership>();
            var founding = new List<CVFounding>();
            var court = new List<CVLECourt>();
            try 
            {
                var production = AppSettings.Get<string>("Enviroment");
                if (production == "Production")
                {
                    var certUser = AppSettings.Get<string>("UploadCertUser");
                    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly);

                    var certificate = store.Certificates
                         .Find(X509FindType.FindBySubjectName, certUser, false)
                         .OfType<X509Certificate2>()
                         .First();
                    var crmClient = new CRMTekovnaSostojba.CRMServiceClient();

                    //var xml = CreateCRM_XML(edb);

                    var xml = Helpers.CRRMServicesTemplates.CreateCRM_XML(edb,AppSettings.Get<string>("TS_ProductName_Production"));
                    var param = SignXml(xml, certificate);
                   
                    output = crmClient.GetTekovnaSostojba(param);
                }
                else if (production == "Test")
                {
                    //Old adapter
                    //var crmClient = new CRMTekovnaSostojbaTest.CRMServiceTestClient();

                    //New corrected adapter
                    var crmClient = new CRMTekovnaSostojbaTest.CRMServiceClient();

                    var certPath = AppSettings.Get<string>("UploadCertPFX");
                    var certPass = AppSettings.Get<string>("UploadCertPass");
                    var certificate = new X509Certificate2(certPath, certPass);

                    //var xml = CreateCRM_XMLTest(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRM_XMLTest(edb);
                    var param = SignXml(xml, certificate);
                    output = crmClient.GetTekovnaSostojba(param);
                }

               // var CRMClient = new CRMTekovnaSostojba.CRMServiceClient();

               // var certPath = AppSettings.Get<string>("UploadCertPFX");
               // var certPass = AppSettings.Get<string>("UploadCertPass");
               // X509Certificate2 certificate = new X509Certificate2(certPath, certPass);
                //var xml = CreateCRM_XML(edb);
                //var param = SignXml(xml, certificate);
                //output = CRMClient.GetTekovnaSostojba(param);
                 //output = CRMClient.GetTekovnaSostojba("6646123", "D:\\UserCU.pfx", "CR!tst");
                _logger.Info("Povikot kon servisot e uspesen");
            }
            catch (FaultException ex)
            {
                _logger.Info("dosol vo fault exception.");
                _logger.Error("Error koj se vrakja od fault exception e: ", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "CRM - GetTekovnaSostojba() - ERROR");
                throw ex;
            }
           
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output);
            var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
            var atrr = "";
            if (crmResponse[0].Attributes["Message"] != null)
            {
                atrr = crmResponse[0].Attributes["Message"].Value;
                throw new Exception(atrr);
            }
            else
            {
                var Items = xmlDoc.GetElementsByTagName("CrmResultItems");
                if (Items[0].HasChildNodes)
                {
                    for (int i = 0; i < Items[0].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[0].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVLEInfo));
                        var temp = serializer.Deserialize(stringReader) as CVLEInfo;
                        info.Add(temp);
                    }
                    outputDTO.Message = "OK";
                }
                if (Items[1].HasChildNodes)
                {
                    for (int i = 0; i < Items[1].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[1].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVUnits));
                        var temp = serializer.Deserialize(stringReader) as CVUnits;
                        units.Add(temp);
                    }
                }
                if (Items[2].HasChildNodes)
                {
                    for (int i = 0; i < Items[2].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[2].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActors));
                        var temp = serializer.Deserialize(stringReader) as CVActors;
                        actors.Add(temp);
                    }
                }
                if (Items[3].HasChildNodes)
                {
                    for (int i = 0; i < Items[3].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[3].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVOwners));
                        var temp = serializer.Deserialize(stringReader) as CVOwners;
                        owners.Add(temp);
                    }
                }
                if (Items[4].HasChildNodes)
                {
                    for (int i = 0; i < Items[4].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[4].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActivities));
                        var temp = serializer.Deserialize(stringReader) as CVActivities;
                        activities.Add(temp);
                    }
                }
                if (Items[5].HasChildNodes)
                {
                    for (int i = 0; i < Items[5].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[5].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVMembership));
                        var temp = serializer.Deserialize(stringReader) as CVMembership;
                        membership.Add(temp);
                    }
                }

                if (Items[6].HasChildNodes)
                {
                    for (int i = 0; i < Items[6].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[6].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVFounding));
                        var temp = serializer.Deserialize(stringReader) as CVFounding;
                        founding.Add(temp);
                    }
                }
                if (Items[7].HasChildNodes)
                {
                    for (int i = 0; i < Items[7].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[7].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVLECourt));
                        var temp = serializer.Deserialize(stringReader) as CVLECourt;
                        court.Add(temp);
                    }
                }
                outputDTO.Info = info;
                outputDTO.Units = units;
                outputDTO.Actors = actors;
                outputDTO.Owners = owners;
                outputDTO.Activities = activities;
                outputDTO.Membership = membership;
                outputDTO.Founding = founding;
                outputDTO.Court = court;
            }
            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var datafortekovnasostojba = outputDTO;
            datafortekovnasostojba.FillBasicPrintInfo("Тековна состојба", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintTekovnaSostojba", datafortekovnasostojba);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlsostojba = "AKN_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            outputDTO.TekovnaSostojbaPDF = namepdfsostojba;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfsostojba;
            File.WriteAllBytes(pathpdf, binary);


            outputDTO.TekovnaSostojbaXML = namexmlsostojba;
            var pathxml = path + namexmlsostojba;
            //XmlDocument myXml = new XmlDocument();
            File.WriteAllText(pathxml, output);
            
            //XPathNavigator xNav = myXml.CreateNavigator();

            //XmlSerializer x = new XmlSerializer(outputDTO.GetType());
            //using (var xs = xNav.AppendChild())
            //{
            //    x.Serialize(xs, outputDTO);
            //}
            //var pathxml = path + namexmlsostojba;
            //File.WriteAllText(pathxml, myXml.OuterXml);

            return outputDTO;
        }

        [System.Web.Http.HttpPost]
        public TekovnaSostojbaAKNDTO GetTekovnaSostojbaAKN(string edb)
        {
            var outputDTO = new TekovnaSostojbaAKNDTO();
            string output = "";

            var info = new List<CVLEInfo>();
            var units = new List<CVUnits>();
            var actors = new List<CVActors>();
            var owners = new List<CVOwners>();
            var activities = new List<CVActivities>();
            var membership = new List<CVMembership>();
            var founding = new List<CVFounding>();
            try
            {
                var certUser = AppSettings.Get<string>("CertUserAKNSystemToSystem");
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2 certificate = store.Certificates.Find(X509FindType.FindBySubjectName, certUser, false).OfType<X509Certificate2>().First();

                var enviroment = AppSettings.Get<string>("EnviromentTekovnaSostojbaAKN");
                if (enviroment == "Production")
                {
                    //var xml = CreateCRMAKN_XML(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMAKN_XML(edb);
                    var param = SignXml(xml, certificate);
                    //var client = new CRM_TS_AKNClient();
                    //new corrected service
                    var client = new CRMTekovnaSostojbaAKN.CRM_TS_AKNClient();
                    output = client.Get_TS_AKN(param);
                }
                else if (enviroment == "Test")
                {
                    //var xml = CreateCRMAKN_XMLTest(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMAKN_XMLTest(edb);
                    var param = SignXml(xml, certificate);

                    //Old adapter
                    //var client = new Test_TS_AKNClient();
                    //new corrected adapter
                    var client = new CRMTekovnaSostojbaAKNTest.CRM_TS_AKNClient();

                    output = client.Get_TS_AKN(param);
                }
                _logger.Info("Povikot kon servisot e uspesen");
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "CRM ERROR on GetTekovnaSostojbaAKN");
                throw exception;
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output);
            var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
            var atrr = "";
            if (crmResponse[0].Attributes["Message"] != null)
            {
                atrr = crmResponse[0].Attributes["Message"].Value;
                throw new Exception(atrr);
            }
            else
            {
                var items = xmlDoc.GetElementsByTagName("CrmResultItems");
                if (items[0].HasChildNodes)
                {
                    for (int i = 0; i < items[0].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[0].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVLEInfo));
                        var temp = serializer.Deserialize(stringReader) as CVLEInfo;
                        info.Add(temp);
                    }
                    outputDTO.Message = "OK";
                }
                if (items[1].HasChildNodes)
                {
                    for (int i = 0; i < items[1].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[1].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVUnits));
                        var temp = serializer.Deserialize(stringReader) as CVUnits;
                        units.Add(temp);
                    }
                }
                if (items[2].HasChildNodes)
                {
                    for (int i = 0; i < items[2].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[2].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActors));
                        var temp = serializer.Deserialize(stringReader) as CVActors;
                        actors.Add(temp);
                    }
                }
                if (items[3].HasChildNodes)
                {
                    for (int i = 0; i < items[3].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[3].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVOwners));
                        var temp = serializer.Deserialize(stringReader) as CVOwners;
                        owners.Add(temp);
                    }
                }
                if (items[4].HasChildNodes)
                {
                    for (int i = 0; i < items[4].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[4].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActivities));
                        var temp = serializer.Deserialize(stringReader) as CVActivities;
                        activities.Add(temp);
                    }
                }
                if (items[5].HasChildNodes)
                {
                    for (int i = 0; i < items[5].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[5].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVMembership));
                        var temp = serializer.Deserialize(stringReader) as CVMembership;
                        membership.Add(temp);
                    }
                }

                if (items[6].HasChildNodes)
                {
                    for (int i = 0; i < items[6].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(items[6].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVFounding));
                        var temp = serializer.Deserialize(stringReader) as CVFounding;
                        founding.Add(temp);
                    }
                }
               
                outputDTO.Info = info;
                outputDTO.Units = units;
                outputDTO.Actors = actors;
                outputDTO.Owners = owners;
                outputDTO.Activities = activities;
                outputDTO.Membership = membership;
                outputDTO.Founding = founding;
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var datafortekovnasostojba = outputDTO;
            datafortekovnasostojba.FillBasicPrintInfo("Тековна состојба АКН", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintTekovnaSostojbaAKN", datafortekovnasostojba);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            outputDTO.TekovnaSostojbaPDF = namepdfsostojba;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfsostojba;
            File.WriteAllBytes(pathpdf, binary);


            outputDTO.TekovnaSostojbaXML = namexmlsostojba;
            var pathxml = path + namexmlsostojba;
            File.WriteAllText(pathxml, output);

            return outputDTO;
        }

        // Опис: Методот го повикува CRMServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: signed XML vo vid na string
        // Излезни параметри: TekovnaSostojbaUJPDTO модел
        [System.Web.Http.HttpPost]
        public TekovnaSostojbaUJPDTO GetTekovnaSostojbaUJP(string edb)
        {
            if (string.IsNullOrEmpty(edb))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесете вредност за параметарот ЕДБ. ", edb);
            }
            if (edb.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесениот ЕДБ содржи карактери/симболи:", edb);
            }
            if (edb.Length < 20)
            {
                long tempEdb = Convert.ToInt64(edb);
                if (tempEdb < 1000000 || tempEdb > 10000000000000)
                {
                    throw new ArgumentException("Вредноста на параметарот 'едб' не е во границите на дозволени вредности.", edb);
                }
            }
            else
            {
                throw new ArgumentException("Должината на параметарот 'едб' не е соодветна.", edb);
            }

            var outputDTO = new TekovnaSostojbaUJPDTO();
            string output = "";

            var info = new List<CVLEInfoUJP>();
            var units = new List<CVUnitsUJP>();
            var actors = new List<CVActorsUJP>();
            var owners = new List<CVOwnersUJP>();
            var activities = new List<CVActivitiesUJP>();
            var membership = new List<CVMembershipUJP>();
            var founding = new List<CVFoundingUJP>();
            var court = new List<CVCourtProc>();
            try
            {
                var certUser = AppSettings.Get<string>("UploadCertUser");
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                var certificate = store.Certificates
                     .Find(X509FindType.FindBySubjectName, certUser, false)
                     .OfType<X509Certificate2>()
                     .First();




               // var certPath = AppSettings.Get<string>("UploadCertPFXUJP");
                //var certPass = AppSettings.Get<string>("UploadCertPassUJP");
                var production = AppSettings.Get<string>("Enviroment");
                //X509Certificate2 certificate = new X509Certificate2(certPath, certPass);
               
                if (production == "Production")
                {
                    //var xml = CreateCRMUJP_XML(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMUJP_XML(edb);
                    var param = SignXml(xml, certificate);
                    //var crmClient = new CRM_TS_UJP.CRM_TS_UJPClient();
                    //new corrected service
                    var crmClient = new CRMTekovnaSostojbaUJP.CRM_TS_UJPClient();
                    output = crmClient.Get_TS_UJP(param);
                }
                else if (production == "Test")
                {
                    //var xml = CreateCRMUJP_XMLTest(edb);
                    //var param = SignXml(xml, certificate);
                    //var CRMClient = new CRMTekovnaSostojba.CRMServiceClient();
                    //output = CRMClient.GetTekovnaSostojba(param);

                    //var xml = CreateCRMUJP_XMLTest(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMUJP_XMLTest(edb);
                    var param = SignXml(xml, certificate);

                    //Old adapter
                    //var crmClient = new Test_TS_UJPClient();
                    //New corrected adapter
                    var crmClient = new CRMTekovnaSostojbaUJPTest.CRM_TS_UJPClient();

                    output = crmClient.Get_TS_UJP(param);
                }
               
                _logger.Info("Povikot kon servisot e uspesen");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "CRM - GetTekovnaSostojbaUJP() - ERROR");
                throw ex;
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output);
            var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
            var atrr = "";
            if (crmResponse[0].Attributes["Message"] != null)
            {
                atrr = crmResponse[0].Attributes["Message"].Value;
                throw new Exception(atrr);
            }
            else
            {
                var Items = xmlDoc.GetElementsByTagName("CrmResultItems");
                if (Items[0].HasChildNodes)
                {
                    for (int i = 0; i < Items[0].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[0].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVLEInfoUJP));
                        var temp = serializer.Deserialize(stringReader) as CVLEInfoUJP;
                        info.Add(temp);
                    }
                    outputDTO.Message = "OK";
                }
                if (Items[1].HasChildNodes)
                {
                    for (int i = 0; i < Items[1].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[1].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVUnitsUJP));
                        var temp = serializer.Deserialize(stringReader) as CVUnitsUJP;
                        units.Add(temp);
                    }
                }
                if (Items[2].HasChildNodes)
                {
                    for (int i = 0; i < Items[2].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[2].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActorsUJP));
                        var temp = serializer.Deserialize(stringReader) as CVActorsUJP;
                        actors.Add(temp);
                    }
                }
                if (Items[3].HasChildNodes)
                {
                    for (int i = 0; i < Items[3].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[3].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVOwnersUJP));
                        var temp = serializer.Deserialize(stringReader) as CVOwnersUJP;
                        owners.Add(temp);
                    }
                }
                if (Items[4].HasChildNodes)
                {
                    for (int i = 0; i < Items[4].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[4].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActivitiesUJP));
                        var temp = serializer.Deserialize(stringReader) as CVActivitiesUJP;
                        activities.Add(temp);
                    }
                }
                if (Items[5].HasChildNodes)
                {
                    for (int i = 0; i < Items[5].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[5].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVMembershipUJP));
                        var temp = serializer.Deserialize(stringReader) as CVMembershipUJP;
                        membership.Add(temp);
                    }
                }

                if (Items[6].HasChildNodes)
                {
                    for (int i = 0; i < Items[6].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[6].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVFoundingUJP));
                        var temp = serializer.Deserialize(stringReader) as CVFoundingUJP;
                        founding.Add(temp);
                    }
                }
                if (Items[7].HasChildNodes)
                {
                    for (int i = 0; i < Items[7].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[7].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVCourtProc));
                        var temp = serializer.Deserialize(stringReader) as CVCourtProc;
                        court.Add(temp);
                    }
                }
                outputDTO.Info = info;
                outputDTO.Units = units;
                outputDTO.Actors = actors;
                outputDTO.Owners = owners;
                outputDTO.Activities = activities;
                outputDTO.Membership = membership;
                outputDTO.Founding = founding;
                outputDTO.Court = court;
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var datafortekovnasostojba = outputDTO;
            datafortekovnasostojba.FillBasicPrintInfo("Тековна состојба УЈП", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintTekovnaSostojbaUJP", datafortekovnasostojba);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            outputDTO.TekovnaSostojbaPDF = namepdfsostojba;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfsostojba;
            File.WriteAllBytes(pathpdf, binary);


            outputDTO.TekovnaSostojbaXML = namexmlsostojba;
            var pathxml = path + namexmlsostojba;
            //  XmlDocument myXml = new XmlDocument();
            File.WriteAllText(pathxml, output);

            //XPathNavigator xNav = myXml.CreateNavigator();

            //XmlSerializer x = new XmlSerializer(outputDTO.GetType());
            //using (var xs = xNav.AppendChild())
            //{
            //    x.Serialize(xs, outputDTO);
            //}
            //var pathxml = path + namexmlsostojba;
            //File.WriteAllText(pathxml, myXml.OuterXml);

            return outputDTO;
        }
        // Опис: Методот го повикува CRMServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: signed XML vo vid na string
        // Излезни параметри: TekovnaSostojbaUJPDTO модел
        [System.Web.Http.HttpPost]
        public TekovnaSostojbaCURMDTO GetTekovnaSostojbaCURM(string edb)
        {
            if (string.IsNullOrEmpty(edb))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесете вредност за параметарот ЕДБ. ", edb);
            }
            if (edb.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесениот ЕДБ содржи карактери/симболи:", edb);
            }
            if (edb.Length < 20)
            {
                long tempEdb = Convert.ToInt64(edb);
                if (tempEdb < 1000000 || tempEdb > 10000000000000)
                {
                    throw new ArgumentException("Вредноста на параметарот 'едб' не е во границите на дозволени вредности.", edb);
                }
            }
            else
            {
                throw new ArgumentException("Должината на параметарот 'едб' не е соодветна.", edb);
            }

            var outputDTO = new TekovnaSostojbaCURMDTO();
            string output = "";

            var info = new List<CVLEInfoCURM>();
            var actors = new List<CVActorsCURM>();
            var owners = new List<CVOwnersCURM>();
            var founding = new List<CVFoundingCURM>();
            try
            {
                var certUser = AppSettings.Get<string>("UploadCertUserCURM");
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                var certificate = store.Certificates
                     .Find(X509FindType.FindBySubjectName, certUser, false)
                     .OfType<X509Certificate2>()
                     .First();

                // var certPath = AppSettings.Get<string>("UploadCertPFXUJP");
                //var certPass = AppSettings.Get<string>("UploadCertPassUJP");
                var production = AppSettings.Get<string>("EnviromentCURM");
                //X509Certificate2 certificate = new X509Certificate2(certPath, certPass);

                if (production == "Production")
                {
                    //var xml = CreateCRMCURM_XML(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMCURM_XML(edb);
                    var param = SignXml(xml, certificate);
                    //var crmClient = new CRM_TS_CURM.CRM_TS_CURMClient();
                    //new corrected service
                    var crmClient = new CRMTekovnaSostojbaCURM.CRM_TS_CURMClient();
                    output = crmClient.Get_TS_CURM(param);
                }
                else if (production == "Test")
                {
                    //var xml = CreateCRMCURM_XMLTest(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMCURM_XMLTest(edb);
                    var param = SignXml(xml, certificate);

                    //Old adapter
                    //var crmClient = new Test_TS_CURMClient();//new CRMTekovnaSostojba.CRMServiceClient();
                    //New corrected adapter
                    var crmClient = new CRMTekovnaSostojbaCURMTest.CRM_TS_CURMClient();

                    output = crmClient.Get_TS_CURM(param);
                }

                _logger.Info("Povikot kon servisot e uspesen");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "CRM - GetTekovnaSostojbaCURM() - ERROR");
                throw ex;
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output);
            //Za CURM greska vrakja vo elementot InfoMessage i toj ke treba da se proveruva
            var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
            var atrr = "";
            if (crmResponse[0].Attributes["Message"] != null)
            {
                atrr = crmResponse[0].Attributes["Message"].Value;
                throw new Exception(atrr);
            }
            else
            {
                var Items = xmlDoc.GetElementsByTagName("CrmResultItems");
                if (Items[0].HasChildNodes)
                {
                    for (int i = 0; i < Items[0].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[0].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVLEInfoCURM));
                        var temp = serializer.Deserialize(stringReader) as CVLEInfoCURM;
                        info.Add(temp);
                    }
                    outputDTO.Message = "OK";
                }
                if (Items[1].HasChildNodes)
                {
                    for (int i = 0; i < Items[1].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[2].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVActorsCURM));
                        var temp = serializer.Deserialize(stringReader) as CVActorsCURM;
                        actors.Add(temp);
                    }
                }
                if (Items[2].HasChildNodes)
                {
                    for (int i = 0; i < Items[2].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[3].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVOwnersCURM));
                        var temp = serializer.Deserialize(stringReader) as CVOwnersCURM;
                        owners.Add(temp);
                    }
                }
               
               

                if (Items[3].HasChildNodes)
                {
                    for (int i = 0; i < Items[3].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[6].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CVFoundingCURM));
                        var temp = serializer.Deserialize(stringReader) as CVFoundingCURM;
                        founding.Add(temp);
                    }
                }
                
                outputDTO.Info = info;
                outputDTO.Actors = actors;
                outputDTO.Owners = owners;
                outputDTO.Founding = founding;
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var datafortekovnasostojba = outputDTO;
            datafortekovnasostojba.FillBasicPrintInfo("Тековна состојба ЦУРМ", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintTekovnaSostojbaCURM", datafortekovnasostojba);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            outputDTO.TekovnaSostojbaPDF = namepdfsostojba;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfsostojba;
            File.WriteAllBytes(pathpdf, binary);


            outputDTO.TekovnaSostojbaXML = namexmlsostojba;
            var pathxml = path + namexmlsostojba;
            //  XmlDocument myXml = new XmlDocument();
            File.WriteAllText(pathxml, output);

            //XPathNavigator xNav = myXml.CreateNavigator();

            //XmlSerializer x = new XmlSerializer(outputDTO.GetType());
            //using (var xs = xNav.AppendChild())
            //{
            //    x.Serialize(xs, outputDTO);
            //}
            //var pathxml = path + namexmlsostojba;
            //File.WriteAllText(pathxml, myXml.OuterXml);

            return outputDTO;
        }

        // Опис: Методот го повикува CRMServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: signed XML vo vid na string
        // Излезни параметри: TekovnaSostojbaUJPDTO модел
        [System.Web.Http.HttpPost]
        public TekovnaSostojbaCURMProducDTO GetTekovnaSostojbaCURMProduc(string edb)
        {
            if (string.IsNullOrEmpty(edb))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесете вредност за параметарот ЕДБ. ", edb);
            }
            if (edb.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                throw new ArgumentException("Погрешен ЕДБ, внесениот ЕДБ содржи карактери/симболи:", edb);
            }
            if (edb.Length < 20)
            {
                long tempEdb = Convert.ToInt64(edb);
                if (tempEdb < 1000000 || tempEdb > 10000000000000)
                {
                    throw new ArgumentException("Вредноста на параметарот 'едб' не е во границите на дозволени вредности.", edb);
                }
            }
            else
            {
                throw new ArgumentException("Должината на параметарот 'едб' не е соодветна.", edb);
            }
            
            string output = "";
           
            try
            {
                var certUser = AppSettings.Get<string>("UploadCertUserCURM");
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                var certificate = store.Certificates
                     .Find(X509FindType.FindBySubjectName, certUser, false)
                     .OfType<X509Certificate2>()
                     .First();

                // var certPath = AppSettings.Get<string>("UploadCertPFXUJP");
                //var certPass = AppSettings.Get<string>("UploadCertPassUJP");
                var production = AppSettings.Get<string>("EnviromentCURM");
                //X509Certificate2 certificate = new X509Certificate2(certPath, certPass);

                if (production == "Production")
                {
                    //var xml = CreateCRMCURM_XML(edb);
                    var xml = Helpers.CRRMServicesTemplates.CreateCRMCURM_XML(edb);
                    var param = SignXml(xml, certificate);
                    //var crmClient = new CRM_TS_CURM.CRM_TS_CURMClient();
                    //new corrected service
                    var crmClient = new CRMTekovnaSostojbaCURM.CRM_TS_CURMClient();
                    output = crmClient.Get_TS_CURM(param);
                }
                else if (production == "Test")
                {
                    //var xml = CreateCRMCURM_XMLTest(edb);
                    //var param = SignXml(xml, certificate);
                    //var CRMClient = new CRMTekovnaSostojba.CRMServiceClient();
                    //output = CRMClient.GetTekovnaSostojba(param);

                    //var xml = CreateCRMCURM_XMLTest(edb);

                    var xml = Helpers.CRRMServicesTemplates.CreateCRMCURM_XMLTest(edb);
                    var param = SignXml(xml, certificate);
                    //r crmClient = new Test_TS_CURMClient();
                    //new corrected adapter
                    var crmClient = new CRMTekovnaSostojbaCURMTest.CRM_TS_CURMClient();
                    output = crmClient.Get_TS_CURM(param);
                }

                _logger.Info("Povikot kon servisot e uspesen");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "CRM - GetTekovnaSostojbaCURMProduc() - ERROR");
                throw ex;
            }

            var xmlDoc = new XmlDocument();
            var outputDTO = new TekovnaSostojbaCURMProducDTO();
            xmlDoc.LoadXml(output);
            _logger.Info("xmlDoc e: " + xmlDoc.InnerXml);
            var crmResponse = xmlDoc.GetElementsByTagName("CrmResponse");
            var atrr = "";

            var info = new List<CU11>();
            var owners = new List<CU12>();
            var actors = new List<CU13>();

            if (crmResponse[0].Attributes["Message"] != null)
            {
                atrr = crmResponse[0].Attributes["Message"].Value;
                throw new Exception(atrr);
            }
            else
            {
                var Items = xmlDoc.GetElementsByTagName("CrmResultItems");
                if (Items[0].HasChildNodes)
                {
                    for (int i = 0; i < Items[0].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[0].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CU11));
                        var temp = serializer.Deserialize(stringReader) as CU11;
                        info.Add(temp);
                    }
                    outputDTO.Message = "OK";
                }
                if (Items[1].HasChildNodes)
                {
                    for (int i = 0; i < Items[1].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[1].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CU12));
                        var temp = serializer.Deserialize(stringReader) as CU12;
                        owners.Add(temp);
                    }
                }
                if (Items[2].HasChildNodes)
                {
                    for (int i = 0; i < Items[2].ChildNodes.Count; i++)
                    {
                        var stringReader = new StringReader(Items[2].ChildNodes[i].OuterXml);
                        var serializer = new XmlSerializer(typeof(CU13));
                        var temp = serializer.Deserialize(stringReader) as CU13;
                        actors.Add(temp);
                    }
                }

                outputDTO.Info = info;
                outputDTO.Actors = actors;
                outputDTO.Owners = owners;
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var datafortekovnasostojba = outputDTO;
            datafortekovnasostojba.FillBasicPrintInfo("Тековна состојба ЦУРМ", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintTekovnaSostojbaCURMProduc", datafortekovnasostojba);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlsostojba = "CRM_GetTekovnaSostojba_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            outputDTO.TekovnaSostojbaPDF = namepdfsostojba;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfsostojba;
            File.WriteAllBytes(pathpdf, binary);


            outputDTO.TekovnaSostojbaXML = namexmlsostojba;
            var pathxml = path + namexmlsostojba;
            //  XmlDocument myXml = new XmlDocument();
            File.WriteAllText(pathxml, output);

            //XPathNavigator xNav = myXml.CreateNavigator();

            //XmlSerializer x = new XmlSerializer(outputDTO.GetType());
            //using (var xs = xNav.AppendChild())
            //{
            //    x.Serialize(xs, outputDTO);
            //}
            //var pathxml = path + namexmlsostojba;
            //File.WriteAllText(pathxml, myXml.OuterXml);

            return outputDTO;
        }

        [System.Web.Http.HttpPost]
        public ListaNaPromeniDTO GetListaNaPromeni(string date)
        {
            var outputDTO = new ListaNaPromeniDTO();

            byte listType = 0;
            //var client = new CRMListaNaPromeni.ListOfChangesCUClient();
            //var test = client.GetListOfChangesCU(date);

            //New corrected adapter
            var client = new CRMListaNaPromeniTest.ListOfChangesCUClient();
            outputDTO.ListType = listType;
            outputDTO.DateFrom = date;
            
            outputDTO.InfoMessage = "";
            return outputDTO;
        }
        [System.Web.Http.HttpPost]
        public List<string> GetListaNaSubjekti(string date)
        {
            byte listType = 1;
            //var client = new CRMListaNaPromeni.ListOfChangesCUClient();
            //var test = client.GetListOfChangesCU(option);

            //New corrected adapter
            var client = new CRMListaPosebenInteresTest.ListOfSubjectsCUClient();
            var list = new List<string>();
            list.Add(date);
            return list;
        }
        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност tekovnasostojbaxml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TekovnaSostojbaXML(string tekovnasostojbaxml)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + tekovnasostojbaxml);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = tekovnasostojbaxml
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност tekovnasostojbapdf
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TekovnaSostojbaPDF(string tekovnasostojbapdf)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + tekovnasostojbapdf);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = tekovnasostojbapdf
            };

            return result;

        }
    }
}