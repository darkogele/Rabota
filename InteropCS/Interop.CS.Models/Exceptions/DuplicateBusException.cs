using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.CS.Models.Models;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен дупликат бас
    public class DuplicateBusException : Exception
    {
        private readonly Buses _bus;

        public DuplicateBusException(Buses bus)
        {
            _bus = bus;
        }
        
        public override string Message
        {
            get
            {
                return String.Format(Resources.DuplicateBus, _bus.Code);
            }
        }
    }
}
