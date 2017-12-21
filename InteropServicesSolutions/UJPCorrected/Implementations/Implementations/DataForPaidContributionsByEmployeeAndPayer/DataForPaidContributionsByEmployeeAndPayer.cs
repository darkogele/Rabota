using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Interfaces.IDataForPaidContributionsByEmployeeAndPayer;

namespace Implementations.Implementations.DataForPaidContributionsByEmployeeAndPayer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class DataForPaidContributionsByEmployeeAndPayer : IDataForPaidContributionsByEmployeeAndPayer
    {
        string PathAvrm = ConfigurationSettings.AppSettings.Get("PathAvrmPP");
        string PathFzom = ConfigurationSettings.AppSettings.Get("PathFzomPP");
        string PathFpiom = ConfigurationSettings.AppSettings.Get("PathFpiomPP");
        public byte[] GetPP_AVRM(string date, int aditionalFile)
        {
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
            byte[] output = null;
            try
            {
                if (aditionalFile == 0)
                    output = File.ReadAllBytes(PathAvrm + "AVRM_" + date + "_PP.zip.gpg");
                else
                    output = File.ReadAllBytes(PathAvrm + "AVRM_" + date + "_PP_D" + aditionalFile + ".zip.gpg");
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
            catch (Exception e)
            { }
            return output;
        }
        public byte[] GetPP_FZOM(string date, int aditionalFile)
        {
            byte[] output = null;
            try
            {
                if (aditionalFile == 0)
                    output = File.ReadAllBytes(PathFzom + "FZOM_" + date + "_PP.zip.gpg");
                else
                    output = File.ReadAllBytes(PathFzom + "FZOM_" + date + "_PP_D" + aditionalFile + ".zip.gpg");
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
            catch (Exception e)
            { }
            return output;
        }
        public byte[] GetPP_FPIOM(string date, int aditionalFile)
        {
            byte[] output = null;
            try
            {
                if (aditionalFile == 0)
                    output = File.ReadAllBytes(PathFpiom + "FPIOM_" + date + "_PP.zip.gpg");
                else
                    output = File.ReadAllBytes(PathFpiom + "FPIOM_" + date + "_PP_D" + aditionalFile + ".zip.gpg");
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
            catch (Exception e)
            { }
            return output;
        }
    }
}
