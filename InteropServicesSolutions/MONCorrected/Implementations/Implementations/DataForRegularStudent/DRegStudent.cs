using System;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using Contracts.DataAccessLibrary;
using Contracts.Interfaces.IDataForRegularStudent;
using Implementations.Helpers;

namespace Implementations.Implementations.DataForRegularStudent
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class DRegStudent : IDRegStudent
    {
        public string GetStuD(string embg)
        {
            if (string.IsNullOrEmpty(embg))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Вредноста за ЕМБГ е празна.",
                    ErrorDetails = "ЕМБГ треба да содржи 13 цифри."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            if (embg.Any(x => (char.IsLetter(x) || char.IsSeparator(x) || char.IsPunctuation(x) || char.IsSymbol(x))))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е погрешен.",
                    ErrorDetails = "ЕМБГ содржи карактери/симболи."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            var embgTemp = new String(embg.Where(Char.IsDigit).ToArray());
            if (embgTemp.Length != 13)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Адаптерот на сервисот врати грешка. Внесениот ЕМБГ е погрешен.",
                    ErrorDetails = "ЕМБГ треба да содржи 13 цифри."
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            var statusForRegularStudent = Helper.GetData(embg);

            if (!string.IsNullOrEmpty(statusForRegularStudent))
            {
                var doc = new XmlDocument();
                doc.LoadXml(statusForRegularStudent);
                var status = doc.GetElementsByTagName("fullName");
                if (status.Count != 0)
                    return statusForRegularStudent;
                var message = "Сервисот на институцијата врати порака: " + doc.GetElementsByTagName("message")[0].InnerText;
                var ex = new InteropFault {Result = false, ErrorMessage = message};
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage);
            }
            else
            {
                var ex = new InteropFault { Result = false, ErrorMessage = "Сервисот на институцијата врати грешка. Понудениот сервис нема информација за овој ученик." };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage);
            }
        }
    }
}
