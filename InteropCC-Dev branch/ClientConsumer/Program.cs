using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ClientConsumer.AKNServiceReference;
using ClientConsumer.EvalServiceReference;
using System;
using ClientConsumer.MetaServiceReference;
using ClientConsumer.Helpers;
using ClientConsumer.PIOMServiceReference;
using Interop.CC.Models.Models;
using System.Web.Services.Protocols;


namespace ClientConsumer
{
    class Program
    {

        private static void Main(string[] args)
        {
            //var client = new NextSenseSerRef.M1M2ServiceSoapClient();

            //NextSenseSerRef.Person person = new NextSenseSerRef.Person();
            //person.FirstName = "Dijana";
            //person.SurName = "Kostovska";
            //client.FullName(person);
            //var client = new NextSenseWebRef.M1M2Service();
            //client.SoapVersion = SoapProtocolVersion.Soap12;
            //NextSenseWebRef.Person person = new NextSenseWebRef.Person();
            //person.FirstName = "Dijana";
            //person.SurName = "Kostovska";
            //client.FullName(person);
            //string wsdl = null;
            //using (var wc = new WebClient())
            //{
            //    using (var stream = wc.OpenRead("http://localhost/EvalServiceSite/eval.svc?wsdl"))
            //    {
            //        using (var sr = new StreamReader(stream))
            //        {
            //            wsdl = sr.ReadToEnd();
            //        }
            //    }
            //}
            //var temp = wsdl;
            //Console.Write(wsdl);
            
            
            //var clientEval = new EvalServiceReference.EvalServiceClient("CustomBinding_IEvalService");
            //var eval = new Eval
            //{
            //    Comments = "InterOP",
            //    Id = Guid.NewGuid().ToString(),
            //    Submitter = "Daniel",
            //    TimeSubmitted = DateTime.Now
            //};

            //try
            //{
            //    clientEval.SubmitEval(eval);
            //    //metaServiceClient.RegisterService(ser);
            //    Console.WriteLine("uspesno!");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //var clientCRRM = new EvalServiceCRRM.EvalServiceClient("CustomBinding_IEvalService1");
            //var evalServiceCRRMData = clientCRRM.GetEvals();
            //if (evalServiceCRRMData != null)
            //{
            //    foreach (var eval1 in evalServiceCRRMData)
            //    {
            //        Console.WriteLine("evalServiceCRRMData ids are: " + eval1.Id);
            //    }
                
            //}

            //var client = new DataForEDB.DataForEDBClient("CustomBinding_IDataForEDB");
            //var data = client.GetEDB("6325270");

            var client = new PIOMServiceReference.DataForEnsurersClient("CustomBinding_IDataForEnsurers");
            var data = client.GetDataForEnsurees("02069864995001");


            //var testEndpoint = clientEval.Endpoint.Address + "/InteropTestCommunicationCall";
            //clientEval.Endpoint.Address = new EndpointAddress(testEndpoint);
            //var clientAKN = new Service_MACEDONIAN_CADASTRESoapClient();
            //var clientPIOM = new MTW112PortTypeClient("MTW112Port";)
            //var clientMeta = new MetaServiceClient("BasicHttpBinding_IMetaService");

            //var cb = new MessageInspectorBehavior();

            //Add the custom behaviour to the list of service behaviours.
            //clientEval.Endpoint.Behaviors.Add(cb);
            //clientAKN.Endpoint.Behaviors.Add(cb);
            //clientAKN.Endpoint.Behaviors.Add(cb);

            //cb.OnMessageInspected += (src, e) =>
            //{
            //    //if (e.MessageInspectionType == eMessageInspectionType.Request) request = e.Message;
            //    //else response = e.Message;
            //};


            //using (var logger = LoggingFactory.GetNLogger("testname"))
            //{
            //    //if (logLevel == "Error")
            //    logger.Error(String.Format("Error on {0} controller, {1} action.", "controllerr", "actionname"), "somthingwentwrong");
            //    // if (logLevel == "Info")
            //    //    logger.Info(String.Format("Info on {0} controller, {1} action.", controllerName, actionName), errors);
            //}

            // PIOM test example
            //try
            //{
            //    var PIOMdata = clientPIOM.penzioner1s("0606949469013");
            //    if (PIOMdata != null)
            //    {
            //        Console.WriteLine(PIOMdata);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            // AKN test example
            //try
            //{
            //    //ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            //    //{
            //    //    return true;
            //    //};
            //    var AKNData = clientAKN.VRATIPODATOCI_STRUKTURA_DZGR("mio", "katastarservis", "25", "997", "3238");
            //    if (AKNData != null)
            //    {
            //        Console.WriteLine(AKNData);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            // EVALS test example
            //try
            //{
            //    var allevall = clientEval.GetEvals();
            //    if (allevall != null)
            //    {
            //        foreach (var evall in allevall)
            //        {
            //            Console.WriteLine(evall.Submitter);
            //        }

            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}



            //try
            //{
            //    var allEvall = clientEval.GetEvals();
            //    if (allEvall != null)
            //    {
            //        foreach (var evall in allEvall)
            //        {
            //            Console.WriteLine(evall.Submitter);
            //        }

            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //try
            //{
            //    client.SubmitEval(eval);
            //    //metaServiceClient.RegisterService(ser);
            //    Console.WriteLine("uspesno!");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            // META test example

            //var metaServiceClient = new MetaServiceClient();



            //var dynamicChangeEndpoint = new EvalDynamicChangeEndpointReference.EvalServiceClient("BasicHttpBinding_IEvalService1");
            //dynamicChangeEndpoint.GetEvals();

            //var test = 0;

            //XDocument xdocument = null;

            //try
            //{

            //    xdocument = XDocument.Load("C:\\test.wsdl");
            //}
            //catch (Exception ex)
            //{

            //}
            //var ser = new Service();
            //ser.Code = "EvalService";
            //ser.Name = "EvalService";
            //ser.Wsdl = XDocument.Load("http://localhost/EvalServiceSite/eval.svc?wsdl").ToString();
            //metaServiceClient.RegisterService(ser);


            //  clientMeta.RegisterService(new Service { Code = "TestInstitut3441", Wsdl = "wsdlghf43gest421" });


            //  metaServiceClient.RegisterService(new Service { Code = "TestInstitut3441", Wsdl = "wsdlghf43gest421" });

            //var services = clientMeta.GetServices("UJP");
            //var service = clientMeta.GetService("UJP", "EvalService", "Request");

            //var providers = metaServiceClient.GetProviders();
            //var services = metaServiceClient.GetServices("AKN");
            //var service = metaServiceClient.GetService("AKN", "EvalService", "");

            //var a = "test";
            //var services = metaServiceClient.GetServices("UJP");
            //var service = metaServiceClient.GetService("UJP", "ProfKorisnici", "");



            //try
            //{
            //    var consumers = metaServiceClient.ListConsumers("ServiceA");
            //    if (consumers != null)
            //    {
            //        foreach (var consumer in consumers)
            //        {
            //            Console.WriteLine(consumer);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message); ;
            //}
            Console.ReadLine();

        }

    }
}
