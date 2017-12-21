namespace Helpers.Models
{
    public class MimSignature
    {
        public Signature Signature { get; set; }
    }
    public class Signature
    {
        public string SignatureValue { get; set; }
        public MimSignedInfo SignedInfo { get; set; }
        public MimKeyInfo KeyInfo { get; set; }
    }
    public class MimSignedInfo
    { }
    public class MimKeyInfo
    { }
}
