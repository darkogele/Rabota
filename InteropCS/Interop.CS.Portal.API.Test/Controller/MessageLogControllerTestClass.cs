using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FizzWare.NBuilder;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Portal.API.Controllers;
using Interop.CS.Models.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interop.CS.Portal.API.Test.Controller
{
    [TestClass]
    public class MessageLogControllerTestClass
    {
        private MessageLogController _controller;
        private List<MessageLogDTO> _messageLogsDTO;
        private IReadOnlyCollection<MessageLog> _messageLogs;
        private Mock<IMessageLogRepository> _repository;

        [TestInitialize]
        public void Init()
        {
            _messageLogs = Builder<MessageLog>.CreateListOfSize(10).All().Build().ToList();
            _messageLogsDTO = Builder<MessageLogDTO>.CreateListOfSize(5).All().Build().ToList();
            _repository = new Mock<IMessageLogRepository>();
            _controller = new MessageLogController(_repository.Object);
        }

        //Опис: Тест метод за успешно пронаоѓање на лог според Id на трансакција
        [TestMethod]
        public void Controller_GetMessageLogByTransactionId_Successfully()
        {
            var messageLog = _messageLogs.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetMessageLogByTransactionId(messageLog.TransactionId, messageLog.Dir)).Returns(messageLog);
            Assert.AreEqual(messageLog.Id, _controller.GetMessageLogByTid(messageLog.TransactionId, messageLog.Dir).Id);
        }

        //Опис: Тест метод за неуспешно пронаоѓање на лог според Id на трансакција
        //Тука се очекува да се јави грешка за неуспешно пронаоѓање на лог
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetMessageLogByTransactionId_NotFoundMessageLogException()
        {
            var messageLog = _messageLogs.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetMessageLogByTransactionId(messageLog.TransactionId, messageLog.Dir)).Throws(new NotFoundMessageLogTransactionIdException(messageLog.TransactionId, messageLog.Dir));
            _controller.GetMessageLogByTid(messageLog.TransactionId, messageLog.Dir);
        }

        //Опис: Тест метод за успешно преземање на сите логови
        [TestMethod]
        public void Controller_GetMessageLogList_Successfully()
        {
            _repository.Setup(m => m.GetMessageLogs()).Returns(_messageLogs);
            Assert.AreEqual(_messageLogs.Count(), _controller.GetMessageLogList().Count());
        }

        //[TestMethod]
        //public void Controller_GetMessageLogListPaged_Successfully()
        //{
        //    _repository.Setup(m => m.GetMessageLogsPaged(1, 1, "", "", "", null, null)).Returns(new PagedCollection<MessageLog>(1, 1, 1, _messageLogs));
        //    Assert.AreEqual(_messageLogs.Count(), _controller.GetMessageLogListPaged(1, 1,"","","",null,null).Items.Count);
        //}

    }
}
