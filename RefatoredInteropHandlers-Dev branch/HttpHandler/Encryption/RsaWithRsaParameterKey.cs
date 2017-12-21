﻿using System.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Encryption
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
            var certA = new X509Certificate2(ConfigurationManager.AppSettings["CertALocation"], "5Aqy9GwE", X509KeyStorageFlags.Exportable);

            var certB = new X509Certificate2(ConfigurationManager.AppSettings["CertBLocation"], "exMF5WW9", X509KeyStorageFlags.Exportable);

            var rsaA = (RSACryptoServiceProvider)certA.PrivateKey;

            var rsaB = (RSACryptoServiceProvider)certB.PrivateKey;

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