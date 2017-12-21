namespace Interop.CC.Encryption
{
    public class EncryptedPacket
    {
        public byte[] EncryptedSessionKey;
        public byte[] EncryptedData;
        public byte[] Iv;
        public RsaWithRsaParameterKey RsaParams;
    }
}
