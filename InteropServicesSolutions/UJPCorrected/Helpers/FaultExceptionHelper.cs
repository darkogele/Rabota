using Contracts.DataAccessLibrary.InteropFault;

namespace Helpers
{
    public class FaultExceptionHelper
    {
        public static InteropFault CreateFaultException(string errorMessage, string errorDetail)
        {
            var exception = new InteropFault
            {
                Result = false,
                ErrorMessage = errorMessage,
                ErrorDetails = errorDetail
            };
            return exception;
        }
    }
}
