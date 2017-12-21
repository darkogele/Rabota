using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
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
using System.Web;
using System.Web.Http;
using CyrilicInterop;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    //[System.Web.Http.Authorize]
    public class UJPController : ApiController
    {
        private const string InstitutionName = "Управа за јавни приходи";
        private ILogger _logger;

        // Опис: Конструктор на UJPController модулот 
        // Влезни параметри: модел ILogger
        public UJPController(ILogger logger)
        {
            _logger = logger;
        }

        // Опис: Методот го повикува EDB_EMBClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb_emb
        // Излезни параметри: EDB_EMB_OutputDTO модел 
        [System.Web.Http.HttpPost]
        public EDB_EMB_OutputDTO GetEdb(string edb_emb)
        {
            //We must send EMB to get data for service EDB
            var output = new EDB_EMB_OutputDTO();
            var edbEmbLst = new List<Edb_EmbDTO>();
            var enviroment = ConfigurationManager.AppSettings["Enviroment"];

            if (enviroment == "Test")
            {
                try
                {
                    //Service without separated methods
                    //var UJPClient = new UJPEDB_EMB.EDB_EMBClient();

                    //Service with separated methods PROD
                    //var ujpClient = new UJPEdinstvenDanocenBroj.DataForEDBClient();
                    //Service with separated methods TEST
                    var ujpClient = new UJPEdinstvenDanocenBrojTest.DataForEDBClient();

                    var resultService = ujpClient.GetEDB(edb_emb);
                    for (int i = 0; i < resultService.Length; i++)
                    {
                        var temp = new Edb_EmbDTO()
                        {
                            Edb = resultService[i].Edb,
                            Naziv = resultService[i].Naziv.ToCyrilic().ToUpper(),
                            Emb = resultService[i].Emb,
                            Ziro = resultService[i].Ziro.ToCyrilic(),
                            BankaZiro = resultService[i].BankaZiro.ToCyrilic(),
                            DatumPrijava = resultService[i].DatumPrijava.ToCyrilic(),
                            PrijavaVid = resultService[i].PrijavaVid.ToCyrilic(),
                            PrijavaStatus = resultService[i].PrijavaStatus.ToCyrilic(),
                            DejnostNace = resultService[i].DejnostNace.ToCyrilic(),
                            SedisteNaziv = resultService[i].SedisteNaziv.ToCyrilic().ToUpper(),
                            SedisteBroj = resultService[i].SedisteBroj.ToCyrilic(),
                            SedisteUlica = resultService[i].SedisteUlica.ToCyrilic(),
                            SedisteTelefon = resultService[i].SedisteTelefon.ToCyrilic(),
                            SedisteTelefax = resultService[i].SedisteTelefax.ToCyrilic()
                        };
                        edbEmbLst.Add(temp);
                    }
                    output.Edb_EmbList = edbEmbLst;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    //Service without separated methods
                    //var UJPClient = new UJPEDB_EMB.EDB_EMBClient();

                    //Service with separated methods PROD
                    //var ujpClient = new UJPEdinstvenDanocenBroj.DataForEDBClient();
                    //Service with separated methods PROD
                    var ujpClient = new UJPEdinstvenDanocenBroj.DataForEDBClient();

                    var resultService = ujpClient.GetEDB(edb_emb);
                    for (int i = 0; i < resultService.Length; i++)
                    {
                        var temp = new Edb_EmbDTO()
                        {
                            Edb = resultService[i].Edb,
                            Naziv = resultService[i].Naziv.ToCyrilic().ToUpper(),
                            Emb = resultService[i].Emb,
                            Ziro = resultService[i].Ziro.ToCyrilic(),
                            BankaZiro = resultService[i].BankaZiro.ToCyrilic(),
                            DatumPrijava = resultService[i].DatumPrijava.ToCyrilic(),
                            PrijavaVid = resultService[i].PrijavaVid.ToCyrilic(),
                            PrijavaStatus = resultService[i].PrijavaStatus.ToCyrilic(),
                            DejnostNace = resultService[i].DejnostNace.ToCyrilic(),
                            SedisteNaziv = resultService[i].SedisteNaziv.ToCyrilic().ToUpper(),
                            SedisteBroj = resultService[i].SedisteBroj.ToCyrilic(),
                            SedisteUlica = resultService[i].SedisteUlica.ToCyrilic(),
                            SedisteTelefon = resultService[i].SedisteTelefon.ToCyrilic(),
                            SedisteTelefax = resultService[i].SedisteTelefax.ToCyrilic()
                        };
                        edbEmbLst.Add(temp);
                    }
                    output.Edb_EmbList = edbEmbLst;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
                //for (int i = 0; i < 5; i++)
                //{
                //    var temp = new Edb_EmbDTO()
                //    {
                //        Edb = "sdfds" + i,
                //        Naziv = "asd" + i,
                //        Emb = "aaa" + i,
                //        Ziro = "asdasd" + i,
                //        BankaZiro = "123" + i,
                //        DatumPrijava = "234" + i,
                //        PrijavaVid = "345" + i,
                //        PrijavaStatus = "456" + i,
                //        DejnostNace = "qwe" + i,
                //        SedisteNaziv = "ert" + i,
                //        SedisteBroj = "rty" + i,
                //        SedisteUlica = "yui" + i,
                //        SedisteTelefon = "iop" + i,
                //        SedisteTelefax = "sadasd" + i
                //    };
                //    edb_EmbList.Add(temp);
                //}
                //if (bool.Parse(ConfigurationManager.AppSettings["UJPTestData"]))
                //{
                //    if (edb_emb == "6857515")
                //    {
                //        var temp = new Edb_EmbDTO()
                //        {
                //            Edb = "4043013513169",
                //            Naziv = "Друштво за компјутерско програмирање КОРВУС МАКЕДОНИЈА ДООЕЛ Скопје",
                //            Emb = "6857515",
                //            Ziro = "000210068575150144",
                //            BankaZiro = "000210068575150144",
                //            DatumPrijava = "07.05.2013",
                //            PrijavaVid = "1044741",
                //            PrijavaStatus = "OK",
                //            DejnostNace = "Компјутерско програмирање",
                //            SedisteNaziv = "СКОПЈЕ - БУТЕЛ",
                //            SedisteBroj = "12",
                //            SedisteUlica = "ЈАДРАНСКА МАГИСТРАЛА",
                //            SedisteTelefon = "/",
                //            SedisteTelefax = "/"
                //        };
                //        edbEmbLst.Add(temp);
                //    }

                //}
                //if (bool.Parse(ConfigurationManager.AppSettings["UjpTestData"]))
                //{
                //    if (edb_emb == "6325270")
                //    {
                //        var temp = new Edb_EmbDTO()
                //        {
                //            Edb = "4030008020265",
                //            Naziv = "Друштво за производство на компјутери и деловно информатички услуги КИНГ ИЦТ ДООЕЛ Скопје",
                //            Emb = "6325270",
                //            Ziro = "000210063252700198",
                //            BankaZiro = "000210063252700198",
                //            DatumPrijava = "01.03.2009",
                //            PrijavaVid = "80412",
                //            PrijavaStatus = "OK",
                //            DejnostNace = "Д.О.О.Е.Л. ДРУШТВО СО ОГРАНИЧЕНА ОДГОВОРНОСТ НА ЕДНО ЛИЦЕ",
                //            SedisteNaziv = "СКОПЈЕ - БУТЕЛ",
                //            SedisteBroj = "12",
                //            SedisteUlica = "ЈАДРАНСКА МАГИСТРАЛА",
                //            SedisteTelefon = "/",
                //            SedisteTelefax = "/"
                //        };
                //        edbEmbLst.Add(temp);
                //    }
                //}
            }

            output.Edb_EmbList = edbEmbLst;

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforedb = output;
            dataforedb.FillBasicPrintInfo("Единствен Даночен Број", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintEDB", dataforedb);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfedb = "UJP_GetEdb_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmledb = "UJP_GetEdb_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            output.EdbPdf = namepdfedb;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfedb;
            File.WriteAllBytes(pathpdf, binary);


            output.EdbXml = namexmledb;
            var myXml = new XmlDocument();
            XPathNavigator xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforedb.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforedb);
            }
            var pathxml = path + namexmledb;
            File.WriteAllText(pathxml, myXml.OuterXml);

            //todo: remove when there is no need for test data
            Thread.Sleep(2000);

            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexmledb
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage EDBXML(string namexmledb)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmledb);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmledb
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdfedb
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage EDBPDF(string namepdfedb)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfedb);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfedb
            };

            return result;

        }

        // Опис: Методот го повикува EDB_EMBClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb_emb
        // Излезни параметри: EDB_EMB_OutputDTO модел 
        [System.Web.Http.HttpPost]
        public EDB_EMB_OutputDTO GetEmb(string edb_emb)
        {
            //We must send EDB to get data for service EMB
            var output = new EDB_EMB_OutputDTO();
            var edbEmbLst = new List<Edb_EmbDTO>();
            var enviroment = ConfigurationManager.AppSettings["Enviroment"];

            if (enviroment == "Test")
            {
                try
                {
                    //Service with unseparated methods
                    //var UJPClient = new UJPEDB_EMB.EDB_EMBClient();
                    
                    //Service with separated methods PROD
                    //var ujpClient = new UJPEdinstvenMaticenBroj.DataForEMBClient();
                    //Service with separated methods TEST
                    var ujpClient = new UJPEdinstvenMaticenBrojTest.DataForEMBClient();
                    var resultService = ujpClient.GetEMB(edb_emb);
                    for (int i = 0; i < resultService.Length; i++)
                    {
                        var temp = new Edb_EmbDTO()
                        {
                            Edb = resultService[i].Edb,
                            Naziv = resultService[i].Naziv.ToCyrilic().ToUpper(),
                            Emb = resultService[i].Emb,
                            Ziro = resultService[i].Ziro.ToCyrilic(),
                            BankaZiro = resultService[i].BankaZiro.ToCyrilic(),
                            DatumPrijava = resultService[i].DatumPrijava.ToCyrilic(),
                            PrijavaVid = resultService[i].PrijavaVid.ToCyrilic(),
                            PrijavaStatus = resultService[i].PrijavaStatus.ToCyrilic(),
                            DejnostNace = resultService[i].DejnostNace.ToCyrilic(),
                            SedisteNaziv = resultService[i].SedisteNaziv.ToCyrilic().ToUpper(),
                            SedisteBroj = resultService[i].SedisteBroj.ToCyrilic(),
                            SedisteUlica = resultService[i].SedisteUlica.ToCyrilic(),
                            SedisteTelefon = resultService[i].SedisteTelefon.ToCyrilic(),
                            SedisteTelefax = resultService[i].SedisteTelefax.ToCyrilic()
                        };
                        edbEmbLst.Add(temp);
                    }
                    output.Edb_EmbList = edbEmbLst;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    //Service with unseparated methods
                    //var UJPClient = new UJPEDB_EMB.EDB_EMBClient();

                    //Service with separated methods PROD
                    //var ujpClient = new UJPEdinstvenMaticenBroj.DataForEMBClient();
                    //Service with separated methods PROD
                    var ujpClient = new UJPEdinstvenMaticenBroj.DataForEMBClient();
                    var resultService = ujpClient.GetEMB(edb_emb);
                    for (int i = 0; i < resultService.Length; i++)
                    {
                        var temp = new Edb_EmbDTO()
                        {
                            Edb = resultService[i].Edb,
                            Naziv = resultService[i].Naziv.ToCyrilic().ToUpper(),
                            Emb = resultService[i].Emb,
                            Ziro = resultService[i].Ziro.ToCyrilic(),
                            BankaZiro = resultService[i].BankaZiro.ToCyrilic(),
                            DatumPrijava = resultService[i].DatumPrijava.ToCyrilic(),
                            PrijavaVid = resultService[i].PrijavaVid.ToCyrilic(),
                            PrijavaStatus = resultService[i].PrijavaStatus.ToCyrilic(),
                            DejnostNace = resultService[i].DejnostNace.ToCyrilic(),
                            SedisteNaziv = resultService[i].SedisteNaziv.ToCyrilic().ToUpper(),
                            SedisteBroj = resultService[i].SedisteBroj.ToCyrilic(),
                            SedisteUlica = resultService[i].SedisteUlica.ToCyrilic(),
                            SedisteTelefon = resultService[i].SedisteTelefon.ToCyrilic(),
                            SedisteTelefax = resultService[i].SedisteTelefax.ToCyrilic()
                        };
                        edbEmbLst.Add(temp);
                    }
                    output.Edb_EmbList = edbEmbLst;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
                //if (bool.Parse(ConfigurationManager.AppSettings["UjpTestData"]))
                //{
                //    if (edb_emb == "4043013513169")
                //    {
                //        var temp = new Edb_EmbDTO()
                //        {
                //            Edb = "4043013513169",
                //            Naziv = "Друштво за компјутерско програмирање КОРВУС МАКЕДОНИЈА ДООЕЛ Скопје",
                //            Emb = "6857515",
                //            Ziro = "000210068575150144",
                //            BankaZiro = "000210068575150144",
                //            DatumPrijava = "07.05.2013",
                //            PrijavaVid = "1044741",
                //            PrijavaStatus = "OK",
                //            DejnostNace = "Компјутерско програмирање",
                //            SedisteNaziv = "СКОПЈЕ - БУТЕЛ",
                //            SedisteBroj = "12",
                //            SedisteUlica = "ЈАДРАНСКА МАГИСТРАЛА",
                //            SedisteTelefon = "/",
                //            SedisteTelefax = "/"
                //        };
                //        edbEmbLst.Add(temp);
                //    }
                //}
                //if (bool.Parse(ConfigurationManager.AppSettings["UjpTestData"]))
                //{
                //    if (edb_emb == "4030008020265")
                //    {
                //        var temp = new Edb_EmbDTO()
                //        {
                //            Edb = "4030008020265",
                //            Naziv = "Друштво за производство на компјутери и деловно информатички услуги КИНГ ИЦТ ДООЕЛ Скопје",
                //            Emb = "6325270",
                //            Ziro = "000210063252700198",
                //            BankaZiro = "000210063252700198",
                //            DatumPrijava = "01.03.2009",
                //            PrijavaVid = "80412",
                //            PrijavaStatus = "OK",
                //            DejnostNace = "Д.О.О.Е.Л. ДРУШТВО СО ОГРАНИЧЕНА ОДГОВОРНОСТ НА ЕДНО ЛИЦЕ",
                //            SedisteNaziv = "СКОПЈЕ - БУТЕЛ",
                //            SedisteBroj = "12",
                //            SedisteUlica = "ЈАДРАНСКА МАГИСТРАЛА",
                //            SedisteTelefon = "/",
                //            SedisteTelefax = "/"
                //        };
                //        edbEmbLst.Add(temp);
                //    }
                //}
            }
            output.Edb_EmbList = edbEmbLst;


            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforedb = output;
            dataforedb.FillBasicPrintInfo("Единствен Матичен Број", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintEMB", dataforedb);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfemb = "UJP_GetEMb_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".pdf";
            var namexmlemb = "UJP_GetEMb_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year + ".xml";
            output.EdbPdf = namepdfemb;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfemb;
            File.WriteAllBytes(pathpdf, binary);


            output.EdbXml = namexmlemb;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforedb.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforedb);
            }
            var pathxml = path + namexmlemb;
            File.WriteAllText(pathxml, myXml.OuterXml);

            //todo: remove when there is no need for test data
            Thread.Sleep(2000);

            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexmlemb
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage EMBXML(string namexmlemb)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmlemb);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmlemb
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdfemb
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage EMBPDF(string namepdfemb)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfemb);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfemb
            };

            return result;

        }

        // Опис: Методот го повикува DataAnnRevClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb и year
        // Излезни параметри: AnnualRevenuesFZOOutputDTO модел  
        [System.Web.Http.HttpPost]
        public AnnualRevenuesFZOOutputDTO GetAnnualRevenuesFZO(string edb, string year)
        {

            var output = new AnnualRevenuesFZOOutputDTO();
            if (!bool.Parse(ConfigurationManager.AppSettings["UJPTestData"]))
            {
                try
                {
                    var ujpClient = new UJPDataForAnnualRevenues.DataAnnRevClient();
                    var outputUJP = ujpClient.GetAnnualRevenuesFZO(edb, year);
                    var annualRevenuesFZO = new AnnualRevenuesFZODTO()
                        {
                            EDB = outputUJP.EDB,
                            FirstName = outputUJP.FirstName,
                            LastName = outputUJP.LastName,
                            Year = outputUJP.Year,
                            FZO_Bruto = outputUJP.FZO_Bruto,
                            FZO_Neto = outputUJP.FZO_Neto,
                            Zabeleska = outputUJP.Zabeleska
                        };
                    output.AnnualRevenuesFZO = annualRevenuesFZO;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                var annualRevenuesFZO = new AnnualRevenuesFZODTO()
                {
                    EDB = "test",
                    FirstName = "test",
                    LastName = "test",
                    Year = "test",
                    FZO_Bruto = "test",
                    FZO_Neto = "test",
                    Zabeleska = "test"
                };
                output.AnnualRevenuesFZO = annualRevenuesFZO;
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforfzo = output;
            dataforfzo.FillBasicPrintInfo("Годишни приходи ФЗО", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintFZO", dataforfzo);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year1 = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdffzo = "UJP_GetAnnualRevenuesFZO_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".pdf";
            var namexmlfzo = "UJP_GetAnnualRevenuesFZO_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".xml";
            output.Pdffzo = namepdffzo;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdffzo;
            File.WriteAllBytes(pathpdf, binary);


            output.Xmlfzo = namexmlfzo;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforfzo.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforfzo);
            }
            var pathxml = path + namexmlfzo;
            File.WriteAllText(pathxml, myXml.OuterXml);

            //todo: remove when there is no need for test data
            Thread.Sleep(2000);
            
            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexmlfzo
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FZOXML(string namexmlfzo)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmlfzo);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmlfzo
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdffzo
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FZOPDF(string namepdffzo)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdffzo);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdffzo
            };

            return result;

        }

        // Опис: Методот го повикува DataAnnRevClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb и year
        // Излезни параметри: AnnualRevenuesKKKSOutputDTO модел   
        [System.Web.Http.HttpPost]
        public AnnualRevenuesKKKSOutputDTO GetAnnualRevenuesKKKS(string edb, string year)
        {
            var output = new AnnualRevenuesKKKSOutputDTO();
            if (!bool.Parse(ConfigurationManager.AppSettings["UJPTestData"]))
            {
                try
                {
                    var ujpClient = new UJPDataForAnnualRevenues.DataAnnRevClient();
                    var outputUJP = ujpClient.GetAnnualRevenuesKKKS(edb, year);
                    var annualRevenuesKKKS = new AnnualRevenuesKKKSDTO()
                    {
                        EDB = outputUJP.EDB,
                        FirstName = outputUJP.FirstName,
                        LastName = outputUJP.LastName,
                        Year = outputUJP.Year,
                        Pospl_mmgg = outputUJP.Pospl_mmgg,
                        Pospl_Bruto = outputUJP.Pospl_Bruto,
                        Pospl_Neto = outputUJP.Pospl_Neto,
                        Mes6_Broj = outputUJP.Mes6_Broj,
                        Mes6_Bruto = outputUJP.Mes6_Bruto,
                        Mes6_Neto = outputUJP.Mes6_Neto,
                        Vk_Bruto = outputUJP.Vk_Bruto,
                        Vk_Neto = outputUJP.Vk_Neto,
                        Drugo_Bruto = outputUJP.Drugo_Bruto,
                        Drugo_Neto = outputUJP.Drugo_Neto,
                        Zabeleska = outputUJP.Zabeleska
                    };
                    output.AnnualRevenueKKKS = annualRevenuesKKKS;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                var annualRevenuesKKKS = new AnnualRevenuesKKKSDTO()
                    {
                        EDB = "aaaaaaa",
                        FirstName = "bbbbbbbbb",
                        LastName = "cccccccccccccc",
                        Year = "ddddddddddd",
                        Pospl_mmgg = "eeeeeeeeee",
                        Pospl_Bruto = "ffffffffff",
                        Pospl_Neto = "ggggggggggggg",
                        Mes6_Broj = "hhhhhhhhhhh",
                        Mes6_Bruto = "iiiiiiiiiii",
                        Mes6_Neto = "ggggggggggg",
                        Vk_Bruto = "kkkkkkkkkkkkk",
                        Vk_Neto = "lllllllllll",
                        Drugo_Bruto = "mmmmmmmmmmmm",
                        Drugo_Neto = "nnnnnnnnnnnn",
                        Zabeleska = "oiooooooooooo"
                    };
                output.AnnualRevenueKKKS = annualRevenuesKKKS;

                
            }
            return output;
        }

        // Опис: Методот го повикува DataAnnRevClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb и year
        // Излезни параметри: AnnualRevenuesMONOutputDTO модел   
        [System.Web.Http.HttpPost]
        public AnnualRevenuesMONOutputDTO GetAnnualRevenuesMON(string edb, string year)
        {
            var output = new AnnualRevenuesMONOutputDTO();
            if (!bool.Parse(ConfigurationManager.AppSettings["UJPTestData"]))
            {
                try
                {
                    var ujpClient = new UJPDataForAnnualRevenues.DataAnnRevClient();
                    var outputUJP = ujpClient.GetAnnualRevenuesMON(edb, year);
                    var annualRevenuesMON = new AnnualRevenuesMONDTO()
                    {
                        EDB = outputUJP.EDB,
                        FirstName = outputUJP.FirstName,
                        LastName = outputUJP.LastName,
                        Year = outputUJP.Year,
                        Licniprim_Bruto = outputUJP.Licniprim_Bruto,
                        Licniprim_Neto = outputUJP.Licniprim_Neto,
                        Prihodi_Svd = outputUJP.Prihodi_Svd,
                        Prihodi_Zem = outputUJP.Prihodi_Zem,
                        Prihodi_Imot = outputUJP.Prihodi_Imot,
                        Prihodi_Avtor = outputUJP.Prihodi_Avtor,
                        Prihodi_Kapital = outputUJP.Prihodi_Kapital,
                        Prihodi_Kapdob = outputUJP.Prihodi_Kapdob,
                        Prihodi_Igri = outputUJP.Prihodi_Igri,
                        Prihodi_Drugo = outputUJP.Prihodi_Drugo,
                        Zabeleska = outputUJP.Zabeleska
                    };
                    output.AnnualRevenuesMON = annualRevenuesMON;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                var annualRevenuesMON = new AnnualRevenuesMONDTO()
                   {
                       EDB = "test",
                       FirstName = "test",
                       LastName = "test",
                       Year = "test",
                       Licniprim_Bruto = "test",
                       Licniprim_Neto = "test",
                       Prihodi_Svd = "test",
                       Prihodi_Zem = "test",
                       Prihodi_Imot = "test",
                       Prihodi_Avtor = "test",
                       Prihodi_Kapital = "test",
                       Prihodi_Kapdob = "test",
                       Prihodi_Igri = "test",
                       Prihodi_Drugo = "test",
                       Zabeleska = "test"
                   };
                output.AnnualRevenuesMON = annualRevenuesMON;
               
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataforfzo = output;
            dataforfzo.FillBasicPrintInfo("Годишни приходи МОН", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintMON", dataforfzo);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year1 = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfmon = "UJP_GetAnnualRevenuesMON_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".pdf";
            var namexmlmon = "UJP_GetAnnualRevenuesMON_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".xml";
            output.MonPdf = namepdfmon;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfmon;
            File.WriteAllBytes(pathpdf, binary);


            output.MonXml = namexmlmon;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataforfzo.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataforfzo);
            }
            var pathxml = path + namexmlmon;
            File.WriteAllText(pathxml, myXml.OuterXml);

            //todo: remove when there is no need for test data
            Thread.Sleep(2000);

            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexmlmon
        // Излезни параметри: HttpResponseMessage модел  
        [System.Web.Http.HttpGet]
        public HttpResponseMessage MONXML(string namexmlmon)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmlmon);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmlmon
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdfmon
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage MONPDF(string namepdfmon)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfmon);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfmon
            };

            return result;

        }

        // Опис: Методот го повикува DataAnnRevClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb и year
        // Излезни параметри: AnnualRevenuesMTSPOutputDTO модел  
        [System.Web.Http.HttpPost]
        public AnnualRevenuesMTSPOutputDTO GetAnnualRevenuesMTSP(string edb, string year)
        {
            var output = new AnnualRevenuesMTSPOutputDTO();
            if (!bool.Parse(ConfigurationManager.AppSettings["UJPTestData"]))
            {
                try
                {
                    var ujpClient = new UJPDataForAnnualRevenues.DataAnnRevClient();
                    var outputUJP = ujpClient.GetAnnualRevenuesMTSP(edb, year);
                    var annualRevenuesMTSP = new AnnualRevenuesMTSPDTO()
                    {
                        EDB = outputUJP.EDB,
                        FirstName = outputUJP.FirstName,
                        LastName = outputUJP.LastName,
                        Year = outputUJP.Year,
                        Plata_3m_Bruto = outputUJP.Plata_3m_Bruto,
                        Plata_3m_Neto = outputUJP.Licniprim_Neto,
                        Licniprim_Bruto = outputUJP.Licniprim_Bruto,
                        Licniprim_Neto = outputUJP.Licniprim_Neto,
                        Prihodi_Svd = outputUJP.Prihodi_Svd,
                        Prihodi_Zem = outputUJP.Prihodi_Zem,
                        Prihodi_Imot = outputUJP.Prihodi_Imot,
                        Prihodi_Avtor = outputUJP.Prihodi_Avtor,
                        Prihodi_Kapital = outputUJP.Prihodi_Kapital,
                        Prihodi_Kapdob = outputUJP.Prihodi_Kapdob,
                        Prihodi_Igri = outputUJP.Prihodi_Igri,
                        Prihodi_Drugo = outputUJP.Prihodi_Drugo,
                        Zabeleska = outputUJP.Zabeleska
                    };
                    output.AnnualRevenuesMTSP = annualRevenuesMTSP;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                var annualRevenuesMTSP = new AnnualRevenuesMTSPDTO()
                   {
                       EDB = "test",
                       FirstName = "test",
                       LastName = "test",
                       Year = "test",
                       Plata_3m_Bruto = "test",
                       Plata_3m_Neto = "test",
                       Licniprim_Bruto = "test",
                       Licniprim_Neto = "test",
                       Prihodi_Svd = "test",
                       Prihodi_Zem = "test",
                       Prihodi_Imot = "test",
                       Prihodi_Avtor = "test",
                       Prihodi_Kapital = "test",
                       Prihodi_Kapdob = "test",
                       Prihodi_Igri = "test",
                       Prihodi_Drugo = "test",
                       Zabeleska = "test"
                   };
                output.AnnualRevenuesMTSP = annualRevenuesMTSP;
            }

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataformtsp = output;
            dataformtsp.FillBasicPrintInfo("Годишни приходи МТСП", InstitutionName);
            var r = new Rotativa.ViewAsPdf("PrintMTSP", dataformtsp);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year1 = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfmtsp = "UJP_GetAnnualRevenuesMTSP_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".pdf";
            var namexmlmtsp = "UJP_GetAnnualRevenuesMTSP_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".xml";
            output.MtspPdf = namepdfmtsp;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfmtsp;
            File.WriteAllBytes(pathpdf, binary);


            output.MtspXml = namexmlmtsp;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataformtsp.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataformtsp);
            }
            var pathxml = path + namexmlmtsp;
            File.WriteAllText(pathxml, myXml.OuterXml);

            //todo: remove when there is no need for test data
            Thread.Sleep(2000);
            return output;
        }

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност namexmlmtsp
        // Излезни параметри: HttpResponseMessage модел  
        [System.Web.Http.HttpGet]
        public HttpResponseMessage MTSPXML(string namexmlmtsp)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namexmlmtsp);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namexmlmtsp
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност namepdfmtsp
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage MTSPPDF(string namepdfmtsp)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + namepdfmtsp);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = namepdfmtsp
            };

            return result;

        }

        // Опис: Методот го повикува RegExcBondsClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност edb и number
        // Излезни параметри: ExciseBondsOutputDTO модел 
        [System.Web.Http.HttpPost]
        public ExciseBondsOutputDTO GetRegisterOfExciseBonds(long edb, string number = "")
        {
            //var ujpClient = new UJPRegisterOfExciseBonds.RegExcBondsClient();
            //var input = new UJPRegisterOfExciseBonds.ExciseBondsInput();
            
            var output = new ExciseBondsOutputDTO();
            var exciseBonds = new List<ExciseBondsDTO>();
            var exciseGoods = new List<ExciseGoodsDTO>();
            var exciseSpaces = new List<ExciseSpacesDTO>();
            var enviroment = ConfigurationManager.AppSettings["Enviroment"];

            if (enviroment == "Test")
            {
                //New corrected adapter
                var ujpClient = new UJPRegistarNaObvrzniciZaAkcizaTest.RegExcBondsClient();
                var input = new UJPRegistarNaObvrzniciZaAkcizaTest.ExciseBondsInput();

                input.EDB = edb;
                input.Number = number;
                try
                {
                    var outputUJP = ujpClient.GetRegExcBonds(input);
                    for (int i = 0; i < outputUJP.ExciseBonds.Length; i++)
                    {
                        var temp = new ExciseBondsDTO()
                        {
                            Id = outputUJP.ExciseBonds[i].id,
                            Edb = outputUJP.ExciseBonds[i].edb,
                            Name = outputUJP.ExciseBonds[i].name,
                            Vid_odobrenie = outputUJP.ExciseBonds[i].vid_odobrenie,
                            Broj_odobrenie = outputUJP.ExciseBonds[i].broj_odobrenie,
                            Datum_odobrenie = outputUJP.ExciseBonds[i].datum_odobrenie,
                            Prethodno_odobrenie = outputUJP.ExciseBonds[i].prethodno_odobrenie,
                            Sledno_odobrenie = outputUJP.ExciseBonds[i].sledno_odobrenie,
                            Embg_odgovorno_lice = outputUJP.ExciseBonds[i].embg_odgovorno_lice,
                            Naziv_odgovorno_lice = outputUJP.ExciseBonds[i].naziv_odgovorno_lice,
                            Embg_polnomosnik = outputUJP.ExciseBonds[i].embg_polnomosnik,
                            Naziv_polnomosnik = outputUJP.ExciseBonds[i].naziv_polnomosnik
                        };
                        exciseBonds.Add(temp);
                    }
                    for (int i = 0; i < outputUJP.ExciseGoods.Length; i++)
                    {
                        var temp = new ExciseGoodsDTO()
                        {
                            Id = outputUJP.ExciseGoods[i].id,
                            Broj_odobrenie = outputUJP.ExciseGoods[i].broj_odobrenie,
                            Stavka = outputUJP.ExciseGoods[i].stavka,
                            Vid = outputUJP.ExciseGoods[i].vid,
                            Proizvod = outputUJP.ExciseGoods[i].proizvod,
                            Tarifa = outputUJP.ExciseGoods[i].tarifa,
                            Kolicina = outputUJP.ExciseGoods[i].kolicina,
                            Opis_merka = outputUJP.ExciseGoods[i].opis_merka,
                            Za_proizvodstvo = outputUJP.ExciseGoods[i].za_proizvodstvo,
                            Za_skladiranje = outputUJP.ExciseGoods[i].za_skladiranje
                        };
                        exciseGoods.Add(temp);
                    }
                    for (int i = 0; i < outputUJP.ExciseSpaces.Length; i++)
                    {
                        var temp = new ExciseSpacesDTO()
                        {
                            Id = outputUJP.ExciseSpaces[i].id,
                            Broj_odobrenie = outputUJP.ExciseSpaces[i].broj_odobrenie,
                            Stavka = outputUJP.ExciseSpaces[i].stavka,
                            Opstina = outputUJP.ExciseSpaces[i].opstina,
                            Naziv_opstina = outputUJP.ExciseSpaces[i].naziv_opstina,
                            Mesto = outputUJP.ExciseSpaces[i].mesto,
                            Naziv_mesto = outputUJP.ExciseSpaces[i].naziv_mesto,
                            Naziv_ulica = outputUJP.ExciseSpaces[i].naziv_ulica,
                            Ulica_broj = outputUJP.ExciseSpaces[i].ulica_broj,
                            Za_proizvodstvo = outputUJP.ExciseSpaces[i].za_proizvodstvo,
                            Za_skladiranje = outputUJP.ExciseSpaces[i].za_skladiranje,
                            Embg_odgovorno_lice = outputUJP.ExciseSpaces[i].embg_odgovorno_lice,
                            Naziv_odgovorno_lice = outputUJP.ExciseSpaces[i].naziv_odgovorno_lice,
                            Zabeleska = outputUJP.ExciseSpaces[i].zabeleska,
                            Status = outputUJP.ExciseSpaces[i].status,
                            Status_datum = outputUJP.ExciseSpaces[i].status_datum
                        };
                        exciseSpaces.Add(temp);
                    }

                    output.ExciseBonds = exciseBonds;
                    output.ExciseGoods = exciseGoods;
                    output.ExciseSpaces = exciseSpaces;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
            }
            else
            {
                //New corrected adapter
                var ujpClient = new UJPRegistarNaObvrzniciZaAkciza.RegExcBondsClient();
                var input = new UJPRegistarNaObvrzniciZaAkciza.ExciseBondsInput();

                input.EDB = edb;
                input.Number = number;
                try
                {
                    var outputUJP = ujpClient.GetRegExcBonds(input);
                    for (int i = 0; i < outputUJP.ExciseBonds.Length; i++)
                    {
                        var temp = new ExciseBondsDTO()
                        {
                            Id = outputUJP.ExciseBonds[i].id,
                            Edb = outputUJP.ExciseBonds[i].edb,
                            Name = outputUJP.ExciseBonds[i].name,
                            Vid_odobrenie = outputUJP.ExciseBonds[i].vid_odobrenie,
                            Broj_odobrenie = outputUJP.ExciseBonds[i].broj_odobrenie,
                            Datum_odobrenie = outputUJP.ExciseBonds[i].datum_odobrenie,
                            Prethodno_odobrenie = outputUJP.ExciseBonds[i].prethodno_odobrenie,
                            Sledno_odobrenie = outputUJP.ExciseBonds[i].sledno_odobrenie,
                            Embg_odgovorno_lice = outputUJP.ExciseBonds[i].embg_odgovorno_lice,
                            Naziv_odgovorno_lice = outputUJP.ExciseBonds[i].naziv_odgovorno_lice,
                            Embg_polnomosnik = outputUJP.ExciseBonds[i].embg_polnomosnik,
                            Naziv_polnomosnik = outputUJP.ExciseBonds[i].naziv_polnomosnik
                        };
                        exciseBonds.Add(temp);
                    }
                    for (int i = 0; i < outputUJP.ExciseGoods.Length; i++)
                    {
                        var temp = new ExciseGoodsDTO()
                        {
                            Id = outputUJP.ExciseGoods[i].id,
                            Broj_odobrenie = outputUJP.ExciseGoods[i].broj_odobrenie,
                            Stavka = outputUJP.ExciseGoods[i].stavka,
                            Vid = outputUJP.ExciseGoods[i].vid,
                            Proizvod = outputUJP.ExciseGoods[i].proizvod,
                            Tarifa = outputUJP.ExciseGoods[i].tarifa,
                            Kolicina = outputUJP.ExciseGoods[i].kolicina,
                            Opis_merka = outputUJP.ExciseGoods[i].opis_merka,
                            Za_proizvodstvo = outputUJP.ExciseGoods[i].za_proizvodstvo,
                            Za_skladiranje = outputUJP.ExciseGoods[i].za_skladiranje
                        };
                        exciseGoods.Add(temp);
                    }
                    for (int i = 0; i < outputUJP.ExciseSpaces.Length; i++)
                    {
                        var temp = new ExciseSpacesDTO()
                        {
                            Id = outputUJP.ExciseSpaces[i].id,
                            Broj_odobrenie = outputUJP.ExciseSpaces[i].broj_odobrenie,
                            Stavka = outputUJP.ExciseSpaces[i].stavka,
                            Opstina = outputUJP.ExciseSpaces[i].opstina,
                            Naziv_opstina = outputUJP.ExciseSpaces[i].naziv_opstina,
                            Mesto = outputUJP.ExciseSpaces[i].mesto,
                            Naziv_mesto = outputUJP.ExciseSpaces[i].naziv_mesto,
                            Naziv_ulica = outputUJP.ExciseSpaces[i].naziv_ulica,
                            Ulica_broj = outputUJP.ExciseSpaces[i].ulica_broj,
                            Za_proizvodstvo = outputUJP.ExciseSpaces[i].za_proizvodstvo,
                            Za_skladiranje = outputUJP.ExciseSpaces[i].za_skladiranje,
                            Embg_odgovorno_lice = outputUJP.ExciseSpaces[i].embg_odgovorno_lice,
                            Naziv_odgovorno_lice = outputUJP.ExciseSpaces[i].naziv_odgovorno_lice,
                            Zabeleska = outputUJP.ExciseSpaces[i].zabeleska,
                            Status = outputUJP.ExciseSpaces[i].status,
                            Status_datum = outputUJP.ExciseSpaces[i].status_datum
                        };
                        exciseSpaces.Add(temp);
                    }

                    output.ExciseBonds = exciseBonds;
                    output.ExciseGoods = exciseGoods;
                    output.ExciseSpaces = exciseSpaces;
                    _logger.Info("Povikot kon servisot e uspesen");
                }
                catch (Exception ex)
                {
                    output.Message = "Error calling the service!";
                    _logger.Error(ex.Message, "UJP ERROR");
                    throw ex;
                }
                //for (int i = 0; i < 5; i++)
                //{
                //    var temp = new ExciseBondsDTO()
                //    {
                //        Id = "test" + i,
                //        Edb = "test" + i,
                //        Name = "test" + i,
                //        Vid_odobrenie = "test" + i,
                //        Broj_odobrenie = "test" + i,
                //        Datum_odobrenie = "test" + i,
                //        Prethodno_odobrenie = "test" + i,
                //        Sledno_odobrenie = "test" + i,
                //        Embg_odgovorno_lice = "test" + i,
                //        Naziv_odgovorno_lice = "test" + i,
                //        Embg_polnomosnik = "test" + i,
                //        Naziv_polnomosnik = "test" + i
                //    };
                //    exciseBonds.Add(temp);
                //}
                //for (int i = 0; i < 10; i++)
                //{
                //    var temp = new ExciseGoodsDTO()
                //    {
                //        Id = "test" + i,
                //        Broj_odobrenie = "test" + i,
                //        Stavka = "test" + i,
                //        Vid = "test" + i,
                //        Proizvod = "test" + i,
                //        Tarifa = "test" + i,
                //        Kolicina = "test" + i,
                //        Opis_merka = "test" + i,
                //        Za_proizvodstvo = "test" + i,
                //        Za_skladiranje = "test" + i
                //    };
                //    exciseGoods.Add(temp);
                //}
                //for (int i = 0; i < 14; i++)
                //{
                //    var temp = new ExciseSpacesDTO()
                //    {
                //        Id = "test" + i,
                //        Broj_odobrenie = "test" + i,
                //        Stavka = "test" + i,
                //        Opstina = "test" + i,
                //        Naziv_opstina = "test" + i,
                //        Mesto = "test" + i,
                //        Naziv_mesto = "test" + i,
                //        Naziv_ulica = "test" + i,
                //        Ulica_broj = "test" + i,
                //        Za_proizvodstvo = "test" + i,
                //        Za_skladiranje = "test" + i,
                //        Embg_odgovorno_lice = "test" + i,
                //        Naziv_odgovorno_lice = "test" + i,
                //        Zabeleska = "test" + i,
                //        Status = "test" + i,
                //        Status_datum = "test" + i
                //    };
                //    exciseSpaces.Add(temp);
                //}

                //output.ExciseBonds = exciseBonds;
                //output.ExciseGoods = exciseGoods;
                //output.ExciseSpaces = exciseSpaces;
            }


            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase, routeData, new FPIOMController.EmptyController());
            var dataformtsp = output;
            dataformtsp.FillBasicPrintInfo("Регистар на обврзници за акциза", "Царинска Управа на Република Македонија");
            var r = new Rotativa.ViewAsPdf("PrintRegistar", dataformtsp);
            var binary = r.BuildPdf(controllerContext);

            var date = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            var year1 = DateTime.Now.Year;
            var hour = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var secods = DateTime.Now.Second;
            var namepdfregistar = "UJP_GetRegisterOfExciseBonds_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".pdf";
            var namexmlregistar = "UJP_GetRegisterOfExciseBonds_" + secods + "_" + minutes + "_" + hour + "_" + date + "_" + month + "_" + year1 + ".xml";
            output.RegistarPDF = namepdfregistar;
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var pathpdf = path + namepdfregistar;
            File.WriteAllBytes(pathpdf, binary);


            output.RegistarXML = namexmlregistar;
            var myXml = new XmlDocument();
            var xNav = myXml.CreateNavigator();

            var x = new XmlSerializer(dataformtsp.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, dataformtsp);
            }
            var pathxml = path + namexmlregistar;
            File.WriteAllText(pathxml, myXml.OuterXml);

            //todo: remove when there is no need for test data
            Thread.Sleep(2000);

            return output;
        }

        // Опис: Методот го повикува BondsDataClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime
        // Излезни параметри: HttpResponseMessage модел 
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetOU_AVRM(DateTime dateTime, string service, string institution)
        //{
        //    byte[] outputUJP;

        //    var result1 = new HttpResponseMessage();
        //    var dateString = dateTime.ToString("ddMMyyyy");
        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();

        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateString, "0");

        //        if (output.HasDocument)
        //        {
        //            var result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = "GetDocOUAVRM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetOU_AVRM.txt.gpg", dateString, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува BondsDataClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime
        // Излезни параметри: HttpResponseMessage модел
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetOU_FPIOM(DateTime dateTime, string service, string institution)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();
        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, "0");

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = "GetDocOUFPIOM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetOU_FPIOM.txt.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува BondsDataClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime
        // Излезни параметри: HttpResponseMessage модел 
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetOU_FZOM(DateTime dateTime, string service, string institution)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();
        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, "0");

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = "GetDocOUFZOM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetOU_FZOM.txt.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува DataPContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime и number
        // Излезни параметри: HttpResponseMessage модел
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetPP_AVRM(DateTime dateTime, int number, string service, string institution, bool additionalInfo)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");
        //    var addInfo = additionalInfo ? "1" : "0";

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();

        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, addInfo);

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //                FileName = "GetDocPPAVRM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        _logger.Info("outputUJP e " + Convert.ToBase64String(outputUJP));
        //        _logger.Info("date e " + dateTimeS);
        //        result1 = await ResultFromService("GetPP_AVRM.zip.gpg", dateTimeS, outputUJP);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува DataPContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime и number
        // Излезни параметри: HttpResponseMessage модел 
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetPP_FPIOM(DateTime dateTime, int number, string service, string institution, bool additionalInfo)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");
        //    var addInfo = additionalInfo ? "1" : "0";

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();

        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, addInfo);

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //                FileName = "GetDocPPFPIOM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetPP_FPIOM.zip.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува DataPContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime и number
        // Излезни параметри: HttpResponseMessage модел
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetPP_FZOM(DateTime dateTime, int number, string service, string institution, bool additionalInfo)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");
        //    var addInfo = additionalInfo ? "1" : "0";

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();

        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, addInfo);

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //                FileName = "GetDocPPFZOM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetPP_FZOM.zip.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува DataUnpContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime
        // Излезни параметри: HttpResponseMessage модел 
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetNP_AVRM(DateTime dateTime, string service, string institution)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();
        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, "0");

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //                FileName = "GetDocNP_AVRM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetNP_AVRM.doc.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува DataUnpContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime
        // Излезни параметри: HttpResponseMessage модел 
        //[System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetNP_FPIOM(DateTime dateTime, string service, string institution)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();
        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, "0");

        //        if (output.HasDocument)
        //        {
        //            var result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //                FileName = "GetDocNP_FPIOM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetNP_FPIOM.doc.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот го повикува DataUnpContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност dateTime
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpPost]
        //public async Task<HttpResponseMessage> GetNP_FZOM(DateTime dateTime, string service, string institution)
        //{
        //    byte[] outputUJP;
        //    var result1 = new HttpResponseMessage();
        //    var dateTimeS = dateTime.ToString("ddMMyyyy");

        //    var client = new GetDocForOUorNPorPP.GetDocForOUorNPorPPClient();
        //    try
        //    {
        //        var output = client.GetDocOUorNPorPP(institution, service, dateTimeS, "0");

        //        if (output.HasDocument)
        //        {
        //            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //            var date = DateTime.Now.ToString("ddMMyyyy");
        //            result.Content = new ByteArrayContent(output.Document);
        //            result.Content.Headers.ContentType =
        //                new MediaTypeHeaderValue("application/octet-stream");
        //            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //        {
        //                FileName = "GetDocNP_FZOM" + date
        //            };
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(output.Message);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.Error(e);
        //        throw new Exception(e.Message);
        //    }

        //    try
        //    {
        //        result1 = await ResultFromService("GetNP_FZOM.doc.gpg", dateTimeS, outputUJP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Сервисот кој го повикавте врати грешка");
        //    }

        //    //todo: remove when there is no need for test data
        //    Thread.Sleep(2000);
        //    return result1;
        //}

        // Опис: Методот врши запис во ХМL 
        // Влезни параметри: податочна вредност registarxml
        // Излезни параметри: HttpResponseMessage модел   
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegistarXML(string registarxml)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + registarxml);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = registarxml
            };

            return result;
        }

        // Опис: Методот врши запис во PDF 
        // Влезни параметри: податочна вредност registarpdf
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegistarPDF(string registarpdf)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            var content = File.ReadAllBytes(path + registarpdf);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = registarpdf
            };

            return result;

        }

        // Опис: Методот го повикува DataUnpContrClient клиентот како начин за пристап до сервис 
        // Влезни параметри: податочна вредност name, dateTime, outputUJP
        // Излезни параметри: HttpResponseMessage модел 
        public async Task<HttpResponseMessage> ResultFromService(string name, string dateTime, byte[] outputUJP)
        {
            var path = WebConfigurationManager.AppSettings["PathToFile"];
            //byte[] content = File.ReadAllBytes(path + name);
            //File.WriteAllBytes(path + "\\" + name, content);
            try
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(outputUJP);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = name + dateTime
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured while trying to create document ", ex);
                throw ex;
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