using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Loads = Interop.CC.Models.DTO.Institutions.Loads;
using Objects = Interop.CC.Models.DTO.Institutions.Objects;
using Owner = Interop.CC.Models.DTO.Institutions.Owner;
using Parcel = Interop.CC.Models.DTO.Institutions.Parcel;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    [System.Web.Http.Authorize]
    public class AKNController: ApiController
    {
        private const string InstitutionName = "Агенција за катастар на недвижности";
        private ILogger _logger;

        // Опис: Конструктор на AKNController модулот 
        // Влезни параметри: модел ILogger
        public AKNController(ILogger logger)
        {
            _logger = logger;
        }
        // Опис: Методот го повикува AKNPListDocClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност opstina, katastarskaOpstina, brImotenList, brParcela, showEMB
        // Излезни параметри: HttpResponseMessage
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreatePropertyListPDF(string opstina, string katastarskaOpstina, bool showEMB, string brImotenList = null, string brParcela = null)
        {
           // string _brImotenList = null;
            //string _brParcela = null;
            if (String.IsNullOrEmpty(opstina))
            {
                throw new ArgumentException("Параметарот 'Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(katastarskaOpstina))
            {
                throw new ArgumentException("Параметарот 'Катастарска Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(brImotenList))
            {
                throw new ArgumentException("Параметарот 'Број на имотен лист' е задолжителен.");
            }
            var enviroment = ConfigurationManager.AppSettings["EnviromentAKN"];

            if (enviroment != null)
            {
                if (enviroment == "Production")
                {
                    //var client = new AKNPListDocProduction.AKNPListDocProductionClient();
                    //new corrected service
                    var client = new AKNImotenListDokument.AKNPListDocProductionClient();
                    //if (brImotenList == null)
                    //    brImotenList = "";
                    if (brParcela == null)
                        brParcela = "";
                    try
                    {
                        var output = client.GetPListDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEMB);
                        if (output.HasDocument)
                        {
                            var context = HttpContext.Current;
                            var contextBase = new HttpContextWrapper(context);
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Template");
                            var controllerContext = new ControllerContext(contextBase,
                                routeData,
                                new EmptyController());

                            var result = new HttpResponseMessage(HttpStatusCode.OK);
                            var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                            result.Content = new ByteArrayContent(output.Document);
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/pdf");
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "ImotenList" + date
                            };
                            return result;
                        }
                        else
                        {
                            throw new Exception(output.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        throw new Exception(e.Message);
                    }
                }
                else
                {
                    var client = new AKNImotenListDokumentTest.AKNPListDocClient();
                    if (brImotenList == null)
                        brImotenList = "";
                    if (brParcela == null)
                        brParcela = "";
                    try
                    {
                        var output = client.GetPListDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEMB);
                        if (output.HasDocument)
                        {
                            var context = HttpContext.Current;
                            var contextBase = new HttpContextWrapper(context);
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Template");
                            var controllerContext = new ControllerContext(contextBase,
                                routeData,
                                new EmptyController());

                            var result = new HttpResponseMessage(HttpStatusCode.OK);
                            var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                            result.Content = new ByteArrayContent(output.Document);
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/pdf");
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "ImotenList" + date
                            };
                            return result;
                        }
                        else
                        {
                            throw new Exception(output.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        throw new Exception(e.Message);
                    }
                }
            }
            else
            {
                _logger.Error("Greska vo konfiguraciski fajl: ", "EnviromentAKN klucot ne postoi");
                throw new Exception("Не постои клуч во конфигурацискиот фајл.");
            }
            
            
        }
        // Опис: Методот го повикува AKNCPlanDocClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност opstina, katastarskaOpstina, brImotenList, brParcela, showEMB
        // Излезни параметри: HttpResponseMessage
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreateCadastralPlanPDF(string opstina, string katastarskaOpstina, bool showEMB, string brImotenList = null, string brParcela = null)
        {
            // string _brImotenList = null;
            //string _brParcela = null;
            if (String.IsNullOrEmpty(opstina))
            {
                throw new ArgumentException("Параметарот 'Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(katastarskaOpstina))
            {
                throw new ArgumentException("Параметарот 'Катастарска Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(brParcela))
            {
                throw new ArgumentException("Параметарот 'Број на парцела' е задолжителен.");
            }
            var enviroment = ConfigurationManager.AppSettings["EnviromentAKN"];

            if (enviroment != null)
            {
                if (enviroment == "Production")
                {
                    //var client = new AKNCPlanDocProduction.AKNCPlanDocProductionClient();
                    //new corrected service
                    var client = new AKNKatastarskaParcelaDokument.AKNCPlanDocProductionClient();
                    if (brImotenList == null)
                        brImotenList = "";
                    //if (brParcela == null)
                    //    brParcela = "";
                    try
                    {
                        var output = client.GetCPlanDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEMB);
                        if (output.HasDocument)
                        {
                            var context = HttpContext.Current;
                            var contextBase = new HttpContextWrapper(context);
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Template");
                            var controllerContext = new ControllerContext(contextBase,
                                routeData,
                                new EmptyController());

                            var result = new HttpResponseMessage(HttpStatusCode.OK);
                            var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                            result.Content = new ByteArrayContent(output.Document);
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/pdf");
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "KopijaOdKatastarskiPlan" + date
                            };
                            return result;
                        }
                        else
                        {
                            throw new Exception(output.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        throw new Exception(e.Message);
                    }
                }
                else
                {
                    var client = new AKNKatastarskaParcelaDokumentTest.AKNCPlanDocClient();
                    if (brImotenList == null)
                        brImotenList = "";
                    if (brParcela == null)
                        brParcela = "";
                    try
                    {
                        var output = client.GetCPlanDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEMB);
                        if (output.HasDocument)
                        {
                            var context = HttpContext.Current;
                            var contextBase = new HttpContextWrapper(context);
                            var routeData = new RouteData();
                            routeData.Values.Add("controller", "Template");
                            var controllerContext = new ControllerContext(contextBase,
                                routeData,
                                new EmptyController());

                            var result = new HttpResponseMessage(HttpStatusCode.OK);
                            var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                            result.Content = new ByteArrayContent(output.Document);
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/pdf");
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "KopijaOdKatastarskiPlan" + date
                            };
                            return result;
                        }
                        else
                        {
                            throw new Exception(output.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        throw new Exception(e.Message);
                    }
                }
            }
            else
            {
                _logger.Error("Greska vo konfiguraciski fajl: ", "EnviromentAKN klucot ne postoi");
                throw new Exception("Не постои клуч во конфигурацискиот фајл.");
            }
        }
        // Опис: Методот го повикува AKNCPlanDocClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност opstina, katastarskaOpstina, brImotenList, brParcela, showEMB
        // Излезни параметри: HttpResponseMessage
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreateDataForIFacilitiesPDF(string opstina, string katastarskaOpstina, bool showEMB, string brImotenList = null, string brParcela = null)
        {
            // string _brImotenList = null;
            //string _brParcela = null;
            if (String.IsNullOrEmpty(opstina))
            {
                throw new ArgumentException("Параметарот 'Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(katastarskaOpstina))
            {
                throw new ArgumentException("Параметарот 'Катастарска Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(brImotenList))
            {
                throw new ArgumentException("Параметарот 'Број на имотен лист' е задолжителен.");
            }
            var enviroment = ConfigurationManager.AppSettings["EnviromentAKN"];

            if (enviroment == "Production")
            {
                //var client = new AKNDataForIFDocProduction.AKNDataForIFDocProductionClient();
                //new corrected service
                var client = new AKNPodatociZaInfraObjekti.AKNDataForIFDocProductionClient();
                if (brImotenList == null)
                    brImotenList = "";
                if (brParcela == null)
                    brParcela = "";
                try
                {
                    var output = client.GetIFDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEMB);
                    if (output.HasDocument)
                    {
                        var context = HttpContext.Current;
                        var contextBase = new HttpContextWrapper(context);
                        var routeData = new RouteData();
                        routeData.Values.Add("controller", "Template");
                        var controllerContext = new ControllerContext(contextBase,
                            routeData,
                            new EmptyController());

                        var result = new HttpResponseMessage(HttpStatusCode.OK);
                        var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                        result.Content = new ByteArrayContent(output.Document);
                        result.Content.Headers.ContentType =
                            new MediaTypeHeaderValue("application/pdf");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "PodatociZaInfrastrukturniObjekti" + date
                        };
                        return result;
                    }
                    else
                    {
                        throw new Exception(output.Message);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    throw new Exception(e.Message);
                }
            }
            else
            {
                var client = new AKNPodatociZaInfraObjektiTest.AKNDataForIFDocClient();
                if (brImotenList == null)
                    brImotenList = "";
                if (brParcela == null)
                    brParcela = "";
                try
                {
                    var output = client.GetDataForIFDoc(opstina, katastarskaOpstina, brImotenList, brParcela, showEMB);
                    if (output.HasDocument)
                    {
                        var context = HttpContext.Current;
                        var contextBase = new HttpContextWrapper(context);
                        var routeData = new RouteData();
                        routeData.Values.Add("controller", "Template");
                        var controllerContext = new ControllerContext(contextBase,
                            routeData,
                            new EmptyController());

                        var result = new HttpResponseMessage(HttpStatusCode.OK);
                        var date = DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
                        result.Content = new ByteArrayContent(output.Document);
                        result.Content.Headers.ContentType =
                            new MediaTypeHeaderValue("application/pdf");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "PodatociZaInfrastrukturniObjekti" + date
                        };
                        return result;
                    }
                    else
                    {
                        throw new Exception(output.Message);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    throw new Exception(e.Message);
                }
            }
        }
        // Опис: Методот го повикува AKNServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност username, password, municipality, cadastralMunicipality, noPropertyList
        // Излезни параметри: DataForPropertyListDTO модел 
        [System.Web.Http.HttpPost]
        public DataForPropertyListDTO GetPropertyList(string municipality, string cadastralMunicipality, string noPropertyList)
        {
            if (String.IsNullOrEmpty(municipality))
            {
                throw new ArgumentException("Параметарот 'Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(cadastralMunicipality))
            {
                throw new ArgumentException("Параметарот 'Катастарска Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(noPropertyList))
            {
                throw new ArgumentException("Параметарот 'Број на имотен лист' е задолжителен.");
            }

            ServicePointManager.ServerCertificateValidationCallback =
             ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira

            // Read from web.config
            var username = WebConfigurationManager.AppSettings["AKNusername"];
            var password = WebConfigurationManager.AppSettings["AKNpassword"];

            var enviroment = ConfigurationManager.AppSettings["EnviromentAKN"];

            if (enviroment == "Production")
            {
                #region production
                //var dzgrObject = new AKNImotenList.dzgr();

                //Test ImotenList
                var dzgrObject = new AKNImotenList.dzgr();



                try
                {
                    //Service without separated methods
                    //var aknClient = new AKNPropertyListCadastralParcelAdapter.AKNServiceClient();

                    //Service with separated methods PROD
                    //var aknClient = new AKNImotenList.PropertyListClient();
                    //dzgrObject = aknClient.GetPropertyList(username, password, municipality, cadastralMunicipality, noPropertyList);
                    //_logger.Info(dzgrObject.message);

                    //Service with separated methods PROD
                    var aknClient = new AKNImotenList.PropertyListClient();
                    dzgrObject = aknClient.GetPropertyList(username, password, municipality, cadastralMunicipality, noPropertyList);
                    _logger.Info(dzgrObject.message);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    throw e;
                }

                var loads = new List<Loads>();
                if (dzgrObject.niztov != null)
                    foreach (var tovar in dzgrObject.niztov)
                    {
                        var load = new Loads { Text = tovar.text };
                        loads.Add(load);
                    }

                var objects = new List<Objects>();
                if (dzgrObject.nizobj != null)
                    foreach (var objekt in dzgrObject.nizobj)
                    {
                        var objectItem = new Objects
                        {
                            Apartment = objekt.stan,
                            Grounds = objekt.povrsina,
                            Entry = objekt.vlez,
                            Floor = objekt.kat,
                            Location = objekt.mesto,
                            Number = objekt.broj,
                            Object = objekt.objekt,
                            Pravo = objekt.pravo,
                            Purpose = objekt.namena,
                            YearBuilt = objekt.godinagradba,
                            OsnovGradba = objekt.osnov
                        };
                        objects.Add(objectItem);
                    }

                var owners = new List<Owner>();
                if (dzgrObject.nizsop != null)
                    foreach (var sopstvenik in dzgrObject.nizsop)
                    {
                        var owner = new Owner()
                        {
                            PersonalNumber = sopstvenik.embg,
                            Name = sopstvenik.ime,
                            Location = sopstvenik.mesto,
                            Number = sopstvenik.broj,
                            Street = sopstvenik.ulica,
                            Part = sopstvenik.del
                        };
                        owners.Add(owner);
                    }

                var parcels = new List<Parcel>();
                if (dzgrObject.nizpar != null)
                    foreach (var parcela in dzgrObject.nizpar)
                    {
                        var parcel = new Parcel
                        {
                            PartNumber = parcela.broj_del,
                            Culture = parcela.kultura,
                            Grounds = parcela.povrsina,
                            Location = parcela.mesto,
                            ObjectParcel = parcela.objekt,
                            Pravo = parcela.pravo
                        };
                        parcels.Add(parcel);
                    }

                var propertyList = new DataForPropertyListDTO
                {
                    Municipality = municipality,
                    CadastralMunicipality = cadastralMunicipality,
                    PropertyList = dzgrObject.ilist,
                    LoadsList = loads,
                    ObjectsList = objects,
                    OwnersList = owners,
                    ParcelsList = parcels,
                    Date = dzgrObject.data,
                    Message = dzgrObject.message,
                };

                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
                var dataforpropertylist = propertyList;
                dataforpropertylist.FillBasicPrintInfo("Имотен лист", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintPropertyList", dataforpropertylist);
                var binary = r.BuildPdf(controllerContext);

                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdfpropertylist = "AKN_GetPropertyList_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                var namexmlpropertylist = "AKN_GetPropertyList_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                propertyList.PropertylistPDF = namepdfpropertylist;
                var path = WebConfigurationManager.AppSettings["PathToFile"];
                var pathpdf = path + namepdfpropertylist;
                File.WriteAllBytes(pathpdf, binary);

                propertyList.PropertylistXML = namexmlpropertylist;
                var myXml = new XmlDocument();
                var xNav = myXml.CreateNavigator();

                var x = new XmlSerializer(dataforpropertylist.GetType());
                using (var xs = xNav.AppendChild())
                {
                    x.Serialize(xs, dataforpropertylist);
                }
                var pathxml = path + namexmlpropertylist;
                File.WriteAllText(pathxml, myXml.OuterXml);

                return propertyList;
                #endregion
            }
            else
            {
                #region test

                var dzgrObject = new AKNImotenListTest.dzgr();

                try
                {
                    //Service with separated methods TEST
                    var aknClient = new AKNImotenListTest.PropertyListClient();
                    dzgrObject = aknClient.GetPropertyList(username, password, municipality, cadastralMunicipality, noPropertyList);
                    _logger.Info(dzgrObject.message);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    throw e;
                }

                var loads = new List<Loads>();
                if (dzgrObject.niztov != null)
                    foreach (var tovar in dzgrObject.niztov)
                    {
                        var load = new Loads { Text = tovar.text };
                        loads.Add(load);
                    }

                var objects = new List<Objects>();
                if (dzgrObject.nizobj != null)
                    foreach (var objekt in dzgrObject.nizobj)
                    {
                        var objectItem = new Objects
                        {
                            Apartment = objekt.stan,
                            Grounds = objekt.povrsina,
                            Entry = objekt.vlez,
                            Floor = objekt.kat,
                            Location = objekt.mesto,
                            Number = objekt.broj,
                            Object = objekt.objekt,
                            Pravo = objekt.pravo,
                            Purpose = objekt.namena,
                            YearBuilt = objekt.godinagradba,
                            OsnovGradba = objekt.osnov
                        };
                        objects.Add(objectItem);
                    }

                var owners = new List<Owner>();
                if (dzgrObject.nizsop != null)
                    foreach (var sopstvenik in dzgrObject.nizsop)
                    {
                        var owner = new Owner()
                        {
                            PersonalNumber = sopstvenik.embg,
                            Name = sopstvenik.ime,
                            Location = sopstvenik.mesto,
                            Number = sopstvenik.broj,
                            Street = sopstvenik.ulica,
                            Part = sopstvenik.del
                        };
                        owners.Add(owner);
                    }

                var parcels = new List<Parcel>();
                if (dzgrObject.nizpar != null)
                    foreach (var parcela in dzgrObject.nizpar)
                    {
                        var parcel = new Parcel
                        {
                            PartNumber = parcela.broj_del,
                            Culture = parcela.kultura,
                            Grounds = parcela.povrsina,
                            Location = parcela.mesto,
                            ObjectParcel = parcela.objekt,
                            Pravo = parcela.pravo
                        };
                        parcels.Add(parcel);
                    }

                var propertyList = new DataForPropertyListDTO
                {
                    Municipality = municipality,
                    CadastralMunicipality = cadastralMunicipality,
                    PropertyList = dzgrObject.ilist,
                    LoadsList = loads,
                    ObjectsList = objects,
                    OwnersList = owners,
                    ParcelsList = parcels,
                    Date = dzgrObject.data,
                    Message = dzgrObject.message,
                };

                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
                var dataforpropertylist = propertyList;
                dataforpropertylist.FillBasicPrintInfo("Имотен лист", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintPropertyList", dataforpropertylist);
                var binary = r.BuildPdf(controllerContext);

                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdfpropertylist = "AKN_GetPropertyList_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                var namexmlpropertylist = "AKN_GetPropertyList_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                propertyList.PropertylistPDF = namepdfpropertylist;
                var path = WebConfigurationManager.AppSettings["PathToFile"];
                var pathpdf = path + namepdfpropertylist;
                File.WriteAllBytes(pathpdf, binary);

                propertyList.PropertylistXML = namexmlpropertylist;
                var myXml = new XmlDocument();
                var xNav = myXml.CreateNavigator();

                var x = new XmlSerializer(dataforpropertylist.GetType());
                using (var xs = xNav.AppendChild())
                {
                    x.Serialize(xs, dataforpropertylist);
                }
                var pathxml = path + namexmlpropertylist;
                File.WriteAllText(pathxml, myXml.OuterXml);

                return propertyList;

                #endregion
            }
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност propertylistxml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage PropertyListXML(string propertylistxml)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + propertylistxml);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = propertylistxml
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност propertylistpdf
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage PropertyListPDF(string propertylistpdf)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + propertylistpdf);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = propertylistpdf
            };

            return result;
        }

        // Опис: Методот го повикува AKNServiceClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност username, password, municipality, cadastralMunicipality, noPropertyList
        // Излезни параметри: CadastralParcelDTO модел 
        [System.Web.Http.HttpPost]
        public CadastralParcelDTO GetCadastralParcel(string municipality, string cadastralMunicipality, string noCadastralParcel)
        {
            if (String.IsNullOrEmpty(municipality))
            {
                throw new ArgumentException("Параметарот 'Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(cadastralMunicipality))
            {
                throw new ArgumentException("Параметарот 'Катастарска Општина' е задолжителен.");
            }
            if (String.IsNullOrEmpty(noCadastralParcel))
            {
                throw new ArgumentException("Параметарот 'Број на Катастарска парцела' е задолжителен.");
            }

            ServicePointManager.ServerCertificateValidationCallback =
            ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
           
            // Read from web.config
            var username = WebConfigurationManager.AppSettings["AKNusername"];
            var password = WebConfigurationManager.AppSettings["AKNpassword"];

            var enviroment = ConfigurationManager.AppSettings["EnviromentAKN"];

            if (enviroment == "Production")
            {
                #region production

                //Prod Kat. parcela
                //var cadastralParcel = new AKNKatastarskaParcela.ATRparceli();

                //Test Kat. parcela
                var cadastralParcel = new AKNKatastarskaParcela.ATRparceli();

                try
                {
                    //Service without separated methods
                    //var aknClient = new AKNPropertyListCadastralParcelAdapter.AKNServiceClient();

                    //Service with separated methods PROD
                    //var aknClient = new AKNKatastarskaParcela.CadastrialParcelClient();
                    //cadastralParcel = aknClient.GetCadastrialParcel(username, password, municipality, cadastralMunicipality, noCadastralParcel);
                    //_logger.Info(cadastralParcel.message);

                    //Service with separated methods PROD
                    var aknClient = new AKNKatastarskaParcela.CadastrialParcelClient();
                    cadastralParcel = aknClient.GetCParcel(username, password, municipality, cadastralMunicipality, noCadastralParcel);
                    _logger.Info(cadastralParcel.message);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    throw e;
                }

                var attributes = new List<ParcelAttributes>();
                foreach (var parcel in cadastralParcel.nizpar)
                {
                    var attribute = new ParcelAttributes()
                    {
                        Area = parcel.povrsina,
                        Municipality = municipality,
                        Location = parcel.mesto,
                        Culture = parcel.kultura,
                        Object = parcel.objekt,
                        CadastralMunicipality = cadastralMunicipality,
                        PartNumber = parcel.broj_del,
                        PropertyList = parcel.ilist,
                        Pravo = parcel.pravo
                    };
                    attributes.Add(attribute);
                }

                var cadastralParcelDto = new CadastralParcelDTO
                {
                    AttributesList = attributes,
                    Message = cadastralParcel.message
                };

                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
                var dataforkatastar = cadastralParcelDto;
                dataforkatastar.FillBasicPrintInfo("Катастарска парцела", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintCadastralParcel", dataforkatastar);
                var binary = r.BuildPdf(controllerContext);

                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdfkatastar = "AKN_GetCadastralParcel_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                var namexmlkatastar = "AKN_GetCadastralParcel_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                cadastralParcelDto.CadastralParcelPDF = namepdfkatastar;
                var path = WebConfigurationManager.AppSettings["PathToFile"];
                var pathpdf = path + namepdfkatastar;
                File.WriteAllBytes(pathpdf, binary);


                cadastralParcelDto.CadastralParcelXML = namexmlkatastar;
                var myXml = new XmlDocument();
                var xNav = myXml.CreateNavigator();

                var x = new XmlSerializer(cadastralParcelDto.GetType());
                using (var xs = xNav.AppendChild())
                {
                    x.Serialize(xs, cadastralParcelDto);
                }
                var pathxml = path + namexmlkatastar;
                File.WriteAllText(pathxml, myXml.OuterXml);

                return cadastralParcelDto;
                #endregion
            }
            else
            {
                #region test

                //Prod Kat. parcela
                //var cadastralParcel = new AKNKatastarskaParcela.ATRparceli();

                //Test Kat. parcela
                var cadastralParcel = new AKNKatastarskaParcelaTest.ATRparceli();

                try
                {
                    //Service without separated methods
                    //var aknClient = new AKNPropertyListCadastralParcelAdapter.AKNServiceClient();

                    //Service with separated methods PROD
                    //var aknClient = new AKNKatastarskaParcela.CadastrialParcelClient();
                    //cadastralParcel = aknClient.GetCadastrialParcel(username, password, municipality, cadastralMunicipality, noCadastralParcel);
                    //_logger.Info(cadastralParcel.message);

                    //Service with separated methods TEST
                    var aknClient = new AKNKatastarskaParcelaTest.CadastrialParcelClient();
                    cadastralParcel = aknClient.GetCParcel(username, password, municipality, cadastralMunicipality, noCadastralParcel);
                    _logger.Info(cadastralParcel.message);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    throw e;
                }

                var attributes = new List<ParcelAttributes>();
                foreach (var parcel in cadastralParcel.nizpar)
                {
                    var attribute = new ParcelAttributes()
                    {
                        Area = parcel.povrsina,
                        Municipality = municipality,
                        Location = parcel.mesto,
                        Culture = parcel.kultura,
                        Object = parcel.objekt,
                        CadastralMunicipality = cadastralMunicipality,
                        PartNumber = parcel.broj_del,
                        PropertyList = parcel.ilist,
                        Pravo = parcel.pravo
                    };
                    attributes.Add(attribute);
                }

                var cadastralParcelDto = new CadastralParcelDTO
                {
                    AttributesList = attributes,
                    Message = cadastralParcel.message
                };

                var context = HttpContext.Current;
                var contextBase = new HttpContextWrapper(context);
                var routeData = new RouteData();
                routeData.Values.Add("controller", "Template");
                var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
                var dataforkatastar = cadastralParcelDto;
                dataforkatastar.FillBasicPrintInfo("Катастарска парцела", InstitutionName);
                var r = new Rotativa.ViewAsPdf("PrintCadastralParcel", dataforkatastar);
                var binary = r.BuildPdf(controllerContext);

                var date = DateTime.Now.Day;
                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var minutes = DateTime.Now.Minute;
                var secods = DateTime.Now.Second;
                var namepdfkatastar = "AKN_GetCadastralParcel_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
                var namexmlkatastar = "AKN_GetCadastralParcel_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
                cadastralParcelDto.CadastralParcelPDF = namepdfkatastar;
                var path = WebConfigurationManager.AppSettings["PathToFile"];
                var pathpdf = path + namepdfkatastar;
                File.WriteAllBytes(pathpdf, binary);


                cadastralParcelDto.CadastralParcelXML = namexmlkatastar;
                var myXml = new XmlDocument();
                var xNav = myXml.CreateNavigator();

                var x = new XmlSerializer(cadastralParcelDto.GetType());
                using (var xs = xNav.AppendChild())
                {
                    x.Serialize(xs, cadastralParcelDto);
                }
                var pathxml = path + namexmlkatastar;
                File.WriteAllText(pathxml, myXml.OuterXml);

                return cadastralParcelDto;
                #endregion
            }

            
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност katastarxml
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage KatastarXML(string katastarxml)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + katastarxml);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = katastarxml
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност katastarpdf
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage KatastarPDF(string katastarpdf)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + katastarpdf);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = katastarpdf
            };

            return result;
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<MunicipalityDTO> GetMunicipalities()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira

            try
            {
                //var aknClient = new AKNMunicipalityService.AKNMunicipalityClient();

                var aknClient = new AKNOpshtini.AKNMunicipalityClient();
                var municipalityList = aknClient.GetMunicipalities();
 
                return municipalityList.Select(x=>new MunicipalityDTO
                {
                    Name = x.Name,
                    Value = x.Value
                });
                
                //_logger.Info(cadastralParcel.messageField);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw e;
            }
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<MunicipalityDTO> GetCadastralMunicipalities(int municipalityValue)
        {
            ServicePointManager.ServerCertificateValidationCallback =
               ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira

            try
            {
                //var aknClient = new AKNMunicipalityService.AKNMunicipalityClient();
                var aknClient = new AKNOpshtini.AKNMunicipalityClient();

                var caadstralMunicipalityList = aknClient.GetCMunicipalities(municipalityValue.ToString());

                return caadstralMunicipalityList.Select(x=>new MunicipalityDTO
                {
                    Name = x.Name,
                    Value = x.Value
                });
               
                //_logger.Info(cadastralParcel.messageField);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw e;
            }
        }
        public class EmptyController : ControllerBase
        {
            protected override void ExecuteCore()
            {
            }
        };
    }

}