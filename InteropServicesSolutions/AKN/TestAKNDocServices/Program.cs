using System;
using System.IO;
using Renci.SshNet;
using TestAKNDocServices.AdapterCListDocTEST;
using TestAKNDocServices.AdapterIFDocTEST;
using TestAKNDocServices.AdapterPListDocTEST;
using TestAKNDocServices.OriginalDocServicesTEST;
using TestAKNDocServices.PListDocPROD;

namespace TestAKNDocServices
{
    class Program
    {
        static void Main(string[] args)
        {
            const string host = "10.177.159.70";
            const int port = 22;
            const string localDestinationFilename = "C:\\IMOTEN_LIST_5527646_1_8712.PDF";
            const string username = "king";
            const string password = "K1NG123";

            //#region Testing Imoten list dokument Original service

            //var realServiceClient = new IntegracijaWSImplClient();
            //var resultRealService = realServiceClient.getPlistInfo("1", "1", "8712", "2", "0");
            //if (resultRealService.idPtype != "2001")
            //{
            //    Console.WriteLine("Не постои имотен лист со дадените парамети!");
            //    Console.ReadLine();
            //}
            //else
            //{
            //    var generateDocFromRealService = realServiceClient.generateDocument("1", "1", "8712", "2", "0", "2001");
            //    if (resultRealService.errmsg == null)
            //    {
            //        using (var sftp = new SftpClient(host, port, username, password))
            //        {
            //            sftp.Connect();

            //            using (var file = File.OpenWrite(localDestinationFilename))
            //            {
            //                byte[] arr = sftp.ReadAllBytes(generateDocFromRealService.filePath + "//" + generateDocFromRealService.fileName);
            //                File.WriteAllBytes("C:\\TempKKP1.pdf", arr);
            //                sftp.DownloadFile(generateDocFromRealService.filePath + "//" + generateDocFromRealService.fileName, file);

            //            }
            //            Console.WriteLine("Успешна операција!");
            //            sftp.Disconnect();
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Настаната е грешка при креирање на документот. Обидете се повторно!");
            //    }
            //}

            //#endregion

            //#region Testing Adapter PListDoc Test service

            //var client = new AKNPListDocClient();
            //try
            //{
            //    var result = client.GetPListDoc("25", "997", "1024", "", false);
            //    if (!result.HasDocument)
            //    {
            //        Console.WriteLine("Не постои имотен лист со дадените парамети!");
            //    }
            //    else
            //    {
            //        Console.WriteLine(result.Message);
            //        using (var sftp = new SftpClient(host, port, username, password))
            //        {
            //            sftp.Connect();
            //            File.WriteAllBytes("C:\\TempKKP1.pdf", result.Document);
            //            Console.WriteLine("Успешна операција!");
            //            sftp.Disconnect();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}

            //Console.ReadLine();


            //#endregion

            //#region Testing Adapter CListDoc Test service

            //var client = new AKNCPlanDocClient();
            //try
            //{
            //    var result = client.GetCPlanDoc("25", "997", "", "1057", false);
            //    if (!result.HasDocument)
            //    {
            //        Console.WriteLine("Не постои катастарски план со дадените парамети!");
            //    }
            //    else
            //    {
            //        Console.WriteLine(result.Message);
            //        using (var sftp = new SftpClient(host, port, username, password))
            //        {
            //            sftp.Connect();
            //            File.WriteAllBytes("C:\\TempKKP1.pdf", result.Document);
            //            Console.WriteLine("Успешна операција!");
            //            sftp.Disconnect();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}

            //Console.ReadLine();

            //#endregion

            //#region Testing Adapter IFDoc Test service

            //var client = new AKNDataForIFDocClient();
            //try
            //{
            //    var result = client.GetDataForIFDoc("25", "83", "67403", "", false);
            //    if (!result.HasDocument)
            //    {
            //        Console.WriteLine("Не постои катастарски план со дадените парамети!");
            //    }
            //    else
            //    {
            //        Console.WriteLine(result.Message);
            //        using (var sftp = new SftpClient(host, port, username, password))
            //        {
            //            sftp.Connect();
            //            File.WriteAllBytes("C:\\TempKKP1.pdf", result.Document);
            //            Console.WriteLine("Успешна операција!");
            //            sftp.Disconnect();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}

            //Console.ReadLine();
            //#endregion


            #region PListDoc production service

            
            try
            {
                var client = new AKNPListDocProductionClient();
                var result = client.GetPListDoc("25", "997", "1024", "", false);
                if (!result.HasDocument)
                {
                    Console.WriteLine("Не постои катастарски план со дадените парамети!");
                }
                else
                {
                    Console.WriteLine(result.Message);
                    using (var sftp = new SftpClient(host, port, username, password))
                    {
                        sftp.Connect();
                        File.WriteAllBytes("C:\\TempKKP1.pdf", result.Document);
                        Console.WriteLine("Успешна операција!");
                        sftp.Disconnect();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
           


            #endregion

        }
    }
}
