using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Interfaces.IBondsDataForCalcAndPayOfContributions;

namespace Implementations.Implementations.BondsDataForCalcAndPayOfContributions
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class BondsDataForCalcAndPayOfContributions : IBondsDataForCalcAndPayOfContributions
    {
        string PathAvrm = ConfigurationSettings.AppSettings.Get("PathAvrmOU");
        string PathFzom = ConfigurationSettings.AppSettings.Get("PathFzomOU");
        string PathFpiom = ConfigurationSettings.AppSettings.Get("PathFpiomOU");
        public byte[] GetOU_AVRM(string date)
        {
            byte[] temp = null;

            if (string.IsNullOrEmpty(date))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Нема внесена вредност за датум!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            try
            {
                temp = File.ReadAllBytes(PathAvrm + "AVRM_" + date + "_OU.txt.gpg");
            }
            catch (FileNotFoundException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Документот не е пронајден!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (DirectoryNotFoundException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Патеката до документот не постои!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (IOException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Грешка при преземање на документот!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            return temp;
        }
        public byte[] GetOU_FZOM(string date)
        {
            byte[] temp = null;

            if (string.IsNullOrEmpty(date))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Нема внесена вредност за датум!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            try
            {
                temp = File.ReadAllBytes(PathFzom + "FZOM_" + date + "_OU.txt.gpg");
            }
            catch (FileNotFoundException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Документот не е пронајден!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (DirectoryNotFoundException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Патеката до документот не постои!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (IOException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Грешка при преземање на документот!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            return temp;
        }
        public byte[] GetOU_FPIOM(string date)
        {
            byte[] temp = null;

            if (string.IsNullOrEmpty(date))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Нема внесена вредност за датум!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            try
            {
                temp = File.ReadAllBytes(PathFpiom + "FPIOM_" + date + "_OU.txt.gpg");
            }
            catch (FileNotFoundException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Документот не е пронајден!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (DirectoryNotFoundException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Патеката до документот не постои!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            catch (IOException)
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Грешка при преземање на документот!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }
            return temp;
        }
    }
}
