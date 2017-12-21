using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using Interop.CS.Models.RepositoryContracts;
using Newtonsoft.Json.Serialization;

namespace Interop.CS.Portal.API.Controllers
{
    [JsonConfig]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExternalController : ApiController
    {

        private readonly IParticipantRepository _participantsRepository;
        private readonly IBusesRepository _busesRepository;

        public ExternalController()
        {

        }
        public ExternalController(IParticipantRepository participantsRepository, IBusesRepository busesRepository)
        {
            _participantsRepository = participantsRepository;
            _busesRepository = busesRepository;
        }

        //Опис: Методот прави повик до GetParticipant од ParticipantRepository
        //Влезни параметри: код на учесник
        //Излезни параметри: Uri на учесникот што има код како влезниот параметар
        [HttpPost]
        public string GetParticipantUri(string participantCode)
        {
            var participant = _participantsRepository.GetParticipant(participantCode);
            var uri = "";
            
                uri = participant.Uri;
            
            return uri;
        }

        //Опис: Методот прави повик до GetParticipants од Participantrepository
        //Излезни параметри: Кодови и Uri за сите учесници
        [HttpGet]
        public Dictionary<string, string> GetAllParticipantsUri()
        {
            var dictionary = new Dictionary<string, string>();
            var participants = _participantsRepository.GetParticipants();
            foreach (var participant in participants)
            {
                dictionary.Add(participant.Code, participant.Uri);
            }
            return dictionary;
        }

        //Опис: Методот прави повик до GetBusUrl од BusesRepository
        //Влезни параметри: Код за бас
        //Излезни параметри: Url на бас со код како влезниот параметар
        public string GetBusUrl(string busCode)
        {
            var busUrl = _busesRepository.GetBusUrl(busCode);
            return busUrl;
        }

    }

    class JsonConfigAttribute : Attribute, IControllerConfiguration
    {
        //Опис: Го форматира response од ЦС да биде во JSON формат
        public void Initialize(HttpControllerSettings config, HttpControllerDescriptor controllerDescriptor)
        {
            var formatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            config.Formatters.Remove(formatter);

            formatter = new JsonMediaTypeFormatter();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(formatter);
        }
    }
}
