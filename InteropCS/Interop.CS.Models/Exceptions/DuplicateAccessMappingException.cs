using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен дупликат запис во пристапната листа
    public class DuplicateAccessMappingException : Exception
    {
        private readonly AccessMapping _accessMapping;

        public DuplicateAccessMappingException(AccessMapping accessMapping)
        {
            _accessMapping = accessMapping;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.DuplicateAccessMapping, _accessMapping.ConsumerCode, _accessMapping.ProviderCode, _accessMapping.ServiceCode, _accessMapping.MethodCode);
            }
        }
    }
}
