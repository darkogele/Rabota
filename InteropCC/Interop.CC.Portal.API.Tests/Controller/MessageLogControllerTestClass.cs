using System.Collections.Generic;
using System.Linq;
using System.Web;
using FizzWare.NBuilder;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Portal.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;

namespace Interop.CC.Portal.API.Tests.Controller
{
    [TestClass]
    public class MessageLogControllerTestClass
    {
        private MessageLogController _controller;
        private IReadOnlyCollection<MessageLog> _messageLogs;
        private List<MessageLogPairsViewModelDetails> _messageLogPairsDetails; 
        private Mock<IMessageLogsRepository> _repository;

        [TestInitialize]
        public void Init()
        {
            _messageLogs = Builder<MessageLog>.CreateListOfSize(10).All().Build().ToList();
            _messageLogPairsDetails =
                Builder<MessageLogPairsViewModelDetails>.CreateListOfSize(5).All().Build().ToList();
            _repository = new Mock<IMessageLogsRepository>();
            _controller = new MessageLogController(_repository.Object);
        }

        // Тест метод за вчитување на Лог според трансакциски број
        [TestMethod]
        public void Controller_GetMessageLogByTransactionId_Successfully()
        {
            var messageLogDetails = _messageLogPairsDetails.Take(1).First();
            var messageLog = _messageLogs.Take(1).First();
            //messageLogDetails.Request = messageLog;
            //messageLogDetails.Response = messageLog;
            _repository.Setup(m => m.GetMessageLogByTransactionId(messageLogDetails.Request.TransactionId)).Returns(messageLogDetails);
            
                Assert.AreEqual(messageLogDetails.Request.TransactionId, _controller.GetMessageLogByTid(messageLogDetails.Request.TransactionId).Request.TransactionId);
        }

        // Тест метод за неуспешно вчитување на непостоечки Лог според трансакциски број
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetMessageLogByTransactionId_NotFoundMessageLogException()
        {
            var messageLog = _messageLogs.Take(1).FirstOrDefault();
            if (messageLog != null)
            {
                _repository.Setup(m => m.GetMessageLogByTransactionId(messageLog.TransactionId)).Throws(new NotFoundMessageLogTransactionIdException(messageLog.TransactionId));
                _controller.GetMessageLogByTid(messageLog.TransactionId);
            }
        }

        // Тест метод за вчитување на листа од Логови
        [TestMethod]
        public void Controller_GetMessageLogList_Successfully()
        {
            _repository.Setup(m => m.GetMessageLogs()).Returns(_messageLogs);
            Assert.AreEqual(_messageLogs.Count(), _controller.GetMessageLogList().Count());
        }
    }
}
