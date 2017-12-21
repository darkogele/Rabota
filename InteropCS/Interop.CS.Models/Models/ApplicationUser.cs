using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CS.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PublicKey { get; set; }
    }
}
