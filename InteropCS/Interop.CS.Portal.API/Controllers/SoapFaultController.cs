
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.Models;
using System.Collections.Generic;
using System.Web.Http;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using OfficeOpenXml;
namespace Interop.CS.Portal.API.Controllers
{
    [System.Web.Http.Authorize]
    public class SoapFaultController : ApiController
    {
        private readonly ISoapFaultRepository _soapFaultRepository;

        public SoapFaultController()
        {

        }

        public SoapFaultController(ISoapFaultRepository soapFaultRepository)
        {
            _soapFaultRepository = soapFaultRepository;
        }

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

        [System.Web.Http.HttpPost]
        public void InsertSoapFault(SoapFault soapFault)
        {
            _soapFaultRepository.InsertSoapFault(soapFault);
        }

        [System.Web.Http.HttpGet]
        public PagedCollection<SoapFault> GetSoapFaultListPaged(int pageIndex, int itemsPerPage, Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol)
        {
            return _soapFaultRepository.GetSoapFaultsPaged(pageIndex, itemsPerPage, filterTransaction, fromDateSoap, toDateSoap, sortDir, sortCol);
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CreatePdf(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol)
        {

            var context = HttpContext.Current;
            var contextBase = new HttpContextWrapper(context);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Template");
            var controllerContext = new ControllerContext(contextBase,
                routeData,
                new EmptyController());
            var soapfaults = _soapFaultRepository.GetFilteredSoapFaults(filterTransaction, fromDateSoap, toDateSoap, sortDir, sortCol);

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
        public HttpResponseMessage CreateExcel(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol)
        {

            HttpResponseMessage response;
            response = Request.CreateResponse(HttpStatusCode.OK);
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content = new StreamContent(GetExcelSheet(filterTransaction, fromDateSoap, toDateSoap, sortDir, sortCol));
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "Greshki.xlsx";
            return response;
        }

        public MemoryStream GetExcelSheet(Guid? filterTransaction, DateTime? fromDateSoap, DateTime? toDateSoap, string sortDir, string sortCol)
        {
            using (var package = new ExcelPackage())
            {
                var resultData = _soapFaultRepository.GetFilteredSoapFaults(filterTransaction, fromDateSoap, toDateSoap, sortDir, sortCol);
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
            protected override void ExecuteCore()
            {
            }
        };
    }
}
