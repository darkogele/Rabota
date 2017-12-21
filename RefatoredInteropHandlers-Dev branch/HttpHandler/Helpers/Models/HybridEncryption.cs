using System;
using System.Text;
using Encryption;

namespace Helpers.Models
{
    public class HybridEncryption
    {
        private readonly AesEncryption _aes = new AesEncryption();

        // Опис: Методот овозможува хибридна енкрипција на податоци
        // Влезни параметри: поворка од бајти original, RsaWithRsaParameterKey rsaParams
        // Излезни параметри: EncryptedPacket модел
        public EncryptedPacket EncryptData(byte[] original, RsaWithRsaParameterKey rsaParams)
        {
            // Generate our session key.
            //var sessionKey = _aes.GenerateRandomNumber(16);
            var sessionKey = _aes.GenerateRandomNumber(32);
            //var key = Convert.ToBase64String(sessionKey);
            //var keyBytes = Encoding.ASCII.GetBytes(key);
            // Create the encrypted packet and generate the IV.
            var sessionString = Convert.ToBase64String(sessionKey);
            var encryptedPacket = new EncryptedPacket { Iv = _aes.GenerateRandomNumber(16) };

            var ivv = Convert.ToBase64String(encryptedPacket.Iv);

            // Encrypt our data with AES.
            //encryptedPacket.EncryptedData = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv);
            encryptedPacket.EncryptedData = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv);

            var dat = Convert.ToBase64String(encryptedPacket.EncryptedData);
            // Encrypt the session key with RSA
            //encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(sessionKey);
            encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(Encoding.UTF8.GetBytes(sessionString));//za da se usoglasime so NextSense

            var ss = Convert.ToBase64String(encryptedPacket.EncryptedSessionKey);
            encryptedPacket.RsaParams = rsaParams;

            return encryptedPacket;
        }

        // Опис: Методот овозможува хибридна декрипција на податоци
        // Влезни параметри: EncryptedPacket encryptedPacket, RsaWithRsaParameterKey rsaParams
        // Излезни параметри: EncryptedPacket модел
        public byte[] DecryptData(EncryptedPacket encryptedPacket, RsaWithRsaParameterKey rsaParams)
        {
            // Decrypt AES Key with RSA.
            var decryptedSessionKey = rsaParams.DecryptData(encryptedPacket.EncryptedSessionKey);

            // Decrypt our data with  AES using the decrypted session key.
            var decryptedData = _aes.Decrypt(encryptedPacket.EncryptedData,
                                             decryptedSessionKey, encryptedPacket.Iv);

            return decryptedData;
        }
    }
}
