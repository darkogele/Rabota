using System;
using System.Web;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Handler.Helper.SOAP;
using Interop.CC.Models.RepositoryContracts;
using Interop.CCSimulation.Handler.External.NinjectConfig;
using KIBS;
using System.Linq;
using Newtonsoft.Json;
using Ninject;

namespace Interop.CCSimulation.Handler.External
{
    public class InteropCCSimulationHandlerExternal : IHttpHandler
    {
       private readonly ISoapFaultRepository _soapFaultRepo;
        private readonly ILogger _logger;
        private readonly IMimMsgHelper _mimMsgHelper;
        private readonly IRequestHelper _requestHelper;
        private readonly ISoapRequestHelper _soapRequestHelper;
        private readonly IValidXmlMsgHelper _validXmlMsgHelper;
        private readonly IMessageLogsRepository _messageLogsRepository;

        public InteropCCSimulationHandlerExternal()
        {
            using (IKernel kernel = new StandardKernel(new WCFNinject()))
            {
                _soapFaultRepo = kernel.Get<ISoapFaultRepository>();
                _logger = kernel.Get<ILogger>();
                _mimMsgHelper = kernel.Get<IMimMsgHelper>();
                _requestHelper = kernel.Get<IRequestHelper>();
                _soapRequestHelper = kernel.Get<ISoapRequestHelper>();
                _validXmlMsgHelper = kernel.Get<IValidXmlMsgHelper>();
                _messageLogsRepository = kernel.Get<IMessageLogsRepository>();
            }
        }
        public bool IsReusable
        {
            get { return false; }
        }

        // Опис: Метод кој овозможува процесирање на НТТР Web повици
        // Влезни параметри: HttpContext context
        // Излезни параметри: /
        public void ProcessRequest(HttpContext context)
        {
            var transactionId = Guid.NewGuid();
            var validTId = true;
            try
            {
                var urlSegments = _requestHelper.GetUrlSegments(context.Request.RawUrl);
                _logger.Info("context.Request.RawUrl: " + context.Request.RawUrl);
                const string ourMim = "MIM1$$";
                const string secoundMim = "MIM2$$";
                bool routingTokenContainOurMim = urlSegments.RoutingToken.Contains(ourMim);
                //_logger.Info("Go sodrzi nasiot Mim: " + routingTokenContainOurMim);
                bool routingTokenContainsSecoundMim = urlSegments.RoutingToken.Contains(secoundMim);
                //_logger.Info("Go sodrzi vtoriot Mim: " + routingTokenContainsSecoundMim);
                if (!routingTokenContainOurMim && !routingTokenContainsSecoundMim)
                {
                    //Dodavame MIM1$$ na RountingToken ako istiot nema
                    urlSegments.RoutingToken = "MIM1$$" + urlSegments.RoutingToken;
                    //_logger.Info("Nas mim e, treba da dodade i sea Routing token e: " + urlSegments.RoutingToken);
                }
                var logUrlSegments = JsonConvert.SerializeObject(urlSegments);

                _logger.Info("urlSegments e: " + JsonConvert.SerializeObject(logUrlSegments) + "urlsegment: " + context.Request.RawUrl);
                var contentType = context.Request.ContentType;

                var action = contentType.Split(';').Last();
                _logger.Info("action e:" + action);
                var actionName = string.Empty;
                var executionMethodName = string.Empty;
                if (!string.IsNullOrEmpty(action))
                {
                    actionName = action.Substring(action.IndexOf('"') + 1);
                    executionMethodName = actionName.Substring(0, actionName.Length - 1);
                }

                _logger.Info("actionName e:" + actionName);
                //_logger.Info("executionMethodName e:" + executionMethodName);
                var soapBody = string.Empty;
                try
                {
                    soapBody = _requestHelper.GetOnlySoapBody(context.Request.InputStream);
                    _logger.Info("soapBody e: " + soapBody);
                }
                catch (Exception ex)
                {
                    _logger.Error("Nastanata e greska kaj GetOnlySoapBody", ex);
                }


                var mimMsg = _mimMsgHelper.CreateMimRequestMsg(urlSegments, transactionId.ToString());

                #region Log SOAP Request Message

                var logSoap = AppSettings.Get<bool>("LogSoap");
                if (logSoap)
                {
                    _logger.Info(soapBody + Environment.NewLine + "ContentType: " + contentType + Environment.NewLine +
                                 "Soap action: " + actionName);
                }

                #endregion

                if (string.IsNullOrEmpty(soapBody) || !urlSegments.IsUrlCorrrect)
                {
                    validTId = false;
                    context.Response.StatusCode = 400;
                    context.Response.End();
                }

                #region Log MIM Message

                _mimMsgHelper.LogInitialMimMessage(mimMsg, _logger);

                #endregion

                if (urlSegments.Async)
                {
                    //TODO Async req
                }
                var ownCert = _mimMsgHelper.LoadOwnCertificate(_logger);
                var privateKey = _mimMsgHelper.GetPrivateKey(ownCert.PrivateKey);
                string original = soapBody;
                var publicKey = _mimMsgHelper.GetPublicKeyForProvider(urlSegments.RoutingToken);
                var encryptedBlock = _mimMsgHelper.EncryptSoapBody(original, publicKey.PublicKeyRsa);
                //var soapMethodName = _requestHelper.GetSoapMethodName(soapBody);

                #region Create MIM Message

                //set values to missing properties
                mimMsg.Body.MimBody.Message = Convert.ToBase64String(encryptedBlock.EncryptedData);
                mimMsg.Header.CryptoHeader.Key = Convert.ToBase64String(encryptedBlock.EncryptedSessionKey);
                mimMsg.Header.CryptoHeader.InitializationVector = Convert.ToBase64String(encryptedBlock.Iv);
                mimMsg.Header.MimHeader.ServiceMethod = executionMethodName;
                mimMsg.Header.MimHeader.PublicKey = ownCert.CertString;
                _logger.Info("mimMsg.Header.MimHeader.PublicKey is: " + ownCert.CertString);


                var doc = _mimMsgHelper.CreateMimXmlMsg(mimMsg);
                _logger.Info("MIM message before KIBS" + doc);

                try
                {
                    KIBSResponse resultkibs = null;
                    if (mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall)
                    {
                        resultkibs = new KIBSResponse
                        {
                            Hash = "InteropTestCommunicationCallHash"
                        };
                    }
                    else
                    {
                        if (AppSettings.Get<string>("KIBSEnviroment") != null)
                        {
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Test")
                            {
                                _logger.Info("Se koristi testen KIBS.");
                                resultkibs = KIBS.KIBS.GenerateTimeStamp(doc.ToString());
                            }
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Production")
                            {
                                _logger.Info("Se koristi produkciski KIBS.");
                                resultkibs = KIBS.KIBS.GenerateTimeStampProduction(doc.ToString());
                            }
                        }
                    }

                    try
                    {
                        //resultkibs = new KIBSResponse
                        //{
                        //    Hash = "InteropTestCommunicationCallHash",
                        //    TimeStamp = DateTime.Now
                        //};
                        if (resultkibs != null)
                        {
                            _logger.Info("Pred update message log: " + mimMsg.Header.MimHeader.Dir + "/" + mimMsg.Header.MimHeader.ServiceMethod + "/" + mimMsg.Header.MimHeader.PublicKey + "/" + mimMsg.Header.MimHeader.TransactionId);

                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp
                            // _messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.TimeStamp,
                            //resultkibs.Hash, mimMsg.Header.MimHeader.ServiceMethod, mimMsg.Header.MimHeader.PublicKey,
                            //mimMsg.Header.MimHeader.TransactionId);

                            _messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.Hash, mimMsg.Header.MimHeader.ServiceMethod, mimMsg.Header.MimHeader.PublicKey,
                           mimMsg.Header.MimHeader.TransactionId);

                            //mimMsg.Header.MimHeader.TimeStamp = resultkibs.TimeStamp; //.ToString();

                            mimMsg.Header.MimHeader.TimeStamp = DateTime.Now;
                            mimMsg.Header.MimAdditionalHeader.TimeStampToken = resultkibs.Hash;

                            //_logger.Info("posle kibs so result: " + resultkibs.TimeStamp);
                        }

                    }
                    catch (Exception exception)
                    {
                        _logger.Error("Error while updating mim message: ", exception);
                        throw new Exception("Неуспешна комуникација со KIBS!");
                    }

                }
                catch (Exception ex)
                {
                    _logger.Info("KIBS error posle CreateMimRequestMsg: " + ex.Message);
                    throw new Exception("KIBS врати грешка!");
                }


                var mimMsgXml = _mimMsgHelper.CreateMimSignedXmlMsg(mimMsg, ownCert, _logger);

                _logger.Info("Created MIM signed xml message: " + mimMsgXml);

                #endregion



                var urlToBizTalk = AppSettings.Get<string>("URLWebRequest");
                ResponseInteropCommunication responseBizTalk = null;
                try
                {
                    _logger.Info("pred responseBizTalk mimMsgXml" + mimMsgXml);
                    _logger.Info("pred responseBizTalk contentType" + contentType);
                    _logger.Info("pred responseBizTalk urlToBizTalk" + urlToBizTalk);
                    responseBizTalk = _soapRequestHelper.Execute(mimMsgXml, contentType, urlToBizTalk, _logger);
                    _logger.Info("Pominal kaj responseBizTalk!");
                    //Real MIM message
                    var soapFaultUnwrapped = _soapRequestHelper.UnwrapSoapFaultMessage(responseBizTalk.Response);
                    _logger.Info("Pominal kaj soapFaultUnwrapped!");
                    _logger.Info(responseBizTalk.Response, "BTResponse");

                    if (soapFaultUnwrapped.Body.Fault != null)
                    {
                        // BizzTalk error
                        if (soapFaultUnwrapped.Body.Fault.Code.Subcode.value == "10000")
                        {
                            if (soapFaultUnwrapped.Body.Fault.Code.value == "10001")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    AppSettings.Get<string>("SoapFaultAccessMapping"));
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10002")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    AppSettings.Get<string>("SoapFaultParticipantInactive"));
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10003")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    AppSettings.Get<string>("SoapFaultServiceUnReachable"));
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10004")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    AppSettings.Get<string>("SoapFaultKIBSUnReachable"));
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10005")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    soapFaultUnwrapped.Body.Fault.Reason.Text.value);
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10010")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    soapFaultUnwrapped.Body.Fault.Reason.Text.value);
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10007")
                            {
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    AppSettings.Get<string>("SoapFaultUnsuccessfulRequest"));
                            }
                            else if (soapFaultUnwrapped.Body.Fault.Code.value == "10000")
                            {
                                //ova e vo slucaj koga CC-to e nedostapno
                                throw new Exception("BTMstatus10000BTMend;" +
                                                    AppSettings.Get<string>("SoapFaultServiceUnReachable"));
                            }
                            throw new Exception("BTMstatus10000BTMend;" +
                                                soapFaultUnwrapped.Body.Fault.Reason.Text.value);
                        }
                        // Our error
                        else
                        {
                            throw new Exception(soapFaultUnwrapped.Body.Fault.Reason.Text.value);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                var mimMsgResponse = _soapRequestHelper.UnwrapMimMessage(responseBizTalk.Response);

                #region Log MIM Message

                _mimMsgHelper.LogMimMessage(mimMsgResponse, _logger);

                #endregion

                #region ValidateMsg

                try
                {
                    if (_soapRequestHelper.ValidateSignature(responseBizTalk.Response, ownCert.CertString, _logger))
                        _validXmlMsgHelper.ValidateXml(responseBizTalk.Response);
                }
                catch (Exception ex)
                {
                    _logger.Error("Nastanala greska kaj ValidateSignature", ex);
                }

                #endregion

                var symmetricKey = mimMsgResponse.Header.CryptoHeader.Key;
                var iVector = mimMsgResponse.Header.CryptoHeader.InitializationVector;
                var decryptBody = string.Empty;

                try
                {
                    _logger.Info("symmetricKey" + symmetricKey);
                    _logger.Info("iVector" + iVector);
                    decryptBody =
                        _mimMsgHelper.DecryptSoapBody(
                            Convert.FromBase64String(mimMsgResponse.Body.MimBody.Message),
                            Convert.FromBase64String(symmetricKey), Convert.FromBase64String(iVector), privateKey);
                }
                catch (Exception ex)
                {
                    _logger.Error("Nastanala greska kaj DecryptSoapBody", ex);
                }


                #region Log DecryptBody

                if (AppSettings.Get<bool>("LogDecryptBody"))
                {
                    _logger.Info("Decripting MIM message: " + decryptBody);
                }

                #endregion

                #region KIBS after BT

                try
                {
                    KIBSResponse resultkibsAfterBt = null;
                    if (mimMsgResponse.Header.MimAdditionalHeader.IsInteropTestCommunicationCall)
                    {
                        resultkibsAfterBt = new KIBSResponse
                        {
                            Hash = "InteropTestCommunicationCallHash"
                        };
                    }
                    else
                    {
                        if (AppSettings.Get<string>("KIBSEnviroment") != null)
                        {
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Test")
                            {
                                _logger.Info("Se koristi testen KIBS.");
                                resultkibsAfterBt = KIBS.KIBS.GenerateTimeStamp(responseBizTalk.Response);
                            }
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Production")
                            {
                                _logger.Info("Se koristi produkciski KIBS.");
                                resultkibsAfterBt = KIBS.KIBS.GenerateTimeStampProduction(responseBizTalk.Response);
                            }
                        }
                    }
                    try
                    {
                        //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                        //Sega se zapishuva DateTime.Now vo poleto Timestamp

                        //_messageLogsRepository.UpdateMessageLog(mimMsgResponse.Header.MimHeader.Dir,
                        //    resultkibsAfterBt.TimeStamp, resultkibsAfterBt.Hash, string.Empty, string.Empty,
                        //    mimMsg.Header.MimHeader.TransactionId);


                        _messageLogsRepository.UpdateMessageLog(mimMsgResponse.Header.MimHeader.Dir, resultkibsAfterBt.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);

                        //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                        //Sega se zapishuva DateTime.Now vo poleto Timestamp

                        //mimMsgResponse.Header.MimHeader.TimeStamp = resultkibsAfterBt.TimeStamp; //.ToString();

                        mimMsgResponse.Header.MimHeader.TimeStamp = DateTime.Now;
                        mimMsgResponse.Header.MimAdditionalHeader.TimeStampToken = resultkibsAfterBt.Hash;
                    }
                    catch (Exception exception)
                    {
                        _logger.Error("Error while updating mim message: ", exception);
                        throw new Exception("Неуспешна комуникација со KIBS!");
                    }

                }
                catch (Exception ex)
                {
                    _logger.Info("KIBS error after BT: " + ex.Message);
                    throw new Exception("KIBS врати грешка!");
                }

                #endregion

                #region Log SOAP Response Message

                if (logSoap)
                {
                    _logger.Info(responseBizTalk.Response, "BizTalk response");
                }

                #endregion

                string decryptedResponseBodyWrappedInSoapEnvelope =
                    "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body>" + decryptBody +
                    "</s:Body></s:Envelope>";
                _logger.Info(decryptedResponseBodyWrappedInSoapEnvelope, "decryptedResponseBodyWrappedInSoapEnvelope");
                HttpContext.Current.Response.ContentType = "application/soap+xml";
                context.Response.Write(decryptedResponseBodyWrappedInSoapEnvelope);

            }
            catch (Exception ex)
            {
                //AccessMap=0;Status=;StatusDescription="";IsActive=1;IsExternal=1;Uri="http://internalHandler-fpiom/";KIBS=True;
                if (validTId)
                {
                    var soapFault = new SoapFaultMessage();

                    if (ex.Message.StartsWith("BTMstatus10000BTMend"))
                    {
                        // BizzTalk error handled - Custom
                        //var soapFaultError = _soapRequestHelper.BTsoapFault(ex.Message);
                        // BizzTalk error handled - Kornel
                        var soapFaultError = ex.Message.Split(';')[1];

                        soapFault = _mimMsgHelper.CreateSoapFault("Code value", "Code - SubCode value",
                            "Details - MaxTime value", soapFaultError);
                    }
                    else
                    {
                        soapFault = _mimMsgHelper.CreateSoapFault("Code value", "Code - SubCode value",
                            "Details - MaxTime value", ex.Message);
                    }

                    _logger.Error(soapFault.Body.Fault.Reason.Text.value, ex, "Error on response");

                    var soapFaultDB = _soapRequestHelper.CreateSoapFaultDB(transactionId,
                        soapFault.Body.Fault.Code.value, soapFault.Body.Fault.Code.Subcode.value,
                        soapFault.Body.Fault.Detail.maxTime, soapFault.Body.Fault.Reason.Text.value);

                    _soapFaultRepo.InsertSoapFault(soapFaultDB);

                    var soapFaultXml = _mimMsgHelper.CreateFaultMessage(soapFault);

                    HttpContext.Current.Response.ContentType = "application/soap+xml";
                    HttpContext.Current.Response.Write(soapFaultXml.InnerXml);
                }

            }
        }
        public void ProcessRequest(HttpContextBase context)
        {
            _logger.Info("Simulation External Test HttpContextBase");
        }
    }
}