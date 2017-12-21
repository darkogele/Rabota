using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Web;
using Interop.CC.CrossCutting;
using Interop.CC.CrossCutting.Logging;
using Interop.CC.Handler.Helper.Contracts;
using Interop.CC.Handler.Helper.Model;
using Interop.CC.Models.RepositoryContracts;
using Interop.CCSimulation.Handler.Internal.NinjectConfig;
using KIBS;
using Ninject;
using Org.BouncyCastle.Cms;

namespace Interop.CCSimulation.Handler.Internal
{
    public class InteropCCSimulationHandlerInternal : IHttpHandler
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
        public InteropCCSimulationHandlerInternal()
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
                var soapBody = _requestHelper.GetSoapBody(context.Request.InputStream);
                var contentType = context.Request.ContentType;
                var soapHeader = _requestHelper.GetSoapHeader(context.Request.Headers);

                #region Log SOAP Request Message

                var logSoap = AppSettings.Get<bool>("LogSoap");
                if (logSoap)
                {
                    _logger.Info(soapBody + Environment.NewLine + "ContentType: " + contentType + Environment.NewLine + "SoapHeader: " + soapHeader, "Request to Internal handler");
                }
                //soapBody = File.ReadAllText(@"C:\NSRequest.txt"); ;
                #endregion

                if (string.IsNullOrEmpty(soapBody))
                {
                    context.Response.StatusCode = 400;
                    context.Response.End();
                }

                var mimMsg = _soapRequestHelper.UnwrapMimMessage(soapBody);
                validTId = true;
                transactionId = new Guid(mimMsg.Header.MimHeader.TransactionId);

                #region Log MIM Message
                _mimMsgHelper.LogMimMessage(mimMsg, _logger);
                #endregion

                var ownCert = _mimMsgHelper.LoadOwnCertificate(_logger);
                var privateKey = _mimMsgHelper.GetPrivateKey(ownCert.PrivateKey);
                var publicKey = mimMsg.Header.MimHeader.PublicKey;
                if (_soapRequestHelper.ValidateSignature(soapBody, publicKey, _logger))
                    _validXmlMsgHelper.ValidateXml(soapBody);
                var symmetricKey = mimMsg.Header.CryptoHeader.Key;
                var iVector = mimMsg.Header.CryptoHeader.InitializationVector;

                var decryptBody = _mimMsgHelper.DecryptSoapBody(Convert.FromBase64String(mimMsg.Body.MimBody.Message.ToString()), Convert.FromBase64String(symmetricKey), Convert.FromBase64String(iVector), privateKey);

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
                                _logger.Info("Se koristi testen KIBS.");
                                resultkibs = KIBS.KIBS.GenerateTimeStamp(soapBody);
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

                            _messageLogsRepository.UpdateMessageLog(mimMsg.Header.MimHeader.Dir, resultkibs.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);

                            //Prethodno se zemashe vrednosta Timestamp od servisot na KIBS, i istata se zapisuvase vo baza
                            //Sega se zapishuva DateTime.Now vo poleto Timestamp

                            //mimMsg.Header.MimHeader.TimeStamp = resultkibs.TimeStamp; //.ToString();

                            mimMsg.Header.MimHeader.TimeStamp = DateTime.Now;
                            mimMsg.Header.MimAdditionalHeader.TimeStampToken = resultkibs.Hash;
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
                    _logger.Info("KIBS error posle DecryptSoapBody: " + ex.Message);
                    throw new Exception("KIBS врати грешка!");
                }
                #endregion

                _logger.Info("posle LogMimMessage");

                var urlToHostedApp = _requestHelper.GetServiceUrl(mimMsg.Header.MimHeader);

                _logger.Info("urlToHostedApp mimheader e: " + mimMsg.Header.MimHeader);
                _logger.Info("urlToHostedApp e: " + urlToHostedApp);

                #region ExecuteRequestToInstitution

                ResponseInteropCommunication responseEndPointInstitution;

                string decryptedRequestBodyWrappedInSoapEnvelope = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body>" + decryptBody + "</s:Body></s:Envelope>";

                if (mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall)
                {
                    responseEndPointInstitution = new ResponseInteropCommunication
                    {
                        Response = @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope""><s:Body><InteropTestCommunicationService xmlns=""http://tempuri.org/"">InteropTestCommunicationCallResponse</InteropTestCommunicationService></s:Body></s:Envelope>",
                        MimeType = "InteropTestCommunicationCallMimeType"
                    };
                }
                else
                {
                    try
                    {
                        _logger.Info("Povikot koj se prakja e: " + decryptedRequestBodyWrappedInSoapEnvelope);
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);//sertifikatot ne im e u red za toa go stavam ova za da go ignorira
                        //ovaa linija da se proveri za custom interop fault greski, od tuka pa natamu da se proveruva povikot
                        responseEndPointInstitution = _soapRequestHelper.Execute(decryptedRequestBodyWrappedInSoapEnvelope, contentType, urlToHostedApp, _logger);
                        //ovie dva loga ne gi pravi bidejki direktno odi na exception!
                        _logger.Info("Response institution is: " + responseEndPointInstitution.Response);
                        _logger.Info("Response institution bez envelope is: " + _requestHelper.GetOnlySoapBodyFromString(responseEndPointInstitution.Response));
                    }
                    //tuka probaj stavi catch (FaultException e) da vidish shto kje vrati
                    catch (FaultException ex)
                    {
                        _logger.Info("dosol vo fault exception.");
                        _logger.Error("Error koj se vrakja od fault exception e: ", ex);
                        _logger.Error("Error koj se vrakja od fault exception Message e: ", ex.Message);
                        _logger.Error("Error koj se vrakja od fault exception inner exception e: ", ex.InnerException.Message);
                        throw;
                    }
                    catch (Exception e)
                    {
                        _logger.Info("catch exception e: " + e.Message);
                        throw new Exception("Сервисот кој го повикавте врати грешка!");
                    }
                }
                #endregion

                #region Create MIM Message

                var mimMsgResponse = _mimMsgHelper.CreateMimResponseMsg(mimMsg, responseEndPointInstitution.MimeType);

                #region Log MIM Message
                _mimMsgHelper.LogMimMessage(mimMsgResponse, _logger);
                #endregion

                string responseWithoutSoapEnvelope = _requestHelper.GetOnlySoapBodyFromString(responseEndPointInstitution.Response);

                var publicKeyRsa = _mimMsgHelper.GetPublicKeyFromString(publicKey);
                var encryptedBlock = _mimMsgHelper.EncryptSoapBody(responseWithoutSoapEnvelope, publicKeyRsa);

                var encryptedSoapBody = Convert.ToBase64String(encryptedBlock.EncryptedData);
                var encryptedSessionKey = Convert.ToBase64String(encryptedBlock.EncryptedSessionKey);
                var encriptedIVector = Convert.ToBase64String(encryptedBlock.Iv);

                mimMsgResponse.Body.MimBody.Message = encryptedSoapBody;
                mimMsgResponse.Header.CryptoHeader.InitializationVector = encriptedIVector;
                mimMsgResponse.Header.CryptoHeader.Key = encryptedSessionKey;

                var doc = _mimMsgHelper.CreateMimXmlMsg(mimMsgResponse);

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
                        if (AppSettings.Get<string>("KIBSEnviroment")!= null)
                        {
                            if (AppSettings.Get<string>("KIBSEnviroment") == "Test")
                            {
                                _logger.Info("Se koristi testen KIBS.");
                                resultkibsResp = KIBS.KIBS.GenerateTimeStamp(doc.ToString());
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

                            _messageLogsRepository.UpdateMessageLog(mimMsgResponse.Header.MimHeader.Dir, resultkibsResp.Hash, string.Empty, string.Empty, mimMsg.Header.MimHeader.TransactionId);

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

                var mimMsgResponseXml = _mimMsgHelper.CreateMimSignedXmlMsg(mimMsgResponse, ownCert, _logger); //Check for need of encryption

                #endregion

                #region Log SOAP Response Message

                if (logSoap)
                {
                    //LogHelper.WriteInNLoc("", "", mimMsgResponseXml, "Response_" + DateTime.Now, "Info");
                    _logger.Info(mimMsgResponseXml, "Response");
                }

                #endregion

                HttpContext.Current.Response.ContentType = "application/soap+xml";
                context.Response.Write(mimMsgResponseXml);
            }
            catch (FaultException ex)
            {
                _logger.Info("dosol vo fault exception.");
                _logger.Error("Error koj se vrakja od fault exception e: ", ex);
                _logger.Info("validTID vo glavniot catch e: " + validTId);
                if (validTId)
                {
                    var soapFault = _mimMsgHelper.CreateSoapFault("Code value", "Code - SubCode value",
                        "Details - MaxTime value", ex.Message);
                    var soapFaultDB = _soapRequestHelper.CreateSoapFaultDB(transactionId,
                        soapFault.Body.Fault.Code.value, soapFault.Body.Fault.Code.Subcode.value,
                        soapFault.Body.Fault.Detail.maxTime, soapFault.Body.Fault.Reason.Text.value);

                    try
                    {
                        _soapFaultRepo.InsertSoapFault(soapFaultDB);
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