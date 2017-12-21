using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.NBuilder;
using Interop.CS.Portal.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interop.CS.Portal.API.Test.Controller
{
    [TestClass]
    public class AccessMappingControllerTestClass
    {
        private AccessMappingController _controller;
        private List<AccessMapping> _accessMappings;
        private DataGenerator _dataGenerator;
        private Mock<IAccessMappingRepository> _repository;


        [TestInitialize]
        public void Init()
        {
            _accessMappings = new List<AccessMapping>();
            _dataGenerator = new DataGenerator();
            _accessMappings.AddRange(_dataGenerator.GenerateAccessMappings(10));
            _repository = new Mock<IAccessMappingRepository>();
            //_controller = new AccessMappingController(_repository.Object);
        }

        //Опис: Тест метод за успешно креирање на запис во пристапната листа
        //[TestMethod]
        //public void Controller_CreateAccessMapping_Successfully()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping("Cons1", "Prov1", "Serv1");
        //    _controller.CreateAccessMapping(accessMapping);
        //}

        //Опис: Тест метод за креирање на запис во пристапната листа што веќе постои
        //[TestMethod]
        //[ExpectedException(typeof(HttpException))]
        //public void Controller_CreateAccessMapping_DuplicateAccessMappingException()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping();
        //    _repository.Setup(m => m.CreateAccessMapping(accessMapping)).Throws(new DuplicateAccessMappingException(accessMapping));
        //    _controller.CreateAccessMapping(accessMapping);
        //}

        //Опис: Тест метод за успешно преземање на сите записи од пристапната листа
        [TestMethod]
        public void Controller_GetAccessMappingList_Successfully()
        {
            _repository.Setup(m => m.GetAccessMappings()).Returns(_accessMappings);
            Assert.AreEqual(_accessMappings.Count(), _controller.GetAccessMappingList().Count());
        }

        //[TestMethod]
        //public void Controller_GetAccessMapping_Successfully()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping();
        //    _repository.Setup(m => m.GetAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode)).Returns(accessMapping);
        //    Assert.AreEqual(accessMapping, _controller.GetAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpException))]
        //public void Controller_GetAccessMapping_NotFoundAccessMappingException()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping("Cons1", "Prov1", "Serv1");
        //    _repository.Setup(m => m.GetAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode)).Throws(new NotFoundAccessMappingException(accessMapping.ConsumerCode, accessMapping.ProviderCode, accessMapping.ServiceCode));
        //    _controller.GetAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(HttpException))]
        //public void Controller_DeleteAccessMapping_NotFoundAccessMappingExceptionn()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping("Cons1", "Prov1", "Serv1");
        //    _repository.Setup(m => m.DeleteAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode)).Throws(new NotFoundAccessMappingException(accessMapping.ConsumerCode, accessMapping.ProviderCode, accessMapping.ServiceCode));
        //    _controller.DeleteAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode);
        //}

        //[TestMethod]
        //public void Controller_DeleteAccessMapping_Successfully()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping();
        //    _controller.DeleteAccessMapping(accessMapping.ConsumerCode, accessMapping.ProviderCode, accessMapping.ServiceCode);
        //}

        //Опис: Тест метод за успешно листање на сите записи од пристапната листа со пагинапција
        [TestMethod]
        public void Controller_GetAccessMappingListPaged_Successfully()
        {
            var accessMapping = _dataGenerator.GenerateOneAccessMapping();
            _repository.Setup(m => m.GetAccessMappingsPaged(1, 1, accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode, accessMapping.IsActive)).Returns(new PagedCollection<AccessMapping>(1, 1, 1, _accessMappings));
            Assert.AreEqual(_accessMappings.Count(), _controller.GetAccessMappingListPaged(1, 1, accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode, accessMapping.IsActive).Items.Count);

        }

    }
}
