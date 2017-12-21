using System.Collections.Generic;
using System.Linq;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.UoW;
using Interop.CS.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interop.CS.Models.Tests.Repository
{
    [TestClass]
    public class AccessMappingRepositoryTestClass
    {
        private AccessMappingRepository _repo;
        private DataGenerator _dataGenerator;
        private IEnumerable<AccessMapping> _accessMappings;

        [TestInitialize]
        public void Init()
        {
            var context = new TestContext(); 
            var uow = new UnitOfWork(context);
            _repo = new AccessMappingRepository(uow);
            _dataGenerator = new DataGenerator();
            _accessMappings = context.AccessMappings.AddRange(_dataGenerator.GenerateAccessMappings(10));
        }
        //Опис: Тест метод за неуспешно креирање на запис во Пристапна листа поради постоење на ист
        [TestMethod]
        [ExpectedException(typeof(DuplicateAccessMappingException))]
        public void Repository_CreateAccessMapping_DuplicateAccessMappingException()
        {
            var accessMapping = _dataGenerator.GenerateOneAccessMapping("ConsumerCode2", "ProviderCode2", "ServiceCode2",
                "MethodCode2", "ProviderBusCode2", "ConsumerBusCode2", true);
            _repo.CreateAccessMapping(accessMapping);
        }

        //Опис: Тест метод за успешно креирање на запис во Пристапна листа
        [TestMethod]
        public void Repository_CreateAccessMapping_Successfully()
        {
            var accessMapping = _dataGenerator.GenerateOneAccessMapping("Cons1", "Prov1", "Serv1");
            _repo.CreateAccessMapping(accessMapping);
            Assert.AreEqual(_accessMappings.Count() + 1, _repo.GetAccessMappings().Count());
        }

        //[TestMethod]
        //[ExpectedException(typeof(NotFoundAccessMappingException))]
        //public void Repository_DeleteAccessMapping_NotFoundAccessMappingException()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping("Cons1", "Prov1", "Serv1");
        //    _repo.DeleteAccessMapping(accessMapping.ConsumerCode, accessMapping.ProviderCode, accessMapping.ServiceCode);
        //}

        //[TestMethod]
        //public void Repository_DeleteAccessMapping_Successfully()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping();
        //    _repo.DeleteAccessMapping(accessMapping.ConsumerCode, accessMapping.ProviderCode, accessMapping.ServiceCode);
        //    Assert.AreEqual(_accessMappings.Count() - 1, _repo.GetAccessMappings().Count());
        //}

        //[TestMethod]
        //public void Repository_GetAccessMappings_Successfully()
        //{
        //    Assert.AreEqual(_accessMappings.Count(), _repo.GetAccessMappings().Count());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NotFoundAccessMappingException))]
        //public void Repository_GetAccessMapping_NotFoundAccessMappingException()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping("Cons1", "Prov1", "Serv1");
        //    _repo.GetAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode);
        //}

        //[TestMethod]
        //public void Repository_GetAccessMapping_Successfully()
        //{
        //    var accessMapping = _dataGenerator.GenerateOneAccessMapping();
        //    var actualAccessMapping = _repo.GetAccessMapping(accessMapping.ProviderCode, accessMapping.ConsumerCode, accessMapping.ServiceCode);
        //    Assert.AreEqual(accessMapping.ConsumerCode, actualAccessMapping.ConsumerCode);
        //    Assert.AreEqual(accessMapping.ProviderCode, actualAccessMapping.ProviderCode);
        //    Assert.AreEqual(accessMapping.ServiceCode, actualAccessMapping.ServiceCode);
        //}

        //Опис: Тест метод за успешно вчитување на нумерирани записи од Пристапната листа
        [TestMethod]
        public void Repository_GetAccessMappingPaged_Successfully()
        {
            var builderCount = _accessMappings.ToList();
            var buildAccessMapping = _dataGenerator.GenerateOneAccessMapping("ConsumerCode", "ProviderCode", "ServiceCode");
            _repo.GetAccessMappingsPaged(1, builderCount.Count, buildAccessMapping.ProviderCode, buildAccessMapping.ConsumerCode, buildAccessMapping.ServiceCode, buildAccessMapping.IsActive);
        }

    }
}
