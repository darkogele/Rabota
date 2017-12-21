using System;
using System.Linq;
using Interop.CC.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interop.CC.IntegrationTests.TestClasses
{
    [TestClass]
    public class MetaServicesIntegrationTestClass
    {
        private MetaServiceReference.MetaServiceClient _csMetaServicesClient;

        [TestInitialize]
        public void SetUp()
        {
            _csMetaServicesClient = new MetaServiceReference.MetaServiceClient("BasicHttpBinding_IMetaService");
        }

        // Тест метод за успешно регистрирање на сервис
        [TestMethod]
        public void Register_Service_Successfully()
        {
            Guid code = Guid.NewGuid();
            var ser = new Service {Code = code.ToString(), Wsdl = code.ToString()};
            _csMetaServicesClient.RegisterService(ser);
        }

        // Тест метод за неуспешно регистрирање на постоечки сервис
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void Register_Service_UnsuccessfullyFirst()
        {
            var ser = new Service { Code = "IntegrationTestCode", Wsdl = "IntegrationTestWsdl" };
            _csMetaServicesClient.RegisterService(ser);
        }

        // Тест метод за успешно вчитување на сервис со користење на пристапна листа
        [TestMethod]
        public void Get_Services_With_AccessMapping()
        {
            var k = _csMetaServicesClient.GetServices("ZEM");
            Assert.AreNotEqual(0, k.Count());
        }

        // Тест метод за успешно вчитување на сервис без користење на пристапна листа
        [TestMethod]
        public void Get_Services_Without_AccessMapping()
        {
            var k = _csMetaServicesClient.GetServices("");
            Assert.AreEqual(0, k.Count());
        }

        // Тест метод за успешно вчитување на сервис
        [TestMethod]
        public void Get_Service_Successfully()
        {
            var k = _csMetaServicesClient.GetService("ZEM", "ServiceA", "");
            Assert.AreEqual("/", k);
        }

        // Тест метод за неуспешно вчитување на непостоечки сервис 
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void Get_Service_Unsuccessfully()
        {
            var k = _csMetaServicesClient.GetService("ZEM", "", "");
        }

        // Тест метод за успешно вчитување на листа на консумери со користење на пристапна листа 
        [TestMethod]
        public void List_Consumers_With_AccessMapping()
        {
            var k = _csMetaServicesClient.ListConsumers("ServiceA");
            Assert.AreNotEqual(0, k.Count());
        }

        // Тест метод за успешно вчитување на листа на консумери без користење на пристапна листа
        [TestMethod]
        public void List_Consumers_Without_AccessMapping()
        {
            var k = _csMetaServicesClient.ListConsumers("");
            Assert.AreEqual(0, k.Count());
        }

        // Тест метод за неуспешно регистрирање на непостоечки сервис
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void Unregister_Service_UnSuccessfully()
        {
            var serviceId = "";
            _csMetaServicesClient.UnRegisterService(serviceId);
        }

        // Тест метод за успешно одрегистрирање на сервис
        [TestMethod]
        public void Unregister_Service_Successfully()
        {
            var code = "IntegrationTestCode";
            _csMetaServicesClient.UnRegisterService(code);
        }

        // Тест метод за успешно вчитување на листа со провајдери
        [TestMethod]
        public void GetProviders()
        {
            var s = _csMetaServicesClient.GetProviders();
            Assert.AreNotEqual(0,s.Count());
        }

        // Тест метод за успешна проверка на состојба со трансакциски број
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void Check_State_By_TransactionId()
        {
            _csMetaServicesClient.CheckStateByTransactionId("");
        }

        // Тест метод за успешно вчитување на Лог со трансакциски број 
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void Get_Message_By_TransactionId()
        {
            _csMetaServicesClient.GetMessageByTransactionId("");
        }

        // Тест метод за успешно постирање на порака
        [TestMethod]
        [ExpectedException(typeof(System.ServiceModel.FaultException))]
        public void Post_Message()
        {
            _csMetaServicesClient.PostMessage("", "");
        }
    }
}
