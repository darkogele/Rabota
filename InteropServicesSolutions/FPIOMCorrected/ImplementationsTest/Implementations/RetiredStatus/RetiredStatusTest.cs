using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Serialization;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IRetiredStatus;
using Contracts.Models.RetiredStatus;
using Helpers;

namespace ImplementationsTest.Implementations.RetiredStatus
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class RetiredStatusTest : IRetiredStatus
    {
        public string GetRetiredStatus(string embg)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(embg))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Параметарот ЕМБГ е празен. Вредноста внесена за ЕМБГ треба да содржи 13 цифри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Внесениот ЕМБГ е невалиден. Вредноста внесена за ЕМБГ не треба да содржи карактери/симболи!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
                if (embgTemp.Length != 13)
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Внесениот ЕМБГ е невалиден. Вредноста внесена за ЕМБГ треба да содржи 13 цифри!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }

                #endregion

                #region CallingInstitutionService

                var client = new RetiredInsuredDataTest.WSIO113PortTypeClient();
                var response = client.penzioner1s(embg);

                #endregion

                #region LogicAfterCallingInstitutionService

                var stringReader = new StringReader(response);
                var serializer = new XmlSerializer(typeof(DataForRetiredDTO));
                var retiredDto = serializer.Deserialize(stringReader) as DataForRetiredDTO;

                if (string.IsNullOrEmpty(retiredDto.NameSurname) || string.IsNullOrEmpty(retiredDto.PensionStatus))
                {
                    return "НЕ Е ПЕНЗИОНЕР";
                }
                return "ПЕНЗИОНЕР";

                #endregion
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (EndpointNotFoundException)//da se proveri ova//LOKALNO RABOTI
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до ФПИОМ сервисот не може да се воспостави!");
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
