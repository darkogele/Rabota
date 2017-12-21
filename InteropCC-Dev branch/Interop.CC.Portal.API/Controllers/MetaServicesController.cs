using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Models;
using Interop.CC.Portal.API.MetaServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Interop.CC.Models.Helper;
using Interop.CC.Models.RepositoryContracts;

namespace Interop.CC.Portal.API.Controllers
{
    public class PhotoMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {

        public PhotoMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        // Опис: Методот врши вчитување на име од локален фајл
        // Влезни параметри: HttpContentHeaders модел
        // Излезни параметри: податочен тип за име на влезниот модел
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            //Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
                ? headers.ContentDisposition.FileName
                : "NoName";
            return name.Trim(new char[] { '"' })
                .Replace("&", "and");
        }
    }

    public class MetaServicesController : ApiController
    {
        private readonly IProvidersRepository _providersRepository;
        private readonly IAuthRepository _authRepository;
        private ILogger _logger;

        // Опис: Конструктор на MetaServicesController модулот
        // Влезни параметри: ILogger модел
        public MetaServicesController(ILogger logger, IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        // Опис: Методот го повикува RegisterService методот на  МetaServiceClient Web клиентот
        // Влезни параметри: Service модел
        // Излезни параметри: /
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "Admin, SuperAdmin")]
        public void ApiRegisterService(Service service)
        {
            _logger.Info("Initial");
            _logger.Info(WindowsIdentity.GetCurrent().Name);
            var path = WebConfigurationManager.AppSettings["UploadCertPath"];
            var content = File.ReadAllText(path + "\\" + service.Wsdl);

            //zemanje na prviot atribut od wsdl za da se postavi kako kod
            var xdocument1 = XDocument.Load(path + "\\" + service.Wsdl);
            var attrib = xdocument1.Root.FirstAttribute.Value;
            service.Code = attrib;

            _logger.Info("path i content" + content);
            service.Wsdl = content;
            

            var metaServiceClient = new MetaServiceClient("BasicHttpBinding_IMetaService");
            _logger.Info("metaServiceClient");
            try
            {
                metaServiceClient.RegisterService(service);
            }
            catch (Exception ex)
            {
                _logger.Error("metaServiceClient error", ex);
                throw new FaultException(ex.Message);
            }

        }
        
            // Опис: Методот го повикува GetProviders методот на  МetaServiceClient Web клиентот
        // Влезни параметри: /
        // Излезни параметри: листа од податоци за ProviderCCDTO модел
        [System.Web.Http.HttpGet]
        public List<ProviderCCDTO> ApiGetProviders()
        {
            _logger.Info("ApiGetProviders");
            try
            {
                var metaServiceClient = new MetaServiceClient("BasicHttpBinding_IMetaService");
                //_logger.Info("metaServiceClient pominal");

                var providers = metaServiceClient.GetProviders();

                return providers.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("metaServiceClient error in ApiGetProviders", ex, "ErrorOnApiGetProviders");
                throw new FaultException(ex.Message);
            }
        }

        [System.Web.Http.HttpGet]
        public List<string> GetServiceRoles(string userName, [FromUri] string[] providerCodes)
        {
            try
            {
                var metaServiceClient = new MetaServiceClient("BasicHttpBinding_IMetaService");
                var serviceRoles = metaServiceClient.GetServiceRolesAfterGetProvider(userName, providerCodes);
                return serviceRoles.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in Api GetServiceRoles", ex, "ErrorOnGetServiceRoles");
                throw new FaultException(ex.Message);
            }
        }

        // Опис: Методот врши конверзија и вчитување на бајти од податочен тип string
        // Влезни параметри: вредност за податочен тип string 
        // Излезни параметри: низа од бајти
        private static byte[] GetBytesFromString(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        // Опис: Методот врши конверзија и вчитување на бајти од податочен тип string
        // Влезни параметри: вредност за податочен тип string 
        // Излезни параметри: низа од бајти
        [System.Web.Http.HttpGet]
        public string ApiGetService(string ProviderCode, string ServiceCode, string CallType)
        {
            var metaServiceClient = new MetaServiceClient("BasicHttpBinding_IMetaService");
            var servicewsdl = metaServiceClient.GetService(ProviderCode, ServiceCode, CallType);
            return servicewsdl;
        }

        // Опис: Методот го повикува GetService методот на МetaServiceClient Web клиентот
        // Влезни параметри: код за провајдер, код за сервис, код за тип на повик
        // Излезни параметри: HttpResponseMessage модел
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DownloadWsdl(string providerCode, string serviceCode, string callType)
        {
            var metaServiceClient = new MetaServiceClient("BasicHttpBinding_IMetaService");
            string servicewsdl = metaServiceClient.GetService(providerCode, serviceCode, callType);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(servicewsdl)
            };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            return result;
        }

        // Опис: Методот го повикува GetServices методот на МetaServiceClient Web клиентот
        // Влезни параметри: код за провајдер
        // Излезни параметри: листа на сервиси
        [System.Web.Http.HttpGet]
        public List<SelectListItem> ApiGetServices(string ProviderCode)
        {
            var metaServiceClient = new MetaServiceClient("BasicHttpBinding_IMetaService");
            var listServices = new List<SelectListItem>();
            var arrayServices = metaServiceClient.GetServices(ProviderCode);
            foreach (var arrayService in arrayServices)
            {
                var item = new SelectListItem();
                item.Value = arrayService.Value;
                item.Text = arrayService.Text;
                listServices.Add(item);
            }
            return listServices;
        }

        // Опис: Методот врши прикачување на сертификат
        // Влезни параметри: /
        // Излезни параметри: НТТР статус и име за прикачениот фајл со соодветна екстензија
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new PhotoMultipartFormDataStreamProvider(AppSettings.Get<string>("UploadCertPath"));
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            var certExtension = Path.GetExtension(result.FileData.First().LocalFileName);

            if (certExtension == ".wsdl" || certExtension == ".xml")
            {
                try
                {
                    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

                    return this.Request.CreateResponse(HttpStatusCode.OK, uploadedFileInfo.Name);
                }
                catch (Exception ex)
                {

                    throw new Exception("Неуспешно прикачување на датотека");
                }
            }
            else
            {
                throw new InvalidCertException(certExtension);
            }

        }
        public PagedCollection<ProviderCCDTO> GetProvidersPaged(int pageIndex, int itemsPerPage, string sortDir, string sortCol, string providerCode)
        {
            var providersList = ApiGetProviders();
            if (String.IsNullOrEmpty(providerCode))
            {
                providerCode = "";
            }
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "Code";
            }

            ICollection<ProviderCCDTO> items = providersList;


            if (sortDir == "asc")
            {
                //items = items.OrderBy(x => x.Code).ToList();
                //items = items.Where
                items = items.Where(x => x.Name.ToLower().Contains(providerCode.ToLower())).Select(x => x).OrderBy(x => x.Name).ToList();
                    
            }
            else if (sortDir == "desc")
            {
                //items = items.OrderByDescending(x => x.Code).ToList();
                items = items.Where(x => x.Name.Contains(providerCode)).Select(x => x).OrderByDescending(x => x.Name).ToList();
            }


            IReadOnlyCollection<ProviderCCDTO> pagedItems = items.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            var totalSize = items.Count();

            
            //a.Items = providersList;
            return new PagedCollection<ProviderCCDTO>(pageIndex, itemsPerPage, totalSize, pagedItems);
        }

        // Опис: Методот врши десеријализација на фајл иекстракција на името на фајлот
        // Влезни параметри: MultipartFileData модел
        // Излезни параметри: име на фајл
        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        // Опис: Методот врши вчитување на име на фајлот
        // Влезни параметри: MultipartFileData модел
        // Излезни параметри: име на фајл
        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

    }
}
