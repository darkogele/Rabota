using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Interop.CS.Models.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.UoW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interop.CS.Models.Exceptions;

namespace Interop.CS.Models.Tests.Repository
{
    [TestClass]
    public class ServiceRepositoryTestClass
    {
        private ServiceRepository _repo;
        private IEnumerable<CSService> _services;
        [TestInitialize]
        public void Init()
        {
            var context = new TestContext();
            var uow = new UnitOfWork(context);
            _repo = new ServiceRepository(uow);
            _services = Builder<CSService>.CreateListOfSize(4).WhereAll().Build();
            context.Services.AddRange(_services);
        }

        //Опис: Метод за успешно вчитување на Сервис
        [TestMethod]
        public void Repository_GetService_Successfully()
        {
            _repo.GetService(_services.Take(1).FirstOrDefault().ParticipantCode, _services.Take(1).FirstOrDefault().Code);
        }

        //Опис: Метод за неуспешно вчитување на непостоечки Сервис
        [TestMethod]
        [ExpectedException(typeof(NotFoundCSServiceException))]
        public void Repository_GetService_NotFoundCSServiceException()
        {
            _repo.GetService("test", "test");
        }

        //Опис: Метод за успешно креирање на Сервис
        [TestMethod]
        public void Repository_CreateService_Successfully()
        {
            var service = Builder<CSService>.CreateNew().With(x => x.Code = "test").Build();
            _repo.CreateService(service);
            Assert.AreEqual(_services.Count() + 1, _repo.GetServices().Count());
        }

        //Опис: Метод за неуспешно креирање на Сервис поради постоење на ист
        [TestMethod]
        [ExpectedException(typeof(DuplicateCSServiceException))]
        public void Repository_CreateService_DuplicateParticipantException()
        {
            var service = _services.Take(1).FirstOrDefault();
            _repo.CreateService(service);
        }

        //Опис: Метод за успешно вчитување на WSDL 
        [TestMethod]
        public void Repository_GetWSDL_Successfully()
        {
            _repo.GetWSDL(_services.Take(1).FirstOrDefault().ParticipantCode, _services.Take(1).FirstOrDefault().Code);
        }

        //Опис: Метод за неуспешно вчитување на непостоечки WSDL  
        [TestMethod]
        [ExpectedException(typeof(NotFoundCSServiceException))]
        public void Repository_GetWSDL_ServiceNotFoundException()
        {
            _repo.GetWSDL("test", "test");
        }

        //Опис: Метод за успешно вчитување на листа на Сервиси 
        [TestMethod]
        public void Repository_GetServices_Successfully()
        {
            Assert.AreEqual(_services.Count(), _repo.GetServices().Count());
        }

        //Опис: Метод за успешно вчитување на нумерирана листа на Сервиси 
        [TestMethod]
        public void Repository_GetServicesPaged_Successfully()
        {
            var service = _services.ToList();
            _repo.GetServicesPaged(1, service.Count,"","");
        }

    }
}
