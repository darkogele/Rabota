using System;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен дупликат сервис
    public class DuplicateCSServiceException : Exception
    {
        private readonly CSService _service;

        public DuplicateCSServiceException(CSService service)
        {
            _service = service;
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.DuplicateCSService, _service.Code, _service.ParticipantCode);
            }
        }
    }
}
