using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Helpers.Models
{
    public class OwnCertificate
    {
        public X509Certificate2 Certificate { get; set; }
        public string PublicKey { get; set; }
        public string CertString { get; set; }
        public RSAParameters PrivateKey { get; set; }
    }
}
