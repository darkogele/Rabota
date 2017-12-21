using System.Collections.Generic;
using System.Linq;
using System.Web;
using FizzWare.NBuilder;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Portal.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interop.CS.Portal.API.Test.Controller
{
    [TestClass]
    public class ServiceControllerTestClass
    {
        private ServiceController _controller;
        private List<CSService> _services;
        private List<ServiceDTO> _servicesDTO;
        private Mock<IServiceRepository> _repository;
        private Mock<IParticipantRepository> _repositoryParticipant;

        [TestInitialize]
        public void Init()
        {
            _services = Builder<CSService>.CreateListOfSize(3).Build().ToList();
            _servicesDTO = Builder<ServiceDTO>.CreateListOfSize(5).Build().ToList();
            _repository = new Mock<IServiceRepository>();
            _controller = new ServiceController(_repository.Object, _repositoryParticipant.Object);
        }

        //Опис: Тест метод за успешно листање на сите сервиси
        [TestMethod]
        public void Controller_GetServiceList_Successfully()
        {
            _repository.Setup(m => m.GetServices()).Returns(_services);
            Assert.AreEqual(_services.Count(), _controller.GetServiceList().Count());
        }

        //Опис: Тест метод за успешно пронаоѓање на сервис
        [TestMethod]
        public void Controller_GetService_Successfully()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetService(service.Code, service.ParticipantCode)).Returns(service);
            Assert.AreEqual(service, _controller.GetService(service.Code, service.ParticipantCode));
        }

        //Опис: Тест метод за неуспешно пронаоѓање на сервис
        //Се очекува грешка дека тој сервис не постои
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetService_NotFoundCSServiceException()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetService(service.Code, service.ParticipantCode)).Throws(new NotFoundCSServiceException(service.ParticipantCode, service.Code));
            _controller.GetService(service.Code, service.ParticipantCode);
        }

        //Опис: Тест метод за успешно листање на сите сервиси со пагинапција
        [TestMethod]
        public void Controller_GetServiceListPaged_Successfully()
        {
            _repository.Setup(m => m.GetServicesPaged(1, 1,"","")).Returns(new PagedCollection<ServiceDTO>(1, 1, 1, _servicesDTO));
            Assert.AreEqual(_servicesDTO.Count(), _controller.GetServiceListPaged(1, 1,"","").Items.Count);
        }

        //Опис: Тест метод за успешно пронаоѓање на WSDl според код на сервис и учесник
        [TestMethod]
        public void Controller_GetWSDL_Successfully()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetWSDL(service.Code, service.ParticipantCode)).Returns(service.Wsdl);
            Assert.AreEqual(service.Wsdl, _controller.GetWSDL(service.Code, service.ParticipantCode));
        }

        //Опис: Тест метод за неуспешно пронаоѓање на WSDl според код на сервис и учесник
        //Се очекува грешка дека сервисот не постои
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetWSDL_NotFoundCSServiceException()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetWSDL(service.ParticipantCode, service.Code)).Throws(new NotFoundCSServiceException(service.ParticipantCode, service.Code));
            _controller.GetWSDL(service.ParticipantCode, service.Code);
        }

        //Опис: Тест метод за неуспешно лситање на сервисите според код на учесник
        [TestMethod]
        public void Controller_GetListByParticipantCode_Successfully()
        {
            _repository.Setup(m => m.GetServices()).Returns(_services.Where(x => x.ParticipantCode == _services.Take(1).FirstOrDefault().ParticipantCode));
            Assert.AreEqual(_services.Where(x => x.ParticipantCode == _services.Take(1).FirstOrDefault().ParticipantCode).Count(), _controller.GetListByParticipantCode(_services.Take(1).FirstOrDefault().ParticipantCode).Count());
        }


    }
}
