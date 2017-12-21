using System;
using System.Collections.Generic;
using System.Web.Http;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.DTO.Institutions;

namespace Interop.CC.Portal.API.Controllers.Institutions
{
    public class InstitutionBController : ApiController
    {
        private ILogger _logger;

        public InstitutionBController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetNameSurname(string name, string surname)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                throw new ArgumentException("Внесете вредност за параметрите.");
            }
            try
            {
                var client = new InstitutionBNameSurname.NameSurnameClient();
                var output = client.GetNameSurname(name, surname);
                return output;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet]
        public List<InstitutionBPersonDataDTO> GetPersonData(string showData)
        {
            var personsData = new List<InstitutionBPersonDataDTO>();
            try
            {
                var client = new InstitutionBPersonData.PersonDataClient();
                var output = client.GetPersonData(bool.Parse(showData));
                foreach (var person in output)
                {
                    personsData.Add(new InstitutionBPersonDataDTO
                    {
                        Name = person.Name,
                        Surname = person.Surname,
                        Age = person.Age,
                        DateOfBirth = person.DateOfBirth,
                        MarriageStatus = person.MarriageStatus,
                        WorkStatus = person.WorkStatus
                    });
                }
                return personsData;
            }
            catch (Exception exception)
            {
                //throw new Exception(exception.Message);
                _logger.Error("Error occured: " + exception.Message, "API_Error_InstitutionBController_GetPersonData");
                throw new Exception("Настана грешка при процесирање на податоците. Контактирајте го администраторот.");
            }
        }
    }
}
