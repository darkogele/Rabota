using System;
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
    public class MessageLogRepositoryTestClass
    {
        private MessageLogsRepository _repository;
        private DataGenerator _dataGenerator;
        private IEnumerable<MessageLog> _messageLogs;

        [TestInitialize]

        public void Init()
        {
            // IoC in EF
            var context = new TestContext();
            var uow = new UnitOfWork(context);
            _repository = new MessageLogsRepository(uow);
            _dataGenerator = new DataGenerator();
            _messageLogs = context.MessageLogs.AddRange(_dataGenerator.GenerateMessageLogs(10));
        }

        // Тест метод за успешно вчитување на Логови
        [TestMethod]
        public void Repository_GetMessageLogs_Successfully()
        {
            Assert.AreEqual(_messageLogs.Count(), _repository.GetMessageLogs().Count());
        }

        // Тест метод за успешно вчитување на Лог според неговиот идентификациски број
        [TestMethod]
        public void Repository_GetMessageLogById_Successfully()
        {
            var buildMessageLog = _dataGenerator.GenerateOneMessageLog();
            _repository.GetMessageLogById(buildMessageLog.Id);

        }

        // Тест метод за неуспешно вчитување на непостоечки Лог според идентификациски број
        [TestMethod]
        [ExpectedException(typeof(NotFoundMessageLogException))]
        public void Repository_GetMessageLogById_NotFoundMessageLogException()
        {
            _repository.GetMessageLogById(-1);
        }

        // Тест метод за успешно вчитување на Лог според неговиот трансакциски број  
        [TestMethod]
        public void Repository_GetMessageLogByTransactionId_Successfully()
        {
            Guid request = new Guid("00000000-0000-0000-0000-000000000001");
            _repository.GetMessageLogByTransactionId(request);
        }

        // Тест метод за неуспешно вчитување на непостоечки Лог според трансакциски број  
        [TestMethod]
        [ExpectedException(typeof(NotFoundMessageLogTransactionIdException))]
        public void Repository_GetMessageLogByTransactionId_NotFoundMessageLogTransactionIdException()
        {
            Guid request = new Guid("25867C0D-F2FB-6A6A-AC61-FF0000852B58");
            _repository.GetMessageLogByTransactionId(request);
        }

        // Тест метод за успешно инсертирање на Лог 
        [TestMethod]
        public void Repository_InsertMessageLog_Successfully()
        {
            var messageLog = _dataGenerator.GenerateOneMessageLog();
            messageLog.Id = _messageLogs.Count()+1;
            _repository.InsertMessageLog(messageLog);
        }

        // Тест метод за неуспешно инсертирање на Лог поради постоење на истиот  
        [TestMethod]
        [ExpectedException(typeof(DuplicateMessageLogException))]
        public void Repository_InsertMessageLog_DuplicateMessageLogException()
        {
            var messageLog = _dataGenerator.GenerateOneMessageLog();
            _repository.InsertMessageLog(messageLog);
        }

    }
}
