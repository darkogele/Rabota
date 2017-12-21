using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Serialization;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IRetiredStatus;
using Contracts.Models.RetiredStatus;

namespace Implementations.Implementations.RetiredStatus
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class RetiredStatus : IRetiredStatus
    {
        public string GetRetiredStatus(string embg)
        {
            if (string.IsNullOrEmpty(embg))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е невалиден.",
                    ErrorDetails = "Параметарот ЕМБГ е празен. Вредноста внесена за ЕМБГ треба да содржи 13 цифри."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е невалиден.",
                    ErrorDetails = "Вредноста внесена за ЕМБГ содржи карактери/симболи."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
            if (embgTemp.Length != 13)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е невалиден.",
                    ErrorDetails = "Вредноста внесена за ЕМБГ треба да содржи 13 цифри."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            try
            {
                string output = "";
                var client = new RetiredInsuredData.WSIO113PortTypeClient();
                var response = client.penzioner1s(embg);
                var stringReader = new StringReader(response);
                var serializer = new XmlSerializer(typeof(DataForRetiredDTO));
                var retiredDto = serializer.Deserialize(stringReader) as DataForRetiredDTO;

                if (retiredDto.NameSurname == "" || retiredDto.PensionStatus == "")
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Сервисот на институцијата врати порака:",
                        ErrorDetails = "Податоци за внесениот ЕМБГ: " + embg + " не постојат."
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }
                if (retiredDto.PensionStatus == "1")
                    output = "ПЕНЗИОНЕР";
                else
                    output = "НЕ Е ПЕНЗИОНЕР";
                return output;
            }
            catch (EndpointNotFoundException e)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Конекцијата до ФПИОМ сервисот не може да се воспостави."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (FaultException<InteropFault> ex)
            {
                throw;
            }
            catch (FaultException e)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка",
                    ErrorDetails = "Барањето за внесениот ЕМБГ: " + embg + " не може да се обработи."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
        }
    }
}
