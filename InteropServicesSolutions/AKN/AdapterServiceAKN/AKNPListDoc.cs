using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdapterServiceAKN
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, Namespace = "http://interop.org/")]
    public class AKNPListDoc : IAKNPListDoc
    {
        String Host = "10.177.159.70";
        int Port = 22;
        String Username = "king";
        String Password = "K1NG123";
        public AKNDocOutput GetPListDoc(string opstina, string katastarskaOpstina, string brImotenList, string brParcela, bool ShowEMB)
        {
            AKNDocOutput output = new AKNDocOutput();
            var client = new AKNOriginalDocSvc.IntegracijaWSImplClient();
            var info = client.getPlistInfo(opstina, katastarskaOpstina, brImotenList, brParcela, "0");
            if (info.idPtype != "2001")
            {
                output.HasDocument = false;
                output.Message = "Не постои имотен лист со дадените парамети!";
                output.Document = null;
            }
            else
            {
                string show = "";
                if (ShowEMB)
                    show = "1";
                else
                    show = "0";
                var docInfo = client.generateDocument(opstina, katastarskaOpstina, brImotenList, brParcela, show, "2001");//2001 imoten list
                if(docInfo.errmsg == null)
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
            }
            return output;
        }
    }
}
