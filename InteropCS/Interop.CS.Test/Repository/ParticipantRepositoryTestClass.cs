using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Models;
using Interop.CS.Models.Repository;
using Interop.CS.Models.Tests.HelperException;
using Interop.CS.Models.UoW;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interop.CS.Models.Tests.Repository
{
    [TestClass]
    public class ParticipantRepositoryTestClass
    {
        private ParticipantRepository _repo;
        private IEnumerable<Participant> _participants;
        [TestInitialize]
        public void Init()
        {
            // IoC in EF
            var context = new TestContext();
            var uow = new UnitOfWork(context);
            _repo = new ParticipantRepository(uow);

            // arrange data for all tests
            _participants = Builder<Participant>.CreateListOfSize(4).WhereAll().Do(x => x.IsActive = true).Build().ToList();
            context.Participants.AddRange(_participants);
        }

        //Опис: Тест метод за успешно креирање на Учесник
        [TestMethod]
        public void Repository_CreateParticipant_Successfully()
        {
            // arrange 
            var participant = Builder<Participant>.CreateNew().With(x => x.Code = "TestCode").Build();

            // act
            _repo.CreateParticipant(participant);
            Assert.AreEqual(_participants.Count() + 1, _repo.GetParticipants().Count());
        }

        //Опис: Тест метод за неуспешно креирање на Учесник поради постоење на ист
        [TestMethod]
        [ExpectedException(typeof(DuplicateParticipantException))]
        public void Repository_CreateParticipant_DuplicateParticipantException()
        {
            var buildParticipant = _participants.Take(1).FirstOrDefault();
            _repo.CreateParticipant(buildParticipant);
        }

        //Опис: Тест метод за успешно изменување на Учесник
        [TestMethod]
        [ExpectedException(typeof(UpdateSuccessfull))]
        public void Repository_UpdateParticipant_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            participant.Name = "TestName";
            _repo.UpdateParticipant(participant);

        }

        //Опис: Тест метод за неуспешно изменување на непронајден Учесник
        [TestMethod]
        [ExpectedException(typeof(NotFoundParticipantException))]
        public void Repository_UpdateParticipant_NotFoundParticipantException()
        {
            var participant = Builder<Participant>.CreateNew().With(x => x.Code = "TestCode").Build();
            _repo.UpdateParticipant(participant);

        }

        //Опис: Тест метод за успешно бришење на Учесник
        [TestMethod]
        public void Repository_DeleteParticipant_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _repo.DeleteParticipant(participant.Code);
            Assert.AreEqual(_participants.Count() - 1, _repo.GetParticipants().Count());

        }

        //Опис: Тест метод за неуспешно бришење на непронајден Учесник 
        [TestMethod]
        [ExpectedException(typeof(NotFoundParticipantException))]
        public void Repository_DeleteParticipant_NotFoundParticipantException()
        {
            _repo.DeleteParticipant("TestCode");

        }

        //Опис: Тест метод за успешно вчитување на Учесници
        [TestMethod]
        public void Repository_GetParticipants_Successfully()
        {
            Assert.AreEqual(_participants.Count(), _repo.GetParticipants().Count());

        }

        //Опис: Тест метод за успешно вчитување на интерни Учесници 
        [TestMethod]
        public void Repository_GetInternalParticipants_Successfully()
        {
            var participants = _participants.Select(x => x).ToList();
            var getInternalParticipantsRepo = _repo.GetInternalParticipants(true);

            Assert.AreEqual(participants.Count(), getInternalParticipantsRepo.Count());

        }

        //[TestMethod]
        //public void Repository_GetExternalParticipants_Successfully()
        //{
        //    var participants = _participants.Select(x => x).ToList();
        //    var getExternalParticipantsRepo = _repo.GetExternalParticipants();

        //    Assert.AreEqual(participants.Count(), getExternalParticipantsRepo.Count());
        //}

        //Опис: Тест метод за успешно вчитување на јавен клуч
        [TestMethod]
        public void Repository_GetPublicKey_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _repo.GetPublicKey(participant.Code);
        }

        //Опис: Тест метод за неуспешно вчитување на непостоечки јавен клуч
        [TestMethod]
        [ExpectedException(typeof(NotFoundPublicKeyException))]
        public void Repository_GetPublicKey_NotFoundParticipantException()
        {
            _repo.GetPublicKey("TestCode");
        }

        //Опис: Тест метод за успешно вчитување на Учесник
        [TestMethod]
        public void Repository_GetParticipant_Successfully()
        {
            var participant = _participants.Take(1).FirstOrDefault();
            _repo.GetParticipant(participant.Code);

        }

        //Опис: Тест метод за неуспешно вчитување на непронајден Учесник
        [TestMethod]
        [ExpectedException(typeof(NotFoundParticipantException))]
        public void Repository_GetParticipant_NotFoundParticipantException()
        {
            _repo.GetParticipant("TestCode");

        }

        //Опис: Тест метод за успешно вчитување на нумерирана листа од Учесници
        [TestMethod]
        public void Repository_GetParticipantsPaged_Successfully()
        {
            var builderCount = _participants.ToList();
            var buildParticipant = Builder<Participant>.CreateNew().With(x => x.Code = "Code").Build();
            //_repo.GetParticipantsPaged(1, builderCount.Count, buildParticipant.Code);
        }

    }
}
