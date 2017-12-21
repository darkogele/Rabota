// Decompiled with JetBrains decompiler
// Type: MVRCallCenter.Crypto
// Assembly: interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D186960-4F98-492C-AEAB-65EED2632965
// Assembly location: D:\interop_test_code\interop_test_code\interop.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MVRCallCenter
{
  public class Crypto
  {
    private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

    public string EncryptStringAES(string plainText, string sharedSecret)
    {
      if (string.IsNullOrEmpty(plainText))
        throw new ArgumentNullException("plainText");
      if (string.IsNullOrEmpty(sharedSecret))
        throw new ArgumentNullException("sharedSecret");
      string str = (string) null;
      RijndaelManaged rijndaelManaged = (RijndaelManaged) null;
      try
      {
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(sharedSecret, Crypto._salt);
        rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
        ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) cryptoStream))
              streamWriter.Write(plainText);
          }
          str = Convert.ToBase64String(memoryStream.ToArray());
        }
      }
      finally
      {
        if (rijndaelManaged != null)
          rijndaelManaged.Clear();
      }
      return str;
    }

    public string DecryptStringAES(string cipherText, string sharedSecret)
    {
      if (string.IsNullOrEmpty(cipherText))
        throw new ArgumentNullException("cipherText");
      if (string.IsNullOrEmpty(sharedSecret))
        throw new ArgumentNullException("sharedSecret");
      RijndaelManaged rijndaelManaged = (RijndaelManaged) null;
      string str = (string) null;
      try
      {
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(sharedSecret, Crypto._salt);
        rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
        ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream) cryptoStream))
              str = streamReader.ReadToEnd();
          }
        }
      }
      finally
      {
        if (rijndaelManaged != null)
          rijndaelManaged.Clear();
      }
      return str;
    }
  }
}
