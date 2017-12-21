using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using System.Collections.Generic;
using System.Web.Http;
using Interop.CC.Models.RepositoryContracts;
using OfficeOpenXml;

namespace Interop.CC.Portal.API.Controllers
{
    [System.Web.Http.Authorize]
    public class SoapFaultController : ApiController
    {
        private readonly ISoapFaultRepository _soapFaultRepository;

        // Опис: Конструктор на SoapFaultController модулот
        // Влезни параметри: ISoapFaultRepository модел
        public SoapFaultController(ISoapFaultRepository soapFaultRepository)
        {
            _soapFaultRepository = soapFaultRepository;
        }

        // Опис: Методот го повикува GetSoapFaultMessages методот на SoapFaultRepository модулот
        // Влезни параметри: /
        // Излезни параметри: листа со податоци за SoapFault моделот
        [System.Web.Http.HttpGet]
        public IEnumerable<SoapFault> GetSoapFaultList()
        {
            return _soapFaultRepository.GetSoapFaultMessages();
        }

        //[System.Web.Http.HttpGet]
        //public SoapFault GetSoapFaultMessageBy(string code)
        //{
        //    return _soapFaultRepository.GetSoapFaultMessageBy(code);
        //}

        // Опис: Методот го повикува InsertSoapFault методот на SoapFaultRepository модулот
        // Влезни параметри: SoapFault модел
        // Излезни параметри: /
        [System.Web.Http.HttpPost]
        public void InsertSoapFault(SoapFault soapFault)
        {
            _soapFaultRepository.InsertSoapFault(soapFault);
        }

        // Опис: Методот го повикува GetSoapFaultsPaged методот на SoapFaultRepository модулот
        // Влезни параметри: број на страна, записи по страна, трансакција, дата од, дата до
        // Излезни параметри: нумерирана колекција со податоци за SoapFault моделот
        [System.Web.Http.HttpGet]
        public PagedCollection<SoapFault> GetSoapFaultListPaged(int pageIndex, int itemsPerPage, Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap)
        {
            
            return _soapFaultRepository.GetSoapFaultsPaged(pageIndex, itemsPerPage,filterTransaction,fromDateSoap,toDateSoap);
        }

        // Опис: Методот го повикува GetFilteredSoapFaults методот на SoapFaultRepository модулот
        // Влезни параметри: параметри за филтрирање - трансакција, дата од, дата до
        // Излезни параметри: HttpResponseMessage модел 
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreatePdf(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap)
        {

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase,
                routeData,
                new EmptyController());
            var soapfaults = _soapFaultRepository.GetFilteredSoapFaults(filterTransaction, fromDateSoap, toDateSoap);

            var r = new Rotativa.ViewAsPdf("PrintSoapFaults", soapfaults)
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

        // Опис: Методот го повикува GetExcelSheet методот
        // Влезни параметри: параметри за филтрирање - трансакција, дата од, дата до
        // Излезни параметри: HttpResponseMessage модел 
        public HttpResponseMessage CreateExcel(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap)
        {

            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK);
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(GetExcelSheet(filterTransaction,fromDateSoap,toDateSoap));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "Greshki.xlsx";
            return response;
        }

        // Опис: Методот го повикува GetFilteredSoapFaults методот на SoapFaultRepository модулот
        // Влезни параметри: параметри за филтрирање - трансакција, дата од, дата до
        // Излезни параметри: MemoryStream модел 
        public MemoryStream GetExcelSheet(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap)
        {
            using (var package = new ExcelPackage())
            {
                var resultData = _soapFaultRepository.GetFilteredSoapFaults(filterTransaction,fromDateSoap,toDateSoap);
                var worksheet = package.Workbook.Worksheets.Add("SoapFaultExcel");
                //if (resultData != null) 
                worksheet.Cells["A1"].LoadFromCollection(resultData, true);
                // package.Save();
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream;
            }
        }
        public class EmptyController : ControllerBase
        {
            // За експортирање на податоци
            protected override void ExecuteCore()
            {
            }
        };
    }
}
