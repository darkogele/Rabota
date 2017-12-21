using System;
using System.Configuration;
using System.Web;
using CrossCutting.AppSettings;
using CrossCutting.Logging;
using Helpers.Contracts;
using Helpers.Models;
using Ninject;

namespace ExternalHandler
{
    public class ExternalHttpHandler : IHttpHandler
    {
        private readonly IRequestTestHelper _requestHelper;
        private readonly IMimMsgHelper _mimMsgHelper;
        private readonly ISoapRequestHelper _soapRequestHelper;
        private readonly IValidXmlMsgHelper _validXmlMsgHelper;
        private readonly ILogger _logger;
        public ExternalHttpHandler()
        {
            _requestHelper = Global.NewKernel.Get<IRequestTestHelper>();
            _mimMsgHelper = Global.NewKernel.Get<IMimMsgHelper>();
            _soapRequestHelper = Global.NewKernel.Get<ISoapRequestHelper>();
            _validXmlMsgHelper = Global.NewKernel.Get<IValidXmlMsgHelper>();
            _logger = Global.NewKernel.Get<ILogger>();
        }

        #region IHttpHandler Members
        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var transactionId = Guid.NewGuid();

                _logger.Info("Raw url is: " + context.Request.RawUrl, "ExternalHandler_RawUrl");

                var contentType = context.Request.ContentType;
                
                var urlSegments = _requestHelper.GetUrlSegments(context.Request.RawUrl);

                _logger.Info("URL segments are: " + "Consumer: " + urlSegments.Consumer + ". Provider: " + urlSegments.RoutingToken + ". Service: " + urlSegments.Service + ". TestCall: " + urlSegments.IsInteropTestCommunicationCall + ". UrlIsCorrect: " + urlSegments.IsUrlCorrrect);

                bool routingTokenContainOurMim = urlSegments.RoutingToken.Contains(AppSettings.Get<string>("OurMim"));
                bool routingTokenContainOtherMim = urlSegments.RoutingToken.Contains(AppSettings.Get<string>("OtherMim"));

                if (!routingTokenContainOurMim && !routingTokenContainOtherMim)
                {
                    //Dodavame MIM1$$ na RountingToken ako istiot nema
                    urlSegments.RoutingToken = "MIM1$$" + urlSegments.RoutingToken;
                }
                
                var soapBody = _requestHelper.GetOnlySoapBody(context.Request.InputStream);
                
                var actionName = _requestHelper.GetActionFromContentType(contentType);

                _logger.Info("ActionName: " + actionName);

                #region CreatelMimMsgStructure

                var mimMsg = _mimMsgHelper.CreateMimRequestMsg(urlSegments, transactionId.ToString());

                #endregion

                if (string.IsNullOrEmpty(soapBody) || !urlSegments.IsUrlCorrrect)
                {
                    context.Response.StatusCode = 400;
                    context.Response.End();
                    _logger.Error("Ërror occured in external handler", "SoapBody is empty or UrlSegments is not url correct", "EmptySoapBodyOrUrlIncorrect");
                }

                #region LogMimMsgRequestIntoDB

                _mimMsgHelper.LogInitialMimMessage(mimMsg);

                #endregion

                #region CertificateAndKeys

                var ownCert = _mimMsgHelper.LoadOwnCertificate();

                var privateKey = _mimMsgHelper.GetPrivateKey(ownCert.PrivateKey);

                var publicKey = _mimMsgHelper.GetPublicKeyForProvider(urlSegments.RoutingToken);

                #endregion

                #region EncryptSoapBody

                var encryptedBlock = _mimMsgHelper.EncryptSoapBody(soapBody, publicKey.PublicKeyRsa);

                #endregion

                #region FillMissingValuesInMimMessage

                mimMsg.Body.MimBody.Message = Convert.ToBase64String(encryptedBlock.EncryptedData);
                mimMsg.Header.CryptoHeader.Key = Convert.ToBase64String(encryptedBlock.EncryptedSessionKey);
                mimMsg.Header.CryptoHeader.InitializationVector = Convert.ToBase64String(encryptedBlock.Iv);
                mimMsg.Header.MimHeader.ServiceMethod = actionName;
                mimMsg.Header.MimHeader.PublicKey = ownCert.CertString;

                #endregion

                #region CreateMimMsgXml

                var mimXmlMsg = _mimMsgHelper.CreateMimXmlMsg(mimMsg);

                #endregion
                
                #region CallKIBSForRequest

                KIBSResponse kibsResponse = null;

                if (mimMsg.Header.MimAdditionalHeader.IsInteropTestCommunicationCall)
                {
                    kibsResponse = new KIBSResponse
                    {
                        Hash = ConfigurationManager.AppSettings["TestCommunicationCallHashKey"]
                    };
                }
                else
                {
                    if (ConfigurationManager.AppSettings["KIBSEnviroment"] != null)
                    {
                        if (ConfigurationManager.AppSettings["KIBSEnviroment"] == "Test")
                        {
                            //kibsResponse = KIBS.KIBS.GenerateTimeStamp(mimXmlMsg.ToString());
                        }
                    }
                }

                #endregion

                #region UpdateMimMsgRequestInDB
                #endregion

                #region SignMimMsg

                var mimMsgXml = _mimMsgHelper.CreateMimSignedXmlMsg(mimXmlMsg, ownCert);

                #endregion

                #region Execute

                //var responseFromInternalHandler = _requestHelper.MakeWebRequest("http://localhost/InternalHandler/internal");
                //var soapFaultUnwrapped = _requestHelper.UnwrapSoapFaultMessage(responseFromInternalHandler);

                ResponseInteropCommunication responseFromInternalHandler = _requestHelper.Execute(mimMsgXml, contentType, "http://localhost/InternalHandler/internal");
                var soapFaultUnwrapped = _requestHelper.UnwrapSoapFaultMessage(responseFromInternalHandler.Response);

                //Soap Fault returned!!!
                if (soapFaultUnwrapped.Body.Fault != null)
                {
                    //Tuka moze ili da se napravi throw samo na obicen Exception ili throw FaultException. Ako se pravi throw Exception dolu samo catch Exception. Ako se pravi throw FaultException<InteropFault> togas dolu catch FaultException<InteropFault>

                    //TODO: Tuka da se vidi kako kakvi da bidat Soap Faults shto se od samiot BizTalk

                    throw new Exception("Error: " + soapFaultUnwrapped.Body.Fault.Reason.Text.value,
                        new Exception("Error details: " + soapFaultUnwrapped.Body.Fault.Detail.maxTime));

                    //throw new FaultException<InteropFault>(new InteropFault
                    //{
                    //    ErrorDetails = soapFaultUnwrapped.Body.Fault.Detail.maxTime,
                    //    ErrorInfo = "Error occured in External Handler: ",
                    //    MessageError = soapFaultUnwrapped.Body.Fault.Reason.Text.value
                    //});
                }

                #endregion

                #region UnwrapResponseFromBizTalk

                var mimMsgResponse = _soapRequestHelper.UnwrapMimMessage(responseFromInternalHandler.Response);

                #endregion

                #region LogMimMsgFromResponse
                #endregion

                #region ValidateMsg

                var validMsg = _soapRequestHelper.ValidateSignature(responseFromInternalHandler.Response, ownCert.CertString);

                if (validMsg)
                {
                    _validXmlMsgHelper.ValidateXml(responseFromInternalHandler.Response);
                }

                #endregion

                #region InfoAboutDecryption

                var symmetricKey = mimMsgResponse.Header.CryptoHeader.Key;
                var iVector = mimMsgResponse.Header.CryptoHeader.InitializationVector;

                #endregion

                #region DecryptionMsgResponseBody

                string decryptBody = _mimMsgHelper.DecryptSoapBody(Convert.FromBase64String(mimMsgResponse.Body.MimBody.Message), Convert.FromBase64String(symmetricKey), Convert.FromBase64String(iVector), privateKey);

                string decryptedResponseBodyWrappedInSoapEnvelope = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body>" + decryptBody + "</s:Body></s:Envelope>";

                #endregion

                HttpContext.Current.Response.ContentType = "application/soap+xml";
                context.Response.Write(decryptedResponseBodyWrappedInSoapEnvelope);//responseFromInternalHandler
            }
            catch (Exception exception)
            {
                //TODO: Ovde da se razmisli koj e najdobriot nacin BizTalk da vrati negovi Soap Fault kako "Ucesnikot e nedostapen", "Servisot e nedostapen"... Dosega tie pocnuvaa so BTMstatus10000BTMend, no ova ne e najdobar nacin
                
                var innerException = exception.InnerException != null ? exception.InnerException.Message : string.Empty;

                //var soapFault = _requestHelper.CreateSoapFault("Code value", "Code - SubCode value", innerException, "Error occurred in ExternalHttpHandler. Details: " + exception.Message);
                var soapFault = _requestHelper.CreateSoapFault("Code value", "Code - SubCode value", innerException, exception.Message);


                //TODO:Create Soap Fault in DB

                var soapFaultXml = _requestHelper.CreateFaultMessage(soapFault);

                HttpContext.Current.Response.ContentType = "application/soap+xml";
                HttpContext.Current.Response.Write(soapFaultXml.InnerXml);
            }

        }

        #endregion
    }
}
