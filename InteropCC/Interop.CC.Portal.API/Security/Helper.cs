using System;
using System.Security.Cryptography;

namespace Interop.CC.Portal.API.Security
{
    public class Helper
    {
        // Опис: Методот врши хаширање на податочна вредност
        // Влезни параметри: податочна вредност input
        // Излезни параметри: податочен тип string
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}