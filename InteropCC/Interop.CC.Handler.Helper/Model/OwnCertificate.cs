using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Handler.Helper.Model
{
    public class OwnCertificate
    {
        public X509Certificate2 Certificate { get; set; }
        public string PublicKey { get; set; }
        public string CertString { get; set; }
        public RSAParameters PrivateKey { get; set; }
    }
}
