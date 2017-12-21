using System;
using System.Net;
using System.ServiceModel;
using System.Web;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Internal.NinjectConfig;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Models.RepositoryContracts;
using KIBS;
using Ninject;

namespace Interop.CC.Handler.Internal
{
    public class InteropCCHandlerInternal : IHttpHandler
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
        public InteropCCHandlerInternal()
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
            var validTId = false;
            var transactionId = new Guid();
            try
            {
                var logGetSoapBody = AppSettings.Get<bool>("LogGetSoapBody");
                if (logGetSoapBody)
                {
                    _logger.Info(
                        "Pred da go zeme samo soap body od porakata " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "BeforeGetSoapBody");
                }
                var soapBody = _requestHelper.GetSoapBody(context.Request.InputStream);
                if (logGetSoapBody)
                {
                    _logger.Info(
                        "Otkako kje go zeme soap body od porakata " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "AfterGetSoapBody");
                }
                var contentType = context.Request.ContentType;



                #region Log SOAP Request Message

                var logSoap = AppSettings.Get<bool>("LogSoap");
                if (logSoap)
                {
                    _logger.Info(soapBody + Environment.NewLine + "ContentType: " + contentType,
                        "Request to Internal handler");
                }
                //soapBody = File.ReadAllText(@"C:\NSRequest.txt"); ;

                #endregion

                if (string.IsNullOrEmpty(soapBody))
                {
                    context.Response.StatusCode = 400;
                    context.Response.End();
                }

                var logUnwrapMimMessage = AppSettings.Get<bool>("LogUnwrapMimMessage");
                if (logUnwrapMimMessage)
                {
                    _logger.Info("Pred UnwrapMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeUnwrapMimMessage");
                }
                var mimMsg = _soapRequestHelper.UnwrapMimMessage(soapBody);
                if (logUnwrapMimMessage)
                {
                    _logger.Info("Posle UnwrapMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterUnwrapMimMessage");
                }

                validTId = true;
                transactionId = new Guid(mimMsg.Header.MimHeader.TransactionId);

                #region Log MIM Message

                var logMimMessageRequest = AppSettings.Get<bool>("LogMimMessageRequest");
                if (logMimMessageRequest)
                {
                    _logger.Info("Pred LogMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeLogMimMessage");
                }
                _mimMsgHelper.LogMimMessage(mimMsg, _logger);
                if (logMimMessageRequest)
                {
                    _logger.Info("Posle LogMimMessage " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterLogMimMessage");
                }

                #endregion

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
                var publicKey = mimMsg.Header.MimHeader.PublicKey;

                var logValidateSignature = AppSettings.Get<bool>("LogValidateSignature");
                if (logValidateSignature)
                {
                    _logger.Info("Pred ValidateSignature " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeValidateSignature");
                }
                if (_soapRequestHelper.ValidateSignature(soapBody, publicKey, _logger))
                {
                    if (logValidateSignature)
                    {
                        _logger.Info("Posle ValidateSignature " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                            "AfterValidateSignature");
                    }
                    var logValidateXml = AppSettings.Get<bool>("LogValidateXml");
                    if (logValidateXml)
                    {
                        _logger.Info("Pred ValidateXml " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                            "BeforeValidateXml");
                    }
                    _validXmlMsgHelper.ValidateXml(soapBody);
                    if (logValidateXml)
                    {
                        _logger.Info("Posle ValidateXml " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                            "AfterValidateXml");
                    }
                }

                var symmetricKey = mimMsg.Header.CryptoHeader.Key;
                var iVector = mimMsg.Header.CryptoHeader.InitializationVector;

                var logEncryption = AppSettings.Get<bool>("LogEncryption");
                if (logEncryption)
                {
                    _logger.Info(
                        "Pred da go dekriptira body koe kje se prati vo requestot " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "BeforeDecryptRequest");
                }
                var decryptBody =
                    _mimMsgHelper.DecryptSoapBody(Convert.FromBase64String(mimMsg.Body.MimBody.Message.ToString()),
                        Convert.FromBase64String(symmetricKey), Convert.FromBase64String(iVector), privateKey);
                if (logEncryption)
                {
                    _logger.Info(
                        "Otkako kje go dekriptira body koe kje se prati vo requestot " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "AfterDecryptRequest");
                }

                #region Log DecryptBody

                if (AppSettings.Get<bool>("LogDecryptBody"))
                    _logger.Info("Decripting MIM message: " + decryptBody);

                #endregion

                #region KIBS after DecryptSoapBody

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
                                resultkibs = KIBS.KIBS.GenerateTimeStamp(soapBody);
                                if (logKibsTimestamp)
                                {
                                    _logger.Info(
                                        "Pred request ." + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                        "KIBSPosleRequest");
                                    var totalMinRequestKibs = (int) (DateTime.Now - requestBeforeKibs).TotalMinutes;
                                    var totalSecRequestKibs = (int) (DateTime.Now - requestBeforeKibs).TotalSeconds;
                                    var totalMilisecRequestKibs =
                                        (int) (DateTime.Now - requestBeforeKibs).TotalMilliseconds;
                                    _logger.Info(
                                        "Kibs ni vratil odgovor za " + totalMinRequestKibs + " min " +
                                        totalSecRequestKibs + " sec " + totalMilisecRequestKibs + " miliseconds",
                                        "RequestKIBS");
                                }

                            }
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Production")
                            {
                                _logger.Info("Se koristi produkciski KIBS.");
                                resultkibs = KIBS.KIBS.GenerateTimeStampProduction(soapBody);
                            }
                        }

                        //resultkibs = new KIBSResponse
                        //{
                        //    Hash = "InteropTestCommunicationCallHash",
                        //    TimeStamp = DateTime.Now
                        //};
                    }

                    try
                    {
                        if (resultkibs != null)
                        {
                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp

                            //_messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.TimeStamp,
                            //    resultkibs.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);
                            var logUpdateMessageLog = AppSettings.Get<bool>("LogUpdateMessageLog");
                            if (logUpdateMessageLog)
                            {
                                _logger.Info(
                                    "Pred UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                    "BeforeUpdateMessageLogRequest");
                            }
                            _messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.Hash,
                                string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);
                            if (logUpdateMessageLog)
                            {
                                _logger.Info(
                                    "Posle UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                    "AfterUpdateMessageLogRequest");
                            }
                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp

                            //mimMsg.Header.MimHeader.TimeStamp = resultkibs.TimeStamp; //.ToString();

                            mimMsg.Header.MimHeader.TimeStamp = DateTime.Now;
                            mimMsg.Header.MimAdditionalHeader.TimeStampToken = resultkibs.Hash;
                        }
                    }
                    catch (Exception exception)
                    {
                        _logger.Error("Error while updating mim message: ", exception.Message);
                        throw new Exception("Грешка при UpdateMessageLog!");
                    }

                }
                catch (Exception ex)
                {
                    _logger.Info("KIBS error posle DecryptSoapBody: " + ex.Message);
                    throw new Exception("KIBS врати грешка!");
                }

                #endregion

                //_logger.Info("posle LogMimMessage");

                var logGetServiceUrl = AppSettings.Get<bool>("LogGetServiceUrl");
                if (logGetServiceUrl)
                {
                    _logger.Info("Pred GetServiceUrl " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeGetServiceUrl");
                }
                var urlToHostedApp = _requestHelper.GetServiceUrl(mimMsg.Header.MimHeader);
                if (logGetServiceUrl)
                {
                    _logger.Info("Posle GetServiceUrl " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterGetServiceUrl");
                }

                //_logger.Info("urlToHostedApp mimheader e: " + mimMsg.Header.MimHeader);
                _logger.Info("urlToHostedApp e: " + urlToHostedApp);

                #region ExecuteRequestToInstitution

                ResponseInteropCommunication responseEndPointInstitution;

                string decryptedRequestBodyWrappedInSoapEnvelope =
                    "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body>" + decryptBody +
                    "</s:Body></s:Envelope>";

                if (mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall)
                {
                    responseEndPointInstitution = new ResponseInteropCommunication
                    {
                        Response =
                            @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope""><s:Body><InteropTestCommunicationService xmlns=""http://tempuri.org/"">InteropTestCommunicationCallResponse</InteropTestCommunicationService></s:Body></s:Envelope>",
                        MimeType = "InteropTestCommunicationCallMimeType"
                    };
                }
                else
                {
                    try
                    {
                        _logger.Info("Povikot koj se prakja e: " + decryptedRequestBodyWrappedInSoapEnvelope);
                        System.Net.ServicePointManager.ServerCertificateValidationCallback =
                            ((sender, certificate, chain, sslPolicyErrors) => true);
                            //sertifikatot ne im e u red za toa go stavam ova za da go ignorira
                        var timeBeforeCallInstitution = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt");
                        //ovaa linija da se proveri za custom interop fault greski, od tuka pa natamu da se proveruva povikot
                        responseEndPointInstitution =
                            _soapRequestHelper.Execute(decryptedRequestBodyWrappedInSoapEnvelope, contentType,
                                urlToHostedApp, _logger);
                        //_logger.Info(
                        //    "decryptedRequestBodyWrappedInSoapEnvelope" + decryptedRequestBodyWrappedInSoapEnvelope +
                        //    "contentType" + contentType + "urltToApp" + urlToHostedApp, "Test");
                        _logger.Info(
                            "Calling institution  in " + timeBeforeCallInstitution + " , receiving response in " +
                            DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"), "InsitutionTotalResponse");

                        //ovie dva loga ne gi pravi bidejki direktno odi na exception!
                        //_logger.Info("Response institution is: " + responseEndPointInstitution.Response, "InstitutionReturnedLength");
                        // _logger.Info("Response institution bez envelope is: " + _requestHelper.GetOnlySoapBodyFromString(responseEndPointInstitution.Response));
                    }
                        //tuka probaj stavi catch (FaultException e) da vidish shto kje vrati
                    catch (FaultException ex)
                    {
                        //_logger.Info("dosol vo fault exception.");
                        //_logger.Error("Error koj se vrakja od fault exception e: ", ex);
                        _logger.Error("Error koj se vrakja od fault exception Message e: ", ex.Message);
                        //_logger.Error("Error koj se vrakja od fault exception inner exception e: ", ex.InnerException.Message);
                        throw new FaultException(ex.Message);
                    }


                    //MARIJA 
                    //DODADENO za da se fati Web exception od povikot do adapterot vo slucaj koga toj e nedostapen
                    //INICIJALNO ova BESE ZAKOMENTIRANO
                    //catch (Exception e)
                    //{
                    //    _logger.Info("catch exception e: " + e.Message, "WebErrorVoPovikotDoAdapterot");
                    //    //throw new Exception("Сервисот кој го повикавте врати грешка!", e);
                    //    throw new FaultException(e.Message);
                    //}
                }

                #endregion

                #region Create MIM Message

                var logCreateMimResponseMsg = AppSettings.Get<bool>("LogCreateMimResponseMsg");
                if (logCreateMimResponseMsg)
                {
                    _logger.Info("Pred CreateMimResponseMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeCreateMimResponseMsg");
                }
                var mimMsgResponse = _mimMsgHelper.CreateMimResponseMsg(mimMsg, responseEndPointInstitution.MimeType);
                if (logCreateMimResponseMsg)
                {
                    _logger.Info("Posle CreateMimResponseMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterCreateMimResponseMsg");
                }

                #region Log MIM Message

                //_logger.Info("Pred LogMimMessage.");
                var logMimMessageResponse = AppSettings.Get<bool>("LogMimMessageResponse");
                if (logMimMessageResponse)
                {
                    _logger.Info("Pred LogMimMessageResponse " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeLogMimMessageResponse");
                }
                _mimMsgHelper.LogMimMessage(mimMsgResponse, _logger);
                if (logMimMessageResponse)
                {
                    _logger.Info("Posle LogMimMessageResponse " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterLogMimMessageResponse");
                }
                //_logger.Info("Posle LogMimMessage.");

                #endregion

                //_logger.Info("Pred responseWithoutSoapEnvelope.");
                var logGetOnlySoapBodyFromString = AppSettings.Get<bool>("LogGetOnlySoapBodyFromString");
                if (logGetOnlySoapBodyFromString)
                {
                    _logger.Info(
                        "Pred GetOnlySoapBodyFromString " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeGetOnlySoapBodyFromString");
                }
                string responseWithoutSoapEnvelope =
                    _requestHelper.GetOnlySoapBodyFromString(responseEndPointInstitution.Response);
                if (logGetOnlySoapBodyFromString)
                {
                    _logger.Info(
                        "Posle GetOnlySoapBodyFromString " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterGetOnlySoapBodyFromString");
                }
                //_logger.Info("Posle responseWithoutSoapEnvelope.");

                var logGetPublicKeyFromString = AppSettings.Get<bool>("LogGetPublicKeyFromString");
                if (logGetPublicKeyFromString)
                {
                    _logger.Info("Pred GetPublicKeyFromString " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeGetPublicKeyFromString");
                }
                var publicKeyRsa = _mimMsgHelper.GetPublicKeyFromString(publicKey);
                if (logGetPublicKeyFromString)
                {
                    _logger.Info("Posle GetPublicKeyFromString " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterGetPublicKeyFromString");
                }
                if (logEncryption)
                {
                    _logger.Info(
                        "Pred da go enkriptira body koe kje se prati vo responsot " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeEncryptResponse");
                }
                var encryptedBlock = _mimMsgHelper.EncryptSoapBody(responseWithoutSoapEnvelope, publicKeyRsa);
                if (logEncryption)
                {
                    _logger.Info(
                        "Otkako kje go enkriptira body koe kje se prati vo responsot " +
                        DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterEncryptResponse");
                }

                var encryptedSoapBody = Convert.ToBase64String(encryptedBlock.EncryptedData);
                var encryptedSessionKey = Convert.ToBase64String(encryptedBlock.EncryptedSessionKey);
                var encriptedIVector = Convert.ToBase64String(encryptedBlock.Iv);

                mimMsgResponse.Body.MimBody.Message = encryptedSoapBody;
                mimMsgResponse.Header.CryptoHeader.InitializationVector = encriptedIVector;
                mimMsgResponse.Header.CryptoHeader.Key = encryptedSessionKey;

                var logCreateMimXmlMsg = AppSettings.Get<bool>("LogCreateMimXmlMsg");
                if (logCreateMimXmlMsg)
                {
                    _logger.Info("Pred CreateMimXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeCreateMimXmlMsg");
                }
                var doc = _mimMsgHelper.CreateMimXmlMsg(mimMsgResponse);
                if (logCreateMimXmlMsg)
                {
                    _logger.Info("Posle CreateMimXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterCreateMimXmlMsg");
                }

                #region KIBS after CreateMimXmlMsg

                try
                {
                    KIBSResponse resultkibsResp = null;
                    if (mimMsgResponse.Header.MimAdditionalHeader.IsInteropTestCommunicationCall)
                    {
                        resultkibsResp = new KIBSResponse
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
                                        "Pred request ." + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                        "KIBSPredResponse");
                                }
                                resultkibsResp = KIBS.KIBS.GenerateTimeStamp(doc.ToString());
                                if (logKibsTimestamp)
                                {
                                    _logger.Info(
                                        "Pred request ." + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                        "KIBSPosleResponse");
                                    var totalMinRequestKibs = (int) (DateTime.Now - responseBeforeKibs).TotalMinutes;
                                    var totalSecRequestKibs = (int) (DateTime.Now - responseBeforeKibs).TotalSeconds;
                                    var totalMilisecRequestKibs =
                                        (int) (DateTime.Now - responseBeforeKibs).TotalMilliseconds;
                                    _logger.Info(
                                        "Kibs ni vratil odgovor za " + totalMinRequestKibs + " min " +
                                        totalSecRequestKibs + " sec " + totalMilisecRequestKibs + " miliseconds",
                                        "ResponseKIBS");
                                }
                            }
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Production")
                            {
                                _logger.Info("Se koristi produkciski KIBS.");
                                resultkibsResp = KIBS.KIBS.GenerateTimeStampProduction(doc.ToString());
                            }
                        }

                        //resultkibsResp = new KIBSResponse
                        //{
                        //    Hash = "InteropTestCommunicationCallHash",
                        //    TimeStamp = DateTime.Now
                        //};
                    }

                    try
                    {
                        if (resultkibsResp != null)
                        {
                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp

                            //_messageLogsRepository.UpdateMessageLog(mimMsgResponse.Header.MimHeader.Dir, resultkibsResp.TimeStamp, resultkibsResp.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);
                            var logUpdateMessageLogResponse = AppSettings.Get<bool>("LogUpdateMessageLogResponse");
                            if (logUpdateMessageLogResponse)
                            {
                                _logger.Info(
                                    "Pred UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                    "BeforeUpdateMessageLogResponse");
                            }
                            _messageLogsRepository.UpdateMessageLog(mimMsgResponse.Header.MimHeader.Dir,
                                resultkibsResp.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);
                            if (logUpdateMessageLogResponse)
                            {
                                _logger.Info(
                                    "Posle UpdateMessageLog " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                                    "AfterUpdateMessageLogResponse");
                            }

                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp
                            //mimMsgResponse.Header.MimHeader.TimeStamp = resultkibsResp.TimeStamp;//.ToString();

                            mimMsgResponse.Header.MimHeader.TimeStamp = DateTime.Now;
                            mimMsgResponse.Header.MimAdditionalHeader.TimeStampToken = resultkibsResp.Hash;
                        }
                    }
                    catch (Exception exception)
                    {
                        _logger.Info("KIBS error: " + exception.Message);
                        throw new Exception("KIBS врати грешка: " + exception.Message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Info("KIBS error after CreateMimXmlMsg: " + ex.Message);
                    throw new Exception("KIBS врати грешка: " + ex.Message);
                }

                #endregion

                var logCreateMimSignedXmlMsg = AppSettings.Get<bool>("LogCreateMimSignedXmlMsg");
                if (logCreateMimSignedXmlMsg)
                {
                    _logger.Info("Pred CreateMimSignedXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "BeforeCreateMimSignedXmlMsg");
                }
                var mimMsgResponseXml = _mimMsgHelper.CreateMimSignedXmlMsg(mimMsgResponse, ownCert, _logger);
                    //Check for need of encryption
                if (logCreateMimSignedXmlMsg)
                {
                    _logger.Info("Posle CreateMimSignedXmlMsg " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss.fff tt"),
                        "AfterCreateMimSignedXmlMsg");
                }

                #endregion

                #region Log SOAP Response Message

                if (logSoap)
                {
                    //LogHelper.WriteInNLoc("", "", mimMsgResponseXml, "Response_" + DateTime.Now, "Info");
                    _logger.Info(mimMsgResponseXml, "ResponseSignedXml");
                }

                #endregion

                HttpContext.Current.Response.ContentType = "application/soap+xml";
                context.Response.Write(mimMsgResponseXml);
                _logger.Info("Zavsilo se na Internal handler vo " + DateTime.Now + " casot.", "EndedOnInternalHandler");
            }
            catch (FaultException ex)
            {
                //_logger.Info("dosol vo fault exception.");
                _logger.Error("Error koj se vrakja od fault exception e: ", ex, "CatchFaultException");
                //_logger.Info("validTID vo glavniot catch e: " + validTId);
                if (validTId)
                {
                    var soapFault = _mimMsgHelper.CreateSoapFault("Code value", "Code - SubCode value", "Details - MaxTime value", ex.Message);
                    var soapFaultDb = _soapRequestHelper.CreateSoapFaultDB(transactionId,
                        soapFault.Body.Fault.Code.value, soapFault.Body.Fault.Code.Subcode.value,
                        soapFault.Body.Fault.Detail.maxTime, soapFault.Body.Fault.Reason.Text.value);

                    try
                    {
                        _soapFaultRepo.InsertSoapFault(soapFaultDb);
                    }
                    catch (Exception exception)
                    {
                        _logger.Error(exception, "Nastanata greska pri kreiranje na soap fault vo baza.");
                    }

                    var soapFaultXml = _mimMsgHelper.CreateFaultMessage(soapFault);
                    _logger.Info(soapFaultXml.InnerXml, "SoapFaultXml");
                    _logger.Error(soapFault.Body.Fault.Reason.Text.value, ex, "ErrorOnResponseInternalHandler");

                    HttpContext.Current.Response.ContentType = "application/soap+xml";
                    HttpContext.Current.Response.Write(soapFaultXml.InnerXml);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("odma u catch", ex);
                _logger.Info("validTID vo glavniot catch e: " + validTId);
                if (validTId)
                {
                    var soapFault = _mimMsgHelper.CreateSoapFault("Code value", "Code - SubCode value",
                        "Details - MaxTime value", ex.Message);
                    var soapFaultDb = _soapRequestHelper.CreateSoapFaultDB(transactionId,
                        soapFault.Body.Fault.Code.value, soapFault.Body.Fault.Code.Subcode.value,
                        soapFault.Body.Fault.Detail.maxTime, soapFault.Body.Fault.Reason.Text.value);

                    try
                    {
                        _soapFaultRepo.InsertSoapFault(soapFaultDb);
                    }
                    catch (Exception exception)
                    {
                        _logger.Error(exception, "Nastanata greska pri kreiranje na soap fault vo baza.");
                    }

                    var soapFaultXml = _mimMsgHelper.CreateFaultMessage(soapFault);
                    _logger.Error(soapFault.Body.Fault.Reason.Text.value, ex, "Error on response");

                    HttpContext.Current.Response.ContentType = "application/soap+xml";
                    HttpContext.Current.Response.Write(soapFaultXml.InnerXml);
                }
            }
        }

    }
}