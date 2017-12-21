using System.Linq;
using System.ServiceModel;
using Implementations.Contracts;
using Implementations.Helpers;
using Implementations.Models;

namespace Implementations.Implementations
{
    public class NameSurname : INameSurname
    {
        public string GetNameSurname(string name, string surname)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname))
            {
                if (!name.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
                {
                    InteropFault faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Внесете име и презиме!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                if (name.Length > 5 || surname.Length > 10)
                {
                    InteropFault faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Името не може да е подолго од 5 карактери, а презимето подолго од 10 карактери!");
                    throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
                }
                result = name + " " + surname;
            }
            if (string.IsNullOrEmpty(result))
            {
                InteropFault faultException = FaultExceptionHelper.CreateFaultException("Грешка во сервисот на институцијата.", "Нема податоци!");
                throw new FaultException<InteropFault>(faultException, faultException.ErrorMessage + " " + faultException.ErrorDetails);
            }

            return result;
        }
    }
}
