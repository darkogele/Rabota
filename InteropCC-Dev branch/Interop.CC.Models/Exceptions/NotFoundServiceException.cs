using System;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за неуспешно пронајден запис во листата на сервиси
    public class NotFoundServiceException : Exception
    {
        private readonly string _code;

        public NotFoundServiceException(string code)
        {
            _code = code;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.NotFoundService, _code);
            }
        }
    }
}
