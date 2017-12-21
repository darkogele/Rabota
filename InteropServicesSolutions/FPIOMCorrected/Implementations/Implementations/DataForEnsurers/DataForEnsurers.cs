using System;
using System.Linq;
using System.ServiceModel;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IDataForEnsurers;

namespace Implementations.Implementations.DataForEnsurers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class DataForEnsurers : IDataForEnsurers
    {
        public string GetDataForEnsurees(string embg)
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
                var output = fpiomOriginal.osigurenik1s(embg);

                //var employeeDto = new DataForEmployeeDTO();

                //var stringReader = new StringReader(output);
                //var serializer = new XmlSerializer(typeof(DataForEmployeeDTO));
                //employeeDto = (DataForEmployeeDTO)serializer.Deserialize(stringReader);
                //var dataforEnsuree = employeeDto;

                return output;
            }
            catch (EndpointNotFoundException e)//da se proveri ova
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка.",
                    ErrorDetails = "Конекцијата до ФПИОМ сервисот не може да се воспостави."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (FaultException e)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот на институцијата врати грешка.",
                    ErrorDetails = "Барањето за внесениот ЕМБГ: " + embg + " не може да се обработи. Не се пронајдени податоци за внесениот ЕМБГ."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
        }
    }
}
