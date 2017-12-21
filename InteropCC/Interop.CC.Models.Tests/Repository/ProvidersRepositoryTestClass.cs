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
    public class ProvidersRepositoryTestClass
    {
        private ProvidersRepository _repository;
        private DataGenerator _dataGenerator;
        private IEnumerable<Provider> _providers;

        [TestInitialize]

        public void Init()
        {
            // IoC in EF
            var context = new TestContext();
            var uow = new UnitOfWork(context);
            _repository = new ProvidersRepository(uow);
            _dataGenerator = new DataGenerator();
            _providers = context.Providers.AddRange(_dataGenerator.GenerateProviders(10));
        }

        // Тест метод за успешно вчитување на Провајдери 
        [TestMethod]
        public void Repository_GetProviders_Successfully()
        {
            Assert.AreEqual(_providers.Count(), _repository.GetProviders().Count());
        }

        // Тест метод за успешно вчитување на Јавен клуч
        [TestMethod]
        public void Repository_GetPublicKey_Successfully()
        {
            var buildProvider = _dataGenerator.GenerateOneProvider();
            _repository.GetPublicKey(buildProvider.RoutingToken);

        }

        // Тест метод за неуспешно вчитување на непостоечки Јавен клуч
        [TestMethod]
        [ExpectedException(typeof(NotFoundPublicKeyException))]
        public void Repository_GetPublicKey_NotFoundProviderException()
        {
            _repository.GetPublicKey("test");
        }

    }
}
