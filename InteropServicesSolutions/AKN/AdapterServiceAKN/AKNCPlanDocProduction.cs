using System;
using Renci.SshNet;

namespace AdapterServiceAKN
{
    public class AKNCPlanDocProduction : IAKNCPlanDocProduction
    {
        private const String Host = "10.177.159.70";
        private const int Port = 22;
        private const String Username = "king";
        private const String Password = "K1NG123";

        public AKNDocOutput GetCPlanDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool showEMB)
        {
            try
            {
                var output = new AKNDocOutput();

                var client = new AKNDocsServiceProduction.IntegracijaWSImplClient();
                var info = client.getPlistInfo(opstina, katastarskaOpstina, brImotenList, brParcela, "0");

                if (info == null || !string.IsNullOrEmpty(info.errmsg) || info.idPtype != "1014")
                {
                    output.HasDocument = false;
                    output.Message = "Не постои катастарски план за дадените параметри!";
                    output.Document = null;
                    return output;
                }

                string show = showEMB ? "1" : "0";
                var docInfo = client.generateDocument(opstina, katastarskaOpstina, brImotenList, brParcela, show, "1014");//1014 kopija od katastarski plan
                if (docInfo.errmsg == null)
                {
                    using (var sftp = new SftpClient(Host, Port, Username, Password))
                    {
                        sftp.Connect();
                        byte[] arr = sftp.ReadAllBytes(docInfo.filePath + "//" + docInfo.fileName);
                        output.Document = arr;
                        output.HasDocument = true;
                        output.Message = "Успешна операција!";
                        sftp.Disconnect();
                    }
                }
                else
                {
                    output.Document = null;
                    output.HasDocument = false;
                    output.Message = "Настаната е грешка при креирање на документот. Обидете се повторно!";
                }
                return output;
            }
            catch (Exception exception)
            {
                throw new Exception("Сервисот кој го повикавте врати грешка: " + exception.Message);
            }
        }
    }
}
