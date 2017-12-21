using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using Renci.SshNet;
using TestAKNWebService.ImotenListDokument;
using TestAKNWebService.KopijaKatastarskiPlanDocument;
using TestAKNWebService.TSAdapterProduction;
using TestAKNWebService.TSAKN_StS;

namespace TestAKNWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            //String Host = "10.177.159.70";
            //int Port = 22;
            //String LocalDestinationFilename = "C:\\IMOTEN_LIST_5527646_1_8712.PDF";
            //String Username = "king";
            //String Password = "K1NG123";
            //var client = new TestAKNWebService.AKNCopyOfCadastralPlan.IntegracijaWSImplClient();
            //var temp = client.generateDocument("1", "1", "8712", "2", "0", "2001");//2001 imoten list
            //var temp = client.generateDocument("25", "23", "", "5837", "0", "1014");//1014 kopija od katastarski plan
            //var temp = client.generateDocument("25", "167", "55736", "", "0", "2005");//2005 podatoci za infrastrukturni objekti
            //string remoteDirectory = temp.filePath;
            //using (var sftp = new SftpClient(Host, Port, Username, Password))
            //{
            //    sftp.Connect();
            //    byte[] arr1 = sftp.ReadAllBytes(temp.filePath + "//" + temp.fileName);
            //    using (var file = File.OpenWrite(LocalDestinationFilename))
            //    {
            //        byte[] arr = sftp.ReadAllBytes(temp.filePath + "//" + temp.fileName);
            //        File.WriteAllBytes("C:\\TempKKP1.pdf", arr);

            //        sftp.DownloadFile(temp.filePath + "//" + temp.fileName, file);

            //    }

            //    sftp.Disconnect();
            //}
           //var temp1 = client.getPlistInfo("1", "1", "8689", "2", "0");
            //var temp1 = client.getPlistInfo("1", "2", "8689", "2", "0");
            // var temp = client.generateDocument("1", "1", "8712", "2", "1", "2001");//2001 imoten list
            // var temp1 = client.getPlistInfo("25", "23", "", "5837", "0");
            //var temp = client.generateDocument("25", "23", "", "5837", "0", "1014");//1014 kopija od katastarski plan


            //55736,55817,55897
            //var temp = client.getPlistInfo("25", "167", "55736", "", "0");
            //var temp1 = client.generateDocument("25", "167", "55736", "", "1", "2005");//2005 podatoci za infrastrukturni objekti

            // var clientPListDoc = new ImotenListDokument.AKNPListDocClient();
            //var temp = clientPListDoc.GetPListDoc("25", "23", "", "5837", false);

            //var clientCPlanDoc = new KopijaKatastarskiPlanDocument.AKNCPlanDocClient();
            //var temp = clientCPlanDoc.GetCPlanDoc("1", "1", "8689", "2", false);

            //var clientDataForIFDoc = new PodatociInfrastrukturniObjektiDokument.AKNDataForIFDocClient();
            //var temp = clientDataForIFDoc.GetDataForIFDoc("1", "1", "8689", "2", false);
            //var client = new AKNmun1.AKNMunicipalityClient();
            // var output = client.GetCMunicipalities("1");


            //Testing TS AKN and Cadastral parcel
            //try
            //{
            //    //System.Net.ServicePointManager.ServerCertificateValidationCallback =
            //    //((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira 
            //    //var client = new TSAdapterTest.TSAdapterTestClient();
            //    //var output = client.GetTSTest("6325270");

            //    //var client = new AKNadapterservice.AKNServiceClient();
            //    //var output = client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");
            //    //Console.WriteLine(output);
            //    //Console.ReadLine();


            //    var client = new AKNCopyOfCadastralPlan.IntegracijaWSImplClient();
            //    var info = client.getPlistInfo("1", "1", "8712", "2", "0");
            //    var genDoc = client.generateDocument("1", "1", "8712", "2", "1", "2001");
            //    Console.WriteLine(info);
            //    Console.WriteLine(genDoc);
            //    Console.ReadLine();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine("Se sluci greska vo client.GetCadastrialParcel: " + exception);
            //    Console.ReadLine();
            //}


            //Testing TSAdapterProduction Tekovna sostojba za AKN system to system
            try
            {
                //PROD
                var client = new CRM_TS_AKNClient();

                // TEST
                //var client = new CRM_TS_AKNClient();

                var param = string.Empty;
                //var certificate = new X509Certificate2("C:\\UserAKN.pfx", "UserAKN#tst!");
                var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");

                var xdoc = new XDocument(
                  new XElement("CrmRequest",
                       new XAttribute("ProductName", "OSSLECView"),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
                       new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
                        new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
                        new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
                        new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
                        new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
                           new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID")))
                       )
                   );


                var xmldoc = new XmlDocument();
                using (var xmlReader = xdoc.CreateReader())
                {
                    xmldoc.Load(xmlReader);
                }
                param = SignXml(xmldoc, certificate);
                var unsigned = xmldoc.OuterXml;

                Console.WriteLine("pred client.Get_TS_AKN");
                var output = client.Get_TS_AKN(param);
                Console.WriteLine("posle client.Get_TS_AKN");
                Console.WriteLine(output);
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Se sluci greska vo client.Get_TS_AKN: " + exception);
                Console.ReadLine();
            }




            ////Testing Fault adapter
            //try
            //{
            //    var client = new FaultAKNService.AdapterServiceAKNClient();
            //    var output = client.GetCadastrialParcel("mio", "katastarservis", "1", "1", "2");
            //    Console.WriteLine(output);
            //    Console.ReadLine();
            //}
            //catch (FaultException ex)
            //{
            //    Console.WriteLine("Se sluci greska vo povikuvanje na servis: {0}", ex.Reason);
            //    //Console.WriteLine("source: {0}", ex.Detail.Source);
            //    //Console.WriteLine("target: {0}", ex.Detail.Target);
            //    //Console.WriteLine("stack trace: {0}", ex.Detail.Stack);

            //    //Console.WriteLine("Se sluci greska vo povikuvanje na servis: " + ex.Message);
            //    Console.ReadLine();
            //}
         
        }
        public static string SignXml(XmlDocument xmlDoc, X509Certificate2 cert)
        {
            var rsaKey = (RSACryptoServiceProvider)cert.PrivateKey;
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (rsaKey == null)
                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(xmlDoc);
            var XMLSignature = signedXml.Signature;
            signedXml.SigningKey = rsaKey;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));
            XMLSignature.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            return xmlDoc.InnerXml;
        }
    }
}
