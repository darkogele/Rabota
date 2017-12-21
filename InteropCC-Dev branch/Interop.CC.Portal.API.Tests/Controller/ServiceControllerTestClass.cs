using System.Collections.Generic;
using System.Linq;
using System.Web;
using FizzWare.NBuilder;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Portal.API.Controllers;
using Interop.CC.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interop.CC.Portal.API.Tests.Controller
{
    [TestClass]
    public class ServiceControllerTestClass
    {
        private ServiceController _controller;
        private List<Service> _services;
        private List<ServiceDTO> _servicesDTO;
        private DataGenerator _dataGenerator;
        private Mock<IServiceRepository> _repository;

        [TestInitialize]
        public void Init()
        {
            _services = new List<Service>();
            _servicesDTO = Builder<ServiceDTO>.CreateListOfSize(5).Build().ToList();
            _dataGenerator = new DataGenerator();
            _services.AddRange(_dataGenerator.GenerateServices(10));
            _repository = new Mock<IServiceRepository>();
            _controller = new ServiceController(_repository.Object);
        }

        // Тест метод за успешно вчитување на Сервиси
        [TestMethod]
        public void Controller_GetServiceList_Successfully()
        {
            _repository.Setup(m => m.GetServices()).Returns(_services);
            Assert.AreEqual(_services.Count(), _controller.GetServiceList().Count());
        }

        // Тест метод за успешно вчитување на WSDL 
        [TestMethod]
        public void Controller_GetWSDL_Successfully()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetWSDL(service.Code)).Returns(service.Wsdl);
            Assert.AreEqual(service.Wsdl, _controller.GetWSDL(service.Code));
        }

        // Тест метод за неуспешно вчитување на непостоечки WSDL 
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetWSDL_NotFoundServiceException()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetWSDL(service.Code)).Throws(new NotFoundServiceException(service.Code));
            _controller.GetWSDL(service.Code);
        }

        // Тест метод за успешно вчитување на нумерирана листа од Сервиси 
        [TestMethod]
        public void Controller_GetServiceListPaged_Successfully()
        {
            _repository.Setup(m => m.GetServicesPaged(1, 1, "asc","name", "", "")).Returns(new PagedCollection<ServiceDTO>(1, 1, 1, _servicesDTO));
            Assert.AreEqual(_servicesDTO.Count(), _controller.GetServiceListPaged(1, 1,"asc","name").Items.Count);
        }

    }
}
