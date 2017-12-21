using System;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден запис во пристапната листа
    public class NotFoundAccessMappingException : Exception
    {
        private readonly string _consumerCode;
        private readonly string _providerCode;
        private readonly string _serviceCode;

        public NotFoundAccessMappingException(string consumerCode, string providerCode, string serviceCode)
        {
            _consumerCode = consumerCode;
            _providerCode = providerCode;
            _serviceCode = serviceCode;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.NotFoundAccessMapping, _providerCode, _consumerCode, _serviceCode);
            }
        }
    }
}
