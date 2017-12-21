using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Interop.CS.CrossCutting;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Interop.CS.Portal.API.Controllers
{

    public class PhotoMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {

        public PhotoMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            //Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Trim(new char[] { '"' })
                        .Replace("&", "and");
        }
    }

    [Authorize]
    public class ParticipantController : ApiController
    {
        private readonly IParticipantRepository _participantsRepository;

        public ParticipantController()
        {

        }
        public ParticipantController(IParticipantRepository participantsRepository)
        {
            _participantsRepository = participantsRepository;
        }

        //Опис: Методот прави повик до методата GetParticipants од ParticipantRepository
        //Излезни параметри: Листа од сите учесници
        [HttpGet]
        public IEnumerable<Participant> GetParticipantList()
        {
            var participants = _participantsRepository.GetParticipants();
            return participants;
        }

        //Опис: Методот прави повик до методата GetParticipant од ParticipantRepository
        //Влезни параметри: код на учесник
        [HttpGet]
        public Participant GetParticipant(string participantCode)
        {
            try
            {
                return _participantsRepository.GetParticipant(participantCode);
            }
            catch (NotFoundParticipantException ex)
            {

                throw new HttpException(ex.Message);
            }

        }

        //Опис: Методот прави повик до методата GetParticipants од ParticipantRepository, притоа прави филтрирање 
        //Се проверува дали учесниците во своите кодови содржат "$$"
        [HttpGet]
        public IEnumerable<string> GetExternals()
        {
            var externalParticipants = _participantsRepository.GetParticipants().Select(x => x.Code).Where(x => x.Contains("$$") == false);
            return externalParticipants;
        }

        //Опис: Методата прави повик до методата CreateParticipant од ParticipantRepository
        //Влезни параметри: објект од класата Participant
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public void CreateParticipant(Participant participant)
        {
            try
            {
                _participantsRepository.CreateParticipant(participant);
            }
            catch (DuplicateParticipantException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот овозможува прикачување на сертификат при креирање на нов учесник или при менување на податоците за одреден учесник
        //Доколку сертификатот што се прикачува е невалиден, се јавува грешка
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }
            var uploadCertPath = AppSettings.Get<string>("UploadCertPath");
            var provider = new PhotoMultipartFormDataStreamProvider(uploadCertPath);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var certExtension = Path.GetExtension(result.FileData.First().LocalFileName);

            if (certExtension == ".cer")
            {
                try
                {
                    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
                    var cert = new X509Certificate2();

                    cert = new X509Certificate2(uploadedFileInfo.FullName);
                    var builder = new StringBuilder();
                    builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
                    var stringBuilder = builder.ToString();
                    //StringBuilder builder = new StringBuilder();
                    //builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
                    //var stringBuilder = builder.ToString();
                    //RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
                    //var publicKey = cert.GetPublicKey();
                    //var publicKey = rsa.ToXmlString(false);
                    return this.Request.CreateResponse(HttpStatusCode.OK, stringBuilder);
                   // return Request.CreateResponse(HttpStatusCode.OK, Convert.ToBase64String(publicKey));
                }
                catch (CryptographicException ex)
                {

                    throw new InvalidPublicKeyForCertificate(ex.Message);
                }
            }
            else
            {
                throw new InvalidCertException(certExtension);
            }

        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {

            var uploadFolder = "~\\App_Data\\Tmp\\FileUploads";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData
                    .GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        //Опис: Методот прави повик до GetPublicKey од ParticipantRepository
        //Влезни параметри:Код за учесник
        [HttpGet]
        public string GetPublicKey(string participantCode)
        {

            try
            {
                return _participantsRepository.GetPublicKey(participantCode);
            }
            catch (NotFoundPublicKeyException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот прави повик до методот UpdateParticipant од ParticipantRepository
        //Влезни параметри: објект од класата Participant
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public void UpdateParticipant(Participant participant)
        {
            try
            {
                _participantsRepository.UpdateParticipant(participant);
            }
            catch (NotFoundParticipantException ex)
            {
                throw new HttpException(ex.Message);
            }

        }

        //Опис: Методот прави повик до методот DeleteParticipant од ParticipantRepository
        //Влезни параметри: Код за учесник
        [HttpDelete]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public void DeleteParticipant(string participantCode)
        {
            try
            {
                _participantsRepository.DeleteParticipant(participantCode);
            }
            catch (NotFoundParticipantException ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        //Опис: Методот прави повик до методот GetParticipantsPaged од ParticipantRepository
        //Влезни параметри: индекс на страна, број на записи по страна, код на учесник
        [HttpGet]
        public PagedCollection<Participant> GetParticipantListPaged(int pageIndex, int itemsPerPage,string sortDir,string sortCol, string participantCode)
        {
            var participantsPaged = _participantsRepository.GetParticipantsPaged(pageIndex, itemsPerPage, sortDir, sortCol, participantCode);
            return participantsPaged;
        }

        //Опис: Методот прави повик до методот GetInternalParticipants од ParticipantRepository
        public IEnumerable<string> GetInternalParticipants()
        {
            return _participantsRepository.GetInternalParticipants(true).Select(p => p.Code).DistinctBy(x => x);
        }
        public IEnumerable<Participant> GetInternalParticipantsAccMap()
        {
            return _participantsRepository.GetInternalParticipants(true).Select(p => p).DistinctBy(x => x);
        }

        //Опис: Методот прави повик до методот GetParticipants од ParticipantRepository
        //Се проверува дали учесниците во своите кодови содржат "$$"
        public IEnumerable<Participant> GetExternalParticipants(string bus)
        {
            var listParticipants = _participantsRepository.GetParticipants().ToList().Select(p => p).Where(p => p.Code.Contains(bus + "$$")).DistinctBy(x => x);
            var lstNames = new List<Participant>();
            foreach (var participant in listParticipants)
            {
                lstNames.Add(participant);
            }
            return lstNames;
        }
        [AllowAnonymous]
        [HttpGet]
        public Dictionary<string, string> GetParticipants()
        {
            var participantsDict = new Dictionary<string, string>();
            var participants = _participantsRepository.GetParticipants();
            foreach (var participant in participants)
            {
                participantsDict.Add(participant.Code, participant.Name);
            }
            return participantsDict;
        } 
       
        [HttpGet]
        public Dictionary<string, string> GetParticipantsWoMim()
        {
            var participantsDict = new Dictionary<string, string>();
            var participants = _participantsRepository.GetParticipants();
            var splitChar = new[] { "$$" };
            foreach (var participant in participants)
            {
                var participantCodeWoMim = participant.Code.Split(splitChar,StringSplitOptions.None).Last();
                participantsDict.Add(participantCodeWoMim, participant.Name);
            }
            return participantsDict;
        }
    }
}
