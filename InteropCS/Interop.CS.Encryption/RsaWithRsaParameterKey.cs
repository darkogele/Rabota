using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Encryption
{
    public class RsaWithRsaParameterKey
    {
        public RSAParameters PublicKey;
        public RSAParameters PrivateKey;


        //Опис: Метод за енкрипција на податоци со RSA алгоритам
        //Влезни параметри: податокот што треба да се енкриптира
        //Излезни параметри: енкриптиран податок
        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;


            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = true;

                rsa.ImportParameters(PublicKey);

                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }

            return cipherbytes;
        }

        //Опис: Метод за декрипција на податоци со RSA алгоритам
        //Влезни параметри: податокот што треба да се декриптира
        //Излезни параметри: декриптиран податок
        public byte[] DecryptData(byte[] dataToEncrypt)
        {
            byte[] plain;


            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = true;

                rsa.ImportParameters(PrivateKey);

                plain = rsa.Decrypt(dataToEncrypt, false);
            }

            return plain;
        }
    }
}
