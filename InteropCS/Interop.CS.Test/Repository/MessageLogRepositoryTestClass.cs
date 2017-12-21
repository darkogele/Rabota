using System;
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
    public class MessageLogRepositoryTestClass
    {
        private MessageLogRepository _repository;
        private DataGenerator _dataGenerator;
        private IEnumerable<MessageLog> _messageLogs;

        [TestInitialize]

        public void Init()
        {
            // IoC in EF
            var context = new TestContext();
            var uow = new UnitOfWork(context);
            _repository = new MessageLogRepository(uow);
            _dataGenerator = new DataGenerator();
            _messageLogs = context.MessageLogs.AddRange(_dataGenerator.GenerateMessageLogs(10));
        }

        //Опис: Тест метод за успешно вчитување на Логови
        [TestMethod]
        public void Repository_GetMessageLogs_Successfully()
        {
            Assert.AreEqual(_messageLogs.Count(), _repository.GetMessageLogs().Count());
        }

        //Опис: Тест метод за успешно вчитување на Лог според идентификациски број
        [TestMethod]
        public void Repository_GetMessageLogById_Successfully()
        {
            var buildMessageLog = _dataGenerator.GenerateOneMessageLog();
            _repository.GetMessageLogById(buildMessageLog.Id);

        }

        //Опис: Тест метод за неуспешно вчитување на непостоечки Лог
        [TestMethod]
        [ExpectedException(typeof(NotFoundMessageLogException))]
        public void Repository_GetMessageLogById_NotFoundMessageLogException()
        {
            _repository.GetMessageLogById(-1);
        }

        //Опис: Тест метод за успешно вчитување на Лог според трансакциски број
        [TestMethod]
        public void Repository_GetMessageLogByTransactionId_Successfully()
        {
            Guid request = new Guid("00000000-0000-0000-0000-000000000001");
            _repository.GetMessageLogByTransactionId(request, "Dir1");
        }

        //Опис: Тест метод за неуспешно вчитување на непостоечки Лог според трансакциски број
        [TestMethod]
        [ExpectedException(typeof(NotFoundMessageLogTransactionIdException))]
        public void Repository_GetMessageLogByTransactionId_NotFoundMessageLogTransactionIdException()
        {
            Guid request = new Guid("25867C0D-F2FB-6A6A-AC61-FF0000852B58");
            _repository.GetMessageLogByTransactionId(request, "Direction");
        }

        //Опис: Тест метод за успешно регистрирање на Лог 
        [TestMethod]
        public void Repository_InsertMessageLog_Successfully()
        {
            var messageLog = _dataGenerator.GenerateOneMessageLog();
            messageLog.Id = _messageLogs.Count()+1;
            _repository.InsertMessageLog(messageLog);
        }

        //Опис: Тест метод за неуспешно регистрирање на Лог 
        [TestMethod]
        [ExpectedException(typeof(DuplicateMessageLogException))]
        public void Repository_InsertMessageLog_DuplicateMessageLogException()
        {
            var messageLog = _dataGenerator.GenerateOneMessageLog();
            _repository.InsertMessageLog(messageLog);
        }

        //[TestMethod]
        //public void Repository_GetMessageLogsPaged_Successfully()
        //{
        //    var builderCount = _messageLogs.ToList();
        //    var buildMessageLogs = _dataGenerator.GenerateOneMessageLog();
        //    _repository.GetMessageLogsPaged(1, builderCount.Count, buildMessageLogs.Consumer, buildMessageLogs.Provider,buildMessageLogs.Dir,null,null);
        //}

    }
}
