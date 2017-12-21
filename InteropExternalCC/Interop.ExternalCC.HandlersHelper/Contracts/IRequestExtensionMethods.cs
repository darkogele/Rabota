using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Interop.ExternalCC.HandlersHelper.Contracts
{
    public interface IRequestExtensionMethods
    {
        string GetSoapBody(HttpContext context);
        string GetSoapAction(HttpContext context);
    }
}
