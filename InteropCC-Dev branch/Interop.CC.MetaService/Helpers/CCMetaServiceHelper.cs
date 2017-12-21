using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Xml.Linq;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Models.Models;
using System;
using Interop.CC.Models.RepositoryContracts;
using Newtonsoft.Json;
using System.ServiceModel;

namespace Interop.CC.MetaService.Helpers
{
    public class CCMetaServiceHelper : ICCMetaServiceHelper
    {
        // Опис: Метод кој овозможува инсертирање на сервис
        // Влезни параметри: IServiceRepository serviceRepository,Service service,string ednPoint, ILogger logger
        // Излезни параметри: /
        public void InsertService(IServiceRepository serviceRepository, Service service, string ednPoint, ILogger logger)
        {
            var newService = new Service
            {
                Code = service.Code,
                Name = service.Name,
                Endpoint = ednPoint,
                Wsdl = service.Wsdl
            };

            try
            {
                serviceRepository.InsertService(newService);
                //serviceRepository.Save();
            }
            catch (Exception e)
            {
                var nameLogerError = "DynamicallySetName";
                logger.Error(JsonConvert.SerializeObject(service) + "------------" + e.Message, e, "Direction " + nameLogerError);
                throw new FaultException(e.Message);
            }
        }

        // Опис: Метод кој овозможува отстранување на сервис
        // Влезни параметри: IServiceRepository serviceRepository, Service service, ILogger logger
        // Излезни параметри: /
        public void DeleteService(IServiceRepository serviceRepository, Service service, ILogger logger)
        {
            try
            {
                serviceRepository.DeleteService(service);
            }
            catch (Exception e)
            {
                var nameLogerError = "DynamicallySetName";
                logger.Error(JsonConvert.SerializeObject(service) + "------------" + e.Message, e, "Direction " + nameLogerError);
                throw new FaultException(e.Message);
            }
        }

        // Опис: Метод кој овозможува вчитување на еndpoint
        // Влезни параметри: string wsdlInput, ILogger logger
        // Излезни параметри: податочен тип string
        public string GetEndpoint(string wsdlInput, ILogger logger)
        {
            XNamespace Wsdl = "http://schemas.xmlsoap.org/wsdl/";
            XNamespace soap12 = "http://schemas.xmlsoap.org/wsdl/soap12/";
            XDocument xdocument;
            try
            {
                //  var set = AppSettings.Get<string>(wsdlInput);
                // xdocument = XDocument.Load(wsdlInput);
                // xdocument = XDocument.Load("C:\\eval.wsdl");
                xdocument = XDocument.Parse(wsdlInput);
            }
            catch (Exception ex)
            {
                logger.Error("Error while parsing xml document.", ex); // by default NAME's value is method's name (GetEndpoint) 
                logger.Error("Error while parsing xml document.", ex, "Request_GetEndpoint"); // by overriding memberName property we MUST manually set or concatenate NAME's value 
                throw new FaultException(ex.Message);
            }

            var endPointAddress = String.Empty;
            var root = xdocument.Check(logger);
            //var customBindingPort = root.CheckElement(wsdl + "service", logger).CheckElements(wsdl + "port", logger).FirstOrDefault(port => port.Attribute("name").Value.Contains(AppSettings.Get<string>("BindingName")));
            var ports = root.CheckElement(Wsdl + "service", logger).CheckElements(Wsdl + "port", logger);


            foreach (var port in ports)
            {
                var portSoap12 = port.Element(soap12 + "address");

                if (portSoap12 != null)
                {
                    endPointAddress = portSoap12.CheckAttribute("location", logger).Value;// = host;
                }

            }
            //if (customBindingPort != null)
            //    endPointAddress = customBindingPort.Element(soap12 + "address").CheckAttribute("location", logger).Value;

            if (String.IsNullOrEmpty(endPointAddress))
                throw new FaultException("Invalid WSDL: Service must be soap12!");

            return endPointAddress;
        }

        public List<string> RebindServiceRoles(IAuthRepository authRepository, List<string> serviceRolesFromGetProviders, string loggedUser, ILogger logger)
        {
            try
            {
                //Ovde zemi gi site momentalni ulogi, sporedi gi so novo zemanite od access mappingot, ako vo starite momentalni ima nekoj shto go nema vo novozemanite, izbrisi go 
                List<string> serviceRolesAfterRebind;
                bool changeExist = false;
                var rolesInDb = authRepository.GetRoles().Where(x => x.RoleName != "Admin" && x.RoleName != "SuperAdmin" && x.RoleName != "User");
                if (serviceRolesFromGetProviders.Any())
                {
                    var rolesToDeleteFromDb = rolesInDb.Where(roles => serviceRolesFromGetProviders.All(x => x != roles.RoleName));
                    if (rolesToDeleteFromDb.Any())
                    {
                        changeExist = true;
                        foreach (var roleToDelete in rolesToDeleteFromDb)
                        {
                            authRepository.DeleteIdentityRole(roleToDelete.RoleName);
                        }
                    }
                    var rolesToCreateInDb = serviceRolesFromGetProviders.Where(newRole => rolesInDb.All(x => x.RoleName != newRole)).ToList();
                    if (rolesToCreateInDb.Any())
                    {
                        changeExist = true;
                        authRepository.CreateRole(rolesToCreateInDb);
                    }
                }
                if (changeExist)
                {
                    serviceRolesAfterRebind =
                        authRepository.GetRoles()
                            .Where(x => x.RoleName != "Admin" && x.RoleName != "SuperAdmin" && x.RoleName != "User")
                            .Select(x => x.RoleName)
                            .ToList();
                    return serviceRolesAfterRebind;
                }
                return rolesInDb.Select(x=>x.RoleName).ToList();
            }
            catch (DbException dbException)
            {
                logger.Error("Error while trying to insert user role in database", dbException.Message, "DbErrorCreatingRoles");
                throw new Exception(dbException.Message);
            }
            catch (Exception exception)
            {
                logger.Error("Error while trying to create user role for provider", exception.Message, "ErrorCreatingRoles");
                throw new Exception(exception.Message);
            }
        }
    }
}
