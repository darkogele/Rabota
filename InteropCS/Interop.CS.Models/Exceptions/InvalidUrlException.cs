using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteropCS.Resources;

namespace Interop.CS.Models.Exceptions
{
    //приказ на грешка за внесен невалидно Url
    public class InvalidUrlException : Exception
    {

        public InvalidUrlException()
        {

        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.InvalidUrlException);
            }
        }
    }
}
