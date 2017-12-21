using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CRRMTest.AdapterListOfChanges;
using CRRMTest.AdapterListOfChangesTest;
using CRRMTest.AdapterListOfSubjects;
using CRRMTest.AdapterListOfSubjectsTest;
using CRRMTest.AdapterTSFull;
using CRRMTest.CRM;

namespace CRRMTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new CRM_TS_CURMClient();

            //PROD ListOfSubjects
            var client = new ListOfSubjectsCUClient();

            //TEST ListOfSubjects
            //var client = new ListOfSubjectsCUTestClient();

            //PROD ListOfChanges
            //var client = new ListOfChangesCUClient();

            //TEST ListOfChanges
            //var client = new ListOfChangesCUTestClient();

            
            //var client = new CURM_TEST.Test_TS_CURMClient();
            //var client1 = new webreference.XmlWebService();
            //X509Certificate2 certificate = new X509Certificate2("C:\\UJP_tst_14.06.2012.pfx", "CR!tst");
            var param = string.Empty;
            var certificate = new X509Certificate2("C:\\UserCU.pfx", "CR!tst");

            //template za povikuvanje na Tekovna sostojba za UJP, testna okolina 
            // XDocument Xdoc = new XDocument(
            //     new XElement("CrmRequest",
            //     //new XAttribute("ProductName", "LECViewTest"),
            //    new XAttribute("ProductName", "LEOSSCurrentView"),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVLEInfo"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVUnits"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVActors"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVOwners"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVActivities"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVMembership"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVFounding"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID"))),
            //    new XElement("Parameters", new XAttribute("TemplateName", "CVLECourt"),
            //        new XElement("Parameter", "4007921", new XAttribute("Name", "@LEID")))
            //   )
            //);


            //template za povikuvanje na Tekovna sostojba za CURM, produkciska okolina 
            //var xdoc = new XDocument(
            //       new XElement("CrmRequest",
            //           new XAttribute("ProductName", "CU1"),
            //           new XElement("Parameters", new XAttribute("TemplateName", "CU11"),
            //               new XElement("Parameter", "7094370", new XAttribute("Name", "@LEID"))),
            //           new XElement("Parameters", new XAttribute("TemplateName", "CU12"),
            //               new XElement("Parameter", "7094370", new XAttribute("Name", "@LEID"))),
            //           new XElement("Parameters", new XAttribute("TemplateName", "CU13"),
            //               new XElement("Parameter", "7094370", new XAttribute("Name", "@LEID")))
            //           )
            //       );


            //template za povikuvanje na ListOfSubjects na produkcija
            var xdoc = new XDocument(
                   new XElement("CrmRequest",
                       new XAttribute("ProductName", "CU_LEIDTableUpdate"),
                       new XElement("Parameters", new XAttribute("TemplateName", "LEIDTableUpdate"),
                           new XElement("Parameter", "4641655", new XAttribute("Name", "@LEID")),
                            new XElement("Parameter", "1", new XAttribute("Name", "@UpdateType")),
                            new XElement("Parameter", "1", new XAttribute("Name", "@ListType")))
                       )
                   );


            var xmldoc = new XmlDocument();
            using (var xmlReader = xdoc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }
            param = SignXml(xmldoc, certificate);
            var unsigned = xmldoc.OuterXml;
            
            try
            {
                //CRM_orig.XmlSoapHeader header = new CRM_orig.XmlSoapHeader();
                //client1.ClientCredentials = new System.ServiceModel.Description.ClientCredentials()
                //client1.ClientCertificates.Add(certificate);
                //string output1 = client1.ProcessSignedRequest(param);

                //var output1 = client1.ProcessRequest();
                //var output = client.Get_TS_CURM(param);

                //var output = client.Get_TS_CURM(param);
                //Console.WriteLine("Uspeshno povikuvanje!");
                //Console.WriteLine(output);

                var result = client.GetSubjectsCU(param);
                Console.WriteLine("Uspeshno povikuvanje!");
                Console.WriteLine(result);
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message + " / " + ex.InnerException);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " / " + ex.InnerException);
            }
            Console.ReadLine();
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
