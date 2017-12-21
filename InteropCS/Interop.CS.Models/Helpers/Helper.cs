using System;
using System.ComponentModel;
using System.Security.Cryptography;
using Interop.CS.Models.DTO;

namespace Interop.CS.Models.Helpers
{
    public class Helper
    {
        // Hashing REFRESH TOKEN
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }


        public static string GetCyrilicCode(CyrilicInstitutionCode code)
        {
            var enumType = code.GetType();
            var field = enumType.GetField(code.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0 ? code.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

    }
}
