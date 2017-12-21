using System;
using System.Web;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Handler.Helper.SOAP;
using Interop.CC.Models.RepositoryContracts;
using Newtonsoft.Json;
using Ninject;
using System.Linq;
using KIBS;

namespace Interop.CC.Handler.External
{
    public class InteropCCHandlerExternal : IHttpHandler
    {
        private readonly ISoapFaultRepository _soapFaultRepo;
        private readonly ILogger _logger;
        private readonly IMimMsgHelper _mimMsgHelper;
        private readonly IRequestHelper _requestHelper;
        private readonly ISoapRequestHelper _soapRequestHelper;
        private readonly IValidXmlMsgHelper _validXmlMsgHelper;
        private readonly IMessageLogsRepository _messageLogsRepository;

        // Опис: конструктор на класата InteropCCHandlerInternal
        // Влезни параметри: / 
        public InteropCCHandlerExternal()
        {
            _soapFaultRepo = Global.NewKernel.Get<ISoapFaultRepository>();
            _logger = Global.NewKernel.Get<ILogger>();
            _mimMsgHelper = Global.NewKernel.Get<IMimMsgHelper>();
            _requestHelper = Global.NewKernel.Get<IRequestHelper>();
            _soapRequestHelper = Global.NewKernel.Get<ISoapRequestHelper>();
            _validXmlMsgHelper = Global.NewKernel.Get<IValidXmlMsgHelper>();
            _messageLogsRepository = Global.NewKernel.Get<IMessageLogsRepository>();
        }

        // Опис: проперти кое овозможува кеширање на инстанцата и реупотреба при нареден повик
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

                _logger.Info("urlSegments e: " + JsonConvert.SerializeObject(logUrlSegments) + "urlsegment: " +
                             context.Request.RawUrl);
                var contentType = context.Request.ContentType;
                var action = contentType.Split(';').Last();
                //_logger.Info("action e:" + action);
                var actionName = string.Empty;
                var executionMethodName = string.Empty;
                if (!string.IsNullOrEmpty(action))
                {
                    actionName = action.Substring(action.IndexOf('"') + 1);
                    executionMethodName = actionName.Substring(0, actionName.Length - 1);
                }

                //_logger.Info("actionName e:" + actionName);
                //_logger.Info("executionMethodName e:" + executionMethodName);
                var soapBody = string.Empty;
                try
                {
                    var logGetSoapBody = AppSettings.Get<bool>("LogGetSoapBody");
                    if (logGetSoapBody)
                    {
                        _logger.Info(
                            "Pred da go zeme samo soap body od porakata " +
                            DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "BeforeGetOnlySoapBody");
                    }
                    soapBody = _requestHelper.GetOnlySoapBody(context.Request.InputStream);
                    if (logGetSoapBody)
                    {
                        _logger.Info(
                            "Otkako kje se zeme samo soap body od porakata " +
                            DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "AfterGetOnlySoapBody");
                    }
                    _logger.Info("soapBody e: " + soapBody);
                }
                catch (Exception ex)
                {
                    _logger.Error("Nastanata e greska kaj GetOnlySoapBody", ex);
                }

                var logCreateMimRequestMsg = AppSettings.Get<bool>("LogCreateMimRequestMsg");
                if (logCreateMimRequestMsg)
                {
                    _logger.Info("Pred CreateMimRequestMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeCreateMimRequestMsg");
                }
                var mimMsg = _mimMsgHelper.CreateMimRequestMsg(urlSegments, transactionId.ToString());
                if (logCreateMimRequestMsg)
                {
                    _logger.Info("Posle CreateMimRequestMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterCreateMimRequestMsg");
                }

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

                var logInsertRequestLogInDb = AppSettings.Get<bool>("LogInsertRequestLogInDb");
                if (logInsertRequestLogInDb)
                {
                    _logger.Info("Pred LogInitialMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeLogInitialMimMessage");
                }
                _mimMsgHelper.LogInitialMimMessage(mimMsg, _logger);
                if (logInsertRequestLogInDb)
                {
                    _logger.Info("Posle LogInitialMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterLogInitialMimMessage");
                }

                #endregion

                if (urlSegments.Async)
                {
                    //TODO Async req
                }
                var logLoadOwnCertificate = AppSettings.Get<bool>("LogLoadOwnCertificate");
                if (logLoadOwnCertificate)
                {
                    _logger.Info("Pred LoadOwnCertificate " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeLoadOwnCertificate");
                }
                var ownCert = _mimMsgHelper.LoadOwnCertificate(_logger);
                if (logLoadOwnCertificate)
                {
                    _logger.Info("Posle LoadOwnCertificate " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterLoadOwnCertificate");
                }

                var logGetPrivateKey = AppSettings.Get<bool>("LogGetPrivateKey");
                if (logGetPrivateKey)
                {
                    _logger.Info("Pred GetPrivateKey " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeGetPrivateKey");
                }
                var privateKey = _mimMsgHelper.GetPrivateKey(ownCert.PrivateKey);
                if (logGetPrivateKey)
                {
                    _logger.Info("Posle GetPrivateKey " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterGetPrivateKey");
                }

                string original = soapBody;

                var logGetPublicKeyForProvider = AppSettings.Get<bool>("LogGetPublicKeyForProvider");
                if (logGetPublicKeyForProvider)
                {
                    _logger.Info("Pred GetPublicKeyForProvider " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeGetPublicKeyForProvider");
                }
                var publicKey = _mimMsgHelper.GetPublicKeyForProvider(urlSegments.RoutingToken);
                if (logGetPublicKeyForProvider)
                {
                    _logger.Info(
                        "Posle GetPublicKeyForProvider " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterGetPublicKeyForProvider");
                }

                var logEncryption = AppSettings.Get<bool>("LogEncryption");
                if (logEncryption)
                {
                    _logger.Info(
                        "Pred da go enkriptira body koe kje se isprati vo requestot " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "BeforeEncryptRequest");
                }
                var encryptedBlock = _mimMsgHelper.EncryptSoapBody(original, publicKey.PublicKeyRsa);
                if (logEncryption)
                {
                    _logger.Info(
                        "Otkako kje go enkriptira body koe kje se isprati vo requestot " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "AfterEncryptRequest");
                }
                //var soapMethodName = _requestHelper.GetSoapMethodName(soapBody);

                #region Create MIM Message

                //set values to missing properties
                mimMsg.Body.MimBody.Message = Convert.ToBase64String(encryptedBlock.EncryptedData);
                mimMsg.Header.CryptoHeader.Key = Convert.ToBase64String(encryptedBlock.EncryptedSessionKey);
                mimMsg.Header.CryptoHeader.InitializationVector = Convert.ToBase64String(encryptedBlock.Iv);
                mimMsg.Header.MimHeader.ServiceMethod = executionMethodName;
                mimMsg.Header.MimHeader.PublicKey = ownCert.CertString;
                _logger.Info("mimMsg.Header.MimHeader.PublicKey is: " + ownCert.CertString);

                var logCreateMimXmlMsg = AppSettings.Get<bool>("LogCreateMimXmlMsg");
                if (logCreateMimXmlMsg)
                {
                    _logger.Info("Pred CreateMimXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeCreateMimXmlMsg");
                }
                var doc = _mimMsgHelper.CreateMimXmlMsg(mimMsg);
                if (logCreateMimXmlMsg)
                {
                    _logger.Info("Posle CreateMimXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterACreateMimXmlMsg");
                }
                //_logger.Info("MIM message before KIBS" + doc);

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
                                var logKibsTimestamp = AppSettings.Get<bool>("LogKIBS");
                                if (logKibsTimestamp)
                                {
                                    _logger.Info("Se koristi testen KIBS.");
                                }
                                var requestBeforeKibs = DateTime.Now;
                                if (logKibsTimestamp)
                                {
                                    _logger.Info(
                                        "Pred request ." + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                        "KIBSPredRequest");
                                }
                                resultkibs = KIBS.KIBS.GenerateTimeStamp(doc.ToString());
                                if (logKibsTimestamp)
                                {
                                    _logger.Info(
                                        "Pred request ." + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                        "KIBSPosleRequest");
                                    var totalMinRequestKibs = (int)(DateTime.Now - requestBeforeKibs).TotalMinutes;
                                    var totalSecRequestKibs = (int)(DateTime.Now - requestBeforeKibs).TotalSeconds;
                                    var totalMilisecRequestKibs =
                                        (int)(DateTime.Now - requestBeforeKibs).TotalMilliseconds;
                                    _logger.Info(
                                        "Kibs ni vratil odgovor za " + totalMinRequestKibs + " min " +
                                        totalSecRequestKibs + " sec " + totalMilisecRequestKibs + " miliseconds",
                                       "RequestKIBS");
                                }

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
                            //_logger.Info("Pred update message log: " + mimMsg.Header.MimHeader.Dir + "/" + mimMsg.Header.MimHeader.ServiceMethod + "/" + mimMsg.Header.MimHeader.PublicKey + "/" + mimMsg.Header.MimHeader.TransactionId);

                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp
                            // _messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.TimeStamp,
                            //resultkibs.Hash, mimMsg.Header.MimHeader.ServiceMethod, mimMsg.Header.MimHeader.PublicKey,
                            //mimMsg.Header.MimHeader.TransactionId);
                            var logUpdateMessageLog = AppSettings.Get<bool>("LogUpdateMessageLog");
                            if (logUpdateMessageLog)
                            {
                                _logger.Info(
                                    "Pred UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                    "BeforeUpdateMessageLog");
                            }
                            _messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.Hash,
                                mimMsg.Header.MimHeader.ServiceMethod, mimMsg.Header.MimHeader.PublicKey,
                                mimMsg.Header.MimHeader.TransactionId);
                            if (logUpdateMessageLog)
                            {
                                _logger.Info(
                                    "Posle UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                    "AfterUpdateMessageLog");
                            }

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

                var logCreateMimSignedXmlMsg = AppSettings.Get<bool>("LogCreateMimSignedXmlMsg");
                if (logCreateMimSignedXmlMsg)
                {
                    _logger.Info("Pred CreateMimSignedXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeCreateMimSignedXmlMsg");
                }
                var mimMsgXml = _mimMsgHelper.CreateMimSignedXmlMsg(mimMsg, ownCert, _logger);
                if (logCreateMimSignedXmlMsg)
                {
                    _logger.Info("Posle CreateMimSignedXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterCreateMimSignedXmlMsg");
                }

                //_logger.Info("Created MIM signed xml message: " + mimMsgXml);

                #endregion



                var urlToBizTalk = AppSettings.Get<string>("URLWebRequest");
                ResponseInteropCommunication responseBizTalk = null;
                try
                {
                    //_logger.Info("pred responseBizTalk urlToBizTalk" + urlToBizTalk);
                    var timeBeforeCallBizTalk = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt");
                    responseBizTalk = _soapRequestHelper.Execute(mimMsgXml, contentType, urlToBizTalk, _logger);

                    _logger.Info(
                        "Calling BizTalk in " + timeBeforeCallBizTalk + " , receving response in " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "BizTalkResponseTime");
                    //Real MIM message
                    var logUnwrapSoapFaultMessage = AppSettings.Get<bool>("LogUnwrapSoapFaultMessage");
                    if (logUnwrapSoapFaultMessage)
                    {
                        _logger.Info(
                            "Pred UnwrapSoapFaultMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                            "BeforeUnwrapSoapFaultMessage");
                    }
                    var soapFaultUnwrapped = _soapRequestHelper.UnwrapSoapFaultMessage(responseBizTalk.Response);
                    if (logUnwrapSoapFaultMessage)
                    {
                        _logger.Info(
                            "Posle UnwrapSoapFaultMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                            "AfterUnwrapSoapFaultMessage");
                    }
                    //_logger.Info("Pominal kaj soapFaultUnwrapped!");
                    _logger.Info(responseBizTalk.Response, "BTResponseReceived");


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
                    _logger.Error("Greska vo povikot do BizTalk", e);
                    throw new Exception(e.Message);
                }

                var logUnwrapMimMessage = AppSettings.Get<bool>("LogUnwrapMimMessage");
                if (logUnwrapMimMessage)
                {
                    _logger.Info("Pred UnwrapMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeUnwrapMimMessage");
                }
                var mimMsgResponse = _soapRequestHelper.UnwrapMimMessage(responseBizTalk.Response);
                if (logUnwrapMimMessage)
                {
                    _logger.Info("Posle UnwrapMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterUnwrapMimMessage");
                }

                #region Log MIM Message

                var logMimMessage = AppSettings.Get<bool>("LogMimMessage");
                if (logMimMessage)
                {
                    _logger.Info("Pred LogMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeLogMimMessage");
                }
                _mimMsgHelper.LogMimMessage(mimMsgResponse, _logger);
                if (logMimMessage)
                {
                    _logger.Info("posle LogMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterLogMimMessage");
                }

                #endregion

                #region ValidateMsg

                try
                {
                    var logValidateSignature = AppSettings.Get<bool>("LogValidateSignature");
                    if (logValidateSignature)
                    {
                        _logger.Info("Pred ValidateSignature " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                            "BeforeValidateSignature");
                        _logger.Info("Own cert string " + ownCert.CertString, "OwnCertString");
                    }
                    if (_soapRequestHelper.ValidateSignature(responseBizTalk.Response, ownCert.CertString, _logger))
                    {
                        if (logValidateSignature)
                        {
                            _logger.Info(
                                "Posle ValidateSignature " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                "AfterValidateSignature");
                        }
                        var logValidateXml = AppSettings.Get<bool>("LogValidateXml");
                        if (logValidateXml)
                        {
                            _logger.Info("Pred ValidateXml " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                "BeforeValidateXml");
                        }
                        _validXmlMsgHelper.ValidateXml(responseBizTalk.Response);
                        if (logValidateXml)
                        {
                            _logger.Info("Posle ValidateXml " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                "AfterValidateXml");
                        }
                    }

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
                    //_logger.Info("symmetricKey" + symmetricKey);
                    //_logger.Info("iVector" + iVector);
                    if (logEncryption)
                    {
                        _logger.Info(
                            "Pred da go dekriptira body koe kje se vrati kako response " +
                            DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "BeforeDecryptResponse");
                    }
                    decryptBody =
                        _mimMsgHelper.DecryptSoapBody(
                            Convert.FromBase64String(mimMsgResponse.Body.MimBody.Message),
                            Convert.FromBase64String(symmetricKey), Convert.FromBase64String(iVector), privateKey);
                    if (logEncryption)
                    {
                        _logger.Info(
                            "Otkako kje go dekriptira body koe kje se vrati kako response " +
                            DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "AfterDecryptResponse");
                    }
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
                                var logKibsTimestamp = AppSettings.Get<bool>("LogKIBS");
                                if (logKibsTimestamp)
                                {
                                    _logger.Info("Se koristi testen KIBS.");
                                }
                                var responseBeforeKibs = DateTime.Now;
                                if (logKibsTimestamp)
                                {
                                    _logger.Info(
                                        "Pred response ." + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                        "KIBSPredResponse");
                                }
                                resultkibsAfterBt = KIBS.KIBS.GenerateTimeStamp(responseBizTalk.Response);
                                if (logKibsTimestamp)
                                {
                                    _logger.Info(
                                        "Posle response ." + DateTime.Now.ToString("dd.MM.yyy hh:mm:ss.fff tt"),
                                        "KIBSPosleResponse");
                                    var totalMinRequestKibs = (int)(DateTime.Now - responseBeforeKibs).TotalMinutes;
                                    var totalSecRequestKibs = (int)(DateTime.Now - responseBeforeKibs).TotalSeconds;
                                    var totalMilisecRequestKibs =
                                        (int)(DateTime.Now - responseBeforeKibs).TotalMilliseconds;
                                    //TimeSpan a = DateTime.Now - responseBeforeKibs;
                                    //int b = a.Seconds;
                                    //int c = a.Milliseconds;
                                    //int d = a.Minutes;
                                    //_logger.Info("minuti ima " + d, "minuti");
                                    //_logger.Info("sekindi ima " + b, "sekundi");
                                    //_logger.Info("milisekunds ima " + c, "milisekunds");
                                    _logger.Info(
                                        "Kibs ni vratil odgovor za " + totalMinRequestKibs + " min " +
                                        totalSecRequestKibs + " sec " + totalMilisecRequestKibs + " miliseconds",
                                        "ResponseKIBS");
                                }

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

                        var logUpdateMessageLogResponse = AppSettings.Get<bool>("LogUpdateMessageLogResponse");
                        if (logUpdateMessageLogResponse)
                        {
                            _logger.Info(
                                "Pred UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                "BeforeUpdateMessageLogResponse");
                        }
                        _messageLogsRepository.UpdateMessageLog(mimMsgResponse.Header.MimHeader.Dir,
                            resultkibsAfterBt.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);
                        if (logUpdateMessageLogResponse)
                        {
                            _logger.Info(
                                "Posle UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                "AfterUpdateMessageLogResponse");
                        }
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
                //_logger.Info(decryptedResponseBodyWrappedInSoapEnvelope, "decryptedResponseBodyWrappedInSoapEnvelope");
                _logger.Info("Zavsilo se na External handler vo " + DateTime.Now + " casot.", "EndedOnExternalHandler");
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

                    _logger.Error("Generalen exception", ex);
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
