using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Hosting;
using Interop.ExternalCC.HandlersHelper.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class RequestExtensionMethodsTest
    {
        private HttpRequest _request;
        private HttpResponse _response;
        private HttpContext _context;
        private IRequestExtensionMethods _requestExtensionMethods;

        [TestInitialize]
        public void SetUp()
        {
            _request = new HttpRequest("D:\\Projects\\Interop\\InteropExternalCC\\Interop.ExternalCC.InternalHandler\\", "http://localhost/Interop.ExternalCC.InternalHandler", "");
            _response = new HttpResponse(new StringWriter());
            _context = new System.Web.HttpContext(_request, _response);
        }

        

        //[TestMethod]
        //public void GetSoapAction_Successfully()
        //{
        //    var soapAction = _requestExtensionMethods.GetSoapAction(_context);
        //    Assert.AreEqual("", soapAction);
        //}
    }
}
