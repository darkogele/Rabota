using System;
using System.IO;
using System.ServiceModel;
using Contracts.DataAccessLibrary.InteropFault;
using Contracts.Interfaces.IGetDoc_OU_NP_PP;
using Contracts.Models.GetDoc_OU_NP_PP;

namespace Implementations.Implementations.GetDoc_OU_NP_PP
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org")]
    public class GetDoc_OU_NP_PP : IGetDoc_OU_NP_PP
    {
        //private const String Host = "10.177.159.70";
        //private const int Port = 22;
        //private const String Username = "king";
        //private const String Password = "K1NG123";

        public DocOuNpPpOutputData GetDocOU_NP_PP(string institution, string service, string date, string additionalInfo)
        {
            if (string.IsNullOrEmpty(institution) || string.IsNullOrEmpty(service) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(additionalInfo))
            {
                var ex = new InteropFault
                {
                    Result = false,
                    ErrorMessage = "Сервисот врати грешка.",
                    ErrorDetails = "Мора да бидат внесени вредности за сите параметри!"
                };
                throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
            }

            var output = new DocOuNpPpOutputData();
            string filepath = "";

            try
            {
                //additionalInfo moze i bool da e
                if (additionalInfo == "1")
                {
                    //var extension = (service.ToLower() == "ou" ? "txt.gpg" : service.ToLower() == "np" ? "doc.gpg" : service.ToLower() == "pp" ? "zip.gpg" : "");
                    //filepath = String.Format(@"D:\WS_okolina\{0}\{1}\{2}_{3}_{4}_D.{5}", institution, service, institution, date, service, extension);
                    filepath = String.Format(@"D:\WS_okolina\{0}\{1}\{2}_{3}_{4}_D.pdf", institution, service, institution, date, service);
                }
                else
                {
                    //var extension = (service.ToLower() == "ou" ? "txt.gpg" : service.ToLower() == "np" ? "doc.gpg" : service.ToLower() == "pp" ? "zip.gpg" : "");
                    //filepath = String.Format(@"D:\WS_okolina\{0}\{1}\{2}_{3}_{4}.{5}", institution, service, institution, date, service, extension);
                    filepath = String.Format(@"D:\WS_okolina\{0}\{1}\{2}_{3}_{4}.pdf", institution, service, institution, date, service);
                }

                //using (var sftp = new SftpClient(Host, Port, Username, Password))
                //{
                //sftp.Connect();
                //byte[] arr = sftp.ReadAllBytes(filepath);
                byte[] arr = File.ReadAllBytes(filepath);

                if (arr != null)
                {
                    output.Document = arr;
                    output.HasDocument = true;
                    output.Message = "Успешна операција!";
                    return output;
                }
                else
                {
                    output.Document = null;
                    output.HasDocument = false;
                    output.Message = "Документот е во фаза на обработка!";
                }
                //sftp.Disconnect();
                //}

                return output;
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
                //output.Document = null;
                //output.HasDocument = false;
                //output.Message = "Документот не е пронајден!";

                //return output;
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
                //output.Document = null;
                //output.HasDocument = false;
                //output.Message = "Патеката до документот не постои!";

                //return output;
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
                //output.Document = null;
                //output.HasDocument = false;
                //output.Message = "Грешка при преземање на документот!";

                //return output;
            }
        }
    }
}
