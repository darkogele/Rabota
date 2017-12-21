using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHandlerHelper.Model
{
    public class UrlSegment
    {
        public UrlSegment()
        {
            IsUrlCorrrect = false;
            Async = false;
        }
        public string Consumer { get; set; }
        public string RoutingToken { get; set; }
        public string Service { get; set; }
        public string Method { get; set; }
        public bool Async { get; set; }
        public bool IsUrlCorrrect { get; set; }
    }
}
