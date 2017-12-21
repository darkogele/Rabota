using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Interop.CC.CrossCutting;

namespace Interop.CC.Encryption
{
    public class RsaWithRsaParameterKey
    {
        public RSAParameters PublicKey;
        public RSAParameters PrivateKey;

        // Опис: Методот овозможува екстрахирање на приватен и јавен клуч за соодветни сертификати
        // Влезни параметри: /
        // Излезни параметри: /
        public void AssignNewKey()
        {
            //using (var rsa = new RSACryptoServiceProvider(2048))
            //{                
            //    rsa.PersistKeyInCsp = false;               
            //    _publicKey = rsa.ExportParameters(false);
            //    _privateKey = rsa.ExportParameters(true);                
            //}
            X509Certificate2 certA = new X509Certificate2(AppSettings.Get<string>("CertALocation"), "5Aqy9GwE", X509KeyStorageFlags.Exportable);

            X509Certificate2 certB = new X509Certificate2(AppSettings.Get<string>("CertBLocation"), "exMF5WW9", X509KeyStorageFlags.Exportable);

            RSACryptoServiceProvider rsaA = (RSACryptoServiceProvider)certA.PrivateKey;

            RSACryptoServiceProvider rsaB = (RSACryptoServiceProvider)certB.PrivateKey;

            PublicKey = rsaA.ExportParameters(false);

            PrivateKey = rsaA.ExportParameters(true);

        }

        // Опис: Методот овозможува симетрична енкрипција на податоци
        // Влезни параметри: поворка од бајти dataToEncrypt
        // Излезни параметри: поворка од бајти
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

        // Опис: Методот овозможува симетрична декрипција на податоци
        // Влезни параметри: поворка од бајти dataToEncrypt
        // Излезни параметри: поворка од бајти
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