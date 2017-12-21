using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Encryption
{
    //Опис: Се користи за дефинирање на стандардот на криптираната порака односно соодветните пропертиа кои ги користи при криптирање 
    public class EncryptedPacket
    {
        public byte[] EncryptedSessionKey;
        public byte[] EncryptedData;
        public byte[] Iv;
        public RsaWithRsaParameterKey RsaParams;
    }
}
