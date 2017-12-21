using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Interop.CC.Encryption
{
    public class AesEncryption 
    {
        // Опис: Методот овозможува генерирање на број по случаен избор
        // Влезни параметри: податочна вредност length
        // Излезни параметри: поворка од бајти
        public byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        // Опис: Методот овозможува асиметрична енкрипција на податоци
        // Влезни параметри: поворка од бајти dataToEncrypt, key, iv
        // Излезни параметри: поворка од бајти
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {            
            using (var aes = new AesCryptoServiceProvider())
            {                
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, 
                        aes.CreateEncryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        // Опис: Методот овозможува асиметрична енкрипција на податоци
        // Влезни параметри: поворка од бајти dataToEncrypt, key, iv
        // Излезни параметри: поворка од бајти
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {                                
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var m = Encoding.UTF8.GetString(key);//za da se usoglasime so NextSense
                var x = Convert.FromBase64String(m);//za da se usoglasime so NextSense
                aes.Key = x;//za da se usoglasime so NextSense
                aes.IV = iv;//za da se usoglasime so NextSense


                //aes.Key = key;
                //aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {                       
                    var cryptoStream = new CryptoStream(memoryStream, 
                        aes.CreateDecryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    var decryptBytes = memoryStream.ToArray();

                    return decryptBytes;
                }
            }                            
        }
    }
}