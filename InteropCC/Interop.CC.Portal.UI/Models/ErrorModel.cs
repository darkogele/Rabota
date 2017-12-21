namespace Interop.CC.Portal.UI.Models
{
    public class ErrorModel
    {
        public bool HasError { get; set; }
        public CertErrorType CertErrorType { get; set; }

        public string ErrorText { get; set; }
        //{
            //get
            //{
            //    if (CertErrorType == CertErrorType.ExpiredCertificate)
            //    {
            //        return "Внесениот сертификат е истечен. Контактирајте го администраторот";
            //    }
            //    if (CertErrorType == CertErrorType.ExpiredCertificate)
            //    {
            //        return "Внесениот сертификат е невалиден";
            //    }
            //    return _errorText;
            //}
            //set
            //{
            //    _errorText = value;
            //}
            
        //}
    }

    public enum CertErrorType
    {
        ExpiredCertificate = 1,
        InvalidCertificate = 2
    }
}