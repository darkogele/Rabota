using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CS.Encryption
{
    public class HybridEncryption
    {
        private readonly AesEncryption _aes = new AesEncryption();

        //Опис: Метод за енкрипција на податоци 
        //Влезни податоци: поворка од бајти original, RsaWithRsaParameterKey rsaParams
        // Излезни параметри: EncryptedPacket модел
        public EncryptedPacket EncryptData(byte[] original, RsaWithRsaParameterKey rsaParams)
        {
            // Generate our session key.
            var sessionKey = _aes.GenerateRandomNumber(16);

            // Create the encrypted packet and generate the IV.
            var encryptedPacket = new EncryptedPacket { Iv = _aes.GenerateRandomNumber(16) };

            // Encrypt our data with AES.
            encryptedPacket.EncryptedData = _aes.Encrypt(original, sessionKey, encryptedPacket.Iv);

            // Encrypt the session key with RSA
            encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(sessionKey);

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
