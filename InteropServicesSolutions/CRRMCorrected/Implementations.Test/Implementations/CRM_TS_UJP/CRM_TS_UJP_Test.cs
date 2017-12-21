using System;
using System.ServiceModel;
using System.Xml;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.ICRM_TS_UJP;
using Helpers;

namespace Implementations.Test.Implementations.CRM_TS_UJP
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class CRM_TS_UJP_Test : ICRM_TS_UJP
    {
        public string Get_TS_UJP(string param)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(param))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.",
                        "Влезниот параметар е празен. ЦРМ барањето не смее да биде непополнето!");
                    throw new FaultException<InteropFault>(faultException,
                        faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region CallingInstitutionService

                var crm = new CRM_Test.XmlWebServiceSoapClient();
                var header = new CRM_Test.XmlSoapHeader();
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
                //Helper.WriteLog(ex.Message);
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Настана грешка во адаптерот или при повикување на сервисот на институцијата:", ex.Message);
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
        }
    }
}
