using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using EvalServiceLibrary;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interop.CC.IntegrationTests.TestClasses
{
    [TestClass]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EvalServiceIntegrationTestClass
    {
        private EvalServiceReference.EvalServiceClient _csEvalServiceClient;
        private IEnumerable<Eval> _evalservice;

        [TestInitialize] 
        public void SetUp()
        {
            _csEvalServiceClient = new EvalServiceReference.EvalServiceClient("BasicHttpBinding_IEvalService");
        }

        // Тест метод за успешно вчитување на Eval (тестни) сервиси
        [TestMethod]
        public void Get_Eval_Successfully()
        {
            _csEvalServiceClient.GetEvals();
        }

        // Тест метод за успешно инсертирање на Eval (тестен) сервис
        [TestMethod]
        public void Add_Eval_Successfully()
        {
            var evalsCountBeforeAdd = _csEvalServiceClient.GetEvals().Count();

            var evalservice = Builder<Eval>.CreateNew().With(x => x.Id = Guid.NewGuid().ToString()).Build();
            _csEvalServiceClient.SubmitEval(evalservice);

            var evalsCount = _csEvalServiceClient.GetEvals().Count();

            Assert.AreEqual(evalsCountBeforeAdd, evalsCount - 1);
        }

        // Тест метод за неуспешно инсертирање на постоечки Eval (тестен) сервиси 
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Add_Eval_throws_DuplicateEvalException()
        {
            var evalservice = _csEvalServiceClient.GetEvals().FirstOrDefault();
            _csEvalServiceClient.SubmitEval(evalservice);
        }

        // Тест метод за успешно бришење на Eval (тестен) сервис 
        [TestMethod]
        public void Remove_Eval_Successfully()
        {
            var evalsbefore = _csEvalServiceClient.GetEvals().ToList();

            var evalService = evalsbefore.FirstOrDefault();
            _csEvalServiceClient.RemoveEval(evalService.Id);

            var evals = _csEvalServiceClient.GetEvals().Count();

            Assert.AreEqual(evalsbefore.Count, evals + 1);

        }

        // Тест метод за неуспешно бришење на непостоечки Eval (тестен) сервис
        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Remove_Eval_throws_NotFoundException()
        {
            var evalId = Guid.NewGuid().ToString();
            _csEvalServiceClient.RemoveEval(evalId);
        }

    }
}
