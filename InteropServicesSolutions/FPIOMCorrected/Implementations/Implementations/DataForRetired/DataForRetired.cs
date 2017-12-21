using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Serialization;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IDataForRetired;
using Contracts.Models.DataForRetired;

namespace Implementations.Implementations.DataForRetired
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class DataForRetired : IDataForRetired
    {
        public string GetDataForRetired(string embg)
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

            var fpiomOriginal = new RetiredInsuredData.WSIO113PortTypeClient();

            try
            {
                var output = fpiomOriginal.penzioner1s(embg);

                var retiredDto = new DataForRetiredDTO();
                var stringReader = new StringReader(output);
                var serializer = new XmlSerializer(typeof (DataForRetiredDTO));
                retiredDto = (DataForRetiredDTO) serializer.Deserialize(stringReader);

                if (retiredDto.NameSurname == "")
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Сервисот на институцијата врати порака:",
                        ErrorDetails = "Податоци за внесениот ЕМБГ: " + embg + " не постојат."
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }

                return output;
            }
            catch (EndpointNotFoundException e)//da se doproveri ova, bidejki samo ednas mi javi endpointexception
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
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Барањето за внесениот ЕМБГ: " + embg + " не може да се обработи."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
        }
    }
}
