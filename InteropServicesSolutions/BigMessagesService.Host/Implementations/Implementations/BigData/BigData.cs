using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using Implementations.Contracts.BigData;
using Implementations.FailtModel;
using Implementations.Models;

namespace Implementations.Implementations.BigData
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class BigData : IBigData
    {
        //public Stream GetLargeObject()
        //{
        //    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"BigDataFile\oversize_pdf_test_0.pdf");

        //    try
        //    {
        //        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //        return stream;
        //        //FileStream imageFile = File.OpenRead(filePath);
        //        //return imageFile;
        //    }
        //    catch (FaultException<InteropFault> ex)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(String.Format("An exception was thrown while trying to open file {0}", filePath));
        //        Console.WriteLine("Exception is: ");
        //        Console.WriteLine(ex.ToString());
        //        throw;
        //    }
        //}
        public DocumentModel GetLargeDoc(string type)
        {
            try
            {
                var output = new DocumentModel();
                string filePath;

                if (type == "doc")
                {
                    filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["BigDataFilePathDoc"]);
                }
                else if (type == "pdf")
                {
                    filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["BigDataFilePathPdf"]);
                }
                else
                {
                    var ex = new InteropFault
                    {
                        Result = false,
                        ErrorMessage = "Сервисот на институцијата врати грешка.",
                        ErrorDetails = "Немате внесено кој документ го сакате!"
                    };
                    throw new FaultException<InteropFault>(ex, ex.ErrorMessage + " " + ex.ErrorDetails);
                }
                
                if (File.Exists(filePath))
                {
                    byte[] documentBytes = File.ReadAllBytes(filePath);
                    output.HasDocument = true;
                    output.Message = "Сервисот на институцијата врати порака: Успешна операција!";
                    output.Document = documentBytes;
                }
                else
                {
                    output.HasDocument = false;
                    output.Message = "Сервисот на институцијата врати порака: Настаната е грешка при наоѓање на документот. Обидете се повторно!";
                    output.Document = null;
                }
                return output;
            }
            catch (FaultException<InteropFault> ex)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new Exception("Сервисот кој го повикавте врати грешка: " + exception.Message);
            }
        }
    }
}
