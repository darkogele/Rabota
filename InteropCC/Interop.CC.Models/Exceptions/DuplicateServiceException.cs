using System;
using Interop.CC.Models.Models;
using InteropCC.Resources;

namespace Interop.CC.Models.Exceptions
{
    //приказ на грешка за внес на постоечки запис за сервис
    public class DuplicateServiceException : Exception
    {
        private readonly Service _service;

        public DuplicateServiceException(Service service)
        {
            _service = service;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.DuplicateService, _service.Code);
            }
        }
    }
}
