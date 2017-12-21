using System;
using System.ServiceModel;
using System.Xml;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.ICRM_TS_AKN;
using Helpers;

namespace Implementations.Implementations.CRM_TS_AKN
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class CRM_TS_AKN : ICRM_TS_AKN
    {
        public string Get_TS_AKN(string param)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(param))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Влезниот параметар е празен. ЦРМ барањето не смее да биде непополнето!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region CallingInstitutionService

                var crm = new CRRMProd.XmlWebServiceSoapClient();
                var header = new CRRMProd.XmlSoapHeader();
                var output = crm.ProcessSignedRequest(ref header, param);

                #endregion

                #region LogicAfterCallingInstitutionService

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(output);
                var responseInfoMsg = xmlDoc.SelectSingleNode("//InfoMessage");
                if (responseInfoMsg != null && !string.IsNullOrEmpty(responseInfoMsg.InnerText))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", responseInfoMsg.InnerText);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                var crmResponseErrorMsg = xmlDoc.SelectSingleNode("//CrmResponse");
                if (crmResponseErrorMsg != null && (crmResponseErrorMsg.Attributes != null && (crmResponseErrorMsg.Attributes["Message"] != null)))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", crmResponseErrorMsg.Attributes["Message"].Value);
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                return output;
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (EndpointNotFoundException)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до ЦРМ сервисот не може да се воспостави!");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (Exception ex)
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при повикување на сервисот на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
        }
    }
}
