using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;
using Interop.CS.MetaService.Models;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Newtonsoft.Json;
using Interop.CS.CrossCutting.Logging;
using System.ServiceModel;
using Interop.CS.CrossCutting;

namespace Interop.CS.MetaService.Helpers
{
    public class MetaServiceHelper : IMetaServiceHelper
    {
        public void InsertService(IServiceRepository serviceRepository, IParticipantRepository participantRepository, CSService cSservice)
        {
            //Downloading the WSDL form WSDL URL taken form CC Register Service
            //string wsdlURL = null;
            //if (cSservice.Wsdl.EndsWith("?wsdl"))
            //    wsdlURL = cSservice.Wsdl.Replace("?wsdl", "?singlewsdl");
            //string wsdl = null;
            //using (var wc = new WebClient())
            //{
            //    using (var stream = wc.OpenRead(wsdlURL))
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            wsdl = sr.ReadToEnd();
            //        }
            //    }
            //}

            var participantExist =
                participantRepository.GetParticipants().Any(x => x.Code == cSservice.ParticipantCode);

            if (participantExist)
            {
                var service = new CSService
                {
                    ParticipantCode = cSservice.ParticipantCode,
                    Code = cSservice.Code,
                    Name = cSservice.Name,
                    Wsdl = cSservice.Wsdl
                };

                try
                {
                    serviceRepository.CreateService(service);
                }
                catch (Exception e)
                {
                    var nameLogerError = service.Name + "_ " + DateTime.Now;
                    using (var logger = LoggingFactory.GetNLogger(nameLogerError))
                    {
                        logger.Error(JsonConvert.SerializeObject(service), e);
                    }
                    throw new FaultException(e.Message);
                }
            }
            else
            {
                throw new FaultException("Не постои учесник со име: " + cSservice.ParticipantCode);
            }
        }

        public List<ProviderCSDTO> GetProviders(IAccessMappingRepository accessMappingRepository, IParticipantRepository participantRepository, IBusesRepository busesRepository, string consumerId)
        {
            var nameLogerError1 = "GetProviders" + "_ " + DateTime.Now;
            using (var logger = LoggingFactory.GetNLogger(nameLogerError1))
            {
                logger.Info("dosol vo get providers za " + consumerId);
            }
            List<ProviderCSDTO> output;
            try
            {
                var accessMappings = accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumerId);

                var participants = participantRepository.GetParticipants();
                var accessMappingList = new List<AccessMapping>();
                foreach (var am in accessMappings)
                {
                    var amm = new AccessMapping()
                     {
                         ProviderCode = am.ProviderCode,
                         ProviderBusCode = am.ProviderBusCode,
                         ConsumerCode = am.ConsumerCode
                     };

                    amm.ProviderCode = amm.ProviderBusCode + "$$" + amm.ProviderCode;
                    accessMappingList.Add(amm);
                }
                var accessMappet = accessMappingList.Select(s => s.ProviderCode).Distinct();

                var joinedAccessMappingsAndParticipants = accessMappet.Join(participants, am => am,
                    p => p.Code, (am, p) => new ProviderCSDTO { Code = am, PublicKey = p.PublicKey, Name = p.Name }).ToList();

                output = joinedAccessMappingsAndParticipants.ToList();

                //foreach (var k in accessMapBusExt)
                //{
                //    var busURL = k.Url;
                //    //the call is made with MIM1$$consumerId
                //    // get lists from other busses and add them to output
                //}
                // }
                //else
                //{//call from external BUS, to be tested
                //    string[] stringSeparators = new string[] { "$$" };
                //    string[] result;
                //    result = consumerId.Split(stringSeparators, StringSplitOptions.None);
                //    var busC = result[0];
                //    var consumer = result[1];
                //    var accessMappings = accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumer);
                //    var accessMapBus = accessMappings.Where(x => x.ProviderBusCode == ourBus && x.ConsumerBusCode == busC);
                //    var accessMappet = accessMapBus.Select(s => s.ProviderCode).Distinct();
                //    var participants = participantRepository.GetParticipants();

                //    var joinedAccessMappingsAndParticipants = accessMappet.Join(participants, am => am,
                //        p => p.Code, (am, p) => new ProviderCSDTO { Code = ourBus + "$$" + am, PublicKey = p.PublicKey }).ToList();
                //    output = joinedAccessMappingsAndParticipants.ToList();
                //}
            }
            catch (Exception ex)
            {
                var nameLogerError = "GetProviders" + "_ " + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLogerError))
                {
                    logger.Info("consumerId = " + consumerId);
                }
                throw new FaultException(ex.Message);
            }
            return output;
        }

        public List<SelectListItem> GetServices(IAccessMappingRepository accessMappingRepository, IBusesRepository busesRepository, IServiceRepository servicesRepository, string providerId, string consumerId)
        {
            //var ourBus = AppSettings.Get<string>("Bus");
            //string[] stringSeparators = new string[] { "$$" };
            //string[] result;

            var nameLogerError = "Vlezni parametri na GetServices" + "_ " + DateTime.Now;
            using (var logger = LoggingFactory.GetNLogger(nameLogerError))
            {
                logger.Info("provider: " + providerId + "; consumer: " + consumerId);
            }

            var output = new List<string>();
            var busP = "";
            //if (providerId.Contains("$$"))
            //{
            string[] stringSeparators = new string[] { "$$" };
            string[] result;
            result = providerId.Split(stringSeparators, StringSplitOptions.None);
            busP = result[0];
            providerId = result[1];
            //}
            //if (String.IsNullOrEmpty(busP))
            //busP = ourBus;

            using (var logger = LoggingFactory.GetNLogger(nameLogerError))
            {
                logger.Info("busP: " + busP + "; providerId: " + providerId);
            }

            output = accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumerId && x.ProviderCode == providerId && x.ProviderBusCode == busP && x.IsActive).Select(x => x.ServiceCode).Distinct().ToList();

            using (var logger = LoggingFactory.GetNLogger(nameLogerError))
            {
                logger.Info("output: " + output.Count);
            }

            var allservices = servicesRepository.GetServices();
            var newOutput = new List<SelectListItem>();

            foreach (var service in allservices)
            {
                if (output.Contains(service.Code))
                {
                    var item = new SelectListItem();

                    item.Value = service.Code;
                    item.Text = service.Name;
                    newOutput.Add(item);
                }
            }

            //if(!providerId.Contains("$$") && !consumerId.Contains("$$"))
            //{
            //    output = accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumerId && x.ProviderCode == providerId && x.ProviderBusCode == ourBus).Select(x => x.ServiceCode).Distinct().ToList();
            //}
            //else if(providerId.Contains("$$"))
            //{
            //result = providerId.Split(stringSeparators, StringSplitOptions.None);
            //var busC = result[0];
            //var busURL = busesRepository.GetBuses().Where(x => x.Code == busC).Select(x => x.Url);
            // Make call to external Bus with parametars GetServices(ourBus$$consumerId, providerId)
            // get list of strings from the external call
            //}
            //else if(providerId.Contains("$$") && consumerId.Contains("$$"))
            //{//call from external BUS, to be tested
            //    result = providerId.Split(stringSeparators, StringSplitOptions.None);
            //    var provId = result[1];
            //    result = consumerId.Split(stringSeparators, StringSplitOptions.None);
            //    var consId = result[1];
            //    var consBus = result[0];
            //    output = accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consId && x.ConsumerBusCode == consBus && x.ProviderCode == provId && x.ProviderBusCode == ourBus).Select(x => x.ServiceCode).Distinct().ToList();
            //}
            return newOutput;
        }

        public string GetService(IAccessMappingRepository accessMappingRepository, IServiceRepository serviceRepository, IBusesRepository busesRepository, string providerId, string consumerId, string serviceId)
        {
            //var ourBus = AppSettings.Get<string>("Bus");
            //string[] stringSeparators = new string[] { "$$" };
            //string[] result;
            var map = accessMappingRepository.GetAccessMappings().ToList();
            var busP = "";
            var provider = "";
            //if (providerId.Contains("$$"))
            //{
            string[] stringSeparators = new string[] { "$$" };
            string[] result;
            result = providerId.Split(stringSeparators, StringSplitOptions.None);
            busP = result[0];
            provider = result[1];
            //}

            //if (String.IsNullOrEmpty(busP))
            // busP = ourBus;
            //if (String.IsNullOrEmpty(provider))
            //provider = providerId;

            //if (!providerId.Contains("$$") && !consumerId.Contains("$$"))
            //{
            //var allAvilableServices = map.Where(x => x.ConsumerCode == consumerId && x.ConsumerBusCode == ourBus && x.ProviderCode == providerId && x.ProviderBusCode == ourBus && x.ServiceCode == serviceId);

            var allAvilableServices = map.Where(x => x.ConsumerCode == consumerId && x.ProviderCode == provider && x.ProviderBusCode == busP && x.ServiceCode == serviceId && x.IsActive);
            var serviceMethodsFromAvilableServices = allAvilableServices.Select(service => service.MethodCode).ToArray();

            if (allAvilableServices.Any())
            {
                //var temp = serviceRepository.GetServices();
                //foreach (var ser in temp)
                //{
                //    if (ser.ParticipantCode.Contains("$$"))
                //    {
                //        string[] stringSeparators = new string[] { "$$" };
                //        string[] result;
                //        result = ser.ParticipantCode.Split(stringSeparators, StringSplitOptions.None);
                //        ser.ParticipantCode = result[1];
                //    }
                //}
                //var temp1 = temp.FirstOrDefault(x => x.ParticipantCode == providerId && x.Code == serviceId);

                var originalWsdl = serviceRepository.GetServices().FirstOrDefault(x => x.ParticipantCode == providerId && x.Code == serviceId).Wsdl;

                if (serviceMethodsFromAvilableServices.Any())
                {
                    return RecreateWsdl(originalWsdl, serviceMethodsFromAvilableServices);
                }
            }
            //}
            //else if (providerId.Contains("$$"))
            //{
            //    result = providerId.Split(stringSeparators, StringSplitOptions.None);
            //    var busC = result[0];
            //    var busURL = busesRepository.GetBuses().Where(x => x.Code == busC).Select(x => x.Url);
            //    // Make call to external Bus with parametars GetService(ourBus$$consumerId, providerId, ServY)
            //    // get list of strings from the external call
            //    return "";
            //}
            //else if (providerId.Contains("$$") && consumerId.Contains("$$"))
            //{//call from external BUS, to be tested
            //    result = providerId.Split(stringSeparators, StringSplitOptions.None);
            //    var provId = result[1];
            //    result = consumerId.Split(stringSeparators, StringSplitOptions.None);
            //    var consId = result[1];
            //    var consBus = result[0];
            //    var allAvilableServices = map.Where(x => x.ConsumerCode == consId && x.ConsumerBusCode == consBus && x.ProviderCode == provId && x.ProviderBusCode == ourBus && x.ServiceCode == serviceId);
            //    var serviceMethodsFromAvilableServices = allAvilableServices.Select(service => service.MethodCode).ToArray();

            //    if (allAvilableServices.Any())
            //    {
            //        var originalWsdl = serviceRepository.GetServices().FirstOrDefault(x => x.ParticipantCode == provId && x.Code == serviceId).Wsdl;

            //        if (serviceMethodsFromAvilableServices.Any())
            //        {
            //            return RecreateWsdl(originalWsdl, serviceMethodsFromAvilableServices);
            //        }
            //    }
            //}
            var nameLogerError = "GetService_ " + DateTime.Now;
            using (var logger = LoggingFactory.GetNLogger(nameLogerError))
            {
                logger.Info("There is no access maping for that provider and consumer");
            }
            throw new FaultException("There is no access maping for that provider and consumer");
        }

        private string RecreateWsdl(string originalWsdl, string[] methodsNames)
        {
            try
            {
                XDocument doc = XDocument.Parse(originalWsdl);
                if (doc.Root != null)
                {
                    //message part
                    doc = RecreateWsdlMessagePart(doc, methodsNames);

                    //port part
                    doc = RecreateWsdlPortTypePart(doc, methodsNames);

                    //binding part
                    doc = RecreateWsdlBindingPart(doc, methodsNames);
                }
                return doc.ToString();
            }
            catch (Exception ex)
            {
                var nameLogerError = "RecreateWSDL_ " + DateTime.Now;
                using (var logger = LoggingFactory.GetNLogger(nameLogerError))
                {
                    logger.Info("There is some error when trying to recreate WSDL. Original wsdl is: " + originalWsdl);
                }
                return originalWsdl;
            }
        }

        private XDocument RecreateWsdlMessagePart(XDocument doc, string[] methodsNames)
        {
            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
            bool toBeDeleted = true;
            XElement[] messageElements = doc.Descendants(wsdl + "message").ToArray();
            if (messageElements.Any())
            {
                foreach (var messageElement in messageElements)
                {
                    for (int i = 0; i < methodsNames.Count(); i++)
                    {
                        /* TESTING */
                        var splitChar = new string[] { "/" };
                        var messageValue = messageElement.Attribute("name").Value;
                        var methodName = methodsNames[i].Split(splitChar, StringSplitOptions.None).Last();
                        if (messageValue.Contains(methodName))
                        /*Previous Code*/
                        //if (messageElement.Attribute("name").Value.Contains(methodsNames[i]))
                        {
                            toBeDeleted = false;
                            break;
                        }
                    }
                    if (toBeDeleted)
                    {
                        doc.Elements().Descendants(wsdl + "message").Where(x => x.Attribute("name").Value.Contains(messageElement.Attribute("name").Value)).Remove();
                    }
                    toBeDeleted = true;
                }
            }
            return doc;
        }

        private XDocument RecreateWsdlPortTypePart(XDocument doc, string[] methodsNames)
        {
            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
            bool toBeDeleted = true;
            XElement[] portElements = doc.Descendants(wsdl + "portType").Descendants(wsdl + "operation").ToArray();
            if (portElements.Any())
            {
                foreach (var portElement in portElements)
                {
                    for (int i = 0; i < methodsNames.Count(); i++)
                    {
                        /* TESTING */
                        var splitChar = new string[] { "/" };
                        var portValue = portElement.Attribute("name").Value;
                        var methodName = methodsNames[i].Split(splitChar, StringSplitOptions.None).Last();
                        if (portValue.Contains(methodName))
                        //if (portElement.Attribute("name").Value.Contains(methodsNames[i]))
                        {
                            toBeDeleted = false;
                            break;
                        }
                    }
                    if (toBeDeleted)
                    {
                        doc.Elements().Descendants(wsdl + "portType").Descendants(wsdl + "operation").Where(x => x.Attribute("name").Value.Contains(portElement.Attribute("name").Value)).Remove();
                    }
                    toBeDeleted = true;
                }
            }
            return doc;
        }

        private XDocument RecreateWsdlBindingPart(XDocument doc, string[] methodsNames)
        {
            XNamespace wsdl = "http://schemas.xmlsoap.org/wsdl/";
            bool toBeDeleted = true;
            XElement[] bindingElements = doc.Descendants(wsdl + "binding").Descendants(wsdl + "operation").ToArray();
            if (bindingElements.Any())
            {
                foreach (var bindingElement in bindingElements)
                {
                    for (int i = 0; i < methodsNames.Count(); i++)
                    {
                        /* TESTING */
                        var splitChar = new string[] { "/" };
                        var bindingValue = bindingElement.Attribute("name").Value;
                        var methodName = methodsNames[i].Split(splitChar, StringSplitOptions.None).Last();
                        if (bindingValue.Contains(methodName))
                        //if (bindingElement.Attribute("name").Value.Contains(methodsNames[i]))
                        {
                            toBeDeleted = false;
                            break;
                        }
                    }
                    if (toBeDeleted)
                    {
                        doc.Elements().Descendants(wsdl + "binding").Descendants(wsdl + "operation").Where(x => x.Attribute("name").Value.Contains(bindingElement.Attribute("name").Value)).Remove();
                    }
                    toBeDeleted = true;
                }
            }
            return doc;
        }

        public List<string> ListConsumers(IAccessMappingRepository accessMappingRepository, string providerId, string serviceId)
        {
            return accessMappingRepository.GetAccessMappings().Where(x => x.ProviderCode == providerId && x.ServiceCode == serviceId).Select(x => x.ConsumerCode).ToList();
        }

        public List<string> GetServiceRoles(IAccessMappingRepository accessMappingRepository, IServiceRepository servicesRepository, List<string> providersCodes, string consumerId)
        {
            var roles = new List<string>();
            var services = servicesRepository.GetServices();
            var csServices = services as CSService[] ?? services.ToArray();
            foreach (var pCode in providersCodes)
            {
                string code = pCode.Contains("MIM1$$") ? pCode.Remove(0, 6) : pCode;

                var serviceCodes = accessMappingRepository.GetAccessMappings().Where(x => x.ConsumerCode == consumerId && x.ProviderCode == code && x.IsActive)
                    .Select(x => new { ProviderService = x.ProviderCode + " - " + x.ServiceCode }).Distinct().ToList();

                foreach (var sCode in serviceCodes)
                {
                    if (csServices.Any())
                    {
                        string serviceCode = sCode.ProviderService.Split('-').Skip(1).First().Trim();
                       
                        string providerCode = sCode.ProviderService.Split('-').First();
                        
                        var cyrilicCode = (CyrilicInstitutionCode) Enum.Parse(typeof (CyrilicInstitutionCode), providerCode, true);

                        var serviceByCode = csServices.FirstOrDefault(x => x.Code == serviceCode);
                        if (serviceByCode != null)
                        {
                            var serviceName = serviceByCode.Name;
                            roles.Add(Helper.GetCyrilicCode(cyrilicCode) + " - " + serviceName);
                        }
                    }
                }
            }
            return roles;
        }

    }
}
