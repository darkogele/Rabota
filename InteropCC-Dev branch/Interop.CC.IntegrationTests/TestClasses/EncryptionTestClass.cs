using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Interop.CC.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interop.CC.IntegrationTests.TestClasses
{
    [TestClass]
    public class EncryptionTestClass
    {
        private string _startText;                
        private HybridEncryption _hybrid; 
        private X509Certificate2 _certForMVR;
        private X509Certificate2 _certForUJP; 

        [TestInitialize]
        public void SetUp()
        {
            _startText = "Text to be Encrypted !!";
            _hybrid = new HybridEncryption();
            _certForMVR = new X509Certificate2("D:\\Projects\\Interop\\InteropCC\\Documents\\KORVCCB.p12", "exMF5WW9", X509KeyStorageFlags.Exportable);
            _certForUJP = new X509Certificate2("D:\\Projects\\Interop\\InteropCC\\Documents\\KORVCCA.p12", "5Aqy9GwE", X509KeyStorageFlags.Exportable);
        }

        // Тест метод за успешна енкрипција и декрипција на податоци 
        [TestMethod]
        public void EncryptionDecryption_Successful()
        {
            var encryptedBlock = Encrypt();
            var decryptedString = Decrypt(encryptedBlock);
            Assert.AreEqual(_startText, decryptedString);
        }

        // Тест метод за успешно вратена крипто грешка при енкрипција и декрипција на податоци
        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void EncryptionDecryption_Throws_CryptographicException()
        {
            var encryptedBlock = Encrypt();
            var decryptedString = DecryptFailed(encryptedBlock);
        }

        // Тест метод за успешна RSA енкрипција со јавен клуч 
        public EncryptedPacket Encrypt()
        {
            var ascii = ASCIIEncoding.Default.GetBytes(_startText);
            var rsa = (RSACryptoServiceProvider)_certForUJP.PrivateKey;
            var publicKey = rsa.ExportParameters(false);
            var rsaParams = new RsaWithRsaParameterKey {PublicKey = publicKey};
            var encryptedBlock = _hybrid.EncryptData(ascii, rsaParams);
            return encryptedBlock;
        }

        // Тест метод за успешна RSA декрипција со јавен клуч 
        public string Decrypt(EncryptedPacket encryptedBlock)
        {
            var rsa = (RSACryptoServiceProvider)_certForUJP.PrivateKey;
            var privateKey = rsa.ExportParameters(true);
            var rsaParams = new RsaWithRsaParameterKey { PrivateKey = privateKey };
            var decrypted = _hybrid.DecryptData(encryptedBlock, rsaParams);
            return Encoding.UTF8.GetString(decrypted);
        }

        // Тест метод за неуспешна RSA декрипција со јавен клуч
        public string DecryptFailed(EncryptedPacket encryptedBlock)
        {
            var rsa = (RSACryptoServiceProvider)_certForMVR.PrivateKey;
            var privateKey = rsa.ExportParameters(true);
            var rsaParams = new RsaWithRsaParameterKey { PrivateKey = privateKey };
            var decrypted = new byte[256];
            try
            {
                decrypted = _hybrid.DecryptData(encryptedBlock, rsaParams);
            }
            catch (CryptographicException e)
            {
                throw new CryptographicException(e.Message);
            }
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
