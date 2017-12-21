using System;
using System.Linq;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IDataForEnsurers;
using Helpers;

namespace ImplementationsTest.Implementations.DataForEnsurers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class DataForEnsurersTest : IDataForEnsurers
    {
        public string GetDataForEnsurees(string embg)
        {
            try
            {
                InteropFault faultException;

                #region ValidationErrors

                if (string.IsNullOrEmpty(embg))
                {
                    faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Внесениот ЕМБГ е празен. Вредноста внесена за ЕМБГ треба да содржи 13 цифри!");
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

                var fpiomOriginal = new RetiredInsuredDataTest.WSIO113PortTypeClient();
                var output = fpiomOriginal.osigurenik1s(embg);

                //var dataforEnsuree = new DataForEmployeeDTO();
                //var stringReader = new StringReader(output);
                //var serializer = new XmlSerializer(typeof(DataForEmployeeDTO));
                //dataforEnsuree = (DataForEmployeeDTO)serializer.Deserialize(stringReader);

                #endregion

                return output;
            }
            catch (EndpointNotFoundException)//da se proveri ova//LOKALNO RABOTI
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Адаптерот на сервисот врати грешка.", "Конекцијата до ФПИОМ сервисот не може да се воспостави!");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }
            catch (FaultException<InteropFault>)
            {
                throw;
            }
            catch (FaultException)//Ova e vo slucaj koga servisot nema podatoci shto moze da gi vrati, vrakja FaultException
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Не се пронајдени податоци за внесениот ЕМБГ " + embg +"!");
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
