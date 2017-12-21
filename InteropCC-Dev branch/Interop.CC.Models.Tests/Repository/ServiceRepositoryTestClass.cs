using System.Collections.Generic;
using System.Linq;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Models;
using Interop.CC.Models.Repository;
using Interop.CC.Models.UoW;
using Interop.CC.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interop.CC.Models.Tests.Repository
{
    [TestClass]
    public class ServiceRepositoryTestClass
    {
        private ServiceRepository _repository;
        private DataGenerator _dataGenerator;
        private IEnumerable<Service> _services;
            
        [TestInitialize]
        public void Init()
        {
            // IoC in EF
            var context = new TestContext();
            var uow = new UnitOfWork(context);
            _repository = new ServiceRepository(uow);
            _dataGenerator = new DataGenerator();
            _services = context.Services.AddRange(_dataGenerator.GenerateServices(10));
        }

        // Тест метод за успешно вчитување на Сервиси 
        [TestMethod]
        public void Repository_GetServices_Successfully()
        {
            Assert.AreEqual(_services.Count(), _repository.GetServices().Count());
        }

        // Тест метод за успешно вчитување на Сервис според код 
        [TestMethod]
        public void Repository_GetServiceByCode_Successfully()
        {
            var service = _dataGenerator.GenerateOneService();
            _repository.GetServiceByCode(service.Code);
        }

        // Тест метод за неуспешно вчитување на непостоечки Сервис според код
        [TestMethod]
        [ExpectedException(typeof(NotFoundServiceException))]
        public void Repository_GetServiceByCode_NotFoundServiceException()
        {
            _repository.GetServiceByCode("testServiceCode");
        }

        // Тест метод за успешно инсертирање на Сервис
        [TestMethod]
        public void Repository_InsertService_Successfully()
        {
            var buildService = _dataGenerator.GenerateOneService("TestCode", "TestName", "TestEndpoint", "TestWSDL");
            _repository.InsertService(buildService);
            Assert.AreEqual(_services.Count() + 1, _repository.GetServices().Count());
        }

        // Тест метод за неуспешно инсертирање на Сервис поради неговото постоење
        [TestMethod]
        [ExpectedException(typeof (DuplicateServiceException))]
        public void Repository_InsertService_DuplicateServiceException()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.InsertService(service);
        }

        // Тест метод за успешно вчитување на WSDL
        [TestMethod]
        public void Repository_GetWSDL_Successfully()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.GetWSDL(service.Code);
        }

        // Тест метод за неуспешно вчитување на непостоечки WSDL
        [TestMethod]
        [ExpectedException(typeof(NotFoundServiceException))]
        public void Repository_GetWSDL_NotFoundServiceException()
        {
            _repository.GetWSDL("testWSDL");
        }

        // Тест метод за успешно вчитување на нумерирана листа Сервиси 
        [TestMethod]
        public void Repository_GetServicesPaged_Successfully()
        {
            var builderCount = _services.ToList();
            var buildServices = _dataGenerator.GenerateOneService();
            _repository.GetServicesPaged(1, builderCount.Count, buildServices.Code, buildServices.Name, "asc", "name");
        }

    }
}
