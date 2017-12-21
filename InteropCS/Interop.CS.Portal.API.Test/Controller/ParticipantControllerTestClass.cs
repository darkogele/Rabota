using System.Collections.Generic;
using System.Linq;
using System.Web;
using FizzWare.NBuilder;
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
    public class ParticipantControllerTestClass
    {
        private ParticipantController _controller;
        private List<Participant> _participants;         
        private Mock<IParticipantRepository> _repository;

        [TestInitialize]
        public void Init()
        {
            _participants = Builder<Participant>.CreateListOfSize(4).All().Do(x => x.IsActive = true).Build().ToList();
            _repository = new Mock<IParticipantRepository>();
            _controller = new ParticipantController(_repository.Object);
        }

        //Опис: Тест метод за успешно креирање на учесник
        [TestMethod]
        public void Controller_CreateParticipant_Successfully()
        {
            var participant = Builder<Participant>.CreateNew().Build();
            _controller.CreateParticipant(participant);
        }

        //Опис: Тест метод за креирање на нов учесник што веќе постои
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_CreateParticipant_DuplicateParticipantException()
        {
            var participant = Builder<Participant>.CreateNew().Build();
            _repository.Setup(m => m.CreateParticipant(participant)).Throws(new DuplicateParticipantException(participant));
            _controller.CreateParticipant(participant);
           
        }

        //Опис: Тест метод за успешно ажурирање на податоците на учесник
        [TestMethod]
        public void Controller_UpdateParticipant_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _controller.UpdateParticipant(participant);
        }

        //Опис: Тест метод за неуспешно ажурирање на податоците на учесник
        // Се очекува грешка дека таков учесник не е пронајден
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_UpdateParticipant_NotFoundParticipantException()
        {
            var participant = Builder<Participant>.CreateNew().With(x => x.Code = "TestCode").Build();
            participant.IsActive = false;
            _repository.Setup(m => m.UpdateParticipant(participant)).Throws(new NotFoundParticipantException(participant.Code));
            _controller.UpdateParticipant(participant);
        }

        //Опис: Тест метод за успешно бришење на учесник
        [TestMethod]
        public void Controller_DeleteParticipant_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _controller.DeleteParticipant(participant.Code);

        }

        //Опис: Тест метод за неуспешно бришење на учесник
        //Се очекува грешка дека таков учесник не е пронајден
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_DeleteParticipant_NotFoundParticipantException()
        {
            var participant = Builder<Participant>.CreateNew().With(x => x.Code = "TestCode").Build();
            _repository.Setup(m => m.DeleteParticipant(participant.Code)).Throws(new NotFoundParticipantException(participant.Code));
            _controller.DeleteParticipant(participant.Code);
        }

        //Опис: Тест метод за успешно пронаоѓање на учесник
        [TestMethod]
        public void Controller_GetParticipantList_Successfully()
        {
            _repository.Setup(m => m.GetParticipants()).Returns(_participants);
            Assert.AreEqual(_participants.Count(), _controller.GetParticipantList().Count());

        }
        //[TestMethod]
        //public void Controller_GetExternalParticipants_Successfully()
        //{
        //    _repository.Setup(m => m.GetExternalParticipants()).Returns(_participants);
        //    Assert.AreEqual(_participants.Count(), _controller.GetExternalParticipants().Count());
        //}

        //Опис: Тест метод за успешно пронаоѓање на сите интерни учесници
        [TestMethod]
        public void Controller_GetInternalParticipants_Successfully()
        {
            _repository.Setup(m => m.GetInternalParticipants(true)).Returns(_participants);
            Assert.AreEqual(_participants.Count(), _controller.GetInternalParticipants().Count());
        }

        //Опис: Тест метод за успешно пронаоѓање на јавен клуч за одреден корисник
        [TestMethod]
        public void Controller_GetPublicKey_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetPublicKey(participant.Code)).Returns(participant.PublicKey);
            Assert.AreEqual(participant.PublicKey, _controller.GetPublicKey(participant.Code));
        }

        //Опис: Тест метод за неуспешно пронаоѓање на јавен клуч за одреден корисник
        //Се очекува грешка дека таков учесник не е пронајден
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetPublicKey_NotFoundParticipantException()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetPublicKey(participant.Code)).Throws(new NotFoundPublicKeyException(participant.Code));
            _controller.GetPublicKey(participant.Code);
        }

        //Опис: Тест метод за успешно пронаоѓање на учесник 
        [TestMethod]
        public void Controller_GetParticipant_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _repository.Setup(m => m.GetPublicKey(participant.Code)).Returns(participant.Code);
            _controller.GetParticipant(participant.Code);

        }

        //Опис: Тест метод за неуспешно пронаоѓање на учесник 
        //Се очекува грешка дека таков учесник не постои
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void Controller_GetParticipant_NotFoundParticipantException()
        {
            var participant = Builder<Participant>.CreateNew().With(x => x.Code = "TestCode").Build();
            _repository.Setup(m => m.GetParticipant(participant.Code)).Throws(new NotFoundParticipantException(participant.Code));
            _controller.GetParticipant(participant.Code);
            
        }
        //Опис: Тест метод за успешно листање на сите учесници со пагинапција
        [TestMethod]
        public void Controller_GetParticipantsPaged_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            //_repository.Setup(m => m.GetParticipantsPaged(1, 1, participant.Code)).Returns(new PagedCollection<Participant>(1, 1, 1, _participants));
            //Assert.AreEqual(_participants.Count(), _controller.GetParticipantListPaged(1, 1,'sortDir','sortCol', participant.Code).Items.Count);

        }

    }
}
