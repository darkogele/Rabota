using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Interop.ExternalCC.HandlersHelper.Exceptions;
using Interop.ExternalCC.HandlersHelper.HelperMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class ExternalCCRequestHelperTest
    {
        private string _soapBody;
        private string _soapAction;
        private ExternalCCRequestHelper _requestHelper;

        [TestInitialize]
        public void SetUp()
        {
            _soapBody = "<soap:Envelope xmlns:mioa=\"http://mioa.gov.mk/interop/mim/v1\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://www.slss.hr/\" xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\">" 
                        + "<soap:Header>" 
                        + "<mioa:MIMHeader id=\"Header\">" 
                        + "<mioa:Consumer>UJP</mioa:Consumer>" 
                        + "<mioa:Provider />"
                        + "<mioa:RoutingToken>MVR</mioa:RoutingToken>"
                        + "<mioa:Service>EvalService</mioa:Service>"
                        + "<mioa:TransactionId>36c4ffe4-72f5-405e-a131-13ad130a7c83</mioa:TransactionId>"
                        + "<mioa:Dir>Request</mioa:Dir>"
                        + "<mioa:PublicKey>TestPublicKey</mioa:PublicKey>"
                        + "<mioa:MimeType />"
                        + "<mioa:TimeStamp>2015-09-24T13:55:48.0187319+02:00</mioa:TimeStamp>"
                        + "<mioa:CorrelationID />"
                        + "<mioa:Signature>"
                        + "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\">"
                        + "<SignedInfo>"
                        + "<CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" />"
                        + "<SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" />"
                        + "<Reference URI=\"#Header\">"
                        + "<Transforms>"
                        + "<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" />"
                        + "<Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\" />"
                        + "</Transforms>"
                        + "<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" />"
                        + "<DigestValue>WVdz/sYWELyDWpIfPKoAMUiS7y0=</DigestValue>"
                        + "</Reference>"
                        + "<Reference URI=\"#Body\">"
                        + "<Transforms>"
                        + "<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" />"
                        + "<Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\" />"
                        + "</Transforms>"
                        + "<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" />"
                        + "<DigestValue>5xC74Y09GFiH2QbA35QLmuEbhEM=</DigestValue>"
                        + "</Reference>"
                        + "</SignedInfo>"
                        + "<SignatureValue>Mjiqj+WIqZeBJrt+CYW2Yb5XS0SxL9o02srR0zLRXadAMIOs+kvj582R4F4qxUeY7XctG3dNA2mkYaFZYWXplxgCdFR+xnlr5tY8oGRJ+2pRluT3QsM1uOR64X+Px9ZSU716TtpdAd4bzXkL+YnssSzLhoc0Wkoq9Cu5h25bNYU=</SignatureValue>"
                        + "<KeyInfo>"
                        + "<X509Data>"
                        + "<X509Certificate>MIIDgDCCAmigAwIBAgIIaOmkQajSf5MwDQYJKoZIhvcNAQEFBQAwLzERMA8GA1UEAwwIRVBvdHBpczIxDTALBgNVBAoMBE1JT0ExCzAJBgNVBAYTAk1LMB4XDTE1MDgwNjA3MDE1MVoXDTE2MDgwNTA3MDE1MVowLjEQMA4GA1UEAwwHS09SVkNDQTENMAsGA1UECgwETUlPQTELMAkGA1UEBhMCTUswgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAJLPi/JFh4gcIj8OMyXGcCJHhq4TT9HyOi6Icm6RozaGUn+G2dYV64C4lqJJQNkGOwg+4T21tndXC1Su1TgGKgLciHa3IRugFUloNrpfdEE/wYLzRXAan/cbkt5dubdToplHI+O/4T8djbvTTDDKRoaqej0dqRMI7zAMR4FuwczjAgMBAAGjggEjMIIBHzAdBgNVHQ4EFgQUgEduZQaKR5Q7nkz3iY5n7prqdpwwDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBSKKmR3Wsd/V11BIdRVOQ1pJRwRAzCBqQYDVR0fBIGhMIGeMIGboGSgYoZgaHR0cDovLzEwLjQ3LjIwLjIzNzo4MDgwL2VqYmNhL3B1YmxpY3dlYi93ZWJkaXN0L2NlcnRkaXN0P2NtZD1jcmwmaXNzdWVyPUNOPUVQb3RwaXMyLE89TUlPQSxDPU1LojOkMTAvMREwDwYDVQQDDAhFUG90cGlzMjENMAsGA1UECgwETUlPQTELMAkGA1UEBhMCTUswDgYDVR0PAQH/BAQDAgWgMBMGA1UdJQQMMAoGCCsGAQUFBwMBMA0GCSqGSIb3DQEBBQUAA4IBAQBKw5yt0A0vzx218o8uzpK6Wx5Iw7DDgWTk+kNWXw87YHaz9ez+T1PdRjWKbVNg/hS86Twe86peoLzbtoBy1fr+AaiklL4iioqxAhAgybKdNK8wnYHvdty9oyga+A2+KTjq+iG+0TxQjm/zm5WJ9Tq36nSm1YfbAJIvIylzamTamBdJ0N9IyMzltqpRMHdVNPf9Keqp5hsMv4yoPYHdtbSZnJJeSQCiK97iHCioU6B5vNW8jVdWVDxfZD+wBIfZeQxiHL+MJehz9aTUdfNBJaGcAi4LRHpIUaC6+/M9ev6pRNSsey7VBFnRKNJCPL2sN/X4OXBadfvaJkjfe1clzDr9</X509Certificate>"
                        + "</X509Data>"
                        + "</KeyInfo>"
                        + "</Signature>"
                        + "</mioa:Signature>"
                        + "<mioa:CallType>synchronous</mioa:CallType>"
                        + "</mioa:MIMHeader>"
                        + "<mioa:MIMadditionalHeader>"
                        + "<mioa:Status />"
                        + "<mioa:StatusMessage />"
                        + "<mioa:ProviderEndpointUrl />"
                        + "<mioa:ExternalEndpointUrl />"
                        + "<mioa:WebServiceUrl />"
                        + "</mioa:MIMadditionalHeader>"
                        + "<mioa:CryptoHeader>"
                        + "<mioa:Key>qoHwqwrvJMqQ07j/KIyIKAb33luJEvpaK0oy7CRHK3G4oD26CuS7Sj1sORz6GIDwLtjfsOywDPBlLphuq5aoDRmNouLBPSL6wA3/VpDYnV7F6/CEhAvM9Ch+vwENq9bFBPuRqvHx50LPDo7nfuxXRFBm2Bw8lVd9NkBDGzTL2pY=</mioa:Key>"
                        + "<mioa:InitializationVector>f6RXbqPzgJ0gQFsPSY4NyA==</mioa:InitializationVector>"
                        + "<mioa:FormatValue>AES</mioa:FormatValue>"
                        + "</mioa:CryptoHeader>"
                        + "</soap:Header>"
                        + "<soap:Body>"
                        + "<mioa:MIMBody id=\"Body\">"
                        + "<mioa:Message d4p1:type=\"xs:string\" xmlns:d4p1=\"http://www.w3.org/2001/XMLSchema-instance\">lrpwSgDJBm975vncc3bZI98UYnKAHMWYZhjxY1TTo6pnubTtqh5o32Qm2IbAvtI7m66Q7B4CpQq8v+yEFpAG0ZEU+joH9q0c7ns9XytRQpNE1BuG+fXTCetZMXgIomPSF4qTsv9e5wdBVPv5Jutakb4WTpzzpTAJ6ii4D9NM82Rp5bu3HiRXcMu4+OFPnZXVgcTiF73iLTjlsxs7piy8t7G7OUChIwSexY8kASVPT7MunejPlIlaPYqbDGViaVoX0C5mEjKPI+9tnXN9oFusrWFI/3vXPm45m5zciT0pszNt+owLOMwwFcPzDNJiBar8i5yz3O2uFTeJudz4eH0GDdaTpGUdZir6DCwhtd83vbVNbQsDY5lhMtYgWfe07QrJQv5z1mIRX6RYf3vyoX87gleuLSySVKE5jNKPBv+O0bbhPXUFbyGZyg68Af5J+SKdlM3MEn3l9rRO/rRKzSl86bDlBZ36rASy3zwiHriptRKbAN/hk5ciyarrAfKMRYqO29eq2dPHTY+cKtAVKvFLzhf/Dy9oIOVX88BqIdG+zAaq9Er/06G2I2AGIaZsb3pb2j/OLyLxa7eXO/y7KgA2uYHaSsNaIgX72G2EKUgjKQaWyNUw3Uo69ql5tdZxtm9rlufG8cHrdOtJP71zbR8qP5cU0Ia4eSd+7MX2uJXBgL9Dt7/TlL3TMcR86EotllceXPsMHz/OwZCG1aAG8m5+JAxPG+PG6cxJ7oR81LzOjuJ0RsnmN9h+7BF8tu/Br+bMQFV72OP4XuPj5vVuPQoQpy52uQiLVMlPOP+9zBLuZ9G9sKIHsTxyOVA42f+2R4nGol45lEQ9bqQPZcAPl01sk2Az5Ym2/+cetdRRYdgP3LBFCmVVfF9Zw/3dv3mfoZLV1rbJG4w2SHozmxgBE8BSog==</mioa:Message>"
                        + "</mioa:MIMBody>"
                        + "</soap:Body>"
                        + "</soap:Envelope>";

            _soapAction = "";
            _requestHelper = new ExternalCCRequestHelper();
        }

        [TestMethod]
        public void GetParticipantCode_Successfully()
        {
            var participantCode = _requestHelper.GetParticipantCode(_soapBody);
            Assert.AreEqual("MVR", participantCode);
        }

        [TestMethod]
        [ExpectedException(typeof (NotFoundParticipantCodeException))]
        public void GetParticipantCode_Throws_NotFoundParticipantCodeException()
        {
            var soapBody = "<soap:Envelope xmlns:mioa=\"http://mioa.gov.mk/interop/mim/v1\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://www.slss.hr/\" xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\">"
                        + "<soap:Header>"
                        + "<mioa:MIMHeader id=\"Header\">"
                        + "<mioa:Consumer>UJP</mioa:Consumer>"
                        + "<mioa:Provider />"
                        //+ "<mioa:RoutingToken>Testing</mioa:RoutingToken>"
                        + "<mioa:Service>EvalService</mioa:Service>"
                        + "<mioa:TransactionId>36c4ffe4-72f5-405e-a131-13ad130a7c83</mioa:TransactionId>"
                        + "<mioa:Dir>Request</mioa:Dir>"
                        + "<mioa:PublicKey>TestPublicKey</mioa:PublicKey>"
                        + "<mioa:MimeType />"
                        + "<mioa:TimeStamp>2015-09-24T13:55:48.0187319+02:00</mioa:TimeStamp>"
                        + "<mioa:CorrelationID />"
                        + "<mioa:Signature>"
                        + "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\">"
                        + "<SignedInfo>"
                        + "<CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" />"
                        + "<SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" />"
                        + "<Reference URI=\"#Header\">"
                        + "<Transforms>"
                        + "<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" />"
                        + "<Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\" />"
                        + "</Transforms>"
                        + "<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" />"
                        + "<DigestValue>WVdz/sYWELyDWpIfPKoAMUiS7y0=</DigestValue>"
                        + "</Reference>"
                        + "<Reference URI=\"#Body\">"
                        + "<Transforms>"
                        + "<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" />"
                        + "<Transform Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\" />"
                        + "</Transforms>"
                        + "<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" />"
                        + "<DigestValue>5xC74Y09GFiH2QbA35QLmuEbhEM=</DigestValue>"
                        + "</Reference>"
                        + "</SignedInfo>"
                        + "<SignatureValue>Mjiqj+WIqZeBJrt+CYW2Yb5XS0SxL9o02srR0zLRXadAMIOs+kvj582R4F4qxUeY7XctG3dNA2mkYaFZYWXplxgCdFR+xnlr5tY8oGRJ+2pRluT3QsM1uOR64X+Px9ZSU716TtpdAd4bzXkL+YnssSzLhoc0Wkoq9Cu5h25bNYU=</SignatureValue>"
                        + "<KeyInfo>"
                        + "<X509Data>"
                        + "<X509Certificate>MIIDgDCCAmigAwIBAgIIaOmkQajSf5MwDQYJKoZIhvcNAQEFBQAwLzERMA8GA1UEAwwIRVBvdHBpczIxDTALBgNVBAoMBE1JT0ExCzAJBgNVBAYTAk1LMB4XDTE1MDgwNjA3MDE1MVoXDTE2MDgwNTA3MDE1MVowLjEQMA4GA1UEAwwHS09SVkNDQTENMAsGA1UECgwETUlPQTELMAkGA1UEBhMCTUswgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAJLPi/JFh4gcIj8OMyXGcCJHhq4TT9HyOi6Icm6RozaGUn+G2dYV64C4lqJJQNkGOwg+4T21tndXC1Su1TgGKgLciHa3IRugFUloNrpfdEE/wYLzRXAan/cbkt5dubdToplHI+O/4T8djbvTTDDKRoaqej0dqRMI7zAMR4FuwczjAgMBAAGjggEjMIIBHzAdBgNVHQ4EFgQUgEduZQaKR5Q7nkz3iY5n7prqdpwwDAYDVR0TAQH/BAIwADAfBgNVHSMEGDAWgBSKKmR3Wsd/V11BIdRVOQ1pJRwRAzCBqQYDVR0fBIGhMIGeMIGboGSgYoZgaHR0cDovLzEwLjQ3LjIwLjIzNzo4MDgwL2VqYmNhL3B1YmxpY3dlYi93ZWJkaXN0L2NlcnRkaXN0P2NtZD1jcmwmaXNzdWVyPUNOPUVQb3RwaXMyLE89TUlPQSxDPU1LojOkMTAvMREwDwYDVQQDDAhFUG90cGlzMjENMAsGA1UECgwETUlPQTELMAkGA1UEBhMCTUswDgYDVR0PAQH/BAQDAgWgMBMGA1UdJQQMMAoGCCsGAQUFBwMBMA0GCSqGSIb3DQEBBQUAA4IBAQBKw5yt0A0vzx218o8uzpK6Wx5Iw7DDgWTk+kNWXw87YHaz9ez+T1PdRjWKbVNg/hS86Twe86peoLzbtoBy1fr+AaiklL4iioqxAhAgybKdNK8wnYHvdty9oyga+A2+KTjq+iG+0TxQjm/zm5WJ9Tq36nSm1YfbAJIvIylzamTamBdJ0N9IyMzltqpRMHdVNPf9Keqp5hsMv4yoPYHdtbSZnJJeSQCiK97iHCioU6B5vNW8jVdWVDxfZD+wBIfZeQxiHL+MJehz9aTUdfNBJaGcAi4LRHpIUaC6+/M9ev6pRNSsey7VBFnRKNJCPL2sN/X4OXBadfvaJkjfe1clzDr9</X509Certificate>"
                        + "</X509Data>"
                        + "</KeyInfo>"
                        + "</Signature>"
                        + "</mioa:Signature>"
                        + "<mioa:CallType>synchronous</mioa:CallType>"
                        + "</mioa:MIMHeader>"
                        + "<mioa:MIMadditionalHeader>"
                        + "<mioa:Status />"
                        + "<mioa:StatusMessage />"
                        + "<mioa:ProviderEndpointUrl />"
                        + "<mioa:ExternalEndpointUrl />"
                        + "<mioa:WebServiceUrl />"
                        + "</mioa:MIMadditionalHeader>"
                        + "<mioa:CryptoHeader>"
                        + "<mioa:Key>qoHwqwrvJMqQ07j/KIyIKAb33luJEvpaK0oy7CRHK3G4oD26CuS7Sj1sORz6GIDwLtjfsOywDPBlLphuq5aoDRmNouLBPSL6wA3/VpDYnV7F6/CEhAvM9Ch+vwENq9bFBPuRqvHx50LPDo7nfuxXRFBm2Bw8lVd9NkBDGzTL2pY=</mioa:Key>"
                        + "<mioa:InitializationVector>f6RXbqPzgJ0gQFsPSY4NyA==</mioa:InitializationVector>"
                        + "<mioa:FormatValue>AES</mioa:FormatValue>"
                        + "</mioa:CryptoHeader>"
                        + "</soap:Header>"
                        + "<soap:Body>"
                        + "<mioa:MIMBody id=\"Body\">"
                        + "<mioa:Message d4p1:type=\"xs:string\" xmlns:d4p1=\"http://www.w3.org/2001/XMLSchema-instance\">lrpwSgDJBm975vncc3bZI98UYnKAHMWYZhjxY1TTo6pnubTtqh5o32Qm2IbAvtI7m66Q7B4CpQq8v+yEFpAG0ZEU+joH9q0c7ns9XytRQpNE1BuG+fXTCetZMXgIomPSF4qTsv9e5wdBVPv5Jutakb4WTpzzpTAJ6ii4D9NM82Rp5bu3HiRXcMu4+OFPnZXVgcTiF73iLTjlsxs7piy8t7G7OUChIwSexY8kASVPT7MunejPlIlaPYqbDGViaVoX0C5mEjKPI+9tnXN9oFusrWFI/3vXPm45m5zciT0pszNt+owLOMwwFcPzDNJiBar8i5yz3O2uFTeJudz4eH0GDdaTpGUdZir6DCwhtd83vbVNbQsDY5lhMtYgWfe07QrJQv5z1mIRX6RYf3vyoX87gleuLSySVKE5jNKPBv+O0bbhPXUFbyGZyg68Af5J+SKdlM3MEn3l9rRO/rRKzSl86bDlBZ36rASy3zwiHriptRKbAN/hk5ciyarrAfKMRYqO29eq2dPHTY+cKtAVKvFLzhf/Dy9oIOVX88BqIdG+zAaq9Er/06G2I2AGIaZsb3pb2j/OLyLxa7eXO/y7KgA2uYHaSsNaIgX72G2EKUgjKQaWyNUw3Uo69ql5tdZxtm9rlufG8cHrdOtJP71zbR8qP5cU0Ia4eSd+7MX2uJXBgL9Dt7/TlL3TMcR86EotllceXPsMHz/OwZCG1aAG8m5+JAxPG+PG6cxJ7oR81LzOjuJ0RsnmN9h+7BF8tu/Br+bMQFV72OP4XuPj5vVuPQoQpy52uQiLVMlPOP+9zBLuZ9G9sKIHsTxyOVA42f+2R4nGol45lEQ9bqQPZcAPl01sk2Az5Ym2/+cetdRRYdgP3LBFCmVVfF9Zw/3dv3mfoZLV1rbJG4w2SHozmxgBE8BSog==</mioa:Message>"
                        + "</mioa:MIMBody>"
                        + "</soap:Body>"
                        + "</soap:Envelope>";

            var participantCode = _requestHelper.GetParticipantCode(soapBody);
        }

        [TestMethod]
        public void GetParticipantUri_Successfully()
        {
            var participantCode = _requestHelper.GetParticipantCode(_soapBody);
            var uri = _requestHelper.GetParticipantUri(participantCode);
            Assert.AreEqual("\"/\"", uri);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundParticipantUriException))]
        public void GetParticipantUri_Throws_NotFoundParticipantUriException()
        {
            var participantCode = "UnrealInstitution";
            var uri = _requestHelper.GetParticipantUri(participantCode);
        }
    }
}
