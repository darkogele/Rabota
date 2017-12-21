using Helpers.Models;

namespace Helpers.Contracts
{
    public interface ISoapRequestHelper
    {
        SoapMessage UnwrapMimMessage(string mimMessage);
        bool ValidateSignature(string mimMessage, string publicKey);
    }
}
