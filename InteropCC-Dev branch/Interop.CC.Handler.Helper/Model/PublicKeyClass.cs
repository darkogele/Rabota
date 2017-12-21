using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.CC.Encryption;

namespace Interop.CC.Handler.Helper.Model
{
    public class PublicKeyClass
    {
        public RsaWithRsaParameterKey PublicKeyRsa { get; set; }

        public string PublicKeyString { get; set; }

        public string CertString { get; set; }
    }
}
