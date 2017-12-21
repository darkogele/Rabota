using System;
using System.ServiceModel;
using System.Web;
using Exceptions;
using Exceptions.Models;
using Helpers.Contracts;
using Helpers.Models;
using Ninject;
using WCFNinject = InternalHandler.NinjectConfig.WCFNinject;

namespace InternalHandler
{
    public class InternalHttpHandler : IHttpHandler
    {
        
        #region IHttpHandler Members

        private readonly IRequestTestHelper _requestHelper;
        private readonly ISoapRequestHelper _soapRequestHelper;
        private readonly IMimMsgHelper _mimMsgHelper;
        private readonly IValidXmlMsgHelper _validXmlMsgHelper;

        public InternalHttpHandler()
        {
            using (IKernel kernel = new StandardKernel(new WCFNinject()))
            {
                _requestHelper = kernel.Get<IRequestTestHelper>();
                _soapRequestHelper = kernel.Get<ISoapRequestHelper>();
                _mimMsgHelper = kernel.Get<IMimMsgHelper>();
                _validXmlMsgHelper = kernel.Get<IValidXmlMsgHelper>();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            ExceptionInfo exceptionAdditionalInfo;

            try
            {
                var transactionId = new Guid();
                var soapBody = _requestHelper.GetSoapBody(context.Request.InputStream);
                var contentType = context.Request.ContentType;

                #region UnwrapMimMessage

                var mimMsg = _soapRequestHelper.UnwrapMimMessage(soapBody);

                #endregion

                transactionId = new Guid(mimMsg.Header.MimHeader.TransactionId);//se koristi za da se zapise soap fault so toj transactionId vo baza

                #region LogMimMsgRequestIntoDB
                #endregion

                #region CertificateAndKeys

                var ownCert = _mimMsgHelper.LoadOwnCertificate();
                var privateKey = _mimMsgHelper.GetPrivateKey(ownCert.PrivateKey);
                var publicKey = mimMsg.Header.MimHeader.PublicKey;

                #endregion

                #region ValidateMsg

                var validSignature = _soapRequestHelper.ValidateSignature(soapBody, publicKey);
                if (validSignature)
                {
                    _validXmlMsgHelper.ValidateXml(soapBody);
                }

                #endregion

                #region InfoAboutDecryption

                var symmetricKey = mimMsg.Header.CryptoHeader.Key;
                var iVector = mimMsg.Header.CryptoHeader.InitializationVector;

                #endregion

                #region DecryptionMsgBody

                var decryptBody = _mimMsgHelper.DecryptSoapBody(Convert.FromBase64String(mimMsg.Body.MimBody.Message), Convert.FromBase64String(symmetricKey), Convert.FromBase64String(iVector), privateKey);

                #endregion

                #region UpdateMimMsgRequestInDB
                #endregion

                var urlToHostedApp = _requestHelper.GetServiceUrl(mimMsg.Header.MimHeader);

                #region PrepareRequestToInstitutionService

                string decryptedRequestBodyWrappedInSoapEnvelope = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body>" + decryptBody + "</s:Body></s:Envelope>";

                #endregion

                /////SERVICE AKNPropertyList
                //const string decryptedRequestBodyWrappedInSoapEnvelope =
                //    "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body><GetPropertyList xmlns=\"http://interop.org/\"><username>mio</username><password>katastarservis</password><opstina>1</opstina><katastarskaOpstina>1</katastarskaOpstina><brImotenList>1</brImotenList></GetPropertyList></s:Body></s:Envelope>";
                //const string contentTypeToUse =
                //    "application/soap+xml; charset=utf-8; action=\"http://interop.org/IPropertyList/GetPropertyList\"";


                const string url = "http://localhost/AKNServicesTest.Host/PropertyListTest.svc/custom";



                /////SERVICE AKNPListDoc
                //const string decryptedRequestBodyWrappedInSoapEnvelope =
                //    "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body><GetPListDoc xmlns=\"http://interop.org/\"><opstina>25</opstina><katastarskaOpstina>997</katastarskaOpstina><brImotenList>1024</brImotenList><brParcela /><showEMB>true</showEMB></GetPListDoc></s:Body></s:Envelope>";
                //const string contentTypeToUse =
                //    "application/soap+xml; charset=utf-8; action=\"http://interop.org/IAKNPListDoc/GetPListDoc\"";
                //const string url = "http://localhost/AKNServicesTest.Host/AKNPListDocTest.svc/custom";

                //ResponseInteropCommunication responseInterop =
                //    _requestHelper.Execute(decryptedRequestBodyWrappedInSoapEnvelope, contentTypeToUse, url);


                #region RequestToInstitutionService

                ResponseInteropCommunication responseInterop = _requestHelper.Execute(decryptedRequestBodyWrappedInSoapEnvelope, contentType, url);

                #endregion

                #region CreateMimResponseMsg

                var mimMsgResponse = _mimMsgHelper.CreateMimResponseMsg(mimMsg, responseInterop.MimeType);

                #endregion

                #region LogMimMsgFromResponse
                #endregion

                string responseWithoutSoapEnvelope = _requestHelper.GetOnlySoapBodyFromString(responseInterop.Response);
                
                #region EncryptSoapBody

                var publicKeyRsa = _mimMsgHelper.GetPublicKeyFromString(publicKey);

                var encryptedBlock = _mimMsgHelper.EncryptSoapBody(responseWithoutSoapEnvelope, publicKeyRsa);

                var encryptedSoapBody = Convert.ToBase64String(encryptedBlock.EncryptedData);
                var encryptedSessionKey = Convert.ToBase64String(encryptedBlock.EncryptedSessionKey);
                var encriptedIVector = Convert.ToBase64String(encryptedBlock.Iv);
               
                #endregion

                #region FillMissingValuesInMimMessage

                mimMsgResponse.Body.MimBody.Message = encryptedSoapBody;
                mimMsgResponse.Header.CryptoHeader.InitializationVector = encriptedIVector;
                mimMsgResponse.Header.CryptoHeader.Key = encryptedSessionKey;

                #endregion

                #region CreateMimXmlMsg

                var mimMsgXml = _mimMsgHelper.CreateMimXmlMsg(mimMsgResponse);

                #endregion

                #region CreateMimSignedXmlMsg

                var mimMsgResponseXml = _mimMsgHelper.CreateMimSignedXmlMsg(mimMsgXml, ownCert);

                #endregion
                
                HttpContext.Current.Response.ContentType = "application/soap+xml";
                context.Response.Write(mimMsgResponseXml);//responseInterop.Response

            }
            //ako se sluci exception koj e Fault exception i koj doagja od samite adapteri!!!
            catch (FaultException faultException)
            {
                exceptionAdditionalInfo = Helper.GetExceptionAdditionalInfo(faultException);

                var errorText = "Error occurred in Internal Handler. File where error occured: " +
                                exceptionAdditionalInfo.FileErrorOccured + ". Method: " +
                                exceptionAdditionalInfo.MethodErrorOccurred + ". Line number: " +
                                exceptionAdditionalInfo.LineErrorOccured;

                var soapFault = _requestHelper.CreateSoapFault("Code value", "Code - SubCode value", errorText,
                    faultException.Message);

                var soapFaultXml = _requestHelper.CreateFaultMessage(soapFault);

                HttpContext.Current.Response.ContentType = "application/soap+xml";
                HttpContext.Current.Response.Write(soapFaultXml.InnerXml);
            }
            //Ako se sluci exception vo nekoja od metodite koi se povikuvaat na Internal Handler!!!
            catch (Exception exception)
            {
                exceptionAdditionalInfo = Helper.GetExceptionAdditionalInfo(exception);

                var errorText = "Error occured in Internal Handler. File where error occured: " +
                                exceptionAdditionalInfo.FileErrorOccured + ". Method: " +
                                exceptionAdditionalInfo.MethodErrorOccurred + ". Line number: " +
                                exceptionAdditionalInfo.LineErrorOccured;

                //Detalite za nastanatata greska kje se zapisat vo baza
                //Nema potreba da se vleckaat do External handler ili Web Api
                //Sekako na korisnikot kje mu se prikaze edna poraka za greska
                //A, vo log kje se zapise se shto treba, kako i vo NLog
                var soapFault = _requestHelper.CreateSoapFault("Code value", "Code - SubCode value", errorText,
                    exception.Message);

                var soapFaultXml = _requestHelper.CreateFaultMessage(soapFault);

                HttpContext.Current.Response.ContentType = "application/soap+xml";
                HttpContext.Current.Response.Write(soapFaultXml.InnerXml);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }

        #endregion
    }
}
