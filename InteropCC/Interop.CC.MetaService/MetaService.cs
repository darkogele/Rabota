using System.Data.Common;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using System.Xml.Linq;
using Interop.CC.CrossCutting;
using Interop.CC.MetaService.CSMetaServiceReference;
using Interop.CC.MetaService.Helpers;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.MetaService.Models;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Newtonsoft.Json;

namespace Interop.CC.MetaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MetaService : IMetaService
    {
        private readonly CSMetaServiceClient _csMetaServiceClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly ICCMetaServiceHelper _ccMetaServiceHelper;
        private readonly IProvidersRepository _providersRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ILogger _logger;


        // Опис: Конструктор на класата MetaService 
        // Влезни параметри: IServiceRepository serviceRepository, ICCMetaServiceHelper ccMetaServiceHelper, IProvidersRepository providersRepository, ILogger logger

        public MetaService(IServiceRepository serviceRepository, ICCMetaServiceHelper ccMetaServiceHelper,
            IProvidersRepository providersRepository, IAuthRepository authRepository, ILogger logger)
        {
            _csMetaServiceClient = new CSMetaServiceClient();
            _serviceRepository = serviceRepository;
            _ccMetaServiceHelper = ccMetaServiceHelper;
            _providersRepository = providersRepository;
            _authRepository = authRepository;
            _logger = logger;
        }

        // Опис: Метод кој врши регистрација на сервис со повикување на CSMetaServiceClient клиент
        // Влезни параметри: Service service
        // Излезни параметри: /
        public void RegisterService(Service service)
        {
            _logger.Info("In RegisterService");
            var endPoint = _ccMetaServiceHelper.GetEndpoint(service.Wsdl, _logger);

            try
            {
                _ccMetaServiceHelper.InsertService(_serviceRepository, service, endPoint, _logger);
                _logger.Info("Zapisot vo baza na CC pomina");
                var providerCode = AppSettings.Get<string>("ParticipantCodeWithMIM");
                try
                {
                    _csMetaServiceClient.RegisterService(new CSService
                    {
                        Code = service.Code,
                        Name = service.Name,
                        ParticipantCode = providerCode,
                        Wsdl = service.Wsdl
                    });

                }
                catch (Exception e)
                {
                    _ccMetaServiceHelper.DeleteService(_serviceRepository, service, _logger);
                    _logger.Error("Error whiletrying to register service on cs", e);
                    throw new FaultException(e.Message);
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Error whiletrying to register service on CC", exception);
                throw new Exception(exception.Message);
            }
        }

        // Опис: Метод кој врши одрегистрација на сервис со повикување на CSMetaServiceClient клиент
        // Влезни параметри: податочна вредност serviceId
        // Излезни параметри: /
        public void UnRegisterService(string serviceId)
        {
            _csMetaServiceClient.UnRegisterService(serviceId);
        }

        // Опис: Метод кој врши вчитување на сите провајдери
        // Влезни параметри: /
        // Излезни параметри: List<ProviderCCDTO>
        public List<ProviderCCDTO> GetProviders()
        {
            try
            {
                var providerCode = AppSettings.Get<string>("ParticipantCode");
                _logger.Info("providerCode" + providerCode, "ProvidersForCosumerAre");
                var providersByCsAccMapp = _csMetaServiceClient.GetProviders(providerCode).ToList();
                var providers = providersByCsAccMapp.Select(providerCsDto => new ProviderCCDTO { Code = providerCsDto.Code, PublicKey = providerCsDto.PublicKey, Name = providerCsDto.Name, ProviderMim = providerCsDto.Code.Substring(0,4)}).ToList();
                foreach (var provider in providers)
                {
                    _logger.Info("providerDto: " + provider.Code + "/" + provider.PublicKey);
                    var consumerProvider = new Provider { PublicKey = provider.PublicKey, RoutingToken = provider.Code };
                    _providersRepository.AddProvider(consumerProvider);
                }
                return providers;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured in CC MetaService GetProviders", ex.Message, "ErrorMetaServiceGetProviders");
                throw new Exception(ex.Message);
            }
        }

        // Опис: Метод кој врши вчитување на сите сервиси
        // Влезни параметри: податочна вредност providerId
        // Излезни параметри: List<string>
        public List<SelectListItem> GetServices(string providerId)
        {
            var providerCode = AppSettings.Get<string>("ParticipantCode");
            return _csMetaServiceClient.GetServices(providerId, providerCode).ToList();
        }

        // Опис: Метод кој врши вчитување на сервис
        // Влезни параметри: податочна вредност providerId, serviceId, callType
        // Излезни параметри: податочен тип string
        public string GetService(string providerId, string serviceId, string callType)
        {
            var consumerCode = AppSettings.Get<string>("ParticipantCode");
            _logger.Info("Pred da se povika get service na stranata na CS" + providerId + "/" + consumerCode + "/" + serviceId + "/" + callType);
            //change endpoint
            var wsdl = _csMetaServiceClient.GetService(providerId, consumerCode, serviceId, callType);
            _logger.Info("proslo get service kaj CS i wsdl-ot e " + wsdl);

            string host = AppSettings.Get<string>("staticUrl") + providerId + "/" + serviceId;
            _logger.Info("hostot e " + host);

            XNamespace Wsdl = "http://schemas.xmlsoap.org/wsdl/";
            XNamespace soap12 = "http://schemas.xmlsoap.org/wsdl/soap12/";
            XNamespace soap = "http://schemas.xmlsoap.org/wsdl/soap/";
            XNamespace wsa10 = "http://schemas.xmlsoap.org/wsdl/wsa10/";

            var xdocument = XDocument.Parse(wsdl);
            var root = xdocument.Check(_logger);

            var ports = root.CheckElement(Wsdl + "service", _logger).CheckElements(Wsdl + "port", _logger);


            foreach (var port in ports)
            {
                var portSoap12 = port.Element(soap12 + "address");
                var portSoap = port.Element(soap + "address");


                if (portSoap12 != null)
                {
                    portSoap12.CheckAttribute("location", _logger).Value = host;
                }
                else if (portSoap != null)
                {
                    portSoap.CheckAttribute("location", _logger).Value = host;
                }

            }
            var wsdlXdoc = xdocument.ToString();
            return wsdlXdoc;
        }

        // Опис: Метод кој врши вчитување на листа на консумери
        // Влезни параметри: податочна вредност serviceId
        // Излезни параметри: List<string>
        public List<string> ListConsumers(string serviceId)
        {
            var providerCode = AppSettings.Get<string>("ParticipantCode");
            try
            {
                return _csMetaServiceClient.ListConsumers(providerCode, serviceId).ToList();
            }
            catch (Exception e)
            {
                _logger.Error("Error while calling cs", e);
                throw new FaultException(e.Message);
            }
        }

        public string CheckStateByTransactionId(string transactionId)
        {
            throw new NotImplementedException();
        }

        public string GetMessageByTransactionId(string transactionId)
        {
            throw new NotImplementedException();
        }

        public void PostMessage(string transactionId, string message)
        {
            throw new NotImplementedException();
        }

        public List<string> GetServiceRolesAfterGetProvider(string loggedUser, string[] providersCodes)
        {
            try
            {
                var participantCode = AppSettings.Get<string>("ParticipantCode");
                var serviceRolesFromGetProviders = _csMetaServiceClient.GetServiceRoles(participantCode, providersCodes).ToList();
                if (serviceRolesFromGetProviders.Any())
                {
                    CreateOrDeleteServicesRoles(serviceRolesFromGetProviders, loggedUser);
                }
                
                return serviceRolesFromGetProviders;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured in CC MetaService GetServiceRolesAfterGetProvider", ex.Message, "ErrorGetServiceRolesAfterGetProvider");
                throw new Exception(ex.Message);
            }
        }

        public List<string> CreateOrDeleteServicesRoles(List<string> serviceRolesFromGetProviders, string loggedUser)
        {
            var serviceRolesAfterRebind = new List<string>();
            try
            {
                serviceRolesAfterRebind = _ccMetaServiceHelper.RebindServiceRoles(_authRepository, serviceRolesFromGetProviders, loggedUser, _logger);
                return serviceRolesAfterRebind;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured in CC MetaService CreateOrDeleteServicesRoles", ex.Message, "ErrorCreateOrDeleteServicesRoles");
            }
            return serviceRolesAfterRebind;
        }
    }
}
